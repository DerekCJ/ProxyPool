using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProxyPool;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Pool Name", typeof(string));
        dt.Columns.Add("Action URL", typeof(string));
        foreach (Pool p in RunningCathe.AllProxyPool)
        {
            DataRow dr = dt.NewRow();
            dr[0] = p.PoolName;
            dr[1] = "GetProxy.aspx?PoolName=" + p.PoolName + "&Count=30";
            dt.Rows.Add(dr);
        }
        gv_test.DataSource = dt;
        gv_test.DataBind();
    }
}