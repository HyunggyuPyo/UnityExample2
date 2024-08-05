using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.DuckState
{
    [RequireComponent(typeof(Duck))]
    public class DuckMachine : MonoBehaviour
    {
        public DuckIdle duckIdle;
        public DuckChase duckChase;
        public DuckAttack duckAttack;
        public Duck duck;

        public DuckBase currentState;

        void Start()
        {
            duckIdle = new DuckIdle();
            duckChase = new DuckChase();
            duckAttack = new DuckAttack();

            duckIdle.Initialize(duck);
            duckChase.Initialize(duck);
            duckAttack.Initialize(duck);
            currentState = duckIdle;
        }

        public void Transition(DuckBase state)
        {
            if (currentState == state)
                return;
            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }

        public void Update()
        {
            currentState.Update();
        }

        private void Reset()
        {
            duck = GetComponent<Duck>();
        }
    }
}
