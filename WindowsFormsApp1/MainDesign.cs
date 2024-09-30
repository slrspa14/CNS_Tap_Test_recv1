using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainDesign : Form
    {
        //tap 할 때에는 시계랑 요일 숨기고 탭 카운트랑 10초로 띄우기
        //class 분할생각
        //고정 패널 : 시계, 날짜 ,메뉴탭, 요일
        Panel startPanel;
        Panel joinPanel;
        Panel loginPanel;
        Panel tapPanel;
        Panel resultPanel;
        Panel commomPanel;

        TableLayoutPanel mTimePanel;
        TableLayoutPanel mMenuPanel;

        Label Time_lbl;
        Label Day_lbl;
        private Timer watch;
        private Button currentStatus = null;
        private string gifPath = @"D:\test.gif";

        public MainDesign()
        {
            InitializeComponent();
            CenterToScreen();
            // 패널 초기화
            InitScreens();

            // 처음에는 첫 번째 화면만 보이게 함
            //ShowScreen(screen1);
        }

        private void InitScreens()
        {
            mMenuPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom,
                ColumnCount = 4, // 4열로 나누기
                RowCount = 1,    // 1행
                Height = 60,
            };
            mTimePanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 1,
                Height = 50,
            };
            Time_lbl = new Label
            {
                Text = "asdqw",
                Font = new Font("맑은 고딕", 14F),
                Dock = DockStyle.Left,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            Day_lbl = new Label
            {
                Text = "asd",
                Font = new Font("맑은 고딕", 14F),
                Dock = DockStyle.Right,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            startPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 23,
            };

            PictureBox Startemotion = new PictureBox
            {
                Image = Image.FromFile(gifPath),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            // 각 열의 크기를 25%로 설정
            for (int i = 0; i < 4; i++)
            {
                mMenuPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
            //회원가입 버튼, 로그인 버튼, 테스트 버튼, 기록 확인 버튼
            Button join_btn = new Button
            { 
                Text = "JOIN",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F),
                Height = 50,
                FlatStyle = FlatStyle.Flat
            };
            Button login_btn = new Button
            { 
                Text = "LOGIN",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F),
                Height = 50,
                FlatStyle = FlatStyle.Flat
            };
            Button test_btn = new Button 
            { 
                Text = "TEST",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F),
                Height = 50,
                FlatStyle = FlatStyle.Flat
            };
            Button record_btn = new Button 
            {
                Text = "RECORD",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F) ,
                Height = 50,
                FlatStyle = FlatStyle.Flat 
            };

            //시작화면 gif 클릭 이벤트
            Startemotion.Click += (s, e) =>
            {
                startPanel.Hide();
                mMenuPanel.Show();
            };

            //메뉴 버튼 클릭 이벤트
            join_btn.MouseDown += MouseDownColor;
            join_btn.MouseUp += MouseUpColor;
            login_btn.MouseDown += MouseDownColor;
            login_btn.MouseUp += MouseUpColor;
            test_btn.MouseDown += MouseDownColor;
            test_btn.MouseUp += MouseUpColor;
            record_btn.MouseDown += MouseDownColor;
            record_btn.MouseUp += MouseUpColor;


            //시작 버튼
            //startPanel.Controls.Add(Start_btn);
            startPanel.Controls.Add(Startemotion);
            //메뉴 버튼
            mMenuPanel.Controls.Add(join_btn);
            mMenuPanel.Controls.Add(login_btn);
            mMenuPanel.Controls.Add(test_btn);
            mMenuPanel.Controls.Add(record_btn);
            //상단 시간 날짜
            mTimePanel.Controls.Add(Time_lbl);
            mTimePanel.Controls.Add(Day_lbl);
            this.Controls.Add(mMenuPanel);
            this.Controls.Add(mTimePanel);
            this.Controls.Add(startPanel);
            mMenuPanel.Hide();
            watch = new Timer
            {
                Interval = 1000,
            };
            watch.Tick += Timer;
            watch.Start();
            UpdateTime();
        }

        private void MouseDownColor(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if(btn != null)
            {
                if(currentStatus != null && currentStatus != btn)
                {
                    //초기화
                    ResetButton(currentStatus);
                }
                btn.BackColor = Color.LightGray;
                currentStatus = btn;
            }
        }
        private void MouseUpColor(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.BackColor = Color.LightGray; // 눌림 상태 유지
            }
        }
        private void ResetButton(Button btn)
        {
            btn.BackColor = SystemColors.Control;
        }
        private void Timer(object sender, EventArgs e)
        {
            UpdateTime();
        }
        private void UpdateTime()
        {
            Time_lbl.Text = DateTime.Now.ToString("HH:mm:ss");
            Day_lbl.Text = DateTime.Now.ToString("dddd");
        }
    }
}
