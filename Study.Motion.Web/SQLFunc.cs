using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Study.Motion.Web
{
    public class SQLFunc
    {
        #region DIALOG_USER 使用者
        public static DataTable Get_DIALOG_USER(string U_IP)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select * from DIALOG_USER where U_IP=@U_IP";
            cmd.Parameters.Add("U_IP", SqlDbType.NVarChar).Value = U_IP;
            DataTable dt = Persister.Execute(cmd);
            return dt;
        }

        public static string Add_DIALOG_USER(MotionClass.DIALOG_USER data)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Insert into DIALOG_USER (U_IP, U_Name, U_CreateTime) values (@U_IP, @U_Name, @U_CreateTime) ";
            cmd.Parameters.Add("U_IP", SqlDbType.NVarChar).Value = data.U_IP;
            cmd.Parameters.Add("U_Name", SqlDbType.NVarChar).Value = data.U_Name;
            cmd.Parameters.Add("U_CreateTime", SqlDbType.NVarChar).Value = data.U_CreateTime;
            cmd.CommandText += "select @@IDENTITY ";
            DataTable dt = Persister.Execute(cmd);
            string id = dt.Rows[0][0].ToString();
            return id;
        }

        public static MotionClass.DIALOG_USER Get_DIALOG_USER(string U_ID, string U_Name, string U_IP, string U_CreateTime)
        {
            MotionClass.DIALOG_USER data = new MotionClass.DIALOG_USER();
            data.U_ID = U_ID;
            data.U_Name = U_Name;
            data.U_IP = U_IP;
            data.U_CreateTime = U_CreateTime;
            return data;
        }
        #endregion

        #region DIALOG_MAIN 對話
        public static DataTable Get_DIALOG_MAIN(string DM_ID)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select * from Dialog_Main where DM_ID=@DM_ID";
            cmd.Parameters.Add("DM_ID", SqlDbType.Int).Value = DM_ID;
            DataTable dt = Persister.Execute(cmd);
            return dt;
        }

        public static string Add_DIALOG_MAIN(MotionClass.DIALOG_MAIN data)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Insert into Dialog_Main (DM_UID, DM_Motion_Total, DM_StartTime) values (@DM_UID, @DM_Motion_Total, @DM_StartTime)";
            cmd.Parameters.Add("DM_UID", SqlDbType.Int).Value = data.DM_UID;
            cmd.Parameters.Add("DM_Motion_Total", SqlDbType.Float).Value = data.DM_Motion_Total;
            cmd.Parameters.Add("DM_StartTime", SqlDbType.DateTime).Value = data.DM_StartTime;
            cmd.CommandText += "select @@IDENTITY ";
            DataTable dt = Persister.Execute(cmd);
            string id = dt.Rows[0][0].ToString();
            return id;
        }

        public static void Upd_DIALOG_MAIN(MotionClass.DIALOG_MAIN data)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Update Dialog_Main set DM_Motion_Total=DM_Motion_Total+@DM_Motion_Total, DM_EndTime=@DM_EndTime where DM_ID=@DM_ID";
            cmd.Parameters.Add("DM_Motion_Total", SqlDbType.Float).Value = data.DM_Motion_Total;
            cmd.Parameters.Add("DM_EndTime", SqlDbType.DateTime).Value = data.DM_EndTime;
            cmd.Parameters.Add("DM_ID", SqlDbType.Int).Value = data.DM_ID;
            Persister.ExecuteNonQuery(cmd);
        }

        public static MotionClass.DIALOG_MAIN Get_DIALOG_MAIN(string DM_UID, string DM_Motion_Total, string DM_StartTime)
        {
            MotionClass.DIALOG_MAIN data = new MotionClass.DIALOG_MAIN();
            data.DM_UID = DM_UID;
            data.DM_Motion_Total = DM_Motion_Total;
            data.DM_StartTime = DM_StartTime;
            return data;
        }
        #endregion

        #region DIALOG_DETAIL 對話明細
        public static DataTable Get_DIALOG_DETAIL(string DD_DMID)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select * from Dialog_Detail where DD_DMID=@DD_DMID order by DD_CreateTime desc";
            cmd.Parameters.Add("DD_DMID", SqlDbType.Int).Value = DD_DMID;
            DataTable dt = Persister.Execute(cmd);
            return dt;
        }

        public static DataTable Get_DIALOG_DETAIL_By_DD_Type(string DD_Type)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select * from Dialog_Detail where DD_Type=@DD_Type";
            cmd.Parameters.Add("DD_Type", SqlDbType.Int).Value = DD_Type;
            DataTable dt = Persister.Execute(cmd);
            return dt;
        }

        public static void Add_DIALOG_DETAIL(MotionClass.DIALOG_DETAIL.ASK data)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"Insert into Dialog_Detail (DD_DMID, DD_Type, DD_Sentence, DD_CreateTime, DD_UpdateTime) 
            values (@DD_DMID, @DD_Type, @DD_Sentence, @DD_CreateTime, @DD_UpdateTime)";
            cmd.Parameters.Add("DD_DMID", SqlDbType.Int).Value = data.DD_DMID;
            cmd.Parameters.Add("DD_Type", SqlDbType.SmallInt).Value = data.DD_Type;
            cmd.Parameters.Add("DD_Sentence", SqlDbType.NVarChar).Value = data.DD_Sentence;
            cmd.Parameters.Add("DD_CreateTime", SqlDbType.DateTime).Value = data.DD_CreateTime;
            cmd.Parameters.Add("DD_UpdateTime", SqlDbType.DateTime).Value = data.DD_UpdateTime;
            Persister.ExecuteNonQuery(cmd);
        }

        public static void Upd_DIALOG_DETAIL_DD_Type(string DD_Type, string DD_ID)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Update Dialog_Detail set DD_Type=@DD_Type,DD_UpdateTime=GetDate() where DD_ID=@DD_ID";
            cmd.Parameters.Add("DD_Type", SqlDbType.SmallInt).Value = DD_Type;
            cmd.Parameters.Add("DD_ID", SqlDbType.Int).Value = DD_ID;
            Persister.ExecuteNonQuery(cmd);
        }

        public static void Upd_DIALOG_DETAIL(MotionClass.DIALOG_DETAIL.REPLY data)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"Update Dialog_Detail set DD_Type=@DD_Type, DD_Motion=@DD_Motion
            , DD_Reply=@DD_Reply, DD_Bad=@DD_Bad, DD_Bad_Term=@DD_Bad_Term, DD_Good=@DD_Good
            , DD_Good_Term=@DD_Good_Term, DD_Judgment=@DD_Judgment, DD_UpdateTime=@DD_UpdateTime
            where DD_ID=@DD_ID
            ";
            cmd.Parameters.Add("DD_Type", SqlDbType.SmallInt).Value = data.DD_Type;
            cmd.Parameters.Add("DD_Motion", SqlDbType.Float).Value = data.DD_Motion;
            cmd.Parameters.Add("DD_Reply", SqlDbType.NVarChar).Value = data.DD_Reply;
            cmd.Parameters.Add("DD_Bad", SqlDbType.Int).Value = data.DD_Bad;
            cmd.Parameters.Add("DD_Bad_Term", SqlDbType.NVarChar).Value = data.DD_Bad_Term;
            cmd.Parameters.Add("DD_Good", SqlDbType.Int).Value = data.DD_Good;
            cmd.Parameters.Add("DD_Good_Term", SqlDbType.NVarChar).Value = data.DD_Good_Term;
            cmd.Parameters.Add("DD_Judgment", SqlDbType.NVarChar).Value = data.DD_Judgment;
            cmd.Parameters.Add("DD_UpdateTime", SqlDbType.DateTime).Value = data.DD_UpdateTime;
            cmd.Parameters.Add("DD_ID", SqlDbType.Int).Value = data.DD_ID;
            Persister.ExecuteNonQuery(cmd);
        }

        public static MotionClass.DIALOG_DETAIL.ASK Get_DIALOG_DETAIL(string DD_DMID, string DD_Type, string DD_Sentence, string DD_CreateTime, string DD_UpdateTime)
        {
            MotionClass.DIALOG_DETAIL.ASK data = new MotionClass.DIALOG_DETAIL.ASK();
            data.DD_DMID = DD_DMID;
            data.DD_Type = DD_Type;
            data.DD_Sentence = DD_Sentence;
            data.DD_CreateTime = DD_CreateTime;
            data.DD_UpdateTime = DD_UpdateTime;
            return data;
        }

        #endregion

        #region DIALOG_FEEDBACK 對話回饋
        public static DataTable Get_DIALOG_FEEDBACK(string DF_DDID)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select * from Dialog_Feedback where DF_DDID=@DF_DDID";
            cmd.Parameters.Add("DF_DDID", SqlDbType.Int).Value = DF_DDID;
            DataTable dt = Persister.Execute(cmd);
            return dt;
        }

        public static MotionClass.DIALOG_FEEDBACK Get_DIALOG_FEEDBACK(string DF_ID, string DF_DDID, string DF_Type, string DF_CreateTime)
        {
            MotionClass.DIALOG_FEEDBACK data = new MotionClass.DIALOG_FEEDBACK();
            data.DF_ID = DF_ID;
            data.DF_DDID = DF_DDID;
            data.DF_Type = DF_Type;
            data.DF_CreateTime = DF_CreateTime;
            return data;
        }

        public static void Add_DIALOG_FEEDBACK(MotionClass.DIALOG_FEEDBACK data)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"Insert into Dialog_Feedback (DF_DDID, DF_Type, DF_CreateTime) 
            values (@DF_DDID, @DF_Type, @DF_CreateTime)";
            cmd.Parameters.Add("DF_DDID", SqlDbType.Int).Value = data.DF_DDID;
            cmd.Parameters.Add("DF_Type", SqlDbType.SmallInt).Value = data.DF_Type;
            cmd.Parameters.Add("DF_CreateTime", SqlDbType.DateTime).Value = data.DF_CreateTime;
            Persister.ExecuteNonQuery(cmd);
        }
        #endregion

        #region Sort_Good 好的詞
        public static DataTable Get_Sort_Good()
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select * from Sort_Good";
            DataTable dt = Persister.Execute(cmd);
            return dt;
        }
        #endregion

        #region Sort_Bad 不好的詞
        public static DataTable Get_Sort_Bad()
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select * from Sort_Bad";
            DataTable dt = Persister.Execute(cmd);
            return dt;
        }
        #endregion

        public static DataTable Get_DIALOG_DETAIL_List(string DD_DMID)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(DD_DMID))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = @"select Dialog_Detail.* from Dialog_Detail
            LEFT JOIN Dialog_Feedback on DD_ID=DF_DDID
            where DF_ID is null";
                dt = Persister.Execute(cmd);
            }
            else
            {
                dt = Get_DIALOG_DETAIL(DD_DMID);
            }

            dt.Columns.Add("DF_ID");
            dt.Columns.Add("DF_Type");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string DD_ID = dt.Rows[i]["DD_ID"].ToString();
                DataTable dt_DF = Get_DIALOG_FEEDBACK(DD_ID);

                if (dt_DF.Rows.Count > 0)
                {
                    dt.Rows[i]["DF_ID"] = dt_DF.Rows[0]["DF_ID"];
                    dt.Rows[i]["DF_Type"] = dt_DF.Rows[0]["DF_Type"];
                }
                else
                {
                    dt.Rows[i]["DF_ID"] = "";
                    dt.Rows[i]["DF_Type"] = "";
                }
            }

            return dt;
        }

    }
}
