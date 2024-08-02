using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.State
{
    public class MoveState : BaseState
    {
        public override void Enter()
        {
           
        }

        public override void Exit()
        {
            Debug.Log("이동 상태 종료");
        }
        public override void Update()
        {
            player.text.text = $"{GetType().Name} : {player.moveDistnace:n1}";
            player.moveDistnace += Time.deltaTime;
        }
    }
}