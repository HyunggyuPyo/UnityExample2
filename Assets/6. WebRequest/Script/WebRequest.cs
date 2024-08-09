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
            // _ : 리턴이 필요한 함수 앞에 사용하면 
        }

        IEnumerator GetWebTexture(string url)
        {
            //http로 웹 요청(Request)를 보낼 객체 생성
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            //비동기식(을 모방한 코루틴)으로 Response를 받을 때 까지 대기
            var operation = www.SendWebRequest();
            yield return operation;
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"HTTP 통신 실패: {www.error}");
            }
            else
            {
                Debug.Log("텍스쳐 다운로드 성공!");
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
