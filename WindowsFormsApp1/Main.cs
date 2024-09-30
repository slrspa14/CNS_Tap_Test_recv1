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
    public partial class Main : Form
    {
        //class 분할생각
        //고정 패널 : 시계, 날짜 ,메뉴탭, 요일
        Panel startPanel;
        Panel joinPanel;
        Panel loginPanel;
        Panel tapPanel;
        Panel resultPanel;
        Panel commomPanel;

        TableLayoutPanel menuPanel;

        private Button currentStatus = null;
        public Main()
        {
            InitializeComponent();

            // 패널 초기화
            InitScreens();

            // 처음에는 첫 번째 화면만 보이게 함
            //ShowScreen(screen1);
        }

        private void InitScreens()
        {
            menuPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom,
                ColumnCount = 4, // 4열로 나누기
                RowCount = 1,    // 1행
                Height = 60,
            };
            // 각 열의 크기를 25%로 설정
            for (int i = 0; i < 4; i++)
            {
                menuPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
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
            join_btn.MouseDown += MouseDownColor;
            join_btn.MouseUp += MouseUpColor;
            login_btn.MouseDown += MouseDownColor;
            login_btn.MouseUp += MouseUpColor;
            test_btn.MouseDown += MouseDownColor;
            test_btn.MouseUp += MouseUpColor;
            record_btn.MouseDown += MouseDownColor;
            record_btn.MouseUp += MouseUpColor;
            menuPanel.Controls.Add(join_btn);
            menuPanel.Controls.Add(login_btn);
            menuPanel.Controls.Add(test_btn);
            menuPanel.Controls.Add(record_btn);
            this.Controls.Add(menuPanel);
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
    }
}
