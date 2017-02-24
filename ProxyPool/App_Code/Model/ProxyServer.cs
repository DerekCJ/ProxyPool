using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace ProxyPool
{
    /// <summary>
    /// ProxyServer 的摘要说明
    /// </summary>
    public class ProxyServer
    {
        #region Attribute
        private int proxyServerId;
        private int proxySourceId;
        private string proxyIpAddress;
        private int proxyPort;
        private string proxyProtocal;
        private string proxyRequestMethod;
        private string proxyLocation;
        private string proxyType;
        private string proxyUser;
        private string proxyPass;
        private string proxyDomain;
        private int proxyStatus;
        private DateTime createTime;

        public int ProxyServerId
        {
            get
            {
                return proxyServerId;
            }

            set
            {
                proxyServerId = value;
            }
        }

        public int ProxySourceId {
            get { return proxySourceId; }
            set { proxySourceId = value; }
        }
        public string ProxyIpAddress
        {
            get
            {
                return proxyIpAddress;
            }

            set
            {
                proxyIpAddress = value;
            }
        }

        public int ProxyPort
        {
            get
            {
                return proxyPort;
            }

            set
            {
                proxyPort = value;
            }
        }

        public string ProxyProtocal
        {
            get
            {
                return proxyProtocal;
            }

            set
            {
                proxyProtocal = value;
            }
        }

        public string ProxyRequestMethod
        {
            get
            {
                return proxyRequestMethod;
            }

            set
            {
                proxyRequestMethod = value;
            }
        }

        public string ProxyLocation
        {
            get
            {
                return proxyLocation;
            }

            set
            {
                proxyLocation = value;
            }
        }

        public string ProxyType
        {
            get
            {
                return proxyType;
            }

            set
            {
                proxyType = value;
            }
        }

        public string ProxyUser
        {
            get
            {
                return proxyUser;
            }

            set
            {
                proxyUser = value;
            }
        }

        public string ProxyPass
        {
            get
            {
                return proxyPass;
            }

            set
            {
                proxyPass = value;
            }
        }

        public string ProxyDomain
        {
            get
            {
                return proxyDomain;
            }

            set
            {
                proxyDomain = value;
            }
        }

        /// <summary>
        /// 0 - 初始化状态；1 - 有效；-1 - 失效
        /// </summary>
        public int ProxyStatus
        {
            get
            {
                return proxyStatus;
            }

            set
            {
                proxyStatus = value;
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

        #endregion
        public ProxyServer() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="protocal"></param>
        /// <param name="requestMethod"></param>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="domain"></param>
        /// <param name="status">1 - 有效；0 - 未验证，-1 - 失效</param>
        /// <param name="cTime"></param>
        /// /// <param name="id"></param>
        public ProxyServer(int src,string ip, int port, string protocal, string requestMethod, string location, string type, string user, string pass, string domain, int status, DateTime cTime, int id = 0)
        {
            proxySourceId = src;
            proxyServerId = id;
            proxyIpAddress = ip;
            proxyPort = port;
            proxyProtocal = protocal;
            proxyRequestMethod = requestMethod;
            proxyLocation = location;
            proxyType = type;
            proxyUser = user;
            proxyPass = pass;
            proxyDomain = domain;
            proxyStatus = status;
            createTime = cTime;
        }

        public WebProxy getProxy()
        {
            WebProxy wb = new WebProxy(ProxyIpAddress, ProxyPort);
            if (!string.IsNullOrEmpty(proxyUser))
            {
                wb.Credentials = new NetworkCredential(proxyUser, proxyPass, proxyDomain);
            }
            return wb;
        }

        public int saveToDB()
        {
            string sql= "insert into tb_proxy (pxy_src_id,pxy_ip_add,pxy_port,pxy_protocal,pxy_request_method,pxy_location,pxy_type,pxy_user,pxy_pass,pxy_domain,pxy_status,pxy_create_time)"
                + "values ("+proxySourceId.ToString()+",'"+proxyIpAddress+"',"+proxyPort.ToString()+",'"+proxyProtocal+"','"+proxyRequestMethod+"','"+proxyLocation+"','"+proxyType+"','"+proxyUser+"','"+proxyPass+"','"+proxyDomain+"',"+proxyStatus.ToString()+",'"+createTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";
            sql = new StringAdapter().sqlSafe(sql);
            return RunningCathe.DbHelper.ExecNonQuery(sql);
        }

        public string toJsonString()
        {
            string jStr = "{\"ip_add\" : \"" + proxyIpAddress + "\" , \"port\" : \"" + proxyPort + "\" , \"protocal\" : \"" + proxyProtocal 
                + "\" , \"request_method\" : \"" + proxyRequestMethod + "\" , \"location\" : \"" + proxyLocation + "\" , \"type\" : \"" + proxyType 
                + "\" , \"user\" : \"" + proxyUser + "\" , \"pass\" : \"" + proxyPass + "\" , \"domain\" : \"" + proxyDomain + "\"}";
            return jStr;
        }
    }

    public class ProxyServerCompare : IEqualityComparer<ProxyServer>
    {

        public bool Equals(ProxyServer x, ProxyServer y)
        {
            if (x.ProxyIpAddress == y.ProxyIpAddress && x.ProxyPort == y.ProxyPort)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(ProxyServer obj)
        {
            if (obj == null)
                return 0;
            else
                return obj.ToString().GetHashCode();
        }

    }
}