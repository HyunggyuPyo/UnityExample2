using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
	public class ArcaneSphere : SkillBehaviour
	{
		public GameObject projectilePrefab;
		private Transform spinner;

		public int projectileCount;

		public override void Apply()
		{
			//양 옆으로 구체 2개 생성
			Instantiate(projectilePrefab, spinner);
			Instantiate(projectilePrefab, spinner);

			spinner.GetChild(0).localPosition = Vector3.right;
			spinner.GetChild(1).localPosition = Vector3.left;

			print("비전 구체 시전");
		}
		public override void Use()
		{
			print("비전 구체 스킬은 패시브입니다.");
		}

		public override void Remove()
		{
            foreach (Transform proj in spinner)
            {
				Destroy(proj.gameObject);
            }
			print("비전 구체 시전 취소");
		}

        void Awake()
        {
			if(transform.childCount > 0)
            {
				spinner = transform.GetChild(0);
            }
            else
            {
				spinner = new GameObject("ArcaneSpinner").transform;
				spinner.parent = transform;
				spinner.localPosition = Vector3.up;
            }
        }

        void Update()
        {
			spinner.Rotate(Vector3.up * 180 * Time.deltaTime);
        }
    }
}
