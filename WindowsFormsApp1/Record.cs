using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Record
    {
        //결과값 시각화(그래프)
        //닉네임 시간 띄우기
        //요일별, 주간, 월간 띄우기
        //
        private Panel recordPanel;
        private TableLayoutPanel mRecordPanel;
        public Record(TableLayoutPanel panel)
        {
            //this.recordPanel = panel;
            this.mRecordPanel = panel;
        }
    }
}
