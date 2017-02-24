using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProxyPool;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using HtmlAgilityPack;
using System.Xml;

public partial class ProxySourceMgmt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 模式切换：新增<->维护
    protected void lkb_Add_Click(object sender, EventArgs e)
    {
        nav_2.Attributes.Add("class", "active");
        nav_1.Attributes.Remove("class");
        fv_pxy_src.ChangeMode(FormViewMode.Insert);
        fv_pxy_src.Visible = true;
    }

    protected void lkb_Mgmt_Click(object sender, EventArgs e)
    {
        nav_1.Attributes.Add("class", "active");
        nav_2.Attributes.Remove("class");
        fv_pxy_src.ChangeMode(FormViewMode.Edit);
        //fv_pxy_src.Visible = false;
    }
    #endregion

    protected void fv_pxy_src_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        saveAlert(e.Exception);
        lkb_Mgmt_Click(null, null);
    }

    protected void btn_test_Click(object sender, EventArgs e)
    {
        JsAdapter ja = new JsAdapter();
        try
        {
            int iptId;
            DateTime iptCTime;
            if (fv_pxy_src.CurrentMode == FormViewMode.Edit)
            {
                iptId = int.Parse(((TextBox)fv_pxy_src.FindControl("iptId")).Text);
                iptCTime = Convert.ToDateTime(((TextBox)fv_pxy_src.FindControl("iptCTime")).Text);
            }
            else
            {
                iptId = 0;
                iptCTime = DateTime.Now;
            }
            string iptName = ((TextBox)fv_pxy_src.FindControl("iptName")).Text;
            int iptCatheSize = int.Parse(((TextBox)fv_pxy_src.FindControl("iptCatheSize")).Text);
            string iptUrl = ((TextBox)fv_pxy_src.FindControl("iptUrl")).Text;
            string iptUrlPara = ((TextBox)fv_pxy_src.FindControl("iptUrlPara")).Text;
            int iptRequestTimeSpan = int.Parse(((TextBox)fv_pxy_src.FindControl("iptRequestTimespan")).Text);
            int iptRefreshTimeSpan = int.Parse(((TextBox)fv_pxy_src.FindControl("iptRefreshTimeSpan")).Text);
            string iptCharset = ((TextBox)fv_pxy_src.FindControl("iptCharset")).Text;
            string iptRequestMethod = ((TextBox)fv_pxy_src.FindControl("iptRequestMethod")).Text;
            string iptDocType = ((TextBox)fv_pxy_src.FindControl("iptDocType")).Text;
            string iptSrchType = ((TextBox)fv_pxy_src.FindControl("iptSrchType")).Text;
            string iptUrlSrch = ((TextBox)fv_pxy_src.FindControl("iptUrlSrch")).Text;
            string iptPortSrch = ((TextBox)fv_pxy_src.FindControl("iptPortSrch")).Text;
            string iptProtocalSrch = ((TextBox)fv_pxy_src.FindControl("iptProtocalSrch")).Text;
            string iptRequesrMethodSrch = ((TextBox)fv_pxy_src.FindControl("iptRequesrMethodSrch")).Text;
            string iptLocationSrch = ((TextBox)fv_pxy_src.FindControl("iptLocationSrch")).Text;
            string iptTypeSrch = ((TextBox)fv_pxy_src.FindControl("iptTypeSrch")).Text;
            string iptNameSrch = ((TextBox)fv_pxy_src.FindControl("iptNameSrch")).Text;
            string iptPassSrch = ((TextBox)fv_pxy_src.FindControl("iptPassSrch")).Text;
            string iptDomainSrch = ((TextBox)fv_pxy_src.FindControl("iptDomainSrch")).Text;

            ProxySource ps = new ProxySource(iptName, iptCatheSize, iptUrl, iptUrlPara, iptRequestTimeSpan, iptRefreshTimeSpan, iptCharset, iptRequestMethod, iptDocType, iptSrchType, iptUrlSrch
                , iptPortSrch, iptProtocalSrch, iptRequesrMethodSrch, iptLocationSrch, iptTypeSrch, iptNameSrch, iptPassSrch, iptDomainSrch, iptCTime, iptId);

            List<ProxyServer> psList = ps.retrieveProxySource(false,false,false);

            Button svBtn = ((Button)fv_pxy_src.FindControl("btn_save"));
            svBtn.Enabled = false;
            if (psList.Count > 0)
            {
                ja.ajaxAlert("发现" + psList.Count.ToString() + "个代理服务器地址", this);
                string cnt = "";
                for (int i = 0; i < psList.Count; i++)
                {
                    cnt = cnt + "<p>" + (i + 1).ToString() + " - " + psList[i].ProxyIpAddress + ":" + psList[i].ProxyPort + "," + psList[i].ProxyProtocal + "," + psList[i].ProxyLocation + "<p>";
                }
                dv_testResult.InnerHtml = cnt;
                svBtn.Enabled = true;
            }
            else
            {
                ja.ajaxAlert("未发现代理服务器地址", this);
                dv_testResult.InnerHtml = "";
            }
        }
        catch (Exception ex)
        {
            dv_testResult.InnerHtml = "";
            if (ex.InnerException != null)
            {
                ja.ajaxAlert(ex.Message + "\\n" + ex.InnerException.Message, this);
            }
            else
            {
                ja.ajaxAlert(ex.Message, this);
            }
        }
        finally
        {
            if (fv_pxy_src.CurrentMode == FormViewMode.Edit)
            {
                nav_1.Attributes.Add("class", "active");
                nav_2.Attributes.Remove("class");
            }
            if (fv_pxy_src.CurrentMode == FormViewMode.Insert)
            {
                nav_2.Attributes.Add("class", "active");
                nav_1.Attributes.Remove("class");
            }
        }
    }

    //protected List<string> processURL(string url, string para)
    //{
    //    List<string> r = new List<string>();
    //    int paraCnt = 0;
    //    Regex reg = new Regex(@"\[\*\]");
    //    paraCnt = reg.Matches(url).Count;
    //    string[] p = para.Split('|');
    //    if (paraCnt != p.Length)
    //    {
    //        return r;
    //    }
    //    else
    //    {
    //        r.Add(url);
    //        for (int i = 0; i < p.Length; i++)
    //        {
    //            r = makeUpUrl(r, "[*]", p[i]);
    //        }
    //    }
    //    return r;
    //}

    //protected List<string> makeUpUrl(List<string> url,string sep, string para)
    //{
    //    List<string> r = new List<string>();
    //    string[] p = para.Split(',');
    //    int sp,cnt,step;
    //    for (int i = 0; i < url.Count; i++)
    //    {
    //        sp = int.Parse(p[0]);
    //        cnt = int.Parse(p[1]);
    //        step = int.Parse(p[2]);
    //        int idx = url[i].IndexOf(sep);
    //        string prefix = url[i].Substring(0, idx);
    //        string suffix = url[i].Substring(idx + sep.Length);
    //        for (int j = sp; cnt > 0; cnt--)
    //        {
    //            r.Add(prefix + j.ToString() + suffix);
    //            j = j + step;
    //        }
    //    }
    //    return r;
    //}

    protected void fv_pxy_src_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        saveAlert(e.Exception);
        lkb_Mgmt_Click(null, null);
    }
    protected void saveAlert(Exception ex = null)
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

    protected void fv_pxy_src_PageIndexChanged(object sender, EventArgs e)
    {
        dv_testResult.InnerHtml = "";
    }
}
