using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DialogHelper
/// </summary>
public class DialogHelper
{
	public DialogHelper()
	{

	}

    public void Get_Dialog_Detail(string DM_ID, string sentence, string now_date)
    {
        MotionClass.DIALOG_DETAIL.ASK data_ask = SQLFunc.Get_DIALOG_DETAIL(DM_ID, "0", sentence, now_date, now_date);
        SQLFunc.Add_DIALOG_DETAIL(data_ask);
    }

}