using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class StartPointer : MonoBehaviour
    {

        //internal Vector3 startPoint; // ���� vecter3�� reference type�̾��� ���, �ʱⰪ �Ҵ� ���θ� null üũ�� ���� ������.
        //public bool isIntialized;

        //public void SetInitialValue(Vector3 startPoint)
        //{
        //    this.startPoint = startPoint;
        //    isIntialized = true;
        //}

        //public void DisplayPoint()
        //{
        //    if(isIntialized)
        //    {
        //        print(startPoint);
        //    }
        //    else
        //    {
        //        Debug.LogError("StartPoint���� �Ҵ���� �ʾҽ��ϴ�. ���� SetInitialValue �Լ��� ���� �ʱⰪ�� �������ּ���.");
        //    }
        //}


        internal Vector3? startPoint = null; //������ boxing�� ����ȴ�.

        public void DisplayPoint()
        {
            //if(startPoint.HasValue)
            //{
            //    print(startPoint.Value);
            //}
            //else
            //{
            //    Debug.LogError("StartPoint���� �Ҵ���� �ʾҽ��ϴ�. ���� startPoint ���� �������ּ���.");
            //}
            print(startPoint?.ToString() ??"startPoint ���� �Ҵ���� �ʾҽ��ϴ�.");
            
        }
    }
}

