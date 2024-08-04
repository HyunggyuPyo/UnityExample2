using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    public class DuckBase : IDuck
    {
        public Duck duck;

        public void Initialize(Duck duck)
        {
            this.duck = duck;
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