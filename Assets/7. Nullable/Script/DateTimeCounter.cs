using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class DateTimeCounter : MonoBehaviour
    {
        //DisplayDateTime() ��Ǯ �� ���õ� �ð��� ����� �ϴµ�,
        //dateTimedp rkqtdl ���� ���� �ʾ��� ��� ���õ��� �ʾҴٴ� ������ print �ϵ��� �ٲ㺸����
        DateTime? dateTime;

        private void Start()
        {
            DisplayDateTime();
        }

        public void DisplayDateTime()
        {
            //if(dateTime.HasValue)
            //{
            //    print($"dateTime : {dateTime}");
            //}
            //else
            //{
            //    Debug.LogError("dateTime���� null�Դϴ�.");
            //}

            print(dateTime?.ToString() ?? "dateTime�� null�Դϴ�.");
        }
    }
}
