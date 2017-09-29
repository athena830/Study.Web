using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Study.Motion.Web.View
{
    public partial class Dialog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //測試用
            //DIALOG_USER data_User = SQLFunc.Get_DIALOG_USER("1", "Athena", "192.168.7.127", "2017-03-23 14:29:15.647");
            //Session["DIALOG_USER"] = data_User;

            if (!IsPostBack)
            {
                txt_Sentence.Attributes.Add("onkeydown", "if(event.keyCode == 13) {document.getElementById('" + btn_Submit.ClientID + "').click(); return false;}");
            }

            if (Session["DIALOG_USER"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                MotionClass.DIALOG_USER DIALOG_USER = (MotionClass.DIALOG_USER)Session["DIALOG_USER"];
                if (Request["DM_ID"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    string DM_ID = Request["DM_ID"].ToString();
                    DataTable dt = SQLFunc.Get_DIALOG_MAIN(DM_ID);
                    Page_Bind(dt);
                    ll_Name.Text = DIALOG_USER.U_Name;
                }
            }
            //ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "scrollTo", "window.scrollTo(0,document.body.scrollHeight);alert('aa');", true);

        }

        private void Page_Bind(DataTable dt)
        {
            hid_DM_ID.Value = dt.Rows[0]["DM_ID"].ToString(); ;
            int Score = int.Parse(dt.Rows[0]["DM_Motion_Total"].ToString());
            hid_Score.Value = Score.ToString();
            negativeFifteen.Attributes["class"] = "";
            negativeTen.Attributes["class"] = "";
            negativeFive.Attributes["class"] = "";
            zero.Attributes["class"] = "";
            five.Attributes["class"] = "";
            ten.Attributes["class"] = "";
            fifteen.Attributes["class"] = "";
            if (Score <= -15)
            {
                negativeFifteen.Attributes["class"] = "active";
            }
            else if (Score > -15 && Score <= -10)
            {
                negativeTen.Attributes["class"] = "active";
            }
            else if (Score > -10 && Score <= -5)
            {
                negativeFive.Attributes["class"] = "active";
            }
            else if (Score > -5 && Score < 5)
            {
                zero.Attributes["class"] = "active";
            }
            else if (Score < 10 && Score >= 5)
            {
                five.Attributes["class"] = "active";
            }
            else if (Score < 15 && Score >= 10)
            {
                ten.Attributes["class"] = "active";
            }
            else if (Score >= 15)
            {
                fifteen.Attributes["class"] = "active";
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            //測試用
            //txt_Sentence.Text = "我相信已有不少台中市民反應過這個問題。";

            string sentence = txt_Sentence.Text;
            string DM_ID = hid_DM_ID.Value;
            string now_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            if (!string.IsNullOrEmpty(sentence))
            {
                MotionClass.DIALOG_DETAIL.ASK data_ask = SQLFunc.Get_DIALOG_DETAIL(DM_ID, "0", sentence, now_date, now_date);
                SQLFunc.Add_DIALOG_DETAIL(data_ask);
            }

            DataTable dt_DD = SQLFunc.Get_DIALOG_DETAIL_By_DD_Type("0");
            if (dt_DD.Rows.Count > 0)
            {
                for (int i = 0; i < dt_DD.Rows.Count; i++)
                {
                    JiebaExecute.jieba_execute(dt_DD.Rows[i]["DD_Sentence"].ToString(), dt_DD.Rows[i]["DD_ID"].ToString(), hid_DM_ID.Value);
                }
            }

            DataTable dt = SQLFunc.Get_DIALOG_MAIN(hid_DM_ID.Value);
            Page_Bind(dt);

            txt_Sentence.Text = "";
            lv_Dialog.DataBind();
        }

        protected void On_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            var cmd = e.CommandName;
            var arg = e.CommandArgument.ToString();
            string now_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string DF_ID = "";
            string DF_DDID = cmd;
            string DF_Type = arg;
            MotionClass.DIALOG_FEEDBACK data_DF = SQLFunc.Get_DIALOG_FEEDBACK(DF_ID, DF_DDID, DF_Type, now_date);

            SQLFunc.Add_DIALOG_FEEDBACK(data_DF);

            lv_Dialog.DataBind();
        }
    }
}