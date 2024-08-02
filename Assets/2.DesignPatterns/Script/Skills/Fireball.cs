using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
	public class Fireball : SkillBehaviour
	{
		public FireballProjectile projectile; //����ü ������
		public float projectileSpeed;

		public override void Apply()
		{
			base.Apply();
		}
		public override void Use()
		{
			Transform shotPoint = context.owner.shotPoint;
			var obj = Instantiate(projectile, shotPoint.position, shotPoint.rotation);
			obj.SetProjectile(projectileSpeed);

			Destroy(obj, 3f);

			base.Use();
		}
		public override void Remove()
		{
			base.Remove();
		}
	}
}
