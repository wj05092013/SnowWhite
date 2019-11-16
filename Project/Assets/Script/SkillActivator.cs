using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActivator : MonoBehaviour
{
	public Skill.SkillType Type;

	public GameObject SkillPrefab;

	public string InputButton;
	public float MaxCoolTime;
	public float CurrentCoolTime;
	public int MpRequired;
	public int LevelRequired;

	public bool Locked = true;
	private bool _usable = true;

	private CharacterStatus _status;


	private void Start()
	{
		CurrentCoolTime = MaxCoolTime;

		_status = FindObjectOfType<PlayerController>().gameObject.GetComponent<CharacterStatus>();
	}

	private void Update()
	{
		if(LevelRequired <= _status.CurrentLevel)
		{
			Locked = false;

			if (!_usable)
			{
				CurrentCoolTime += Time.deltaTime;
			}

			if (_usable && Input.GetButtonDown(InputButton))
			{
				ActivateSkill();
			}
		}		
	}

	protected virtual void ActivateSkill()
	{
		if (_status.CurrentMp - MpRequired < 0)
		{
			SoundManager.instance.PlayNotEnoughSound();
			return;
		}

		_status.LoseMp(MpRequired);

		GameObject skillObj = Instantiate(SkillPrefab);
		skillObj.transform.position = transform.position;

		_usable = false;
		CurrentCoolTime = 0.0f;
		Invoke("SetUsable", MaxCoolTime);
	}

	private void SetUsable()
	{
		_usable = true;
		CurrentCoolTime = MaxCoolTime;
	}
}
