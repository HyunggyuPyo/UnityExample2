using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    public class DuckChase : DuckBase
    {

        public override void Enter()
        {
            Debug.Log("���� ���� ����");
            duck.ani.SetBool("Chase", true);
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("���� ���� ����");
            duck.ani.SetBool("Chase", false);
        }
    }
}
