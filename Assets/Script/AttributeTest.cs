using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTest : MonoBehaviour
{
    //Attribute (속성, 특성)
    //c#에서의 Attribute의 정확한 의미: 필드, 메소드등 멤버에 대한 메타데이터를 생성할 수 있는 [클래스]이다.
    //Attribute를 직접 작성하기 위해서는 System.Attribute 클래스를 상속하고, 클래스명 뒤에 Attribute를 붙인다.
    //Attribute 클래스를 활용하기 위해서는 특정 멤버(클래스, 변수, 함수(propertie 포함)등의 선언 앞에 [Attribute 이름에서 Attribute를 뺀 이름]

    private static int myStaticInt;

    [MyCustom(name = "MyIntager", value = 1)] //[MyCustomAttribute]와 동일
    public int myInt; //멤버 변수

    [MyCustom] // MyCustomAttribute의 기본 생성자를 호출하여 Attribute(메타데이터) 생성
    public int myInt2;

    public string myString; // TextArea Attribute가 부착되지 않은 string 멤버변수

    [TextArea(minLines: 1, maxLines: 10)]
    public string myTextArea; // TextArea Attribute가 부착된 string 멤버변수

    [Space(300)]
    public int anotherInt;

    [MethodMessage("이건 private 메소드입니다.")]
    void TestMethod()
    {
        print("비밀스러운 Test중.");
    }
}

public class MyCustomAttribute : Attribute
{
    public string name;
    public float value;

    public MyCustomAttribute()
    {
        name = "No name";
        value = -1;
    }
}

[AttributeUsage(AttributeTargets.Method)] //Attribute에 제약조건을 설정할 때 클래스 앞에 부착할 Attribute
public class MethodMessageAttribute : Attribute //메소드에 붙일 Attribute
{
    public string msg;

    public MethodMessageAttribute(string msg)
    {

    }
}
