using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSkill : ShootingSkill
{
	public GameObject HitMonsterPrefab;
	public float LastingTime;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			SoundManager.instance.PlaySkill1Sound();

			GameObject inst = Instantiate(HitMonsterPrefab, other.gameObject.transform.position + Vector3.up, Quaternion.identity);

			Destroy(inst, LastingTime);
		}
	}
}
