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
			//�� ������ ��ü 2�� ����
			Instantiate(projectilePrefab, spinner);
			Instantiate(projectilePrefab, spinner);

			spinner.GetChild(0).localPosition = Vector3.right;
			spinner.GetChild(1).localPosition = Vector3.left;

			print("���� ��ü ����");
		}
		public override void Use()
		{
			print("���� ��ü ��ų�� �нú��Դϴ�.");
		}

		public override void Remove()
		{
            foreach (Transform proj in spinner)
            {
				Destroy(proj.gameObject);
            }
			print("���� ��ü ���� ���");
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
