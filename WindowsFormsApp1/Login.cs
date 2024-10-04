using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    internal class Login
    {
        //ID, PW 검사해서 로그인 성공/실패
        //로그인시 닉네임 가져와서 띄우기'
        //워크팬츠(검정), 흑청데님, 청바지, 나일론 팬츠(회색), 카고팬츠(카키, 버릴거), 연청바지
        //회색 맨투맨, 검정 맨투맨, 회색 가디건, 회색 후드집업, 검정 v넥 후리스
        //컨버스 하이 블랙, 에어포스 올백, 993, 반스36, 
        //나이키 갈색 에어포스, 카키색 바지, 가을 아우터
        //갈색 코트, 구두(발 편한거)
        //사고싶은게 에어포스 15, 아우터, 카키 바지, 스트라이프 셔츠
        DataBase dataBase = new DataBase();
        //패널을 바꾸기
        private TableLayoutPanel mLoginPanel;
        //private Panel loginPanel;
        private System.Windows.Forms.TextBox mUserID;
        private System.Windows.Forms.TextBox mUserPW;
        private Label ID_lbl;
        private Label PW_lbl;

        public Login(TableLayoutPanel LoginPanel)
        {
            this.mLoginPanel = LoginPanel;
            Initialize();
        }
       
        public void Initialize()
        {

            mUserID = new System.Windows.Forms.TextBox
            {
                Dock = DockStyle.Fill,
                Text = "ID",
                Font = new Font("맑은 고딕", 10F),
                ForeColor = Color.Gray,
                TextAlign = HorizontalAlignment.Left,
            };

            mUserPW = new System.Windows.Forms.TextBox
            {
                Dock = DockStyle.Fill,
                Text = "PW",
                ForeColor = Color.Gray,
                PasswordChar = '\0',
                MaxLength = 14,
                Font = new Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            System.Windows.Forms.Button loginbtn = new System.Windows.Forms.Button
            {
                Text = "로그인",
                //Dock = DockStyle.Fill,
                Height = 40,
                Width = 240,
                Font = new Font("맑은 고딕", 10F),
                FlatStyle = FlatStyle.Flat,
            };

            ID_lbl = new Label
            {
                Text = "ID :",
                Font = new Font("맑은 고딕", 10F),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleRight,
            };

            PW_lbl = new Label
            {
                Text = "PW :",
                Font = new Font("맑은 고딕", 10F),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleRight,
            };

            //로그인 이벤트
            mUserID.KeyDown += LoginKeyEnter;
            mUserPW.KeyDown += LoginKeyEnter;

            mUserID.GotFocus += (s, e) => ClearTextBox(mUserID, "ID");
            mUserPW.GotFocus += (s, e) => ClearTextBox(mUserPW, "PW");

            mUserID.LostFocus += (s, e) => RestoreTextBox(mUserID, "ID");
            mUserPW.LostFocus += (s, e) => RestoreTextBox(mUserPW, "PW");

            loginbtn.Click += LoginKeyClick;

            //로그인 패널
            mLoginPanel.Controls.Add(mUserID, 1, 1);
            mLoginPanel.Controls.Add(mUserPW, 1, 2);
            mLoginPanel.Controls.Add(loginbtn, 1, 3);
            mLoginPanel.Controls.Add(ID_lbl, 0, 1);
            mLoginPanel.Controls.Add(PW_lbl, 0, 2);

        }

        private void LoginKeyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ID = mUserID.Text;
                string PW = mUserPW.Text;
                if (ID == "ID")
                {
                    ID = "";
                }
                if (PW == "PW")
                {
                    PW = "";
                }
                //공백 검사
                if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(PW))
                {
                    MessageBox.Show("ID, PW를 공백없이 입력해주세요.");
                    return;
                }

                //DB 쿼리문으로 검사하기
                ValidUserInformation(ID, PW);

            }
        }
        private void LoginKeyClick(object s, EventArgs e)
        {
            string ID = mUserID.Text;
            string PW = mUserPW.Text;
            if (ID == "ID")
            {
                ID = "";
            }
            if (PW == "PW")
            {
                PW = "";
            }
            //공백 검사
            if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(PW))
            {
                MessageBox.Show("ID, PW를 공백없이 입력해주세요.");
                return;
            }

            //DB 쿼리문으로 검사하기
            ValidUserInformation(ID, PW);
        }
        private void ValidUserInformation(string ID, string PW)
        {
            //로그인 DB 검사
            //dataBase.SelectTapData(ID, PW);
        }
        private void ClearTextBox(System.Windows.Forms.TextBox txt, string defaultText)
        {
            if (txt.Text == defaultText)
            {
                txt.Text = "";  // 클릭하면 텍스트 사라짐
                txt.ForeColor = SystemColors.WindowText;  // 글자색 검정으로 변경

                if (defaultText == "PW")
                {
                    txt.PasswordChar = '*';  // 비밀번호 입력 시 가림
                }
            }
        }

        private void RestoreTextBox(System.Windows.Forms.TextBox txt, string defaultText)
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