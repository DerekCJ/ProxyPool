using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace ProxyPool
{
    /// <summary>
    /// ProxySource 的摘要说明
    /// </summary>
    public class ProxySource
    {
        #region Parameter
        private int id;
        private string name;
        private int catheSize;
        private string url;
        private string para;
        private int requestTimeSpan;
        private int refreshTimeSpan;
        private string charset;
        private string requestMethod;
        private string docType;
        private string searchType;
        private string ipAddressSearch;
        private string portSearch;
        private string protocalSearch;
        private string requestMethodSearch;
        private string locationSearch;
        private string typeSearch;
        private string userSearch;
        private string passSearch;
        private string domainSearch;
        private DateTime createTime;
        private List<string> allUrl;
        private List<ProxyServer> proxyServerList;
        private Thread refreshThread;
        private Object thisLock = new Object();
        #endregion

        #region Attribute
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string Para
        {
            get
            {
                return para;
            }

            set
            {
                para = value;
            }
        }

        public int RequestTimeSpan
        {
            get { return requestTimeSpan; }
            set { requestTimeSpan = value; }
        }
        public int RefreshTimeSpan
        {
            get { return refreshTimeSpan; }
            set { refreshTimeSpan = value; }
        }
        public string Charset
        {
            get
            {
                return charset;
            }

            set
            {
                charset = value;
            }
        }

        public string RequestMethod
        {
            get
            {
                return requestMethod;
            }

            set
            {
                requestMethod = value;
            }
        }

        public string DocType
        {
            get
            {
                return docType;
            }

            set
            {
                docType = value;
            }
        }

        public string SearchType
        {
            get
            {
                return searchType;
            }

            set
            {
                searchType = value;
            }
        }

        public string IpAddressSearch
        {
            get
            {
                return ipAddressSearch;
            }

            set
            {
                ipAddressSearch = value;
            }
        }

        public string PortSearch
        {
            get
            {
                return portSearch;
            }

            set
            {
                portSearch = value;
            }
        }

        public string ProtocalSearch
        {
            get
            {
                return protocalSearch;
            }

            set
            {
                protocalSearch = value;
            }
        }

        public string RequestMethodSearch
        {
            get
            {
                return requestMethodSearch;
            }

            set
            {
                requestMethodSearch = value;
            }
        }

        public string LocationSearch
        {
            get
            {
                return locationSearch;
            }

            set
            {
                locationSearch = value;
            }
        }

        public string TypeSearch
        {
            get
            {
                return typeSearch;
            }

            set
            {
                typeSearch = value;
            }
        }

        public string UserSearch
        {
            get
            {
                return userSearch;
            }

            set
            {
                userSearch = value;
            }
        }

        public string PassSearch
        {
            get
            {
                return passSearch;
            }

            set
            {
                passSearch = value;
            }
        }

        public string DomainSearch
        {
            get
            {
                return domainSearch;
            }

            set
            {
                domainSearch = value;
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

        public List<string> AllUrl
        {
            get
            {
                return allUrl;
            }

            set
            {
                allUrl = value;
            }
        }

        public List<ProxyServer> ProxyServerList
        {
            get
            {
                return proxyServerList;
            }

            set
            {
                proxyServerList = value;
            }
        }

        public Thread RefreshThread
        {
            get
            {
                return refreshThread;
            }

            set
            {
                refreshThread = value;
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

        #endregion

        public ProxySource(string srcName,int cSize,string srcUrl, string srcPara,int srcRequestTimeSpan, int srcRefreshTimeSpan, string srcCharset, string srcRequestMethod, string srcDocType
            , string srcSearchType, string srcIpAddressSearch, string srcPortSearch, string srcProtocalSearch, string srcRequestMethodSearch
            , string srcLocationSearch, string srcTypeSearch, string srcUserSearch, string srcPassSearch, string srcDomainSearch, DateTime cTime, int srcId = 0)
        {
            id = srcId;
            name = srcName;
            catheSize = cSize;
            url = srcUrl;
            para = srcPara;
            requestTimeSpan = srcRequestTimeSpan;
            refreshTimeSpan = srcRefreshTimeSpan;
            charset = srcCharset;
            requestMethod = srcRequestMethod;
            docType = srcDocType;
            searchType = srcSearchType;
            ipAddressSearch = srcIpAddressSearch;
            portSearch = srcPortSearch;
            protocalSearch = srcProtocalSearch;
            requestMethodSearch = srcRequestMethodSearch;
            locationSearch = srcLocationSearch;
            typeSearch = srcTypeSearch;
            userSearch = srcUserSearch;
            passSearch = srcPassSearch;
            domainSearch = srcDomainSearch;
            createTime = cTime;
            allUrl = processURL(srcUrl, srcPara);
            proxyServerList = new List<ProxyServer>();
        }

        public List<string> processURL(string url, string para)
        {
            List<string> r = new List<string>();
            int paraCnt = 0;
            Regex reg = new Regex(@"\[\*\]");
            paraCnt = reg.Matches(url).Count;
            string[] p = para.Split('|');
            if (paraCnt != p.Length)
            {
                return r;
            }
            else
            {
                r.Add(url);
                for (int i = 0; i < p.Length; i++)
                {
                    r = makeUpUrl(r, "[*]", p[i]);
                }
            }
            return r;
        }

        protected List<string> makeUpUrl(List<string> url, string sep, string para)
        {
            List<string> r = new List<string>();
            string[] p = para.Split(',');
            int sp, cnt, step;
            for (int i = 0; i < url.Count; i++)
            {
                sp = int.Parse(p[0]);
                cnt = int.Parse(p[1]);
                step = int.Parse(p[2]);
                int idx = url[i].IndexOf(sep);
                string prefix = url[i].Substring(0, idx);
                string suffix = url[i].Substring(idx + sep.Length);
                for (int j = sp; cnt > 0; cnt--)
                {
                    r.Add(prefix + j.ToString() + suffix);
                    j = j + step;
                }
            }
            return r;
        }

        /// <summary>
        /// 根据配置的url及参数，从代理源查询最新的代理清单。
        /// 根据入参将结果保存到ProxyServerList，并去重
        /// </summary>
        /// <param name="updateProxyServerList">是否更新ProxyServerList</param>
        /// <param name="saveToDB">是否保存到DB</param>
        /// <param name="distinct">是否去重</param>
        /// <returns></returns>
        public List<ProxyServer> retrieveProxySource(bool updateProxyServerList=true,bool saveToDB=true,bool rtnDistinct=true)
        {
            //校验缓存池是否超容
            if (catheSize < proxyServerList.Count)
            {
                RunningCathe.addLog(new Log(DateTime.Now, "从代理源[" + this.name + "]获取代理服务器时发生异常：缓存池容量已满。当前缓存代理数量："+ proxyServerList.Count+",缓存池容量："+catheSize.ToString(), 0, 0, this.id));
                return null;
            }

            if (allUrl.Count > 0)
            {
                List<ProxyServer> psList = new List<ProxyServer>();
                for (int i = 0; i < allUrl.Count; i++)
                {
                    try
                    {
                        System.Net.WebProxy wb = RunningCathe.getAnyActiveProxy();
                        var task = HttpAdapter.CreateGetHttpResponse(allUrl[i], "", null, wb, 0);
                        var response = task.Result;
                        List<ProxyServer> psl = new List<ProxyServer>();
                        using (Stream stream = response.GetResponseStream())
                        {
                            //StreamReader sr = new StreamReader(stream, System.Text.Encoding.GetEncoding(charset));
                            StreamReader sr = new StreamReader(stream, System.Text.Encoding.GetEncoding(charset), true);
                            //StreamReader sr = new StreamReader(stream, true);

                            string content = sr.ReadToEnd();


                            if (searchType.Equals("XPATH", StringComparison.OrdinalIgnoreCase))
                            {
                                psl = getProxyServerByXpath(content);
                            }
                            else
                            {
                                psl = getProxyServerByRegex(content);
                            }
                        }
                        psList.AddRange(psl);
                        RunningCathe.addLog(new Log(DateTime.Now, "从" + allUrl[i] + "获取代理服务器" + psl.Count.ToString() + "个", 0, 0, this.id));
                        Thread.Sleep(requestTimeSpan);
                    }
                    catch (Exception ex)
                    {
                        RunningCathe.addLog(new Log(DateTime.Now, "从" + allUrl[i] + "获取代理服务器时发生异常：" + ex.Message, 0, 0, this.id));
                    }
                }
                int originalSize;
                lock (thisLock)
                {
                    originalSize = proxyServerList.Count;
                    if (updateProxyServerList)
                    {
                        proxyServerList.AddRange(psList);
                        proxyServerList = proxyServerList.Distinct(new ProxyServerCompare()).ToList();
                    }
                }
                RunningCathe.addLog(new Log(DateTime.Now, "从代理源\"" + this.id + "-" + this.name + "\"获取代理服务器" + (proxyServerList.Count - originalSize).ToString() + "个",0,0,this.id));
                if (saveToDB)
                {
                    for (int i = originalSize; i < proxyServerList.Count; i++)
                    {
                        try
                        {
                            proxyServerList[i].saveToDB();
                        }
                        catch (Exception ex)
                        {
                            RunningCathe.addLog(new Log(DateTime.Now, "保存代理服务器信息时出错：" + proxyServerList[i].ProxyIpAddress + "," + proxyServerList[i].ProxyPort + "," + proxyServerList[i].ProxyProtocal
                                + "," + proxyServerList[i].ProxyLocation + "," + proxyServerList[i].ProxyType + "。" + ex.Message, 0, 0, this.id));
                        }
                    }
                }
                if (rtnDistinct)
                {
                    return proxyServerList.GetRange(originalSize, proxyServerList.Count - originalSize);
                }
                else
                {
                    return psList;
                }
            }
            else
            {
                return null;
            }            
        }

        private void threadRunRefreshSource()
        {
            while (true)
            {
                List<ProxyServer> ps = retrieveProxySource(true, true, true);
                //推送代理更新到代理池
                RunningCathe.refreshPoolProxyFromSource(this);
                //如果proxyServerList元素个数超过缓存容量，则调用缓存垃圾收集
                if (proxyServerList.Count > catheSize)
                {
                    RunningCathe.catheTrashCollectBySource(this);
                }
                Thread.Sleep(refreshTimeSpan);
            }
        }

        public void startRefreshThread()
        {
            if (refreshThread==null||!refreshThread.IsAlive)
            {
                refreshThread = new Thread(new ThreadStart(threadRunRefreshSource));
                refreshThread.Start(); 
            }
            else
            {
                Exception ex = new Exception("已存在刷新进程");
                throw ex;
            }
        }
        public void stopRefreshThread()
        {
            if (refreshThread == null)
            {
                Exception ex = new Exception("不存在刷新进程");
                throw ex;
            }
            else
            {
                try
                {
                    refreshThread.Abort();
                }
                catch { throw; }
            }
        }

        #region 以XPATH方式解析代理IP服务器信息
        
        public List<ProxyServer> getProxyServerByXpath(string htmlContent)
        {
            List<ProxyServer> r = new List<ProxyServer>();
            HtmlDocument hd = new HtmlDocument();

            hd.LoadHtml(htmlContent);
            int pxyCount = 0;
            HtmlNodeCollection pxyIp = null;
            if (string.IsNullOrEmpty(ipAddressSearch))
            {
                throw new Exception("IP Address Search cannot be empty");
            }
            else {
                pxyIp = hd.DocumentNode.SelectNodes(ipAddressSearch);
            }
            if (pxyIp == null)
            {
                throw new Exception("No Proxy IP Founded");
            }
            else {
                pxyCount = pxyIp.Count;
            }

            HtmlNodeCollection pxyPort = null;
            if (string.IsNullOrEmpty(portSearch))
            {
                throw new Exception("Port Search cannot be empty");
            }
            else {
                pxyPort = getSearchResultbyXpath(hd, portSearch, pxyCount, "Number Does Not Match : Port vs IP");
            }
            if (pxyPort == null)
            {
                throw new Exception("No Proxy Port Founded");
            }

            HtmlNodeCollection pxyProtocal = getSearchResultbyXpath(hd, protocalSearch, pxyCount, "Number Does Not Match : Protocal vs IP");
            HtmlNodeCollection pxyRequestMethod = getSearchResultbyXpath(hd, requestMethodSearch, pxyCount, "Number Does Not Match : RequestMethod vs IP");
            HtmlNodeCollection pxyLocation = getSearchResultbyXpath(hd, locationSearch, pxyCount, "Number Does Not Match : Location vs IP");
            HtmlNodeCollection pxyType = getSearchResultbyXpath(hd, typeSearch, pxyCount, "Number Does Not Match : Type vs IP");
            HtmlNodeCollection pxyUser = getSearchResultbyXpath(hd, userSearch, pxyCount, "Number Does Not Match : User vs IP");
            HtmlNodeCollection pxyPass = getSearchResultbyXpath(hd, passSearch, pxyCount, "Number Does Not Match : Password vs IP");
            HtmlNodeCollection pxyDomain = getSearchResultbyXpath(hd, domainSearch, pxyCount, "Number Does Not Match : Domain vs IP");

            for (int i = 0; i < pxyCount; i++)
            {
                string ip = pxyIp[i].InnerText;
                int port = int.Parse(pxyPort[i].InnerText);
                string protocal = string.IsNullOrEmpty(protocalSearch) ? "" : pxyProtocal[i].InnerText;
                string requestMethod = string.IsNullOrEmpty(requestMethodSearch) ? "" : pxyRequestMethod[i].InnerText;
                string location = string.IsNullOrEmpty(locationSearch) ? "" : pxyLocation[i].InnerText;
                string type = string.IsNullOrEmpty(typeSearch) ? "" : pxyType[i].InnerText;
                string user = string.IsNullOrEmpty(userSearch) ? "" : pxyUser[i].InnerText;
                string pass = string.IsNullOrEmpty(passSearch) ? "" : pxyPass[i].InnerText;
                string domain = string.IsNullOrEmpty(domainSearch) ? "" : pxyDomain[i].InnerText;
                r.Add(new ProxyServer(id, ip, port, protocal, requestMethod, location, type, user, pass, domain, 0, DateTime.Now));
            }
            return r;
        }

        private HtmlNodeCollection getSearchResultbyXpath(HtmlDocument hd, string srch, int nbr, string exMsg)
        {

            HtmlNodeCollection hnc = string.IsNullOrEmpty(srch)? null:hd.DocumentNode.SelectNodes(srch);
            if (hnc != null && hnc.Count != nbr)
            {
                throw new Exception(exMsg);
            }
            return hnc;
        }

        #endregion


        #region 以Regex方式解析代理IP服务器信息
        /// <summary>
        /// 以Regex方式解析HTML源码获取代理服务器信息
        /// </summary>
        /// <param name="content">HTML源码</param>
        /// <returns></returns>
        public List<ProxyServer> getProxyServerByRegex(string htmlContent)
        { 

            return null;
        }

        #endregion
    }
}