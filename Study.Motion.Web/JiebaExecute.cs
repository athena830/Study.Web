﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using JiebaNet.Segmenter;

namespace Study.Motion.Web
{
    public class JiebaExecute
    {
        public static void jieba_execute(string sentence, string DD_ID, string DM_ID)
        {
            var data_detail = GetMotionDialogDetail(sentence, DD_ID);

            MotionClass.DIALOG_MAIN data_main = new MotionClass.DIALOG_MAIN();
            data_main.DM_Motion_Total = data_detail.DD_Motion;
            data_main.DM_EndTime = data_detail.DD_UpdateTime;
            data_main.DM_ID = DM_ID;

            //更新對話內容
            SQLFunc.Upd_DIALOG_DETAIL(data_detail);
            SQLFunc.Upd_DIALOG_MAIN(data_main);
        }

        public static MotionClass.DIALOG_DETAIL.REPLY GetMotionDialogDetail(string sentence, string DD_ID)
        {
            //正面辭典
            Dictionary<string, int> HappyDic = new Dictionary<string, int>();
            //負面辭典
            Dictionary<string, int> SadDict = new Dictionary<string, int>();
            //正面分數
            int GoodVal = 0;
            //負面分數
            int BadVal = 0;

            List<MotionClass.MotionWords> GoodList = new List<MotionClass.MotionWords>();
            List<MotionClass.MotionWords> BadList = new List<MotionClass.MotionWords>();
            MotionClass.MotionResult results = new MotionClass.MotionResult();

            var segmenter = new JiebaSegmenter();
            //var userDictPath = ConfigurationManager.AppSettings["UserDictFile"];

            segmenter.LoadUserDict(@"D:\\Practise\\Study\\Jieba.dict\\new_dict.txt");

            // ============== 正面用語 =================
            DataTable feelGood = SQLFunc.Get_Sort_Good(); //Get_Excel("Sort_Good");
            for (int i = 0; i < feelGood.Rows.Count; i++)
            {
                HappyDic.Add(feelGood.Rows[i]["S_Word"].ToString(), int.Parse(feelGood.Rows[i]["S_Score"].ToString()));
            }

            // ============== 負面用語 =================	
            DataTable feelBad = SQLFunc.Get_Sort_Bad(); //Get_Excel("Sort_Bad");
            for (int i = 0; i < feelBad.Rows.Count; i++)
            {
                SadDict.Add(feelBad.Rows[i]["S_Word"].ToString(), int.Parse(feelBad.Rows[i]["S_Score"].ToString()));
            }

            var tokens = segmenter.Cut(sentence);
            foreach (var token in tokens)
            {
                if (HappyDic.ContainsKey(token))
                {
                    int ss = HappyDic[token];
                    MotionClass.MotionWords wg = new MotionClass.MotionWords();
                    wg.Terms = token;
                    wg.Scores = ss;
                    GoodList.Add(wg);

                    GoodVal += ss;
                }

                if (SadDict.ContainsKey(token))
                {
                    int ss = SadDict[token];
                    MotionClass.MotionWords wg = new MotionClass.MotionWords();
                    wg.Terms = token;
                    wg.Scores = ss;
                    BadList.Add(wg);

                    BadVal += ss;
                }
            }

            if (GoodVal > BadVal)
            {
                results.Judgment = "正面";
            }
            else if (BadVal > GoodVal)
            {
                results.Judgment = "負面";
            }
            else
            {
                results.Judgment = "中立";
            }

            results.Good = GoodVal;
            results.Bad = BadVal;
            results.Good_Term = GoodList;
            results.Bad_Term = BadList;

            string jsonData = JsonParse.Json(results);
            string now_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            float score = GoodVal - BadVal;

            MotionClass.DIALOG_DETAIL.REPLY data = new MotionClass.DIALOG_DETAIL.REPLY();
            data.DD_ID = DD_ID;
            data.DD_Type = "3";
            data.DD_Motion = (score).ToString();
            data.DD_Reply = jsonData;
            data.DD_Bad = results.Bad.ToString();
            data.DD_Bad_Term = JsonParse.Json_word(results.Bad_Term);
            data.DD_Good = results.Good.ToString();
            data.DD_Good_Term = JsonParse.Json_word(results.Good_Term);
            data.DD_Judgment = results.Judgment;
            data.DD_UpdateTime = now_date;

            return data;
        }
    }
}