using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Threading;
using System.Net;

namespace ProxyPool
{
    /// <summary>
    /// RunningCathe 的摘要说明
    /// </summary>
    public static class RunningCathe
    {
        public static DbAdapter DbHelper = new DbAdapter();
        public static List<ProxySource> AllProxySource = new List<ProxySource>();
        public static List<Pool> AllProxyPool = new List<Pool>();
        public static List<ProxyValidation> AllProxyValidation = new List<ProxyValidation>();
        public static List<Log> SavedLog = new List<Log>();
        public static List<Log> ToBeSavedLog = new List<Log>();
        public static Thread LogSaveThread, LogCatheCheckThread;
        private static object savedLogLock = new object();
        private static object toBeSavedLogLock = new object();
        private static object poolProxyListUpdateLock = new object();
        //日志保存操作间隔（毫秒）
        private static int logSaveTimeSpan = 30000;
        //日志缓存清理操作间隔（毫秒）
        private static int logCatheCheckTimeSpan = 60000;
        //最大日志缓存数量
        private static int maxLogCathe = 10000;
        //工作台显示缓存清单
        public static DataTable DisplayList = new DataTable();

        public static bool SourceRunning = false;
        public static bool PoolRunning = false;
        /// <summary>
        /// 从数据库中读取记录，初始化pool,validation,source以及source中的历史代理记录
        /// </summary>
        public static void initialConfigurations()
        {
            DataTable pxySource = DbHelper.ExecQuery("select * from tb_proxy_source");
            AllProxySource.Clear();
            for (int i = 0; i < pxySource.Rows.Count; i++)
            {
                AllProxySource.Add(new ProxySource(pxySource.Rows[i][1].ToString(), int.Parse(pxySource.Rows[i][2].ToString()), pxySource.Rows[i][3].ToString(), pxySource.Rows[i][4].ToString()
                    , int.Parse(pxySource.Rows[i][5].ToString()), int.Parse(pxySource.Rows[i][6].ToString()), pxySource.Rows[i][7].ToString(), pxySource.Rows[i][8].ToString(), pxySource.Rows[i][9].ToString()
                    , pxySource.Rows[i][10].ToString(), pxySource.Rows[i][11].ToString(), pxySource.Rows[i][12].ToString(), pxySource.Rows[i][13].ToString(), pxySource.Rows[i][14].ToString()
                    , pxySource.Rows[i][15].ToString(), pxySource.Rows[i][16].ToString(), pxySource.Rows[i][17].ToString(), pxySource.Rows[i][18].ToString(), pxySource.Rows[i][19].ToString()
                    , DateTime.Parse(pxySource.Rows[i][20].ToString()), int.Parse(pxySource.Rows[i][0].ToString())));
            }

            DataTable pxyValidation = DbHelper.ExecQuery("select * from tb_validation");
            AllProxyValidation.Clear();
            for (int i = 0; i < pxyValidation.Rows.Count; i++)
            {
                AllProxyValidation.Add(new ProxyValidation(pxyValidation.Rows[i][1].ToString(), pxyValidation.Rows[i][2].ToString(), pxyValidation.Rows[i][3].ToString(), pxyValidation.Rows[i][4].ToString()
                    , int.Parse(pxyValidation.Rows[i][5].ToString()), int.Parse(pxyValidation.Rows[i][6].ToString()), int.Parse(pxyValidation.Rows[i][7].ToString()), DateTime.Parse(pxyValidation.Rows[i][8].ToString())
                    , int.Parse(pxyValidation.Rows[i][0].ToString())));
            }

            DataTable pxyPool = DbHelper.ExecQuery("select * from tb_pool order by pool_id asc");
            AllProxyPool.Clear();
            int maxSize = 0;
            for (int i = 0; i < pxyPool.Rows.Count; i++)
            {
                AllProxyPool.Add(new Pool(pxyPool.Rows[i][1].ToString(), AllProxyValidation.Find(x => x.ValidationId == int.Parse(pxyPool.Rows[i][2].ToString())), int.Parse(pxyPool.Rows[i][3].ToString())
                    , int.Parse(pxyPool.Rows[i][4].ToString()), AllProxySource.FindAll(x => pxyPool.Rows[i][5].ToString().Split(',').Contains(x.Id.ToString())), int.Parse(pxyPool.Rows[i][6].ToString())
                    , int.Parse(pxyPool.Rows[i][7].ToString()), DateTime.Parse(pxyPool.Rows[i][8].ToString()), int.Parse(pxyPool.Rows[i][0].ToString())));

                maxSize = AllProxyPool[i].CatheSize > maxSize ? AllProxyPool[i].CatheSize : maxSize;
            }

            DataTable pxyServer = DbHelper.ExecQuery("select top " + maxSize + " * from tb_proxy order by pxy_src_id asc,pxy_create_time desc");
            for (int i = 0; i < pxyServer.Rows.Count; i++)
            {
                AllProxySource.Find(x => x.Id.ToString() == pxyServer.Rows[i][1].ToString()).ProxyServerList.Add(new ProxyServer(int.Parse(pxyServer.Rows[i][1].ToString()), pxyServer.Rows[i][2].ToString(), int.Parse(pxyServer.Rows[i][3].ToString())
                    , pxyServer.Rows[i][4].ToString(), pxyServer.Rows[i][5].ToString(), pxyServer.Rows[i][6].ToString(), pxyServer.Rows[i][7].ToString(), pxyServer.Rows[i][8].ToString(), pxyServer.Rows[i][9].ToString()
                    , pxyServer.Rows[i][10].ToString(), int.Parse(pxyServer.Rows[i][11].ToString()), DateTime.Parse(pxyServer.Rows[i][12].ToString()), int.Parse(pxyServer.Rows[i][0].ToString())));
            }

            DisplayList = new DataTable();
            DisplayList.Columns.Add("pool_id", typeof(int));
            DisplayList.Columns.Add("pool_name", typeof(string));
            DisplayList.Columns.Add("pool_ip_table", typeof(string));
            DisplayList.Columns.Add("display_count", typeof(int));
            DisplayList.Columns.Add("active_count", typeof(int));
            DisplayList.Columns.Add("tbd_count", typeof(int));
            DisplayList.Columns.Add("cathe_count", typeof(int));
            DisplayList.Columns.Add("status", typeof(string));
            DisplayList.PrimaryKey = new DataColumn[] { DisplayList.Columns[0] };
        }

