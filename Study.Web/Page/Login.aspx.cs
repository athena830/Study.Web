using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_Name.Attributes.Add("onkeydown", "if(event.keyCode == 13) {document.getElementById('" + btn_Save.ClientID + "').click(); return false;}");
            string ip_string = GetClientIP();

            lb_IP.Text = ip_string;
            DataTable dt = SQLFunc.Get_DIALOG_USER(ip_string);
            if (dt.Rows.Count > 0)
            {
                txt_Name.Text = dt.Rows[0]["U_Name"].ToString();
                hid_U_ID.Value = dt.Rows[0]["U_ID"].ToString();
                MotionClass.DIALOG_USER data = SQLFunc.Get_DIALOG_USER(dt.Rows[0]["U_ID"].ToString(), dt.Rows[0]["U_Name"].ToString(), dt.Rows[0]["U_IP"].ToString(), dt.Rows[0]["U_CreateTime"].ToString());
                Session["DIALOG_USER"] = data;
            }
            else
            {
                Session.Clear();
            }
        }
    }

    /// <summary>
    /// 取得正確的Client端IP
    /// </summary>
    /// <returns></returns>
    protected string GetClientIP()
    {
        ////把HostName換成網址可查該網址對應的IP位址
        //String name = Dns.GetHostName();
        //IPAddress[] ip = Dns.GetHostEntry(name).AddressList;
        //for (int i = 0; i < ip.Length; i++)
        //{
        //    //System.Net.Sockets.AddressFamily.InterNetwork為IPv4位址
        //    //System.Net.Sockets.AddressFamily.InterNetworkV6為IPv6位址
        //    if (ip[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //    {
        //        ip_string = ip[i].ToString();
        //    }
        //}

        //判所client端是否有設定代理伺服器
        if (Request.ServerVariables["HTTP_VIA"] == null)
        {
            return Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        else
        {
            return Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MotionClass.DIALOG_USER data = null;
        string now_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        string name = txt_Name.Text;
        if (string.IsNullOrEmpty(name))
        {
            Alert_msg("交出名字，饒你一命");
        }
        else
        {
            if (name == "gis@5200")
            {
                data = SQLFunc.Get_DIALOG_USER("", "Manager", lb_IP.Text, now_date);
                Session["DIALOG_USER"] = data;

                Response.Redirect("~/Page/Summary.aspx");
            }
            else
            {
                if (Session["DIALOG_USER"] == null)
                {
                    data = SQLFunc.Get_DIALOG_USER("", name, lb_IP.Text, now_date);
                    string U_ID = SQLFunc.Add_DIALOG_USER(data);
                    data = SQLFunc.Get_DIALOG_USER(U_ID, name, lb_IP.Text, now_date);
                    Session["DIALOG_USER"] = data;
                }

                MotionClass.DIALOG_USER data_USER = (MotionClass.DIALOG_USER)Session["DIALOG_USER"];
                MotionClass.DIALOG_MAIN data_Main = SQLFunc.Get_DIALOG_MAIN(data_USER.U_ID, "0", now_date);
                string DM_ID = SQLFunc.Add_DIALOG_MAIN(data_Main);
                //string DM_ID = "1";
                Response.Redirect("Page/Dialog.aspx?DM_ID=" + DM_ID);
            }
        }
    }
    protected void Alert_msg(string msg)
    {
        msg = msg.Replace("\n", "");
        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "alertMessage", "alert('" + msg + "');", true);
    }
}