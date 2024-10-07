using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Chart = System.Windows.Forms.DataVisualization.Charting.Chart;

namespace WindowsFormsApp1
{
    internal class Record
    {
        //결과값 시각화(그래프)
        //닉네임 시간 띄우기
        //요일별, 주간, 월간 띄우기
        //
        private DataBase mDB = new DataBase();
        private TableLayoutPanel mRecordPanel;
        private Chart visualize;
        public Record(TableLayoutPanel panel)
        {
            this.mRecordPanel = panel;
            Initialize();

        }
        private void Initialize()
        {
            mRecordPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            //DB 값 올려놓을 패널 만들기
            visualize = new Chart
            {
                Dock = DockStyle.Fill,
            };
            ChartArea chartArea = new ChartArea("MainChart");
            visualize.ChartAreas.Add(chartArea);

            mRecordPanel.Controls.Add(visualize);
        }
        public void VisualizeResult(List<Result> results, Chart chart)
        {
            chart.Series.Clear();
            Series series = new Series("Result");
            series.ChartType = SeriesChartType.Line;
            foreach(var result in results)
            {
                series.Points.AddXY(result.Time, result.Record);
            }
            chart.Series.Add(series);
        }
        public List<Result> GetResult()
        {
            List<Result> results = mDB.SelectTapData();
            return results;

        }
        public static void InsertResult(string NickName, int Result, string time)
        {
            //mDB.InsertTapData(NickName, Result, time);
        }
    }
    public class Result
    {
        public string NickName { get; set; }
        public string Time { get; set; }
        public int Record {  get; set; }

        public Result(string nickName, string time, int record)
        {
            NickName = nickName;
            Time = time;
            Record = record;

        }
    }

}
