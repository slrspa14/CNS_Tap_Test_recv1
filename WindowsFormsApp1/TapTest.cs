using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class TapTest
    {
        //10초 카운트 다운
        //tap 횟수
        //결과 전송
        //상단에 닉네임 띄우기
        //tap 치면 메뉴판 안나오게 하기
        //10초 지나면 메뉴판 다시 나오면서 결과화면
        private TableLayoutPanel tapTime;
        private Panel panel;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        string name;
        Button Tapbtn;
        Label TenCount;
        Label TapCount;
        private int tapCount = 0;
        private int tenCount = 10;
        public TapTest(Panel TestPanel, TableLayoutPanel TapTimePanel)
        {
            this.panel = TestPanel;
            this.tapTime = TapTimePanel;
            Initialize();
        }

        public void TimerTick(object s, EventArgs e)
        {
            if(tenCount >= 1)
            {
                tenCount--;
            }
            UpdateTime();
        }
        private void UpdateTime()
        {
            TenCount.Text = tenCount.ToString();
            TapCount.Text = tapCount.ToString();
        }
        public void Initialize()
        {
            //tapCount
            Tapbtn = new Button
            {
                Text = "TAP",
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
            };
            TapCount = new Label
            {
                Text = tapCount.ToString(),
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                Dock = DockStyle.Left,
                Height = 30,
            };
            TenCount = new Label
            {
                Text = tenCount.ToString(),
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                Dock = DockStyle.Right,
                Height = 30,
            };

            Tapbtn.Click += (s, e) =>
            {
                ++tapCount;
                timer.Start();
                
            };
            //10초 타이머
            timer.Interval = 1000;
            timer.Tick += TimerTick;
            UpdateTime();

            //패널에 추가
            this.panel.Controls.Add(Tapbtn);
            this.tapTime.Controls.Add(TapCount);
            this.tapTime.Controls.Add(TenCount);

        }
    }
}
