using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    public class DuckAttack : DuckBase
    {
        public override void Enter()
        {
            Debug.Log("오리 공격 시작");
            duck.ani.SetBool("Attack", true);
            duck.attack = true;
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("오리 공격 종료");
            duck.ani.SetBool("Attack", false);
            duck.attack = false;
        }

    }
}
