using System.Web;

namespace ProxyPool
{
    /// <summary>
    /// JsAdapter 的摘要说明
    /// </summary>

    public class JsAdapter
    {
        System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        public void AjaxRotation(string img, string angle, string minAngle, string duration, string message, System.Web.UI.Control control)
        {
            message = message.Replace("'", @"\'");
            string js = "$(\"#" + img + "\").rotate({" +
                      "angle:0," +
                  "duration: " + duration + "," +
                  "animateTo: " + angle + "+" + minAngle + "," +
                  "callback:function(){ alert(\"" + message + "\"); document.title=\"" + message + "\";document.getElementById('hf_desc').value=\"" + message + "\";}" +
                  "});";
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", js, true);
        }
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public void alert(string message)
        {
            message = message.Replace("'", @"\'");
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }
        public void ajaxAlert(string message, System.Web.UI.Control control)
        {
            message = message.Replace("'", @"\'");
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "alert('" + message + "')", true);
        }
        public void Alertto(string message, string url)
        {
            message = message.Replace("'", @"\'");
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');window.parent.location=' " + url + " ';</Script>";
            HttpContext.Current.Response.Write(js);
        }
        public void ajaxAlertto(string message, string url, System.Web.UI.Control control)
        {
            message = message.Replace("'", @"\'");
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "alert('" + message + "');window.parent.location='" + url + "';", true);
        }
        public void ajaxAlertExt(string message, string url, System.Web.UI.Control control)
        {
            message = message.Replace("'", @"\'");
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "if(confirm('" + message + "')){var w = window.parent.open('','_newtab');w.opener=null;w.document.location = '" + url + "';}", true);
        }
        public void ajaxPopup(string url, System.Web.UI.Control control)
        {
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "var w = window.parent.open('','_newtab');w.opener=null;w.document.location = '" + url + "';", true);
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "var w = window.open('','_self');w.opener=null;w.document.location = '" + url + "';", true);
        }
        public void ajaxAlertPostBack(string message, System.Web.UI.Control control)
        {
            message = message.Replace("'", @"\'");
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "alert('" + message + "');__doPostBack('__Page', '')", true);
        }
        public void alertback(string message)
        {
            message = message.Replace("'", @"\'");
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');location='javascript:history.go(-1) ';</Script>";
            HttpContext.Current.Response.Write(js);
        }
        public void ajaxTest(System.Web.UI.Control control)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "window.open('','_newtab');", true);
        }
        public void ajaxAlertback(string message, System.Web.UI.Control control)
        {
            message = message.Replace("'", @"\'");
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "alert('" + message + "');location='javascript:history.go(-1) ';", true);
        }
        public void ajaxPostBackParent(System.Web.UI.Control control)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "parent.__doPostBack('parent.__Page', '');", true);
        }
        public void ajaxReloadParent(System.Web.UI.Control control)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "parent.location.reload();", true);
        }
        public void ajaxPostBack(System.Web.UI.Control control)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "__doPostBack('__Page', '');", true);
        }
        public void ajaxReload(System.Web.UI.Control control)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "location.reload();", true);
        }
        public void closeWindow()
        {
            string js = @"<script language=javascript>window.opener=null;window.open('','_self');window.close();</script>";
            HttpContext.Current.Response.Write(js);
        }

        public void ajaxCloseWindow(System.Web.UI.Control control)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "click", "window.opener = null; window.open('', '_self'); window.close();", true);
        }
    }
}