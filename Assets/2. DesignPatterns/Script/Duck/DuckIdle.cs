using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    public class DuckIdle : DuckBase
    {
        public override void Enter()
        {
            Debug.Log("���� ��� ����");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("���� ��� ����");
        }
    }
}
