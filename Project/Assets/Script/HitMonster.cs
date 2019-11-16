using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitMonster : MonoBehaviour {

	public enum CharacterObjectType
	{
		Player,
		AI
	}
	public CharacterObjectType CharacterType;
	private GameObject _character;

	public enum HitObjType
	{
		Respawned,
		Weapon
	}
	public HitObjType Type;

	private GameObject _monster;
	private float _hitForceMagnitude = 8.0f;

	public GameObject ParticleOnHit;

	// Respawned 타입의 경우
	public bool DestroyOnHit;
	private bool _alreadyHit = false;

	// Weapon 타입의 경우
	public bool WeaponEnabled = true;
	public float WeaponActionTime;


	// Use this for initialization
	void Start ()
	{
		if (CharacterType == CharacterObjectType.Player)
			_character = FindObjectOfType<PlayerController>().gameObject;
		else if (CharacterType == CharacterObjectType.AI)
			_character = FindObjectOfType<AICharacterMovement>().gameObject;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_alreadyHit)
			return;

		if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			if (Type == HitObjType.Weapon)
				return;

			_alreadyHit = true;

			HitTargetMonster(other);

			if(Type == HitObjType.Respawned && DestroyOnHit)
				Destroy(gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			if (Type != HitObjType.Weapon)
				return;

			if (!WeaponEnabled)
				return;

			HitTargetMonster(other);

			gameObject.GetComponentInParent<AICharacterMovement>().SetAttack(true);
			WeaponEnabled = false;

			Invoke("SetWeaponEnabled", WeaponActionTime);
		}
	}
	private void SetWeaponEnabled()
	{
		gameObject.GetComponentInParent<AICharacterMovement>().SetAttack(false);
		WeaponEnabled = true;
	}

	private void HitTargetMonster(Collider2D other)
	{
		SoundManager.instance.PlayHitSound();

		MonsterStatus monsterStatus = other.gameObject.GetComponent<MonsterStatus>();

		_monster = other.gameObject;

		Vector3 forceDir;
		if (_monster.transform.position.x >= transform.position.x)
			forceDir = Vector3.right;
		else
			forceDir = Vector3.left;

		_monster.GetComponent<MonsterMovement>().OnHit(_hitForceMagnitude, forceDir);

		GameObject particle = Instantiate(ParticleOnHit, _monster.transform.position, _monster.transform.rotation);
		Destroy(particle, 2.0f);

		monsterStatus.LoseHp(_character.GetComponent<CharacterStatus>().AttackPower);
	}
}
