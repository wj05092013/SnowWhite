using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteManaBuff : BuffSkill
{
	protected void Start()
	{
		base.Start();
		SoundManager.instance.PlaySkill2Sound();
	}

	protected override void DoBuff()
	{
		base.DoBuff();

		_status.CurrentMp = _status.MaxMp;
	}
}
