using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProxyPool;
using System.Data;

public partial class DashBoard : System.Web.UI.Page
{
    int defaultDisplatCnt = 10;
    int moreDisplatCnt=10;
    int refreshTimeSpan = 300000;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDisplayList();
            refreshGeneralStatus();

            lkb_start_source.Enabled = !RunningCathe.SourceRunning;
            lkb_stop_source.Enabled = RunningCathe.SourceRunning;
            lkb_start_source.CssClass = RunningCathe.SourceRunning? "btn btn-default btn-lg": "btn btn-primary btn-lg";
            lkb_stop_source.CssClass = !RunningCathe.SourceRunning ? "btn btn-default btn-lg" : "btn btn-primary btn-lg";

            lkb_start_pool.Enabled = !RunningCathe.PoolRunning;
            lkb_stop_pool.Enabled = RunningCathe.PoolRunning;
            lkb_start_pool.CssClass = RunningCathe.PoolRunning ? "btn btn-default btn-lg" : "btn btn-primary btn-lg";
            lkb_stop_pool.CssClass = !RunningCathe.PoolRunning ? "btn btn-default btn-lg" : "btn btn-primary btn-lg";
        }
    }


    protected void bindDisplayList()
    {
        foreach (Pool p in RunningCathe.AllProxyPool)
        {
            DataRow dr = RunningCathe.DisplayList.Rows.Find(p.Id);
            if (dr == null)
            {
                dr = RunningCathe.DisplayList.NewRow();
                dr[0] = p.Id;
                dr[1] = p.PoolName;
                dr[2] = p.getDisplayTableHtml(defaultDisplatCnt);
                dr[3] = defaultDisplatCnt;
                RunningCathe.DisplayList.Rows.Add(dr);
            }
            else
            {
                dr[2] = p.getDisplayTableHtml(int.Parse(dr[3].ToString()));
            }
            dr[4] = p.ActiveProxyServerList.Count;
            dr[5] = p.ToBeValidProxyServerList.Count;
            dr[6] = p.AllProxyServerList.Count;
            // 0：禁用；1：预备中；2：启动中；3：运行中；4：停止中；5：启动异常中止；6：停止异常中止
            switch (p.PoolStatus)
            {
                case 0:
                    {
                        dr[7] = "禁用";
                        break;
                    }
                case 1:
                    {
                        dr[7] = "预备中";
                        break;
                    }
                case 2:
                    {
                        dr[7] = "启动中";
                        break;
                    }
                case 3:
                    {
                        dr[7] = "运行中";
                        break;
                    }
                case 4:
                    {
                        dr[7] = "停止中";
                        break;
                    }
                case 5:
                    {
                        dr[7] = "启动异常中止";
                        break;
                    }
                case 6:
                    {
                        dr[7] = "停止异常中止";
                        break;
                    }
                default: {
                        dr[7] = "未知";
                        break;
                    }
            }
        }
        dl_Pool.DataSource = RunningCathe.DisplayList;
        dl_Pool.DataBind();
    }

    protected void displayMore(int poolId,int moreCount)
    {
        foreach (DataRow dr in RunningCathe.DisplayList.Rows)
        {
            if (dr[0].ToString() == poolId.ToString())
            {
                Pool p = RunningCathe.AllProxyPool.Find(x => x.Id == poolId);
                dr[2] = p.getDisplayTableHtml(int.Parse(dr[3].ToString())+moreCount);
                dr[3] = int.Parse(dr[3].ToString()) + moreCount;
                dr[4] = p.ActiveProxyServerList.Count;
                dr[5] = p.ToBeValidProxyServerList.Count;
                dl_Pool.DataSource = RunningCathe.DisplayList;
                dl_Pool.DataBind();
                break;
            }
        }
    }
    protected void displayDefault(int poolId)
    {
        foreach (DataRow dr in RunningCathe.DisplayList.Rows)
        {
            if (dr[0].ToString() == poolId.ToString())
            {
                Pool p = RunningCathe.AllProxyPool.Find(x => x.Id == poolId);
                dr[2] = p.getDisplayTableHtml(defaultDisplatCnt);
                dr[3] = defaultDisplatCnt;
                dr[4] = p.ActiveProxyServerList.Count;
                dr[5] = p.ToBeValidProxyServerList.Count;
                dl_Pool.DataSource = RunningCathe.DisplayList;
                dl_Pool.DataBind();
                break;
            }
        }
    }
    public void refreshGeneralStatus()
    {
        lb_pool_count.Text = ProxyPool.RunningCathe.AllProxyPool.Count.ToString();
        lb_source_count.Text = ProxyPool.RunningCathe.AllProxySource.Count.ToString();
        lb_active_proxy_count.Text = ProxyPool.RunningCathe.getCurrentAllActiveProxy().Count.ToString();
        //lb_toBeValid_count.Text = ProxyPool.RunningCathe.getCurrentAllToBeValidProxy().Count.ToString();
        lb_all_proxy_count.Text = ProxyPool.RunningCathe.getCurrentAllProxy().Count.ToString();
        lb_refresh_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    protected void lkb_start_source_Click(object sender, EventArgs e)
    {
        t_refresh.Enabled = true;
        lkb_start_source.Enabled = false;
        lkb_stop_source.Enabled = true;
        t_refresh.Interval = refreshTimeSpan;
        lkb_start_source.CssClass = "btn btn-default btn-lg";
        lkb_stop_source.CssClass = "btn btn-primary btn-lg";
        if (!RunningCathe.SourceRunning)
        {
            RunningCathe.startRefreshAllSource();
            RunningCathe.SourceRunning = true;
        }
    }
    protected void lkb_stop_source_Click(object sender, EventArgs e)
    {
        t_refresh.Enabled = false;
        lkb_start_source.Enabled = true;
        lkb_stop_source.Enabled = false;
        t_refresh.Interval = 99999999;
        lkb_start_source.CssClass = "btn btn-primary btn-lg";
        lkb_stop_source.CssClass = "btn btn-default btn-lg";
        if (RunningCathe.SourceRunning)
        {
            RunningCathe.stopRefreshAllSource();
            RunningCathe.SourceRunning = false;
        }
        
    }
    protected void lkb_start_pool_Click(object sender, EventArgs e)
    {
        t_refresh.Enabled = true;
        lkb_stop_pool.Enabled = true;
        lkb_start_pool.Enabled = false;
        t_refresh.Interval = refreshTimeSpan;
        lkb_start_pool.CssClass = "btn btn-default btn-lg";
        lkb_stop_pool.CssClass = "btn btn-primary btn-lg";
        if (!RunningCathe.PoolRunning)
        {
            //RunningCathe.oneKeyStart(true);
            //RunningCathe.initialConfigurations();
            RunningCathe.startAllPool();
            RunningCathe.PoolRunning = true;
        }
        
    }

    protected void lkb_stop_pool_Click(object sender, EventArgs e)
    {
        lkb_stop_pool.Enabled = false;
        lkb_start_pool.Enabled = true;
        t_refresh.Interval = 99999999;
        lkb_stop_pool.CssClass = "btn btn-default btn-lg";
        lkb_start_pool.CssClass = "btn btn-primary btn-lg";
        if (RunningCathe.PoolRunning)
        {
            //RunningCathe.oneKeyStop();
            RunningCathe.stopAllPool();
            RunningCathe.PoolRunning = false;
        }
    }

    protected void lkb_refresh_Click(object sender, EventArgs e)
    {
        refreshGeneralStatus();
        bindDisplayList();
    }

    protected void t_refresh_Tick(object sender, EventArgs e)
    {
        refreshGeneralStatus();
        bindDisplayList();
        //new JsAdapter().ajaxAlert(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),this);
    }

    protected void lkb_more_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        int poolId = int.Parse(lb.ToolTip);
        displayMore(poolId, moreDisplatCnt);
    }

    protected void lkb_defult_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        int poolId = int.Parse(lb.ToolTip);
        displayDefault(poolId);
    }


}