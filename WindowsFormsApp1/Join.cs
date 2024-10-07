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
        DataBase mDB = new DataBase();

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
        public void ClearTextBox()
        {
            mUserID.Clear();
            mUserPW.Clear();
            mUserNickName.Clear();
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
                Text = "중복확인",
                Font = new System.Drawing.Font("맑은 고딕", 8F),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Width = 100,
            };

            mJoin_btn = new Button
            {
                Width = 190,
                Height = 35,
                Text = "회원가입",
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            mUserID = new TextBox
            {
                Dock = DockStyle.Fill,
                Text = "ID",
                ForeColor = Color.Gray,
                Font = new Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            mUserPW = new TextBox
            {
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
                Dock = DockStyle.Fill,
                Text = "NickName",
                ForeColor = Color.Gray,
                Font = new Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            //이벤트
            mCheckID_btn.Click += Click_CheckID;
            mJoin_btn.Click += Click_Join;

            mUserID.KeyDown += JoinKeyEnter;
            mUserPW.KeyDown += JoinKeyEnter;
            mUserNickName.KeyDown += JoinKeyEnter;

            mUserID.GotFocus += (s, e) => MainDesign.ClearTextBox(mUserID, "ID");
            mUserPW.GotFocus += (s, e) => MainDesign.ClearTextBox(mUserPW, "PW");
            mUserNickName.GotFocus += (s, e) => MainDesign.ClearTextBox(mUserNickName, "NickName");

            //mUserID.LostFocus += (s, e) => RestoreTextBox(mUserID, "ID");
            //mUserPW.LostFocus += (s, e) => RestoreTextBox(mUserPW, "PW");
            //mUserNickName.LostFocus += (s, e) => RestoreTextBox(mUserNickName, "NickName");

            mUserID.LostFocus += (s, e) => MainDesign.RestoreTextBox(mUserID, "ID");
            mUserPW.LostFocus += (s, e) => MainDesign.RestoreTextBox(mUserPW, "PW");
            mUserNickName.LostFocus += (s, e) => MainDesign.RestoreTextBox(mUserNickName, "NickName");

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
            mDB = new DataBase();
            string ID = mUserID.Text;
            //dataBase.SelectTapData();
            if(ID != mDB.SelectID(ID))
            {
                //사용 가능 ID
                MessageBox.Show("사용 가능한 ID입니다.");
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
            if(mUserID.Text == "ID" ||  mUserPW.Text == "PW" || mUserNickName.Text == "NickName")
            {
                MessageBox.Show("공백없이 입력해주세요.");
            }
            else
            {
                MessageBox.Show("회원가입이 완료되었습니다.");
                mDB.InsertJoinData(mUserID.Text, mUserPW.Text, mUserNickName.Text);
                ClearTextBox();
                isDuplication = false;
            }

        }
        private void JoinKeyEnter(object sender, KeyEventArgs e)
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

            }
        }

        private void JoinKeyClick(object s, EventArgs e)
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
        }
    }
}
