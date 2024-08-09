using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace MyProject
{
    public class WebRequest : MonoBehaviour
    {
        public string imageURL;
        public RawImage rawImage;
        public Image image;

        private void Start()
        {
            _= StartCoroutine(GetWebTexture(imageURL));
            // _ : ������ �ʿ��� �Լ� �տ� ����ϸ� 
        }

        IEnumerator GetWebTexture(string url)
        {
            //http�� �� ��û(Request)�� ���� ��ü ����
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            //�񵿱��(�� ����� �ڷ�ƾ)���� Response�� ���� �� ���� ���
            var operation = www.SendWebRequest();
            yield return operation;
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"HTTP ��� ����: {www.error}");
            }
            else
            {
                Debug.Log("�ؽ��� �ٿ�ε� ����!");
                //rawImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                Sprite sprite = Sprite.Create
                    (
                    (Texture2D)texture,
                    new UnityEngine.Rect(0, 0, texture.width, texture.height),
                    new Vector2(.5f, .5f)
                    );

                image.sprite = sprite;
                image.SetNativeSize();
            }

            
        }
    }
}
