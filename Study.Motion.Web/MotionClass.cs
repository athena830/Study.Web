using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Study.Motion.Web
{
    public class MotionClass
    {
        public class DIALOG_USER
        {
            public string U_ID { get; set; }
            public string U_IP { get; set; }
            public string U_Name { get; set; }
            public string U_CreateTime { get; set; }

        }

        public class DIALOG_MAIN
        {
            public string DM_ID { get; set; }
            public string DM_UID { get; set; }
            public string DM_Motion_Total { get; set; }
            public string DM_StartTime { get; set; }
            public string DM_EndTime { get; set; }

        }

        public class DIALOG_DETAIL
        {
            public class ASK
            {
                public string DD_ID { get; set; }
                public string DD_DMID { get; set; }
                /// <summary>
                /// 目前狀態 1:尚末處理；2:處理中；3:結束
                /// </summary>
                public string DD_Type { get; set; }
                public string DD_Sentence { get; set; }
                public string DD_CreateTime { get; set; }
                public string DD_UpdateTime { get; set; }
            }
            public class REPLY
            {
                public string DD_ID { get; set; }
                public string DD_Type { get; set; }
                public string DD_Motion { get; set; }
                public string DD_Reply { get; set; }
                public string DD_Bad { get; set; }
                public string DD_Bad_Term { get; set; }
                public string DD_Good { get; set; }
                public string DD_Good_Term { get; set; }
                public string DD_Judgment { get; set; }
                public string DD_UpdateTime { get; set; }
            }
        }

        public class DIALOG_FEEDBACK
        {
            public string DF_ID { get; set; }
            public string DF_DDID { get; set; }
            /// <summary>
            /// 目前狀態 1:同意；-1:不同意；0:未判斷
            /// </summary>
            public string DF_Type { get; set; }
            public string DF_CreateTime { get; set; }
        }

        public class MotionResult
        {
            public string Judgment { get; set; }
            public int Good { get; set; }
            public int Bad { get; set; }
            public List<MotionWords> Good_Term { get; set; }
            public List<MotionWords> Bad_Term { get; set; }
        }

        public class MotionWords
        {
            public string Terms { get; set; }
            public int Scores { get; set; }
        }

    }
}