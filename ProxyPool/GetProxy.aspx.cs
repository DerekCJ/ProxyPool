using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProxyPool;

public partial class GetProxy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string responseStr = "";
        string poolName = "";
        string method = "";
        try
        {
            poolName = Request.QueryString.Get("PoolName");
            int count = int.Parse(Request.QueryString.Get("Count"));
            method = Request.QueryString.Get("Method");
            responseStr = getProxy(poolName, count);
        }
        catch(Exception ex)
        {
            responseStr = "{\"name\" : \"" + poolName + "\" , \"count\" : \"\" , \"proxy_list\" : [] , \"status\" : \"error\" , \"message\" : \""+ex.Message+"\"}";
        }
        Response.Write(responseStr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="count"></param>
    /// <param name="method">默认"",rnd:随机取;seq顺序取</param>
    /// <returns></returns>
    protected string getProxy(string poolName, int count,string method="")
    {
        Pool p = RunningCathe.AllProxyPool.Find(x => x.PoolName == poolName);
        string rStr = "";
        if (p != null)
        {
            int poolSize = p.ActiveProxyServerList.Count;
            int returnCnt = count > poolSize ? poolSize : count;
            if (returnCnt > 0)
            {
                rStr = "{\"name\" : \"" + poolName + "\" , \"count\" : \"" + returnCnt.ToString() + "\" , \"proxy_list\" : [";
                switch (method)
                {
                    case "seq":
                        {
                            for (int i = 1; i < returnCnt; i++)
                            {
                                rStr = rStr + p.ActiveProxyServerList[i].toJsonString() + " , ";
                            }
                            break;
                        }
                    case "":
                    case "rnd":
                        {
                            List<ProxyServer> ps = p.ActiveProxyServerList.Take(p.ActiveProxyServerList.Count).ToList();
                            if (returnCnt < poolSize / 2)
                            {
                                for (int i = 0; i < returnCnt; i++)
                                {
                                    int idx = new Random().Next(ps.Count - 1);
                                    rStr = rStr + ps[idx].toJsonString() + " , ";
                                    ps.RemoveAt(idx);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < (poolSize - returnCnt); i++)
                                {
                                    int idx = new Random().Next(ps.Count - 1);
                                    ps.RemoveAt(idx);
                                }
                                foreach (ProxyServer pServer in ps)
                                {
                                    rStr = rStr + pServer.toJsonString() + " , ";
                                }
                            }
                            break;
                        }
                    default:break;
                }

                rStr = rStr.Substring(0, rStr.Length - 3) + "] , \"status\" : \"success\" , \"message\" : \"success\"}";
            }
            else
            {
                rStr = "{\"name\" : \"" + poolName + "\" , \"count\" : \"0\" , \"proxy_list\" : [] , \"status\" : \"error\" , \"message\" : \"invalid count\"}";
            }
        }
        else
        {
            rStr = "{\"name\" : \"" + poolName + "\" , \"count\" : \"0\" , \"proxy_list\" : [] , \"status\" : \"error\" , \"message\" : \"invalid pool name\"}";
        }
        return rStr;
    }
}