using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : Skill
{
	public float BuffLastingTime;
	private float _elapsedTime = 0.0f;

	protected CharacterStatus _status;


	protected void Start()
	{
		_status = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		transform.parent = _status.gameObject.transform;
	}

	private void Update()
	{
		_elapsedTime += Time.deltaTime;
		if (_elapsedTime >= BuffLastingTime)
			Destroy(gameObject);

		DoBuff();
	}

	protected virtual void DoBuff()
	{

	}
}
