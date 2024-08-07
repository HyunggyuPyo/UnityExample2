using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class DatabaseUIManager : MonoBehaviour
    {
        public GameObject loginPanel;
        public GameObject infoPanel;
        public GameObject signUPPanel;

        public InputField emailInput;
        public InputField pwInput;
        public InputField signUpEmailInput;
        public InputField signUpPwInput;
        public InputField signUpnameInput;

        public Button signUpButton;
        public Button loginButton;
        public Button tureSignUpButton;

        public Text infoText;
        public Text levelText;

        private UserData userData;

        void Awake()
        {
            loginButton.onClick.AddListener(LoginButtonClick);
            signUpButton.onClick.AddListener(SignUpButtonClick);
            tureSignUpButton.onClick.AddListener(TrueSignUpButtonClick);
        }

        public void LoginButtonClick()
        {
            DatabaseManager.Instance.Login(emailInput.text, pwInput.text, OnLoginSuccess, OnLoginFailure);
        }

        public void SignUpButtonClick()
        {
            signUPPanel.SetActive(true);
        }

        public void TrueSignUpButtonClick()
        {
            DatabaseManager.Instance.SingnUp(signUpEmailInput.text, signUpPwInput.text, signUpnameInput.text, SignPanelDown);
        }

        public void SignPanelDown()
        {
            signUPPanel.SetActive(false);
            print("회원가입 성공");
        }

        public void OnLevelButtonClick()
        {
            DatabaseManager.Instance.LevelUp(userData, OnLevelSuccess);
        }

        private void OnLevelSuccess()
        {
            levelText.text = $"레벨 : {userData.level}";
        }

        private void OnLoginSuccess(UserData data)
        {
            print("로그인 성공");
            userData = data;

            loginPanel.SetActive(false);
            infoPanel.SetActive(true);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"안녕하세요, {data.name}님");
            sb.AppendLine($"이메일 : {data.email}");
            sb.AppendLine($"직업 : {data.charClass}");
            sb.AppendLine($"소개글 : {data.proFileText}");

            infoText.text = sb.ToString();

            levelText.text = $"레벨 : {data.level.ToString()}";
        }


        private void OnLoginFailure()
        {
            print("로그인 실패");
        }
    }
}