        public static void oneKeyStart(bool refreshConfiguration = true)
        {
            if (refreshConfiguration)
            {
                initialConfigurations();
            }
            startRefreshAllSource();
            startAllPool();
        }

        public static void oneKeyStop()
        {
            stopRefreshAllSource();
            stopAllPool();
        }

        #region Information Summary
        public static List<ProxyServer> getCurrentAllProxy()
        {
            List<ProxyServer> r = new List<ProxyServer>();
            foreach (Pool p in AllProxyPool)
            {
                r.AddRange(p.AllProxyServerList);
            }
            r = r.Distinct(new ProxyServerCompare()).ToList();
            return r;
        }
        public static List<ProxyServer> getCurrentAllActiveProxy()
        {
            List<ProxyServer> r = new List<ProxyServer>();
            foreach (Pool p in AllProxyPool)
            {
                r.AddRange(p.ActiveProxyServerList);
            }
            r = r.Distinct(new ProxyServerCompare()).ToList();
            return r;
        }

        public static List<ProxyServer> getCurrentAllToBeValidProxy()
        {
            List<ProxyServer> r = new List<ProxyServer>();
            foreach (Pool p in AllProxyPool)
            {
                r.AddRange(p.ToBeValidProxyServerList);
            }
            r = r.Distinct(new ProxyServerCompare()).ToList();
            return r;
        }
        #endregion

        #region 缓存清理
        /// <summary>
        /// 针对某一代理源的缓存垃圾回收
        /// 当某一代理源中缓存代理服务器数量超过，该代理源缓存容量时，触发该代理源的缓存垃圾回收事件
        /// 
        /// 缓存垃圾是指在所有将其纳入的代理池中均被验证为无效代理的ProxyServer
        /// 即仅存在于allProxyServerList中的ProxyServer（不存在于activeProxyServerList,toBeValidProxyServerList,inactiveProxyServerList中）
        /// </summary>
        public static void catheTrashCollectBySource(ProxySource pSrc)
        {
            foreach (Pool p in AllProxyPool)
            {
                if (p.PoolSource.Contains(pSrc))
                {
                    List<ProxyServer> toBeCollected = p.AllProxyServerList.Except(p.ActiveProxyServerList).Except(p.ToBeValidProxyServerList).ToList();
                    lock (poolProxyListUpdateLock)
                    {
                        pSrc.ProxyServerList = pSrc.ProxyServerList.Except(toBeCollected).ToList();
                        p.AllProxyServerList = p.AllProxyServerList.Except(toBeCollected).ToList();
                    }
                    toBeCollected = null;
                }
            }
        }

        /// <summary>
        /// 全部代理源缓存垃圾回收
        /// 
        /// </summary>
        public static void catheTrashCollect()
        {
            foreach (ProxySource pSrc in AllProxySource)
            {
                catheTrashCollectBySource(pSrc);
            }
            GC.Collect();
        }

        #endregion

