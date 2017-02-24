using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProxyPool;

public partial class ProxyValidationMgmt : System.Web.UI.Page
{
    ProxyValidation pVld;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lkb_Add_Click(object sender, EventArgs e)
    {
        nav_2.Attributes.Add("class", "active");
        nav_1.Attributes.Remove("class");
        fv_pxy_vld.ChangeMode(FormViewMode.Insert);
        fv_pxy_vld.Visible = true;
    }

    protected void lkb_Mgmt_Click(object sender, EventArgs e)
    {
        nav_1.Attributes.Add("class", "active");
        nav_2.Attributes.Remove("class");
        fv_pxy_vld.ChangeMode(FormViewMode.Edit);
    }

    protected void btn_test_Click(object sender, EventArgs e)
    {
        JsAdapter ja = new JsAdapter();
        try
        {
            string name = ((TextBox)fv_pxy_vld.FindControl("vld_name")).Text;
            string url = ((TextBox)fv_pxy_vld.FindControl("vld_url")).Text;
            string method = ((TextBox)fv_pxy_vld.FindControl("vld_request_method")).Text;
            string regex = ((TextBox)fv_pxy_vld.FindControl("vld_pass_regex")).Text;
            int timeout = int.Parse(((TextBox)fv_pxy_vld.FindControl("vld_timeout")).Text);
            int attemps = int.Parse(((TextBox)fv_pxy_vld.FindControl("vld_attemps")).Text);
            int status = int.Parse(((TextBox)fv_pxy_vld.FindControl("vld_status")).Text);

            pVld = new ProxyValidation(name, url, method, regex, timeout, attemps, status, DateTime.Now);

            if (pVld.validateProxyServer())
            {
                ((Button)fv_pxy_vld.FindControl("btn_save")).Enabled = true;
                ja.ajaxAlert("测试成功", this);
            }
            else
            {
                ((Button)fv_pxy_vld.FindControl("btn_save")).Enabled = false;
                ja.ajaxAlert("测试失败，请检查配置", this);
            }
        }
        catch (Exception ex)
        {
            ja.ajaxAlert(ex.Message, this);
        }
        finally {
            if (fv_pxy_vld.CurrentMode == FormViewMode.Edit)
            {
                nav_1.Attributes.Add("class", "active");
                nav_2.Attributes.Remove("class");
            }
            if (fv_pxy_vld.CurrentMode == FormViewMode.Insert)
            {
                nav_2.Attributes.Add("class", "active");
                nav_1.Attributes.Remove("class");
            } 
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        nav_1.Attributes.Add("class", "active");
        nav_2.Attributes.Remove("class");
        fv_pxy_vld.ChangeMode(FormViewMode.Edit);
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        JsAdapter ja = new JsAdapter();
        if (pVld == null)
        {
            ja.ajaxAlert("缺少有效的代理配置", this);
        }
        else
        {
            try
            {
                sds_pxy_vld.InsertParameters["vld_name"].DefaultValue = pVld.ValidationName;
                sds_pxy_vld.InsertParameters["vld_url"].DefaultValue = pVld.ValidationUrl;
                sds_pxy_vld.InsertParameters["vld_request_method"].DefaultValue = pVld.ValidationRequestMethod;
                sds_pxy_vld.InsertParameters["vld_pass_regex"].DefaultValue = pVld.PassRegex;
                sds_pxy_vld.InsertParameters["vld_timeout"].DefaultValue = pVld.FailTimeout.ToString();
                sds_pxy_vld.InsertParameters["vld_attemps"].DefaultValue = pVld.FailAttemps.ToString();
                sds_pxy_vld.InsertParameters["vld_status"].DefaultValue = pVld.ValidationStatus.ToString();
                sds_pxy_vld.InsertParameters["vld_create_time"].DefaultValue = pVld.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                sds_pxy_vld.Insert();
                ja.ajaxAlert("保存成功", this);
            }
            catch (Exception ex)
            {
                ja.ajaxAlert("保存验证方式失败：" + ex.Message, this);
            }
        }
    }

    protected void fv_pxy_vld_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        JsAdapter ja = new JsAdapter();
        ProxyValidation pv = new ProxyValidation();
        if (Session["currentPV"] != null)
        {
            pv = (ProxyValidation)Session["currentPV"];
        }
        if (string.IsNullOrEmpty(pv.ValidationName))
        {
            e.Cancel = true;
            ja.ajaxAlert("缺少有效的代理配置", this);
        }
    }

    protected void fv_pxy_vld_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        saveAlert(e.Exception);
        lkb_Mgmt_Click(null, null);
    }

    protected void fv_pxy_vld_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        saveAlert(e.Exception);
        lkb_Mgmt_Click(null, null);
    }

    protected void saveAlert(Exception ex=null)
    {
        JsAdapter ja = new JsAdapter();
        if (ex == null)
        {
            ja.ajaxAlert("保存成功", this);
        }
        else
        {
            ja.ajaxAlert("保存失败：" + ex.Message, this);
        }
    }
}