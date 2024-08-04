using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    public class DuckChase : DuckBase
    {

        public override void Enter()
        {
            Debug.Log("오리 추적 시작");
            duck.ani.SetBool("Chase", true);
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("오리 추적 종료");
            duck.ani.SetBool("Chase", false);
        }
    }
}
