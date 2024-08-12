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

            //// httpClient ��� �Ŀ� �޸� ������ �ʿ�
            //// C++�� ��� ~HttpClient(); ���� ������ �Ҹ��� ȣ��
            //client.Dispose();

            // �Լ� ���� using���� ���� Ư�� ��� �ȿ����� ���ǰ� ��� �ۿ����� �ڵ����� �����Ǵ� IDisposable Ŭ������ �����Ͽ� ���

            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client.GetByteArrayAsync(url);
                //byte[]�� unity���� Ȱ���� �� �ִ� Texture Instance�� ��ȯ
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(response);
                image.sprite = Sprite.Create(texture, new UnityEngine.Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            }                    
        }

    }
}
