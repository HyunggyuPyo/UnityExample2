using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTest : MonoBehaviour
{
    //Attribute (�Ӽ�, Ư��)
    //c#������ Attribute�� ��Ȯ�� �ǹ�: �ʵ�, �޼ҵ�� ����� ���� ��Ÿ�����͸� ������ �� �ִ� [Ŭ����]�̴�.
    //Attribute�� ���� �ۼ��ϱ� ���ؼ��� System.Attribute Ŭ������ ����ϰ�, Ŭ������ �ڿ� Attribute�� ���δ�.
    //Attribute Ŭ������ Ȱ���ϱ� ���ؼ��� Ư�� ���(Ŭ����, ����, �Լ�(propertie ����)���� ���� �տ� [Attribute �̸����� Attribute�� �� �̸�]

    private static int myStaticInt;

    [MyCustom(name = "MyIntager", value = 1)] //[MyCustomAttribute]�� ����
    public int myInt; //��� ����

    [MyCustom] // MyCustomAttribute�� �⺻ �����ڸ� ȣ���Ͽ� Attribute(��Ÿ������) ����
    public int myInt2;

    public string myString; // TextArea Attribute�� �������� ���� string �������

    [TextArea(minLines: 1, maxLines: 10)]
    public string myTextArea; // TextArea Attribute�� ������ string �������

    [Space(300)]
    public int anotherInt;

    [MethodMessage("�̰� private �޼ҵ��Դϴ�.")]
    void TestMethod()
    {
        print("��н����� Test��.");
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

[AttributeUsage(AttributeTargets.Method)] //Attribute�� ���������� ������ �� Ŭ���� �տ� ������ Attribute
public class MethodMessageAttribute : Attribute //�޼ҵ忡 ���� Attribute
{
    public string msg;

    public MethodMessageAttribute(string msg)
    {

    }
}