using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Crmf;
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

        DataBase mDB = new DataBase();
        //패널을 바꾸기
        private TableLayoutPanel mLoginPanel;
        //private Panel loginPanel;
        private System.Windows.Forms.TextBox mUserID;
        private System.Windows.Forms.TextBox mUserPW;
        private Label ID_lbl;
        private Label PW_lbl;
        private bool login_out = false;
        private List<Control> controls;

        private string UserNickName { get; set; }

        public Login(TableLayoutPanel LoginPanel)
        {
            this.mLoginPanel = LoginPanel;
            Initialize();
        }
       
        public void Initialize()
        {
             controls = new List<Control>();

            if(!login_out)
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

                mUserID.GotFocus += (s, e) => MainDesign.ClearTextBox(mUserID, "ID");
                mUserPW.GotFocus += (s, e) => MainDesign.ClearTextBox(mUserPW, "PW");

                mUserID.LostFocus += (s, e) => MainDesign.RestoreTextBox(mUserID, "ID");
                mUserPW.LostFocus += (s, e) => MainDesign.RestoreTextBox(mUserPW, "PW");

                loginbtn.Click += LoginKeyClick;

                //로그인 패널
                mLoginPanel.Controls.Add(mUserID, 1, 1);
                mLoginPanel.Controls.Add(mUserPW, 1, 2);
                mLoginPanel.Controls.Add(loginbtn, 1, 3);
                mLoginPanel.Controls.Add(ID_lbl, 0, 1);
                mLoginPanel.Controls.Add(PW_lbl, 0, 2);

                //추가
                controls.Add(mUserID);
                controls.Add(mUserPW);
                controls.Add(loginbtn);
                controls.Add(PW_lbl);
            }

        }

        public void ShowLogout_btn()
        {
            if(login_out == true)
            {
                foreach(Control con in controls)
                {
                    con.Visible = false;
                }
                // 다 숨기고 로그아웃 버튼 생기게
                //회원 정보랑 로그아웃 버튼 생기게
                Label UserID = new Label
                {
                    Text = mUserID.Text,
                    Font = new Font("맑은 고딕", 10F),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                Label NickName = new Label
                {
                    Text = UserNickName,
                    Font = new Font("맑은 고딕", 10F),
                    ForeColor= Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                PW_lbl = new Label
                {
                    Text = "NickName :",
                    Font = new Font("맑은 고딕", 10F),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleRight,
                };
                System.Windows.Forms.Button Logout_btn = new System.Windows.Forms.Button
                {
                    Height = 40,
                    Width = 240,
                    Text = "LOG OUT",
                    Font = new Font("맑은 고딕", 10F),
                    ForeColor=Color.Black,
                    TextAlign= ContentAlignment.MiddleCenter,
                };
                mLoginPanel.Controls.Add(UserID, 1, 1);
                mLoginPanel.Controls.Add(NickName, 1, 2);
                mLoginPanel.Controls.Add(PW_lbl, 0, 2);
                mLoginPanel.Controls.Add(Logout_btn, 1, 3);

                Logout_btn.Click += Logout_Click;
            }
        }
        private void Logout_Click(object sender, EventArgs e)
        {
            //if()
            //{
            //    MessageBox.Show("로그아웃 하시겠습니까?")
            //}
            DialogResult LogoutQuestion =  MessageBox.Show("ㅇ", "d", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(LogoutQuestion == DialogResult.Yes)
            {
                MessageBox.Show("로그아웃 되었습니다.");
                foreach(Control con in controls)
                {
                    con.Visible = true;
                }
            }
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
            string loginResult = mDB.SelectLogin(ID, PW);
            if(!string.IsNullOrEmpty(loginResult))
            {
                //로그인 성공을 하면 로그인 입력창이 안 보이게 로그아웃 버튼 생기게
                MessageBox.Show("로그인 성공");
                mUserID.Clear(); 
                mUserPW.Clear();
                login_out = true;
                UserNickName = loginResult;
                ShowLogout_btn();
            }
            else
            {
                mUserID.Clear();
                mUserPW.Clear();
                MessageBox.Show("ID, PW 를 다시 한 번 확인해주세요.");
                return;
            }
            
        }
    }
}