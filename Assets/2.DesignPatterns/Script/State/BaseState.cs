using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.State
{
    public class BaseState : IState
    {
        public Player player;

        public void Initialize(Player player)
        {
            this.player = player;
        }

        public virtual void Enter()
        {
           
        }

        public virtual void Exit()
        {

        }

        public virtual void Update()
        {
            
        }
    }
}