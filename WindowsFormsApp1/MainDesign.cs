using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
        
        Panel testPanel;
        Panel recordPanel;
        Panel commomPanel;

        TableLayoutPanel joinPanel;
        TableLayoutPanel TapTimePanel;
        TableLayoutPanel mTimePanel;
        TableLayoutPanel loginPanel;
        public static TableLayoutPanel mMenuPanel;

        Label Time_lbl;
        Label Day_lbl;
        private Timer watch;
        private Button currentStatus = null;
        private string gifPath = @"D:\test.gif";


        public MainDesign()
        {
            InitializeComponent();
            CenterToScreen();
            InitScreens();
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
            TapTimePanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 1,
                Height = 50,
            };
            startPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60,
            };
            loginPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 5,
                Height = 60,
            };
            testPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60,
            };
            joinPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 5,
            };
            recordPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60,
            };
            Join join = new Join(joinPanel);
            Login logIN = new Login(loginPanel);
            TapTest tapTest = new TapTest(testPanel, TapTimePanel);
            //Record record = new Record(recordPanel);

            PictureBox Startemotion = new PictureBox
            {
                Image = Image.FromFile(gifPath),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            
            //회원가입 버튼, 로그인 버튼, 테스트 버튼, 기록 확인 버튼
            Button joinMenu = new Button
            { 
                Text = "JOIN",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F),
                Height = 50,
                FlatStyle = FlatStyle.Flat
            };
            Button loginMenu = new Button
            { 
                Text = "LOGIN",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F),
                Height = 50,
                FlatStyle = FlatStyle.Flat
            };
            Button testMenu = new Button 
            { 
                Text = "TEST",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F),
                Height = 50,
                FlatStyle = FlatStyle.Flat
            };
            Button recordMenu = new Button 
            {
                Text = "RECORD",
                Dock = DockStyle.Bottom,
                Font = new Font("맑은 고딕", 12F) ,
                Height = 50,
                FlatStyle = FlatStyle.Flat 
            };

            // 메뉴 영역
            for (int i = 0; i < 4; i++)
            {
                mMenuPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            //로그인 패널
            loginPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            loginPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            loginPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            loginPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            loginPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            loginPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            loginPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            loginPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            //회원가입 패널
            joinPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            joinPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            joinPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            joinPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            joinPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            joinPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            joinPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            joinPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));

            Time_lbl = new Label
            {
                Font = new Font("맑은 고딕", 14F),
                Dock = DockStyle.Left,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            Day_lbl = new Label
            {
                Font = new Font("맑은 고딕", 14F),
                Dock = DockStyle.Right,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            //시작화면 gif 클릭 이벤트
            Startemotion.Click += (s, e) =>
            {
                startPanel.Hide();
                mMenuPanel.Show();
                ShowPanel(loginPanel);
            };

            //메뉴 버튼 클릭 이벤트
            joinMenu.MouseDown += MouseDownColor;
            joinMenu.MouseUp += MouseUpColor;
            joinMenu.Click += Click_JoinPanel;
            loginMenu.MouseDown += MouseDownColor;
            loginMenu.MouseUp += MouseUpColor;
            loginMenu.Click += Click_LoginPanel;
            testMenu.MouseDown += MouseDownColor;
            testMenu.MouseUp += MouseUpColor;
            testMenu.Click += Click_TapTestPanel;
            recordMenu.MouseDown += MouseDownColor;
            recordMenu.MouseUp += MouseUpColor;
            recordMenu.Click += Click_RecordPanel;

            //시작 버튼
            startPanel.Controls.Add(Startemotion);

            //메뉴 버튼
            mMenuPanel.Controls.Add(joinMenu);
            mMenuPanel.Controls.Add(loginMenu);
            mMenuPanel.Controls.Add(testMenu);
            mMenuPanel.Controls.Add(recordMenu);

            //상단 시간 날짜
            mTimePanel.Controls.Add(Time_lbl);
            mTimePanel.Controls.Add(Day_lbl);

            this.Controls.Add(mMenuPanel);
            this.Controls.Add(mTimePanel);
            this.Controls.Add(startPanel);
            this.Controls.Add(joinPanel);
            this.Controls.Add(loginPanel);
            this.Controls.Add(testPanel);
            this.Controls.Add(TapTimePanel);
            this.Controls.Add(recordPanel);

            TapTimePanel.Hide();
            mMenuPanel.Hide();
            watch = new Timer
            {
                Interval = 1000,
            };
            watch.Tick += Timer;
            watch.Start();
            UpdateTime();
        }

        private void LoginMenu_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

        private void ShowPanel(Panel showPanel)
        {
            List<Panel> allPanels = new List<Panel> { joinPanel, loginPanel, testPanel, recordPanel, TapTimePanel};
            foreach(Panel panel in allPanels)
            {
                panel.Hide();
            }
            showPanel.BringToFront();
            showPanel.Show();
        }
        //메뉴 버튼 이벤트
        private void Click_JoinPanel(object s, EventArgs e)
        {
            mTimePanel.Show();
            ShowPanel(joinPanel);
        }
        private void Click_LoginPanel(object s, EventArgs e)
        {
            mTimePanel.Show();
            ShowPanel(loginPanel);
        }
        private void Click_TapTestPanel(object s, EventArgs e)
        {
            mTimePanel.Hide();//시간 숨기기
            ShowPanel(testPanel);
            TapTimePanel.Show();
        }
        private void Click_RecordPanel(object s, EventArgs e)
        {
            mTimePanel.Show();
            ShowPanel(recordPanel);
        }

        public static void RestoreTextBox(TextBox txt, string defaultText)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Text = defaultText;  // 기본 텍스트로 복구
                txt.ForeColor = Color.Gray;  // 회색으로 기본 텍스트 표시

                if (defaultText == "PW")
                {
                    txt.PasswordChar = '\0';  // 비밀번호 가림 해제
                }
            }
        }
    }
}
