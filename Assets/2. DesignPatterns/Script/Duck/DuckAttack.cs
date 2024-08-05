using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    public class DuckAttack : DuckBase
    {
        public override void Enter()
        {
            Debug.Log("���� ���� ����");
            duck.ani.SetBool("Attack", true);
            duck.attack = true;
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("���� ���� ����");
            duck.ani.SetBool("Attack", false);
            duck.attack = false;
        }

    }
}
