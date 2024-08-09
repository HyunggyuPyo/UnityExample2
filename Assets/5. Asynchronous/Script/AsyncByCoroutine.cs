using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class AsyncByCoroutine : MonoBehaviour
    {
        //�ܹ��Ÿ� ����� �ʹ�

        int bread = 0; // ������
        int patty = 0; // ��Ƽ ����
        int pickle = 0; // ��Ŭ ����
        int lettuce = 0; // �����

        public Text text;

        FoodMakerThread breadMaker = new FoodMakerThread();
        FoodMakerThread pattyMaker = new FoodMakerThread();
        FoodMakerThread pickleMaker = new FoodMakerThread();
        FoodMakerThread lettuceMaker = new FoodMakerThread();

        void Start()
        {
            breadMaker.StartCook();
            pattyMaker.StartCook();
            pickleMaker.StartCook();
            lettuceMaker.StartCook();

            StartCoroutine(CheckHamberger());
        }

        void Update()
        {
            bread = breadMaker.amount;
            patty = pickleMaker.amount;
            pickle = pickleMaker.amount;
            lettuce = lettuceMaker.amount;

            text.text = $"�� ���� : {bread}, ��Ƽ ���� : {patty}, ��Ŭ ���� : {pickle}, ����� ���� : {lettuce}";
        }

        IEnumerator CheckHamberger()
        {
            yield return new WaitUntil(HambergerReady);//������ �����Ǳ� ������ �ƹ��͵� �� �ϵ��� null return 

            MakeHamberger();
        }

        bool HambergerReady()
        {
            return bread >= 2 && patty >= 2 && pickle >= 8 && lettuce >= 4;
        }

        void MakeHamberger()
        {
            bread -= 2;
            patty -= 2;
            pickle -= 8;
            lettuce -= 4;

            print($"�ܹ��Ű� ����� �����ϴ�. �ҿ�ð� : {Time.time}");
        }
    }


    public class FoodMakerThread
    {
        public int amount; //����� ��

        private System.Random rand = new System.Random();

        public void StartCook()
        {
            Thread cookThread = new Thread(Cook);
            cookThread.Start();
        }

        private void Cook()
        {
            while(true)
            {
                int time = rand.Next(1000, 3000);
                Thread.Sleep(time);
                amount++;
            }
        }
    }
}