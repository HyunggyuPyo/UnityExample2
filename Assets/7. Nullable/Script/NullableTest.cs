using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class NullableTest : MonoBehaviour
    {
        //nullable 자료형 : 원래라면 null값이 올 수 없는 타입(리털타입, enum, struct)에 null값을 할당 할 수 있도록 하는 선언 방식

        int normalInt;  //필드에서 선언한 리터럴타입의 경우, 초기화 할당을 하지 않아도 기본값으로 할당이된다.
        int? nullableInt;  //nullable 변수의 경우, 레퍼런스 타입과 같이 기본값이 null이다.

        Vector3 vecter3; //리터럴타입이기 때문에, 초기값 할당을 하지 않아도 됨

        GameObject obj; //레퍼런스 타입이기 떄문에 초기값 할당을 하지 않을 경우 null

        private void Start()
        {
            //print($"normal int : {normalInt}");
            //print($"nullable Int : {nullableInt}");

            
            //vecter3.x = 1f;
            //print(vecter3);

            // .? ?? : null값이 올수 있는 변수의 내용이 null인지 아닌지를 체크하는 연산자
            // [변수]?.[값]??[변수가 null일 경우 반환할 값];
            // [대리자(델리게이트)]?.[함수](); : 대리자 또는 클래스가 null일 경우 함수를 호출하지 않음.

            StartPointer sp = new GameObject().AddComponent<StartPointer>();
            //sp.startPoint = Vector3.zero;
            sp.DisplayPoint();
        }
    }
}
