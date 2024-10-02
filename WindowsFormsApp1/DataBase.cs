using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class DataBase
    {
        //DB TABLE CREATE, INSERT, SELECT, LOAD
        //DB OPEN, CLOSE
        //경로 지정 C로
        //회원 테이블이 있고 기록 테이블 있고 기록 테이블에 컬럼을 id나 닉네임으로 회원 구분하기

        //private static string DBpath = "../TapTestData.db";
        private static string DBpath = @"../TapTest.db";
        private static SqliteConnection mConnectDB = new SqliteConnection($"Data Source = {DBpath};");
        private string mQuery;

        public void OpenDB()
        {
            mConnectDB.Open();
        }

        public void InsertTapData(string Daily, string tap)
        {
            mQuery = "INSERT INTO RECORD(date, dayRecord) VALUES(@Daily, @tap)";
            using (SqliteCommand command = new SqliteCommand(mQuery, mConnectDB))
            {
                command.Parameters.AddWithValue("@Daily", Daily);
                command.Parameters.AddWithValue(@"tap", tap);
                command.ExecuteNonQuery();
            }
        }

        public void InsertJoinData(string UserID, string UserPW, string NickName)
        {
            mQuery = "INSERT INTO INFORMATION(ID, PW, NICKNAME) VALUES(@UserID, @UserPW, @NickName)";
            using (SqliteCommand command = new SqliteCommand(mQuery, mConnectDB))
            {
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@UserPW", UserPW);
                command.Parameters.AddWithValue("@NickName", NickName);
                command.ExecuteNonQuery();
            }
        }

        public void CreateTable()
        {
            string[] Query = {
        "CREATE TABLE IF NOT EXISTS RECORD(NICKNAME STRING, date STRING, dayRecord STRING)",
        "CREATE TABLE IF NOT EXISTS INFORMATION(ID STRING, PW STRING, NICKNAME STRING)"
            };

            using (SqliteCommand command = new SqliteCommand(Query.ToString(), mConnectDB))
            {
                foreach (var qry in Query)
                {
                    command.CommandText = qry;
                    command.ExecuteNonQuery();
                }
            }
        }

        public string SelectTapData()
        {
            StringBuilder record = new StringBuilder();
            mQuery = "SELECT dayRecord FROM RECORD";
            using (SqliteCommand command = new SqliteCommand(mQuery, mConnectDB))
            {
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        record.AppendLine($"{reader["dayRecord"]}");
                    }
                }
                return record.ToString();
            }

        }
    }

}
