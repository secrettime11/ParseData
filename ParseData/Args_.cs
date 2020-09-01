using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseData
{
    class Args_
    {
        public static readonly List<string> Capital_header = new List<string>() { "公司名稱", "上市時股本", "現有股本", "公司代號" };
        public static readonly List<string> Listed_header = new List<string>() { "公司代號", "公司名稱", "成交股數", "成交筆數", "成交金額", "開盤價", "最高價", "最低價", "收盤價", "漲跌", "漲跌價差", "最後揭示買價", "最後揭示買量", "最後揭示賣價", "最後揭示賣量", "本益比", "現有資本額", "周轉率" };
        public static readonly List<string> ListedAlert_header = new List<string>() { "公司代號", "公司名稱" };

        public const string Capital_saveDir = @"Database\Capital.db";
        public const string Listed_saveDir = @"Database\Listed.db";
        public const string ListedAlert_saveDir = @"Database\ListedAlert.db";
        public const string Temp_saveDir = @"Database\Temp.db";
    }
}
