using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
	public class FireballProjectile : SkillBehaviour
	{
		private Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void SetProjectile(float speed)
        {
            rb.velocity = Vector3.forward * speed;
        }
    }
}
