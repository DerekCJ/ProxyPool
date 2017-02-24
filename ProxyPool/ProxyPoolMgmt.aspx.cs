using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProxyPool;

public partial class ProxyPoolMgmt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 模式切换：新增<->维护
    protected void lkb_Add_Click(object sender, EventArgs e)
    {
        nav_2.Attributes.Add("class", "active");
        nav_1.Attributes.Remove("class");
        fv_pxy_pool.ChangeMode(FormViewMode.Insert);
        fv_pxy_pool.Visible = true;
    }

    protected void lkb_Mgmt_Click(object sender, EventArgs e)
    {
        nav_1.Attributes.Add("class", "active");
        nav_2.Attributes.Remove("class");
        fv_pxy_pool.ChangeMode(FormViewMode.Edit);
        //fv_pxy_src.Visible = false;
    }
    #endregion

    protected void fv_pxy_pool_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.AffectedRows == 1)
        {
            new JsAdapter().ajaxAlert("保存成功", this);
        }
        else {
            if (e.Exception != null)
            {
                new JsAdapter().ajaxAlert(e.Exception.Message, this);
            }
        }
    }
}