using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private GameManager _gameMgr;
	public GameObject ParticleOnRespawn;	// 리스폰 시 스폰할 이펙트(파티클)
	public GameObject CheckPoint;			// 체크포인트 속성
	private GameObject _player;				// 현재 플레이어

	public bool IsPlayerDead = false;


	// Use this for initialization
	void Start () {

		_gameMgr = FindObjectOfType<GameManager>();
		_player = FindObjectOfType<PlayerController>().gameObject;
		
		RespawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {

		if(IsPlayerDead)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				RespawnPlayer();
			}
		}
		
		if (_player.transform.position.y < -50.0f)
			RespawnPlayer();

		if(Input.GetButtonDown("Cheat1"))
		{
			UseLevelUpCheat();
		}
		if(Input.GetButtonDown("Cheat2"))
		{
			UseAddCoinCheat();
		}
	}


	public void OnMonsterIsDead(float exp)
	{
		_player.GetComponent<CharacterStatus>().GainExp(exp);
	}

	public void OnPlayerIsDead()
	{
		_player.GetComponent<PlayerController>().enabled = false;
		_player.GetComponent<SkillManager>().enabled = false;
		_player.GetComponentInChildren<Animator>().SetBool("isDead", true);
		_player.GetComponent<CharacterStatus>().OnDead();

		IsPlayerDead = true;
	}

	public void RespawnPlayer()
	{
		_player.GetComponent<PlayerController>().enabled = true;
		_player.GetComponent<SkillManager>().enabled = true;
		_player.GetComponentInChildren<Animator>().SetBool("isDead", false);
		_player.GetComponent<CharacterStatus>().OnRespawn();

		if (_gameMgr.SavedDataChanged)
			_gameMgr.LoadData();
		
		_player.transform.position = CheckPoint.transform.position;
		_player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		GameObject particle = Instantiate(ParticleOnRespawn, _player.transform.position + 2*Vector3.down, _player.transform.rotation);
		Destroy(particle, 3.0f);

		IsPlayerDead = false;
	}


	private void UseLevelUpCheat()
	{
		CharacterStatus status = _player.GetComponent<CharacterStatus>();
		status.LevelUp();
		status.CurrentExp = status.PreviousLvlMaxExp;
	}
	private void UseAddCoinCheat()
	{
		CharacterStatus status = _player.GetComponent<CharacterStatus>();

		for (int i = 0; i < 10; ++i)
			status.AddCoin();
	}
}
