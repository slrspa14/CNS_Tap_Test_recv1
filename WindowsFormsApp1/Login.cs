using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Login
    {
        //ID, PW 검사해서 로그인 성공/실패
        //로그인시 닉네임 가져와서 띄우기'
        DataBase dataBase = new DataBase();
        private Panel loginPanel;
        private TextBox mUserID;
        private TextBox mUserPW;
        public Login(Panel LoginPanel)
        {
            this.loginPanel = LoginPanel;
            Initialize();
        }
        public void LoginKeydown(object sender, KeyEventArgs e)
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
        private void ValidUserInformation(string ID, string PW)
        {

        }
        public void Initialize()
        {
            mUserID = new TextBox
            {
                //Dock = DockStyle.None,
                Height = 30,
                Width = 100,
                //Location = new Point(125, 100),
                Text = "ID",
                Font = new Font("맑은 고딕", 10F),
                ForeColor = Color.Gray,
                TextAlign = HorizontalAlignment.Left,
            };

            mUserPW = new TextBox
            {
                //Dock = DockStyle.None,
                Height = 30,
                Width = 30,
                Text = "PW",
                ForeColor = Color.Gray,
                PasswordChar = '*',
                MaxLength = 14,
                Font = new Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            Button loginbtn = new Button
            {
                Text = "로그인",
                Dock = DockStyle.Bottom,
                Height = 40,
                Font = new Font("맑은 고딕", 10F),
                FlatStyle = FlatStyle.Flat,
            };

            //로그인 이벤트
            mUserID.KeyDown += LoginKeydown;
            mUserPW.KeyDown += LoginKeydown;
            mUserID.GotFocus += (s, e) => ClearTextBox(mUserID, "ID");
            mUserPW.GotFocus += (s, e) => ClearTextBox(mUserID, "PW");

            loginbtn.Click += (s, e) => dataBase.SelectTapData();

            //로그인 패널
            loginPanel.Controls.Add(mUserID);
            loginPanel.Controls.Add(mUserPW);
            loginPanel.Controls.Add(loginbtn);
        }
        private void ClearTextBox(TextBox textBox, string information)
        {
            if (textBox.Text == information)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
            }
        }
    }
}