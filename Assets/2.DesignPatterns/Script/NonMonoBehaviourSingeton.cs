using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour�� ������� ���� �Ϲ����� ��ü�� �̱��� ���� ���� ���
public class NonMonoBehaviourSingeton
{
    // ���� instance �ʵ�� private static �ʵ�� ���� 
    private static NonMonoBehaviourSingeton instance;
    // �ܺ� ��ü�� instance�� �������� ���ؼ��� Getter �޼ҵ� �Ǵ� c#�� Get ���� property�� ���� �б��������� ������ �� �ֵ��� ��.
    public static NonMonoBehaviourSingeton Instance 
    { 
        get 
        {
            if (instance == null)
                instance = new NonMonoBehaviourSingeton();
            return instance;
        } 
    }

    // �ٸ� ��ü���� �����ڸ� ȣ������ ���ϵ��� �⺻ �������� ���� �����ڸ� private���� ��ȣ
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
