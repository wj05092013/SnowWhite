using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCharacter : MonoBehaviour {

	public GameObject ParticleOnHit;

	public int AttackPower;			// 공격력
	public int ForceMagnitude;		// 밀어내는 힘의 크기


	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (FindObjectOfType<LevelManager>().IsPlayerDead)
				return;
			
			// 플레이어가 무적 상태(최근에 피격된 경우)이면 피격 처리를 하지 않는다.
			if (other.gameObject.GetComponent<CharacterStatus>().IsUnbeatable)
				return;

			// 플레이어를 단시간 무적 상태로 변경
			other.gameObject.GetComponent<CharacterStatus>().IsUnbeatable = true;


			Vector2 forceDir;

			// Player가 HitPlayer를 컴포넌트로 가지는 오브젝트보다 오른쪽에 있으면 오른쪽으로 힘을 가한다.
			if (other.gameObject.transform.position.x >= transform.position.x)
				forceDir = new Vector2(0.8f, 0.6f);			// (4/5)^2 + (3/5)^2 = 1 (정규화된 방향 벡터)
			else
				forceDir = new Vector2(-0.8f, 0.6f);

			other.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * ForceMagnitude, ForceMode2D.Impulse);

			// HitParticle 오브젝트 생성
			GameObject particle = Instantiate(ParticleOnHit, other.transform.position, other.transform.rotation);
			Object.Destroy(particle, 2.0f);

			// 현재 몬스터의 공격력 전달
			other.gameObject.GetComponent<CharacterStatus>().LoseHp(AttackPower);
		}
		else if(other.gameObject.layer == LayerMask.NameToLayer("AICharacter"))
		{
			if (other.gameObject.GetComponent<CharacterStatus>().IsUnbeatable)
				return;

			other.gameObject.GetComponent<CharacterStatus>().IsUnbeatable = true;


			Vector2 forceDir;

			if (other.gameObject.transform.position.x >= transform.position.x)
				forceDir = new Vector2(0.8f, 0.6f);
			else
				forceDir = new Vector2(-0.8f, 0.6f);

			other.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * ForceMagnitude, ForceMode2D.Impulse);

			GameObject particle = Instantiate(ParticleOnHit, other.transform.position, other.transform.rotation);
			Object.Destroy(particle, 3.0f);
		}
	}
}
