using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseData
{
    public class MyFunction
    {
        /// <summary>
        /// 取得區間日期(無周末)(不包含待測日期)
        /// </summary>
        /// <param name="dateStart">起始日期(2020/08/20)</param>
        /// <param name="dateEnd">結束日期(2020/08/27)</param>
        /// <returns></returns>
        public static List<string> GetAllDaysNoWeekend(string dateStart, string dateEnd)
        {
            string[] start = dateStart.Split('/');
            string[] end = dateEnd.Split('/');
            List<string> Date = new List<string>();
            for (DateTime dt = new DateTime(Convert.ToInt32(start[0]), Convert.ToInt32(start[1]), Convert.ToInt32(start[2])); dt <= new DateTime(Convert.ToInt32(end[0]), Convert.ToInt32(end[1]), Convert.ToInt32(end[2])); dt = dt.AddDays(1))
            {
                // 取得日期是星期幾
                string date = Convert.ToDateTime(dt.ToString("yyyy-MM-dd")).DayOfWeek.ToString();
                if (dt.ToString("yyyy/MM/dd") != dateEnd)
                {
                    if (date != "Saturday" && date != "Sunday")
                        Date.Add(dt.ToString("yyyyMMdd"));
                }
            }
            return Date;
        }
        /// <summary>
        /// 國曆轉西元
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Slash"></param>
        /// <returns></returns>
        public static string SolarToVids(string date, bool Slash)
        {
            string[] dateSplit = date.Trim().Split('/');
            string year = (Convert.ToInt32(dateSplit[0]) + 1911).ToString();
            string returnDate = "";

            if (Slash)
                returnDate = $"{year}/{dateSplit[1]}/{dateSplit[2]}";
            else
                returnDate = $"{year}{dateSplit[1]}{dateSplit[2]}";

            return returnDate;
        }

        /// <summary>
        /// 讀取上市當沖標的資料寫入SQLite
        /// </summary>
        /// <param name="date">日期</param>
        public static bool WriteListedAlertToSQL(string date)
        {
            List<string> Data = new List<string>();
            // 爬取資料
            var AlertIndfo = ParseListedAlert(date);
            if (AlertIndfo != null)
            {
                foreach (var item in AlertIndfo)
                    Data.Add(item.Key + "_" + item.Value);

                // 寫入SQLite
                SQliteDb sQlite = new SQliteDb();
                string insertString = "";
                if (sQlite.DataAdd(Args_.ListedAlert_saveDir, $"Data{date}", Args_.ListedAlert_header, Data, insertString))
                {
                    Console.WriteLine($"{date} : 完成");
                    return true;
                }

            }

            Console.WriteLine($"{date}:上市當沖標的取得失敗!");
            return false;
        }
        /// <summary>
        /// 抓當日上市處置資料
        /// </summary>
        /// <param name="date">Example : 20200824 </param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseListedAlert(string date)
        {
            Dictionary<string, string> Securities = new Dictionary<string, string>();
            HtmlWeb webClient = new HtmlWeb();
            var doc = webClient.Load($"https://www.twse.com.tw/exchangeReport/TWTB4U?response=html&date={date}&selectType=All");
            var table = doc.DocumentNode.SelectSingleNode("/html/body//div[contains(text(),'日沖銷交易標的')]");
            if (table != null)
            {
                table = FindTable(table);

                var tbody = table.SelectNodes(".//tbody//tr");
                if (tbody != null)
                {
                    foreach (var tr in tbody)
                    {
                        List<string> Info = new List<string>();
                        foreach (var td in tr.SelectNodes(".//td"))
                        {
                            Info.Add(td.InnerText.Trim());
                        }
                        // 無法當沖股
                        if (Info[2] == "Y")
                            //  0 => 公司代號 1 => 公司名稱
                            Securities.Add(Info[0], Info[1]);
                    }
                    return Securities;
                }
            }
            return null;
        }

        public static Dictionary<string, string> ParseStopSell(string date)
        {
            Dictionary<string, string> Data = new Dictionary<string, string>();
            HtmlWeb webClient = new HtmlWeb();
            var doc = webClient.Load($"https://www.twse.com.tw/exchangeReport/TWTBAU1?response=html");
            var table = doc.DocumentNode.SelectSingleNode("/html/body//div[contains(text(),'暫停先賣後買當日沖銷')]");
            if (table != null)
            {
                table = FindTable(table);

                var tbody = table.SelectNodes(".//tbody//tr");
                if (tbody != null)
                {
                    foreach (var tr in tbody)
                    {
                        List<string> Info = new List<string>();
                        foreach (var td in tr.SelectNodes(".//td"))
                        {
                            Console.WriteLine(td.InnerText.Trim());
                            //Data.Add(td.InnerText.Trim());
                        }
                    }
                    return Data;
                }
            }
            return null;
        }

        private static HtmlNode FindTable(HtmlNode node)
        {
            if (node.ParentNode.Name.Equals("table", StringComparison.OrdinalIgnoreCase))
            {
                return node.ParentNode;
            }
            else
            {
                return FindTable(node.ParentNode);
            }
        }
    }
}
