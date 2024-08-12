using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class DateTimeCounter : MonoBehaviour
    {
        //DisplayDateTime() 초풀 시 세팅된 시간을 출력을 하는데,
        //dateTimedp rkqtdl 세팅 되지 않아을 경우 세팅되지 않았다는 문구를 print 하도록 바꿔보세요
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
            //    Debug.LogError("dateTime값이 null입니다.");
            //}

            print(dateTime?.ToString() ?? "dateTime이 null입니다.");
        }
    }
}
