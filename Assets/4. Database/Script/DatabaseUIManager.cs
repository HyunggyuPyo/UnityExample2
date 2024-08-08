using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public partial class DatabaseUIManager : MonoBehaviour
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

        public GameObject correctionPanel;
        public Button correctionButton;
        public Button trueCorrectionButton;
        public InputField correctionPw;
        public InputField correctionName;
        public InputField correctionProfile;

        public Button deleteButton;

        public InputField findInput;
        public Button findButton;

        void Awake()
        {
            loginButton.onClick.AddListener(LoginButtonClick);
            signUpButton.onClick.AddListener(SignUpButtonClick);
            tureSignUpButton.onClick.AddListener(TrueSignUpButtonClick);
            correctionButton.onClick.AddListener(CorrectionButtonClick);
            trueCorrectionButton.onClick.AddListener(TrueCorrectionButtonClick);
            deleteButton.onClick.AddListener(DeleteId);
            findButton.onClick.AddListener(FindUser);
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

        public void CorrectionButtonClick()
        {
            correctionPanel.SetActive(true);
        }

        public void TrueCorrectionButtonClick()
        {
            DatabaseManager.Instance.InfoChange(userData, correctionPw.text, correctionName.text, correctionProfile.text, CorrectioPanelDown);
        }

        public void CorrectioPanelDown()
        {
            userData.name = correctionName.text;
            userData.proFileText = correctionProfile.text;

            correctionPanel.SetActive(false);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"�ȳ��ϼ���, {userData.name}��");
            sb.AppendLine($"�̸��� : {userData.email}");
            sb.AppendLine($"���� : {userData.charClass}");
            sb.AppendLine($"�Ұ��� : {userData.proFileText}");

            infoText.text = sb.ToString();

            levelText.text = $"���� : {userData.level.ToString()}";
            print("�������� �Ϸ�");

        }

        public void DeleteId()
        {
            DatabaseManager.Instance.DeleteId(userData.email, DeleteSuccess);
        }

        public void DeleteSuccess()
        {
            infoPanel.SetActive(false);
            loginPanel.SetActive(true);
        }

        public void FindUser()
        {
            DatabaseManager.Instance.FindUser(findInput.text, FindSuccess);
        }

        private void FindSuccess(UserData data)
        {
            print("ã�� ��");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"�ȳ��ϼ���, {data.name}��");
            sb.AppendLine($"�̸��� : {data.email}");
            sb.AppendLine($"���� : {data.charClass}");
            sb.AppendLine($"�Ұ��� : {data.proFileText}");

            infoText.text = sb.ToString();

            levelText.text = $"���� : {data.level.ToString()}";
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
