using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace ProxyPool
{
    /// <summary>
    /// 
    /// </summary>
    public class Pool
    {

        #region Parameters
        private int poolId;
        private string poolName;
        private ProxyValidation poolProxyValidation;
        private int validationTimespan;
        private int validationThread;

        private int catheSize;
        private int poolStatus;
        private DateTime createTime;

        private List<ProxySource> poolSource;

        private List<Thread> monitorThread;
        private Object refreshSourceLock = new Object();
        private Object toBeValidationLock = new Object();
        private Object activeValidationLock = new Object();
        private int activeIndex = 0;

        private List<ProxyServer> allProxyServerList;
        private List<ProxyServer> activeProxyServerList;
        private List<ProxyServer> toBeValidProxyServerList;

        private int noUpdateRound = 0;
        #endregion

        #region Attribute
        public int Id
        {
            get
            {
                return poolId;
            }

            set
            {
                poolId = value;
            }
        }

        public string PoolName
        {
            get
            {
                return poolName;
            }

            set
            {
                poolName = value;
            }
        }

        public ProxyValidation PoolProxyValidation
        {
            get
            {
                return poolProxyValidation;
            }

            set
            {
                poolProxyValidation = value;
            }
        }

        public int ValidationTimespan
        {
            get
            {
                return validationTimespan;
            }

            set
            {
                validationTimespan = value;
            }
        }

        public int ValidationThread
        {
            get
            {
                return validationThread;
            }

            set
            {
                validationThread = value;
            }
        }

        public int CatheSize
        {
            get
            {
                return catheSize;
            }

            set
            {
                catheSize = value;
            }
        }
        /// <summary>
        /// 0：禁用；1：预备中；2：启动中；3：运行中；4：停止中；5：启动异常中止；6：停止异常中止
        /// </summary>
        public int PoolStatus
        {
            get
            {
                return poolStatus;
            }

            set
            {
                poolStatus = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return createTime;
            }

            set
            {
                createTime = value;
            }
        }

        public List<ProxySource> PoolSource
        {
            get { return poolSource; }
            set { poolSource = value; }
        }

        public List<Thread> MonitorThread
        {
            get
            {
                return monitorThread;
            }

            set
            {
                monitorThread = value;
            }
        }

        public List<ProxyServer> AllProxyServerList
        {
            get
            {
                return allProxyServerList;
            }

            set
            {
                allProxyServerList = value;
            }
        }

        public List<ProxyServer> ActiveProxyServerList
        {
            get
            {
                return activeProxyServerList;
            }

            set
            {
                activeProxyServerList = value;
            }
        }

        public List<ProxyServer> ToBeValidProxyServerList
        {
            get
            {
                return toBeValidProxyServerList;
            }

            set
            {
                toBeValidProxyServerList = value;
            }
        }
        #endregion

        public Pool()
        {

        }
        public Pool(string name, ProxyValidation validation, int tSpan, int threadNum, List<ProxySource> pSource, int cSize, int status, DateTime cTime, int id = 0)
        {
            poolId = id;
            poolName = name;
            poolProxyValidation = validation;
            validationTimespan = tSpan;
            validationThread = threadNum;
            poolSource = pSource;
            catheSize = cSize;
            poolStatus = status;
            createTime = cTime;
            monitorThread = new List<Thread>();
            allProxyServerList = new List<ProxyServer>();
            activeProxyServerList = new List<ProxyServer>();
            //inactiveProxyServerList = new List<ProxyServer>();
            toBeValidProxyServerList = new List<ProxyServer>();
        }

        /// <summary>
        /// 初始化Pool中的代理缓存list
        /// </summary>
        public void initialPoolProxyServerList()
        {
            if (poolStatus != 0)
            {
                allProxyServerList.Clear();
                activeProxyServerList.Clear();
                toBeValidProxyServerList.Clear();
                foreach (ProxySource pSrc in poolSource)
                {
                    int size = Math.Min(catheSize, pSrc.ProxyServerList.Count);
                    toBeValidProxyServerList.AddRange(pSrc.ProxyServerList.Take(size).ToList());
                }
                allProxyServerList = new List<ProxyServer>(toBeValidProxyServerList);
            }
        }

        /// <summary>
        /// 从source中获取更新的代理服务器配置
        /// runningcathe静态类中调用该方法，用于推送source中的代理更新
        /// </summary>
        /// <param name="pSrc">代理源</param>
        /// <param name="pServerList">更新的代理服务器清单（可选参数）</param>
        public void refreshProxyServerFromSource(ProxySource pSrc)
        {
            if (poolStatus != 0)
            {
                //缓存满后等待三轮，清空allProxyServerList中的失效代理
                if (noUpdateRound > 3)
                {
                    allProxyServerList = activeProxyServerList.Take(activeProxyServerList.Count).ToList();
                }
                List<ProxyServer> ps = pSrc.ProxyServerList.Except(allProxyServerList, new ProxyServerCompare()).ToList();
                if (ps.Count > 0)
                {
                    noUpdateRound = 0;
                    //从各个source中平均获取新代理
                    int toBeAdded = (catheSize - allProxyServerList.Count) / poolSource.Count;
                    if (toBeAdded > 0)
                    {
                        toBeAdded = toBeAdded > ps.Count ? ps.Count : toBeAdded;
                        ps = ps.Take(toBeAdded).ToList();
                        lock (refreshSourceLock)
                        {
                            toBeValidProxyServerList.AddRange(ps);
                            allProxyServerList.AddRange(ps);
                        }
                    }
                    else
                    {
                        noUpdateRound = noUpdateRound + 1;
                    }
                }
            }
        }

        /// <summary>
        /// 多线程验证Pool中代理，分两步进行
        /// 1 - 验证active代理缓存，失效的代理移除
        /// 2 - 验证toBeValid代理缓存,有效的移到active缓存，失效的移除
        /// </summary>
        private void threadValidateProxyServer()
        {
            ProxyServer ps;
            while (true)
            {
                lock (activeValidationLock)
                {
                    if (activeIndex < activeProxyServerList.Count)
                    {
                        ps = activeProxyServerList[activeIndex];
                        activeIndex++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!poolProxyValidation.validateProxyServer(ps))
                {
                    lock (activeValidationLock)
                    {
                        activeProxyServerList.Remove(ps);
                        activeIndex--;
                    }
                    //lock (inactiveValidationLock)
                    //{
                    //    inactiveProxyServerList.Add(ps);
                    //}
                }
            }

            while (true)
            {
                lock(toBeValidationLock)
                {
                    if (0 < toBeValidProxyServerList.Count)
                    {
                        ps = toBeValidProxyServerList[0];
                        toBeValidProxyServerList.Remove(ps);
                    }
                    else
                    {
                        break;
                    }
                }

                if (poolProxyValidation.validateProxyServer(ps))
                {
                    lock (activeValidationLock)
                    {
                        activeProxyServerList.Add(ps);
                    }
                }
                //else
                //{
                //    lock (inactiveValidationLock)
                //    {
                //        inactiveProxyServerList.Add(ps);
                //    }
                //}
            }

            //while (true)
            //{
            //    lock (inactiveValidationLock)
            //    {
            //        if (0 < inactiveProxyServerList.Count)
            //        {
            //            ps = inactiveProxyServerList[0];
            //            inactiveProxyServerList.Remove(ps);
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //    if (poolProxyValidation.validateProxyServer(ps))
            //    {
            //        lock (activeValidationLock)
            //        {
            //            activeProxyServerList.Add(ps);
            //        }
            //    }
            //}

            Thread.Sleep(validationTimespan);
        }

        public void startValidation()
        {
            if (poolStatus != 1)
            {
                Exception ex = new Exception("代理池无法启动，状态：" + poolStatus.ToString());
                throw ex;
            }
            else
            {
                try
                {
                    poolStatus = 2;
                    for (int i = 0; i < validationThread; i++)
                    {
                        monitorThread.Add(new Thread(new ThreadStart(threadValidateProxyServer)));
                        monitorThread[i].Start();
                    }
                    poolStatus = 3;
                }
                catch
                {
                    poolStatus = 5;
                    throw;
                }
            }
        }
        public void stopValidation()
        {
            if (poolStatus != 0)
            {
                poolStatus = 4;
                try
                {
                    foreach (Thread t in monitorThread)
                    {
                        t.Abort();
                    }
                    monitorThread.Clear();
                    poolStatus = 1;
                }
                catch
                {
                    poolStatus = 6;
                    throw;
                }
            }
        }


        public string getDisplayTableHtml(int rowCount)
        {
            string cnt = "<table class=\"table table-striped table-condensed\"><tr><td>#</td><td>IP</td><td>Port</td><td>Location</td><td>Type</td><td>Protocal</td><td>Status</td></tr>";
            if (poolStatus != 0)
            {
                List<ProxyServer> psl = new List<ProxyServer>();
                psl.AddRange(activeProxyServerList);
                psl.AddRange(toBeValidProxyServerList);
                int tag1 = 0;
                int tag2 = 0;
                if (rowCount < activeProxyServerList.Count)
                {
                    tag1 = rowCount;
                }
                else
                {
                    tag1 = activeProxyServerList.Count;
                    if (rowCount - activeProxyServerList.Count < toBeValidProxyServerList.Count)
                    {
                        tag2 = rowCount;
                    }
                    else
                    {
                        tag2 = activeProxyServerList.Count + toBeValidProxyServerList.Count;
                    }
                }
                for (int i = 0; i < tag1; i++)
                {
                    cnt = cnt + "<tr><td>" + (i + 1).ToString() + "</td><td>" + psl[i].ProxyIpAddress + "</td><td>"
                        + psl[i].ProxyPort.ToString() + "</td><td>"
                        + psl[i].ProxyLocation.Replace(" ", "").Replace("<br>", "") + "</td><td>"
                        + psl[i].ProxyType.Replace(" ", "").Replace("<br>", "") + "</td><td>"
                        + psl[i].ProxyProtocal.Replace(" ", "").Replace("<br>", "") + "</td><td>active</td></tr>";
                }
                for (int i = tag1; i < tag2; i++)
                {
                    cnt = cnt + "<tr><td>" + (i + 1).ToString() + "</td><td>" + psl[i].ProxyIpAddress + "</td><td>"
                        + psl[i].ProxyPort.ToString() + "</td><td>"
                        + psl[i].ProxyLocation.Replace(" ", "").Replace("<br>", "") + "</td><td>"
                        + psl[i].ProxyType.Replace(" ", "").Replace("<br>", "") + "</td><td>"
                        + psl[i].ProxyProtocal.Replace(" ", "").Replace("<br>", "") + "</td><td>TBD</td></tr>";
                }
            }
            cnt = cnt + "</table>";
            return cnt;
        }
    }
}