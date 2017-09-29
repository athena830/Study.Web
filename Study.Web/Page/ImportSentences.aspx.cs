using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page_ImportSentences : System.Web.UI.Page
{
    DialogHelper dialogHelper = new DialogHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = SQLFunc.Get_FB_Message_List("0");
        JiebaExecute jiebaExecute = new JiebaExecute();

        string DM_ID = "1";
        string now_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sentence = jiebaExecute.MakePlainText(dt.Rows[i]["Message"].ToString());
            string id = dt.Rows[i]["id"].ToString();

            dialogHelper.Get_Dialog_Detail(DM_ID, sentence, now_date);

            jiebaExecute.Get_Dialog_Detail_to_Jieba(DM_ID);

            SQLFunc.Update_FB_Message_Dialog_Chk(id);
        }

    }

}