using System;
using System.Collections.Generic;
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

        private Panel panel;
        private Label mID_lbl;
        private Label mPW_lbl;
        private Label mNickname_lbl;
        private Button mCheckID_btn;
        private Button mJoin_btn;
        private TextBox mID_txt;
        private TextBox mPW_txt;
        private TextBox mNickname_txt;

        private bool isDuplication = false;
        public Join(Panel joinPanel) 
        { 
            this.panel = joinPanel;
            Initialize();
        }

        public void Initialize()
        {
            //라벨 3개(id, pw, nickname), 버튼 2개(id 중복체크, 회원가입), textbox 3개(id, pw, nickname)
            mID_lbl = new Label
            {
                Text = "ID",
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            };
            mPW_lbl = new Label
            {
                Text = "PW",
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            };
            mNickname_lbl = new Label
            {
                Text = "NICKNAME",
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            };
            mCheckID_btn = new Button
            {
                Text = "중복확인",
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };
            mJoin_btn = new Button
            {
                Text = "회원가입",
                Font = new System.Drawing.Font("맑은 고딕", 15F),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };
            mID_txt = new TextBox
            {
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };
            mPW_txt = new TextBox
            {
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };
            mNickname_txt = new TextBox
            {
                Font = new System.Drawing.Font("맑은 고딕", 10F),
                TextAlign = HorizontalAlignment.Left,
            };

            //이벤트
            mCheckID_btn.Click += Click_CheckID;
            mJoin_btn.Click += Click_Join;
            //패널에 추가
        }
        private void Click_CheckID(object s, EventArgs e)
        {
            dataBase = new DataBase();
            string ID = mID_txt.Text;
            //dataBase.SelectTapData();
            if(ID == dataBase.SelectID(ID))
            {
                //사용 가능 ID

                isDuplication = true;
            }
            else
            {
                MessageBox.Show("중복된 ID입니다.\n다시 입력해주세요");
                mID_txt.Clear();
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
        
    }
}