        #region 代理源刷新启停
        public static void startRefreshAllSource()
        {
            foreach (ProxySource pSrc in AllProxySource)
            {
                pSrc.startRefreshThread();
            }
        }
        public static void stopRefreshAllSource()
        {
            foreach (ProxySource pSrc in AllProxySource)
            {
                try
                {
                    pSrc.stopRefreshThread();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "不存在刷新进程")
                    { continue; }
                    else
                    { throw; }
                }
            }
        }
        public static void startRefreshSource(int sourceID)
        {
            ProxySource pSrc = AllProxySource.Find(x => x.Id == sourceID);
            pSrc.startRefreshThread();
        }
        public static void startRefreshSource(ProxySource pSrc)
        {
            pSrc.startRefreshThread();
        }
        public static void stopRefreshSource(int sourceID)
        {
            ProxySource pSrc = AllProxySource.Find(x => x.Id == sourceID);
            pSrc.stopRefreshThread();
        }
        public static void stopRefreshSource(ProxySource pSrc)
        {
            pSrc.stopRefreshThread();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="pSrc"></param>
        /// <param name="pServerList"></param>
        public static void refreshPoolProxyFromSource(ProxySource pSrc, List<ProxyServer> pServerList = null)
        {
            foreach (Pool p in AllProxyPool)
            {
                lock (poolProxyListUpdateLock)
                {
                    p.refreshProxyServerFromSource(pSrc);
                }
            }
        }
        #endregion

        #region 代理池验证启停
        public static void startAllPool()
        {
            foreach (Pool p in AllProxyPool)
            {
                try
                {
                    p.initialPoolProxyServerList();
                    p.startValidation();
                }
                catch
                {
                    //日志待添加
                }
            }
        }

        public static void stopAllPool()
        {
            foreach (Pool p in AllProxyPool)
            {
                p.stopValidation();
            }
        }
        public static void startPool(int poolId)
        {
            Pool p = AllProxyPool.Find(x => x.Id == poolId);
            p.initialPoolProxyServerList();
            p.startValidation();
        }
        public static void startPool(Pool p)
        {
            p.initialPoolProxyServerList();
            p.startValidation();
        }
        public static void stopPool(int poolId)
        {
            Pool p = AllProxyPool.Find(x => x.Id == poolId);
            p.stopValidation();
        }
        public static void stopPool(Pool p)
        {
            p.stopValidation();
        }
        #endregion

        #region 日志缓存管理

        public static void addLog(Log l)
        {
            lock(toBeSavedLogLock)
            {
                ToBeSavedLog.Add(l);
            }
        }

        /// <summary>
        /// 这里需要优化数据库写入
        /// </summary>
        private static void saveLogProcessing()
        {
            foreach (Log l in ToBeSavedLog)
            {
                try
                {
                    l.saveLog();
                    lock (savedLogLock)
                    {
                        SavedLog.Add(l);
                    }
                    lock (toBeSavedLogLock)
                    {
                        ToBeSavedLog.Remove(l);
                    }
                }
                catch {
                    //如果保存日志时出错的处理
                }
            }
            Thread.Sleep(logSaveTimeSpan);
        }
        private static void checkLogCatheProcessing()
        {
            int savedLogSize = SavedLog.Count;
            int toBeSavedLogSize = ToBeSavedLog.Count;
            int toBeClear = savedLogSize + toBeSavedLogSize - maxLogCathe;
            if (toBeClear > 0)
            {
                lock(savedLogLock)
                {
                    SavedLog.RemoveRange(0, toBeClear);
                }
            }
            Thread.Sleep(logCatheCheckTimeSpan);
        }
        public static void runLogSaveThread()
        {
            if (LogSaveThread == null)
            {
                LogSaveThread = new Thread(new ThreadStart(saveLogProcessing));
            }
            else
            {
                throw new Exception("已存在日志保存线程");
            }
        }
        public static void stopLogSaveThread()
        {
            if (LogSaveThread != null)
                LogSaveThread.Abort();
        }
        public static void runLogCatheCheckThread()
        {
            if (LogCatheCheckThread == null)
            {
                LogCatheCheckThread = new Thread(new ThreadStart(checkLogCatheProcessing));
            }
            else
            {
                throw new Exception("已存在日志缓存清理线程");
            }
        }
        public static void stopLogCatheCheckThread()
        {
            if (LogCatheCheckThread != null)
                LogCatheCheckThread.Abort();
        }
        #endregion

        public static WebProxy getAnyActiveProxy()
        {
            List<Pool> pList = AllProxyPool.FindAll(x => x.PoolStatus != 0);
            Pool p = null;
            while (pList.Count > 0)
            {
                int idx = new Random().Next(pList.Count - 1);
                p = pList[idx];
                if (p.ActiveProxyServerList.Count > 0)
                {
                    break;
                }
                else
                {
                    pList.RemoveAt(idx);
                }
            }
            WebProxy wb = null;
            int activeSize = p.ActiveProxyServerList.Count;
            if (activeSize > 0)
            {
                wb = p.ActiveProxyServerList[new Random().Next(activeSize - 1)].getProxy();
            }
            return wb;
        }
    }
}