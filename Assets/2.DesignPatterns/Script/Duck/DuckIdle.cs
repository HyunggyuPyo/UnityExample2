using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    public class DuckIdle : DuckBase
    {
        public override void Enter()
        {
            Debug.Log("오리 대기 시작");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("오리 대기 종료");
        }
    }
}
