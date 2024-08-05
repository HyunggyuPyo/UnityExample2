using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MyProject.Skill;
using MyProject.State;

namespace MyProject
{
    //상태 패턴 구현을 위해, 현재 움직이는 중인지 아니면 멈춰있는지 판단을 하고 싶음.
    public class Player : MonoBehaviour
    {
        public enum State
        {
            Idle = 0,
            Move = 1
        }

        private CharacterController cc;
        public float moveSpeed = 5;
        public TextMeshPro text;
        public State currentState; // 0이면 멈춰있는 상태, 1이면 움직이는 상태 / 추후 화장이 되면 2 공격 이런식으로..
        public float stateStay; //현재 상태에 머문 시간
        public float moveDistnace; //총 이동시간

        public Transform shotPoint;//스킬 시저시 투사체 생성될 자리
        private SkillContext skillContext;
        private StateMachine stateMachine;

        void Awake()
        {
            cc = GetComponent<CharacterController>();
            skillContext = GetComponentInChildren<SkillContext>();
            SkillBehaviour[] skills = skillContext.GetComponentsInChildren<SkillBehaviour>();
            stateMachine = GetComponentInChildren<StateMachine>();

            foreach (SkillBehaviour sk in skills)
            {
                skillContext.AddSkill(sk);
            }
            skillContext.SetCurrentSkill(0);
        }

        void Start()
        {
            //currentState = State.Idle;
        }


        void Update()
        {
            Move();
            //StateUpdate();
            if (Input.GetButtonDown("Fire1"))
                skillContext.UseSkill();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                skillContext.SetCurrentSkill(0);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                skillContext.SetCurrentSkill(1);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                skillContext.SetCurrentSkill(2);
        }

        public void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(x, 0, y);

            cc.Move(moveDir * moveSpeed * Time.deltaTime);

            ///*mabnitude : 길이*/
            //if (moveDir.magnitude < 0.1f) //상태전이를 결정하는 조건 (condition)
            //    ChangeState(State.Idle);
            //else
            //    ChangeState(State.Move);

            if (moveDir.magnitude < 0.1f)
                stateMachine.Transition(stateMachine.idleState);
            else
                stateMachine.Transition(stateMachine.moveState);
        }

        //상태 전이
        public void ChangeState/*Transition*/(State nextState)
        {
            if (currentState != nextState)
            {
                //exit
                switch (currentState)
                {
                    case State.Idle:
                        print("대기 상태 종료");
                        break;
                    case State.Move:
                        print("이동 상태 종료");
                        break;
                    default:
                        break;
                }

                //enter
                switch (nextState)
                {
                    case State.Idle:
                        print("대기 상태 시작");
                        break;
                    case State.Move:
                        print("이동 상태 시작");
                        break;
                    default:
                        break;
                }

                currentState = nextState;
                stateStay = 0;
            }
        }

        public void StateUpdate()
        {
            // 현재 상태에 따른 행동 정의
            switch (currentState)
            {
                case State.Idle:
                    text.text = $"{State.Idle} state : {stateStay.ToString("n0")}";
                    break;
                case State.Move:
                    text.text = $"{State.Move} state : {stateStay.ToString("n0")}";
                    break;
            }
            stateStay += Time.deltaTime;
        }
    }
}