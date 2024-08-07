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
    public class DatabaseManager : MonoBehaviour
    {
        private string serverIP = "127.0.0.1";
        private string dbName = "game";
        private string tableName = "users";
        private string rootPasswd = "1234"; //[SerializeField] �Ἥ �Է¹޵� / �׽�Ʈ �ÿ� Ȱ���� �� ������ ���ȿ� ����ϹǷ� ����

        private MySqlConnection conn; // mysql DB�� ������¸� �����ϴ� ��ü

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

        //�α����� �Ϸ��� �Ҷ�, �α��� ������ ���� ��� �����Ͱ� ���� ���� �� �����Ƿ�,
        //�α����� �Ϸ� �Ǿ��� �� ȣ��� �Լ��� �Ķ���ͷ� �Բ� �޾��ֵ��� ��.
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

            print(pwhash);


            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT * FROM {tableName} WHERE email='{email}' AND pw='{passwd}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataSet set = new DataSet();

            dataAdapter.Fill(set);
            bool isLoginSuccess = set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0;

            if(isLoginSuccess)
            {
                //�α��� ���� (email�� pw ���� ���ÿ� ��ġ�ϴ� ���� ������)
                DataRow row = set.Tables[0].Rows[0];

                print(row["LEVEL"]);
                print(row["level"]);

                UserData data = new UserData(row);
                print(data.email);
                
                successCallback?.Invoke(data);
            }
            else
            {
                //�α��� ����
                failureCallback?.Invoke();
            }

        }

        public void SingnUp(string email, string passwd, string name, Action SuccessCallback) //SuccessCallback -> ���� ���� �˾�
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT uid FROM users WHERE email='{email}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataSet set = new DataSet();

            dataAdapter.Fill(set);
            bool emailPossible = set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0;

            if(!emailPossible)
            {
                cmd.CommandText = $"INSERT INTO users (email, pw, name) VALUES ('{email}', '{passwd}', '{name}')";
                cmd.ExecuteNonQuery();
                SuccessCallback?.Invoke();
            }
            else
            {
                print("�ߺ��� �̸����Դϴ�.");
            }
        }

        public void InfoChange(UserData data, string passwd, string name, string profile, Action SuccessCallback)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"UPDATE {tableName} SET pw = {passwd}, name = {name}, PROFILE_TEXT ={profile} WHERE uid ={data.UID}";

            int queryCount = cmd.ExecuteNonQuery();
            if (queryCount > 0)
            {
                SuccessCallback?.Invoke();
            }
        }

        public void DeleteId()
        {

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
                //������ ���������� �����
                data.level = nextLevel;
                SuccessCallback?.Invoke();
            }
            else
            {
                //���� ���� ����
            }
        }
    }
}
