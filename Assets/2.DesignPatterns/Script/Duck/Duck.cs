using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.DuckState;

namespace MyProject
{
    public class Duck : MonoBehaviour
    {
        DuckMachine machine;

        Rigidbody rb;
        [HideInInspector]
        public Animator ani;
        Transform target;
        float moveSpeed = 1f;
        public bool attack = false;

        void Awake()
        {
            machine = GetComponentInChildren<DuckMachine>();
            rb = GetComponent<Rigidbody>();
            ani = GetComponentInChildren<Animator>();

            InvokeRepeating("UpdateTarget", 0f, 0.3f);
        }

        void Start()
        {
            GameManager.Instance.onDayNightChange += NightBuff;
            NightBuff(GameManager.Instance.isDay);
        }

        void UpdateTarget()
        {
            int playerLayerMask = LayerMask.GetMask("Player");

            Collider[] cols = Physics.OverlapSphere(transform.position, 6f, playerLayerMask);
            Collider[] atkCols = Physics.OverlapSphere(transform.position, 1f, playerLayerMask);

            if (cols.Length > 0)
            {
                if (atkCols.Length > 0)
                {
                    machine.Transition(machine.duckAttack);
                }
                else
                {
                    target = cols[0].gameObject.transform;
                    machine.Transition(machine.duckChase);
                }
            }
            else
            {
                target = null;
                machine.Transition(machine.duckIdle);
            }
                
        }

        void Update()
        {
            if(!attack && target != null)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                rb.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }

        void NightBuff(bool isDay)
        {
            if (isDay)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(2, 2, 2);
        }
    }
}
