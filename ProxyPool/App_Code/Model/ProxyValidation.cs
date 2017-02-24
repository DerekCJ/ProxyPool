using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace ProxyPool
{
    /// <summary>
    /// ProxyValidation 的摘要说明
    /// </summary>
    public class ProxyValidation
    {
        #region Attribute
        private int validationId;
        private string validationName;
        private string validationUrl;
        private string validationRequestMethod;
        private string passRegex;
        private int failTimeout;
        private int failAttemps;
        private int validationStatus;
        private DateTime createTime;
        public int ValidationId
        {
            get
            {
                return validationId;
            }

            set
            {
                validationId = value;
            }
        }

        public string ValidationName
        {
            get
            {
                return validationName;
            }

            set
            {
                validationName = value;
            }
        }

        public string ValidationUrl
        {
            get
            {
                return validationUrl;
            }

            set
            {
                validationUrl = value;
            }
        }

        public string ValidationRequestMethod
        {
            get
            {
                return validationRequestMethod;
            }

            set
            {
                validationRequestMethod = value;
            }
        }

        public string PassRegex
        {
            get
            {
                return passRegex;
            }

            set
            {
                passRegex = value;
            }
        }

        public int FailTimeout
        {
            get
            {
                return failTimeout;
            }

            set
            {
                failTimeout = value;
            }
        }

        public int FailAttemps
        {
            get
            {
                return failAttemps;
            }

            set
            {
                failAttemps = value;
            }
        }

        public int ValidationStatus
        {
            get
            {
                return validationStatus;
            }

            set
            {
                validationStatus = value;
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
        public ProxyValidation() { }
       public ProxyValidation(string name, string url, string requestMethod, string regex, int tOut, int attemps, int status, DateTime cTime, int id = 0)
        {
            validationId = id;
            validationName = name;
            validationUrl = url;
            validationRequestMethod = requestMethod;
            passRegex = regex;
            failTimeout = tOut;
            failAttemps = attemps;
            validationStatus = status;
            createTime = cTime;
        }
        public bool validateProxyServer(ProxyServer proxyServer = null)
        {
            bool r = false;
            WebProxy wb = null;
            if (proxyServer != null)
                wb = proxyServer.getProxy();
            var task = HttpAdapter.CreateGetHttpResponse(validationUrl, "", null, wb, failTimeout);
            int flag = 0;
            while (flag < FailAttemps)
            {
                try
                {
                    var response = task.Result;
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream, true);
                        Regex reg = new Regex(passRegex);
                        if (reg.Matches(sr.ReadToEnd()).Count > 0)
                        {
                            r = true;
                        }
                    }
                    break;
                }
                catch
                {
                    flag = flag + 1;
                }
            }
            return r;
        }
    }
}