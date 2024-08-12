using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class NullableTest : MonoBehaviour
    {
        //nullable �ڷ��� : ������� null���� �� �� ���� Ÿ��(����Ÿ��, enum, struct)�� null���� �Ҵ� �� �� �ֵ��� �ϴ� ���� ���

        int normalInt;  //�ʵ忡�� ������ ���ͷ�Ÿ���� ���, �ʱ�ȭ �Ҵ��� ���� �ʾƵ� �⺻������ �Ҵ��̵ȴ�.
        int? nullableInt;  //nullable ������ ���, ���۷��� Ÿ�԰� ���� �⺻���� null�̴�.

        Vector3 vecter3; //���ͷ�Ÿ���̱� ������, �ʱⰪ �Ҵ��� ���� �ʾƵ� ��

        GameObject obj; //���۷��� Ÿ���̱� ������ �ʱⰪ �Ҵ��� ���� ���� ��� null

        private void Start()
        {
            //print($"normal int : {normalInt}");
            //print($"nullable Int : {nullableInt}");

            
            //vecter3.x = 1f;
            //print(vecter3);

            // .? ?? : null���� �ü� �ִ� ������ ������ null���� �ƴ����� üũ�ϴ� ������
            // [����]?.[��]??[������ null�� ��� ��ȯ�� ��];
            // [�븮��(��������Ʈ)]?.[�Լ�](); : �븮�� �Ǵ� Ŭ������ null�� ��� �Լ��� ȣ������ ����.

            StartPointer sp = new GameObject().AddComponent<StartPointer>();
            //sp.startPoint = Vector3.zero;
            sp.DisplayPoint();
        }
    }
}
