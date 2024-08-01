using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ReflectionTest : MonoBehaviour
{
    //Reflection 
    //System.Reflection���ӽ����̽��� ���Ե� ���
    //������ Ÿ�ӿ��� ������ Ŭ����, �޼ҵ�, ����������� �����͸� ����ϴ� class
    //Attribute�� Ư�� ��ҿ� ���� ��Ÿ������ �̹Ƿ�, Reflection�� ���ؼ� ������ �����ϴ�.

    private AttributeTest attTest;

    private void Awake()
    {
        attTest = GetComponent<AttributeTest>();
    }

    private void Start()
    {
        //attTest�� Type�� Ȯ��
        MonoBehaviour attTestBoxingForm = attTest; // ���� Ŭ������ boxing�� �ص� ���� ��ü�� type�� ��ȯ

        //Type attTestType = typeof(AttributeTest);
        Type attTestType = attTestBoxingForm.GetType();
        //print(attTestType);

        //Attribute��� Ŭ������ �����͸� Ȯ���غ���
        BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
        FieldInfo[] fis = attTestType.GetFields(bind); //�ʵ� (�������)
        print(fis.Length);
        foreach (FieldInfo fi in fis)
        {
            if (fi.GetCustomAttribute<MyCustomAttribute>() == null)
                continue; // FieldInfo�� MyCustomAttribute ��Ʈ����Ʈ�� �����Ǿ� ���� ������ �ǳʲ�

            //print(fi.Name);
            MyCustomAttribute customAtt = fi.GetCustomAttribute<MyCustomAttribute>();

            print($"Name : {fi.Name}, Type : {fi.FieldType}, AttName : {customAtt.name}, AttValue : {customAtt.value}");

        }
        //TestMethod�� MethodInfo �Ǵ� MemberInfo�� Ž���ؼ� MethodMessageAttribute.msg�� ����غ�����.

        bind = BindingFlags.NonPublic | BindingFlags.Instance;
        //MethodInfo someMi = attTestType.GetMethod("TestMethod"); //sendMseeage�� ����� ���
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
