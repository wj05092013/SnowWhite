using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour {

	public enum MonsterType
	{
		Mushroom,
		MagicSphere,
		MagicRock,
		Crow
	}
	public MonsterType Type;

	public float Exp;
	public int ItemCount;

	public int MaxHp;
	public int CurrentHp;

	public int MaxMp;
	public int CurrentMp;

	public bool IsDead = false;
	public float DisappearingTime;
	public const float DisappearingTerm = 0.0333f;

	public bool IsRevivable;
	public const float RevivalTime = 10.0f;

	private LevelManager _lvlMgr;
	private SpawnManager _spawnMgr;


	// Use this for initialization
	void Start ()
	{
		_lvlMgr = FindObjectOfType<LevelManager>();
		_spawnMgr = gameObject.GetComponent<SpawnManager>();
	}

	public void Die()
	{
		switch(Type)
		{
			case MonsterType.Mushroom:
				SoundManager.instance.PlayMushroomSound();
				break;

			case MonsterType.MagicSphere:
				SoundManager.instance.PlayMagicSphereSound();
				break;

			case MonsterType.MagicRock:
				SoundManager.instance.PlayMagicRockSound();
				break;

			case MonsterType.Crow:
				SoundManager.instance.PlayCrowSound();
				FindObjectOfType<LastPortal>().OnCrowDead();
				break;
		}

		IsDead = true;
		_lvlMgr.OnMonsterIsDead(Exp);

		for(int i=0; i<ItemCount; ++i)
			_spawnMgr.Spawn(transform.position);

		gameObject.GetComponent<MonsterMovement>().OnDead();

		gameObject.GetComponentInChildren<Animator>().enabled = false;
		gameObject.GetComponent<MonsterMovement>().enabled = false;
		gameObject.GetComponent<Collider2D>().enabled = false;
		gameObject.GetComponent<Rigidbody2D>().simulated = false;
		
		StartCoroutine("OnDead");
	}
	public void Respawn()
	{
		IsDead = false;

		CurrentHp = MaxHp;
		CurrentMp = MaxMp;

		gameObject.GetComponentInChildren<Animator>().enabled = true;
		gameObject.GetComponent<MonsterMovement>().enabled = true;
		gameObject.GetComponent<Collider2D>().enabled = true;
		gameObject.GetComponent<Rigidbody2D>().simulated = true;

		gameObject.GetComponent<MonsterMovement>().OnRespawned();
	}

	IEnumerator OnDead()
	{
		SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();			

		int disappearCount = (int)(DisappearingTime / DisappearingTerm);
		int count = disappearCount;

		while(count >= 0)
		{
			byte alphaValue = (byte)(255.0f * count / disappearCount);
			renderer.color = new Color32(255, 255, 255, alphaValue);

			yield return new WaitForSeconds(DisappearingTerm);

			count--;
		}

		if(IsRevivable)
		{
			yield return new WaitForSeconds(RevivalTime);

			renderer.color = new Color32(255, 255, 255, 255);
			Respawn();
		}

		yield return null;
	}

	public void GainHp(int hp)
	{
		CurrentHp += hp;
		if (CurrentHp > MaxHp)
			CurrentHp = MaxHp;
	}
	public void LoseHp(int attackPower)
	{
		CurrentHp -= attackPower;
		if (CurrentHp < 0)
		{
			CurrentHp = 0;

			Die();
		}
	}

	public void GainMp(int mp)
	{
		CurrentMp += mp;
		if (CurrentMp > MaxMp)
			CurrentMp = MaxMp;
	}
	public void LoseMp(int mp)
	{
		CurrentMp -= mp;
		if (CurrentMp < 0)
			CurrentMp = 0;
	}
}
