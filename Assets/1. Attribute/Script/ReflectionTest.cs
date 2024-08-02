using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ReflectionTest : MonoBehaviour
{
    //Reflection 
    //System.Reflection네임스페이스에 포함된 기능
    //컴파일 타임에서 생성된 클래스, 메소드, 멤버변수등의 데이터를 취급하는 class
    //Attribute는 특정 요소에 대한 메타데이터 이므로, Reflection에 의해서 접근이 가능하다.

    private AttributeTest attTest;

    private void Awake()
    {
        attTest = GetComponent<AttributeTest>();
    }

    private void Start()
    {
        //attTest의 Type을 확인
        MonoBehaviour attTestBoxingForm = attTest; // 상위 클래스로 boxing을 해도 원래 객체의 type을 반환

        //Type attTestType = typeof(AttributeTest);
        Type attTestType = attTestBoxingForm.GetType();
        //print(attTestType);

        //Attribute라는 클래스의 데이터를 확인해보자
        BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
        FieldInfo[] fis = attTestType.GetFields(bind); //필드 (멤버변수)
        print(fis.Length);
        foreach (FieldInfo fi in fis)
        {
            if (fi.GetCustomAttribute<MyCustomAttribute>() == null)
                continue; // FieldInfo에 MyCustomAttribute 어트리뷰트가 부착되어 있지 않으면 건너뀜

            //print(fi.Name);
            MyCustomAttribute customAtt = fi.GetCustomAttribute<MyCustomAttribute>();

            print($"Name : {fi.Name}, Type : {fi.FieldType}, AttName : {customAtt.name}, AttValue : {customAtt.value}");

        }
        //TestMethod의 MethodInfo 또는 MemberInfo를 탐색해서 MethodMessageAttribute.msg를 출력해보세요.

        bind = BindingFlags.NonPublic | BindingFlags.Instance;
        //MethodInfo someMi = attTestType.GetMethod("TestMethod"); //sendMseeage랑 비슷한 용법
        MethodInfo[] methodInfo = attTestType.GetMethods(bind); 

        foreach (MethodInfo mi in methodInfo)
        {
            if (mi.GetCustomAttribute<MethodMessageAttribute>() == null)
                continue;

            var msgAtt = mi.GetCustomAttribute<MethodMessageAttribute>();
            print(msgAtt.msg);
            mi.Invoke(attTest, null);
        }

    }
}
