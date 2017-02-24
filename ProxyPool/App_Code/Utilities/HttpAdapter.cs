using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPool
{
    public class HttpAdapter
    {
        //private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.8.1.16) Gecko/20080702 Firefox/2.0.0.16";
        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:51.0) Gecko/20100101 Firefox/51.0";
        //private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2986.0 Safari/537.36";
        //public static WebProxy defaultProxy = new WebProxy("10.2.2.254",50001);
        /// <summary>         
        /// 创建POST方式的HTTP请求          
        /// </summary>         
        /// <param name="url">请求的URL</param>          
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>          
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>         
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>         
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>    
        /// <returns></returns>         
        public static Task<WebResponse> CreatePostHttpResponse(string url, IDictionary<string, string> parameters, string userAgent, Encoding requestEncoding, CookieContainer cookieContainer, WebProxy wb,int timeOut)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }
            if (cookieContainer == null)
            {
                request.CookieContainer = new CookieContainer();
            }
            else
            {
                request.CookieContainer = cookieContainer;
            }
            if (wb != null)
            {
                request.Proxy = wb;
            }
            if (timeOut != 0)
            {
                request.Timeout = timeOut;
                request.ReadWriteTimeout = timeOut;
            }
            //如果需要POST数据             
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                var task = Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, request, TaskCreationOptions.None);               //等待任务完成               
                task.Wait();                //执行完本任务后再连续执行写入留和返回response对象           '
                using (Stream stream = task.Result)//如果上面没有等待任务完成那一句，在这里直接获取结果也是可以的           
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, request, TaskCreationOptions.None);
        }

        /// <summary>         
        /// 创建POST方式的HTTP请求          
        /// </summary>         
        /// <param name="url">请求的URL</param>          
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>          
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>         
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>         
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>    
        /// <returns></returns>         
        public static Task<WebResponse> CreateJsonPostHttpResponse(string url, string json, string userAgent, Encoding requestEncoding, CookieContainer cookieContainer, WebProxy wb, int timeOut)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }
            if (cookieContainer == null)
            {
                request.CookieContainer = new CookieContainer();
            }
            else
            {
                request.CookieContainer = cookieContainer;
            }
            if (wb != null)
            {
                request.Proxy = wb;
            }
            if (timeOut != 0)
            {
                request.Timeout = timeOut;
                request.ReadWriteTimeout = timeOut;
            }
            //如果需要POST数据             
            if (!string.IsNullOrEmpty(json))
            {
                byte[] data = requestEncoding.GetBytes(json);
                var task = Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, request, TaskCreationOptions.None);               //等待任务完成               
                task.Wait();                //执行完本任务后再连续执行写入留和返回response对象           '
                using (Stream stream = task.Result)//如果上面没有等待任务完成那一句，在这里直接获取结果也是可以的           
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, request, TaskCreationOptions.None);
        }

        /// <summary>          
        /// 创建GET方式的HTTP请求        
        /// </summary>        
        /// <param name="url">请求的URL</param>          
        /// <param name="timeout">请求的超时时间(毫秒)</param>         
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>       
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>         
        /// <returns>WebResponse Task</returns>   
        public static Task<WebResponse> CreateGetHttpResponse(string url, string userAgent, CookieContainer cookieContainer,WebProxy wb, int timeOut)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            HttpWebRequest request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            request.Method = "GET";
            if (string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = DefaultUserAgent;
            }
            else
            {
                request.UserAgent = userAgent;
            }
            if (cookieContainer == null)
            {
                request.CookieContainer = new CookieContainer();
            }
            else
            {
                request.CookieContainer = cookieContainer;
            }
            if (wb != null)
            {
                request.Proxy= wb;
            }
            if (timeOut != 0)
            {
                request.Timeout = timeOut;
                request.ReadWriteTimeout= timeOut;
            }
            return Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, request, TaskCreationOptions.None);
        }
    }
}
