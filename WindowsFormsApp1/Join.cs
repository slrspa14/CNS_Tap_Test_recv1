using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Join
    {
        //id 중복검사
        //DB 객체
        DataBase dataBase;

        private TableLayoutPanel mJoinpanel;
        private Label mID_lbl;
        private Label mPW_lbl;
        private Label mNickname_lbl;
        private Button mCheckID_btn;
        private Button mJoin_btn;
        private TextBox mUserID;
        private TextBox mUserPW;
        private TextBox mUserNickName;

        private bool isDuplication = false;
        public Join(TableLayoutPanel joinPanel) 
        { 
            this.mJoinpanel = joinPanel;
            Initialize();
        }

        public void Initialize()
        {
            //라벨 3개(id, pw, nickname), 버튼 2개(id 중복체크, 회원가입), textbox 3개(id, pw, nickname)
            mID_lbl = new Label
            {
                Text = "ID",
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            };

            mPW_lbl = new Label
            {
                Text = "PW",
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            };

            mNickname_lbl = new Label
            {
                Text = "NICKNAME",
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            };

            mCheckID_btn = new Button
            {
                //Dock = DockStyle.Fill,
                Text = "중복확인",
                Font = new System.Drawing.Font("맑은 고딕", 8F),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Width = 100,
            };

            mJoin_btn = new Button
            {
                Width = 190,
                Height = 35,
                //Dock = DockStyle.Fill,
                Text = "회원가입",
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            mUserID = new TextBox
            {
                //Width = 120,
                Dock = DockStyle.Fill,
                Text = "ID",
                ForeColor = Color.Gray,
                Font = new Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            mUserPW = new TextBox
            {
                //Width = 120,
                Dock = DockStyle.Fill,
                Text = "PW",
                ForeColor = Color.Gray,
                PasswordChar = '\0',
                MaxLength = 14,
                Font = new Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            mUserNickName = new TextBox
            {
                //Width = 120,
                Dock = DockStyle.Fill,
                Text = "NickName",
                ForeColor = Color.Gray,
                Font = new Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            //이벤트
            mCheckID_btn.Click += Click_CheckID;
            mJoin_btn.Click += Click_Join;

            mUserID.KeyDown += LoginKeyEnter;
            mUserPW.KeyDown += LoginKeyEnter;
            mUserNickName.KeyDown += LoginKeyEnter;

            mUserID.GotFocus += (s, e) => ClearTextBox(mUserID, "ID");
            mUserPW.GotFocus += (s, e) => ClearTextBox(mUserPW, "PW");
            mUserNickName.GotFocus += (s, e) => ClearTextBox(mUserNickName, "NickName");

            //mUserID.LostFocus += (s, e) => RestoreTextBox(mUserID, "ID");
            //mUserPW.LostFocus += (s, e) => RestoreTextBox(mUserPW, "PW");
            //mUserNickName.LostFocus += (s, e) => RestoreTextBox(mUserNickName, "NickName");

            mUserID.LostFocus += (s, e) => MainDesign.RestoreTextBox(mUserID, "ID");

            //패널 추가
            mJoinpanel.Controls.Add(mID_lbl, 0, 1);
            mJoinpanel.Controls.Add(mPW_lbl, 0, 2);
            mJoinpanel.Controls.Add(mNickname_lbl, 0, 3);
            mJoinpanel.Controls.Add(mUserID, 1, 1);
            mJoinpanel.Controls.Add(mUserPW, 1, 2);
            mJoinpanel.Controls.Add(mUserNickName, 1, 3);
            mJoinpanel.Controls.Add(mCheckID_btn, 2, 1);
            mJoinpanel.Controls.Add(mJoin_btn, 1, 4);

        }
        private void Click_CheckID(object s, EventArgs e)
        {
            dataBase = new DataBase();
            string ID = mUserID.Text;
            //dataBase.SelectTapData();
            if(ID == dataBase.SelectID(ID))
            {
                //사용 가능 ID

                isDuplication = true;
                mCheckID_btn.Enabled = false;
            }
            else
            {
                MessageBox.Show("중복된 ID입니다.\n다시 입력해주세요");
                mUserID.Clear();
                isDuplication = false;
                return;
            }
        }
        private void Click_Join(object s, EventArgs e)
        {
            if(!isDuplication) 
            {
                MessageBox.Show("ID 중복확인을 해주세요");
                return;
            }
            else
            {
                //회원가입 완료

                isDuplication = false;
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
                    MessageBox.Show("공백없이 입력해주세요.");
                    return;
                }

                //DB 쿼리문으로 검사하기
                ValidUserInformation(ID, PW);

            }
        }
        private void ValidUserInformation(string ID, string PW)
        {
            //로그인 DB 검사
            //dataBase.SelectTapData(ID, PW);
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
                MessageBox.Show("공백없이 입력해주세요.");
                return;
            }

            //DB 쿼리문으로 검사하기
            ValidUserInformation(ID, PW);
        }

        private void ClearTextBox(TextBox txt, string defaultText)
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

        //private void RestoreTextBox(TextBox txt, string defaultText)
        //{
        //    if (string.IsNullOrWhiteSpace(txt.Text))
        //    {
        //        txt.Text = defaultText;  // 기본 텍스트로 복구
        //        txt.ForeColor = Color.Gray;  // 회색으로 기본 텍스트 표시

        //        if (defaultText == "PW")
        //        {
        //            txt.PasswordChar = '\0';  // 비밀번호 가림 해제
        //        }
        //    }
        //}
    }
}
