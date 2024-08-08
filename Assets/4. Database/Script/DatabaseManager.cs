using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace MyProject
{
    public partial class DatabaseManager : MonoBehaviour
    {
        private string serverIP = "127.0.0.1"; //52.79.249.166
        private string dbName = "game";
        private string tableName = "users";
        private string rootPasswd = "1234"; //[SerializeField] 써서 입력받든 / 테스트 시에 활용할 수 있지만 보안에 취약하므로 주의

        private MySqlConnection conn; // mysql DB와 연결상태를 유지하는 객체

        public static DatabaseManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            DBConnect();
        }

        public void DBConnect()
        {
            string config = $"server={serverIP};port=3306;database={dbName};uid=root;pwd={rootPasswd};charset=utf8;";

            conn = new MySqlConnection(config);
            conn.Open();
            print(conn.State);
        }

        //로그인을 하려고 할때, 로그인 쿼리를 날린 즉시 데이터가 오지 않을 수 있으므로,
        //로그인이 완료 되었을 때 호출될 함수를 파라미터로 함께 받아주도록 함.
        public void Login(string email, string passwd, Action<UserData> successCallback, Action failureCallback)
        {
            string pwhash = "";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                foreach (byte b in hashArray)
                {
                    pwhash += $"{b:X2}"; //pwhash += b.ToString("X2");
                }
            }
            
            //SHA256 sha256 = SHA256.Create();
            //byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
            //foreach (byte b in hashArray)
            //{
            //    pwhash += $"{b:X2}"; //pwhash += b.ToString("X2");
            //}
            //sha256.Dispose();

            //print(pwhash);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT * FROM {tableName} WHERE email='{email}' AND pw='{pwhash}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataSet set = new DataSet();

            dataAdapter.Fill(set);
            bool isLoginSuccess = set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0;

            if(isLoginSuccess)
            {
                //로그인 성공 (email과 pw 값이 동시에 일치하는 행이 존재함)
                DataRow row = set.Tables[0].Rows[0];

                print(row["LEVEL"]);
                print(row["level"]);

                UserData data = new UserData(row);
                print(data.email);
                
                successCallback?.Invoke(data);
            }
            else
            {
                //로그인 실패
                failureCallback?.Invoke();
            }

        }

        public void SingnUp(string email, string passwd, string name, Action SuccessCallback) //SuccessCallback -> 가입 성공 팝업
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT uid FROM users WHERE email='{email}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataSet set = new DataSet();

            dataAdapter.Fill(set);
            bool emailPossible = set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                passwd = "";
                foreach (byte b in hashArray)
                {
                    passwd += $"{b:X2}"; 
                }
            }

            if (!emailPossible)
            {
                cmd.CommandText = $"INSERT INTO users (email, pw, name) VALUES ('{email}', '{passwd}', '{name}')";
                cmd.ExecuteNonQuery();
                SuccessCallback?.Invoke();
            }
            else
            {
                print("중복된 이메일입니다.");
            }
        }

        public void InfoChange(UserData data, string passwd, string name, string profile, Action SuccessCallback)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"UPDATE {tableName} SET pw = '{passwd}', name = '{name}', PROFILE_TEXT ='{profile}' WHERE uid ='{data.UID}'";

            int queryCount = cmd.ExecuteNonQuery();
            if (queryCount > 0)
            {
                SuccessCallback?.Invoke();
            }
        }

        public void DeleteId(string email, Action SuccessCallback)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"DELETE FROM {tableName} WHERE email ='{email}'";

            int queryCount = cmd.ExecuteNonQuery();

            if (queryCount > 0)
            {
                SuccessCallback?.Invoke();
            }
        }

        public void FindUser(string email, Action<UserData> SuccessCallback)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT * FROM users WHERE email='{email}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataSet set = new DataSet();

            dataAdapter.Fill(set);
            bool emailPossible = set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0;

            if (emailPossible)
            {
                DataRow row = set.Tables[0].Rows[0];
                UserData data = new UserData(row);

                SuccessCallback?.Invoke(data);
                print("아이디 찾았어요!");
            }
            else
            {
                print("그런 아이디 없는데요");
            }

        }

        public void LevelUp(UserData data, Action SuccessCallback)
        {
            int level = data.level;
            int nextLevel = level + 1;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"UPDATE {tableName} SET level = {nextLevel} WHERE uid ={data.UID}";

            int queryCount = cmd.ExecuteNonQuery();

            if(queryCount > 0)
            {
                //쿼리가 정상적으로 수행됨
                data.level = nextLevel;
                SuccessCallback?.Invoke();
            }
            else
            {
                //쿼리 수행 실패
            }
        }
    }
}
