using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class MultipleRequestByDotnet : MonoBehaviour
    {
        private string url = "https://picsum.photos/200";

        public List<Image> images;

        private void Start()
        {
            foreach(var image in images)
            {
                DownloadImage(image);
            }

            images.ForEach(DownloadImage);
            print($"다운로드 요청(Request) 완료");
        }

        async void DownloadImage(Image targetImage)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client.GetByteArrayAsync(url);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(response);
                targetImage.sprite = Sprite.Create(texture, new UnityEngine.Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            }
        }
    }
}
