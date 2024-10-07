using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private static string DBpath = @"../../TapTest.db";
        private static SqliteConnection mConnectDB = new SqliteConnection($"Data Source = {DBpath};");
        private string mQuery;
        private bool HasDB = false;

        public void OpenDB()
        {
            mConnectDB.Open();
        }

        public void InsertTapData(string NickName, int Record, string Time)
        {
            mQuery = "INSERT INTO RECORD(NickName, Time, Record) VALUES(@NickName, @Time, @Record)";
            using (SqliteCommand command = new SqliteCommand(mQuery, mConnectDB))
            {
                command.Parameters.AddWithValue("@NickName", NickName);
                command.Parameters.AddWithValue("@Time", Time);
                command.Parameters.AddWithValue(@"Record", Record);
                command.ExecuteNonQuery();
            }
        }

        public void InsertJoinData(string UserID, string UserPW, string NickName)
        {
            mQuery = "INSERT INTO USERINFORMATION(ID, PW, NICKNAME) VALUES(@UserID, @UserPW, @NickName)";
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
            if(!HasDB)
            {
                string[] Query =
                {
                    "CREATE TABLE IF NOT EXISTS RECORD(NICKNAME STRING, Time STRING, Record INT)",
                    "CREATE TABLE IF NOT EXISTS USERINFORMATION(ID STRING, PW STRING, NICKNAME STRING)",
                };
                foreach(var query in Query)
                {
                    using(SqliteCommand command = new SqliteCommand(query, mConnectDB))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                HasDB = true;
                //using (SqliteCommand command = new SqliteCommand(Query.ToString(), mConnectDB))
                //{
                //    foreach (var qry in Query)
                //    {
                //        command.CommandText = qry;
                //        command.ExecuteNonQuery();
                //    }
                //    HasDB = true;
                //}
            }
        }

        //public string SelectTapData()
        //{
        //    StringBuilder record = new StringBuilder();
        //    mQuery = "SELECT * FROM RECORD";
        //    using (SqliteCommand command = new SqliteCommand(mQuery, mConnectDB))
        //    {
        //        using (SqliteDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                record.AppendLine($"{reader["dayRecord"]}");
        //            }
        //        }
        //        return record.ToString();
        //    }

        //}
        public List<Result> SelectTapData()
        {
            List<Result> results = new List<Result> ();
            string Query = "SELECT * FROM RECORD";
            using(SqliteCommand command = new SqliteCommand(Query, mConnectDB))
            {
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string NickName = reader["NickName"].ToString();
                        string Time = reader["Time"].ToString();
                        int Record = Convert.ToInt32(reader["Record"]);

                        results.Add(new Result(NickName, Time, Record));
                    }

                }
            }
            return results;
        }
        public string SelectID(string ID)
        {
            StringBuilder record = new StringBuilder();
            mQuery = "SELECT ID FROM USERINFORMATION WHERE ID = @ID";
            using (SqliteCommand command = new SqliteCommand(mQuery, mConnectDB))
            {
                command.Parameters.AddWithValue("@ID", ID);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        record.AppendLine($"{reader["ID"]}");
                    }
                }
                return record.ToString();
            }
        }

        public string SelectLogin(string ID, string PW)
        {
            StringBuilder record = new StringBuilder();
            mQuery = "SELECT NickName FROM USERINFORMATION WHERE ID = @ID AND PW = @PW";
            using (SqliteCommand command = new SqliteCommand(mQuery, mConnectDB))
            {
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@PW", PW);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        record.AppendLine($"{reader["NickName"]}");
                    }
                }
                return record.ToString();
            }
        }
    }
}
