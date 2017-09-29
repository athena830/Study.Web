using JiebaNet.Segmenter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.UI;
using System.Xml;

public partial class Page_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //測試用
        MotionClass.DIALOG_USER DIALOG_USER = (MotionClass.DIALOG_USER)Session["DIALOG_USER"];
        Session["DIALOG_USER"] = DIALOG_USER;

        if (Session["dialog_user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
        //    DIALOG_USER DIALOG_USER = (DIALOG_USER)Session["DIALOG_USER"];
        //    if (Request["DM_ID"] == null)
        //    {
        //        Response.Redirect("../Default.aspx");
        //    }
        //    else
        //    {
        //        string DM_ID = Request["DM_ID"].ToString();
        //        DataTable dt = SQLFunc.Get_DIALOG_MAIN(DM_ID);
        //        Page_Bind(dt);
        //        ll_Name.Text = DIALOG_USER.U_Name;
        //    }
        }
        //ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "scrollTo", "window.scrollTo(0,document.body.scrollHeight);alert('aa');", true);

    }

    protected void On_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        var cmd = e.CommandName;
        var arg = e.CommandArgument.ToString();
        string now_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        string DF_ID = "";
        string DF_DDID = cmd;
        string DF_Type = arg;

        if (arg == "del")
        {
            SQLFunc.Del_DIALOG_FEEDBACK(DF_DDID);
            SQLFunc.Del_DIALOG_DETAIL(DF_DDID);
        }
        else
        {
            MotionClass.DIALOG_FEEDBACK data_DF = SQLFunc.Get_DIALOG_FEEDBACK(DF_ID, DF_DDID, DF_Type, now_date);

            SQLFunc.Add_DIALOG_FEEDBACK(data_DF);
        }

        lv_Dialog.DataBind();
    }
}