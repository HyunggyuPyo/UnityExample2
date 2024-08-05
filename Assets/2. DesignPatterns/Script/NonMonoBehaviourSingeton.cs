using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour를 상속하지 않은 일반적인 객체의 싱글톤 패턴 적용 방법
public class NonMonoBehaviourSingeton
{
    // 실제 instance 필드는 private static 필드로 관리 
    private static NonMonoBehaviourSingeton instance;
    // 외부 객체가 instance에 접근하지 위해서는 Getter 메소드 또는 c#의 Get 전용 property를 통해 읽기전용으로 접근할 수 있도록 함.
    public static NonMonoBehaviourSingeton Instance 
    { 
        get 
        {
            if (instance == null)
                instance = new NonMonoBehaviourSingeton();
            return instance;
        } 
    }

    // 다른 객체에서 생성자를 호출하지 못하도록 기본 생성자의 접근 지정자를 private으로 보호
    private NonMonoBehaviourSingeton()
    {

    }

    public static NonMonoBehaviourSingeton GetInstance()
    {
        if (instance == null)
            instance = new NonMonoBehaviourSingeton();
        return instance;
    }
}
