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
            print("ȸ������ ����");
        }

        public void OnLevelButtonClick()
        {
            DatabaseManager.Instance.LevelUp(userData, OnLevelSuccess);
        }

        private void OnLevelSuccess()
        {
            levelText.text = $"���� : {userData.level}";
        }

        private void OnLoginSuccess(UserData data)
        {
            print("�α��� ����");
            userData = data;

            loginPanel.SetActive(false);
            infoPanel.SetActive(true);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"�ȳ��ϼ���, {data.name}��");
            sb.AppendLine($"�̸��� : {data.email}");
            sb.AppendLine($"���� : {data.charClass}");
            sb.AppendLine($"�Ұ��� : {data.proFileText}");

            infoText.text = sb.ToString();

            levelText.text = $"���� : {data.level.ToString()}";
        }


        private void OnLoginFailure()
        {
            print("�α��� ����");
        }
    }
}
