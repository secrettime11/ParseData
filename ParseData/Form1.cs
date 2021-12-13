using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParseData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_ListedAlert_Click(object sender, EventArgs e)
        {
            Thread runner = new Thread(alertRun);
            runner.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void txt_endday_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_fromday_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void alertRun()
        {
            string startDate = "";
            string startDateSlash = "";
            string endDate = "";
            string endDateSlash = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                label1.Text = "新增中..";
                startDate = MyFunction.SolarToVids(txt_fromday.Text, false);
                startDateSlash = MyFunction.SolarToVids(txt_fromday.Text, true);
                endDate = MyFunction.SolarToVids(txt_endday.Text, false);
                endDateSlash = MyFunction.SolarToVids(txt_endday.Text, true);
            });

            List<string> TestDays = MyFunction.GetAllDaysNoWeekend(startDateSlash, endDateSlash);
            SQliteDb sQlite = new SQliteDb();
            foreach (var item in TestDays)
            {
                // 資料表不存在 => 寫入SQLite
                if (!sQlite.CheckDatatable(Args_.ListedAlert_saveDir, "Data" + item))
                {
                    MyFunction.WriteListedAlertToSQL(item);
                }
                Thread.Sleep(5000);
            }
            this.Invoke((MethodInvoker)delegate ()
            {
                label1.Text = "新增完畢";
            });

        }

        private void btn_OTCAlert_Click(object sender, EventArgs e)
        {
            Thread runner = new Thread(OTCalertRun);
            runner.Start();
        }
        private void OTCalertRun()
        {
            string startDate = "";
            string startDateSlash = "";
            string endDate = "";
            string endDateSlash = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                label1.Text = "新增中..";
                startDate = MyFunction.SolarToVids(txt_fromday.Text, false);
                startDateSlash = MyFunction.SolarToVids(txt_fromday.Text, true);
                endDate = MyFunction.SolarToVids(txt_endday.Text, false);
                endDateSlash = MyFunction.SolarToVids(txt_endday.Text, true);
            });

            List<string> TestDays = MyFunction.GetAllDaysNoWeekend(startDateSlash, endDateSlash);
            SQliteDb sQlite = new SQliteDb();
            foreach (var item in TestDays)
            {
                // 資料表不存在 => 寫入SQLite
                if (!sQlite.CheckDatatable(Args_.ListedAlert_saveDir, "Data" + item))
                {
                    MyFunction.WriteListedAlertToSQL(item);
                }
                Thread.Sleep(5000);
            }
            this.Invoke((MethodInvoker)delegate ()
            {
                label1.Text = "新增完畢";
            });

        }

        private void CantTrade() 
        {
            string startDate = "";
            string startDateSlash = "";
            string endDate = "";
            string endDateSlash = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                label1.Text = "新增中..";
                startDate = MyFunction.SolarToVids(txt_fromday.Text, false);
                startDateSlash = MyFunction.SolarToVids(txt_fromday.Text, true);
                endDate = MyFunction.SolarToVids(txt_endday.Text, false);
                endDateSlash = MyFunction.SolarToVids(txt_endday.Text, true);
            });

            List<string> TestDays = MyFunction.GetAllDaysNoWeekend(startDateSlash, endDateSlash);
            SQliteDb sQlite = new SQliteDb();
            foreach (var item in TestDays)
            {
                // 資料表不存在 => 寫入SQLite
                //if (!sQlite.CheckDatatable(Args_.ListedAlert_saveDir, "Data" + item))
                //{
                //    MyFunction.WriteListedAlertToSQL(item);
                //}
                MyFunction.ParseStopSell(item);
                Thread.Sleep(5000);
            }
            this.Invoke((MethodInvoker)delegate ()
            {
                label1.Text = "新增完畢";
            });

        }

        private void btn_LstopTrade_Click(object sender, EventArgs e)
        {
            Thread runner = new Thread(CantTrade);
            runner.Start();
        }
    }
}
