using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MyProject.Skill;
using MyProject.State;

namespace MyProject
{
    //���� ���� ������ ����, ���� �����̴� ������ �ƴϸ� �����ִ��� �Ǵ��� �ϰ� ����.
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
        public State currentState; // 0�̸� �����ִ� ����, 1�̸� �����̴� ���� / ���� ȭ���� �Ǹ� 2 ���� �̷�������..
        public float stateStay; //���� ���¿� �ӹ� �ð�
        public float moveDistnace; //�� �̵��ð�

        public Transform shotPoint;//��ų ������ ����ü ������ �ڸ�
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

            ///*mabnitude : ����*/
            //if (moveDir.magnitude < 0.1f) //�������̸� �����ϴ� ���� (condition)
            //    ChangeState(State.Idle);
            //else
            //    ChangeState(State.Move);

            if (moveDir.magnitude < 0.1f)
                stateMachine.Transition(stateMachine.idleState);
            else
                stateMachine.Transition(stateMachine.moveState);
        }

        //���� ����
        public void ChangeState/*Transition*/(State nextState)
        {
            if (currentState != nextState)
            {
                //exit
                switch (currentState)
                {
                    case State.Idle:
                        print("��� ���� ����");
                        break;
                    case State.Move:
                        print("�̵� ���� ����");
                        break;
                    default:
                        break;
                }

                //enter
                switch (nextState)
                {
                    case State.Idle:
                        print("��� ���� ����");
                        break;
                    case State.Move:
                        print("�̵� ���� ����");
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
            // ���� ���¿� ���� �ൿ ����
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