using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class WebRequestByDotnet : MonoBehaviour
    {
        public string url;

        public Image image;

        async void Start()
        {
            //HttpClient client = new HttpClient();

            //// httpClient 사용 후에 메모리 해제가 필요
            //// C++의 경우 ~HttpClient(); 같은 식으로 소멸자 호출
            //client.Dispose();

            // 함수 내의 using문을 통해 특정 블록 안에서만 사용되고 블록 밖에서는 자동으로 해제되는 IDisposable 클래스를 선언하여 사용

            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client.GetByteArrayAsync(url);
                //byte[]을 unity에서 활용할 수 있는 Texture Instance로 변환
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(response);
                image.sprite = Sprite.Create(texture, new UnityEngine.Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            }                    
        }

    }
}
