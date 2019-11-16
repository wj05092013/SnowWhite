using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetector : MonoBehaviour {

	public GameObject TargetMonster;
	private MonsterStatus _targetStatus;
	private List<GameObject> _targetCandidates;


	// Use this for initialization
	void Start ()
	{
		_targetCandidates = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_targetStatus != null && _targetStatus.IsDead)
		{
			TargetMonster = null;
			_targetStatus = null;
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			_targetCandidates.Add(collision.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Monster") && TargetMonster == null)
		{
			float minDist = float.PositiveInfinity;
			float currDist = 0.0f;
			
			for (int i = 0; i < _targetCandidates.Count; ++i)
			{
				currDist = Math.Abs(_targetCandidates[i].transform.position.x - gameObject.transform.position.x);
				
				if (currDist < minDist)
				{
					minDist = currDist;
					
					TargetMonster = _targetCandidates[i];
					_targetStatus = TargetMonster.GetComponent<MonsterStatus>();

					_targetCandidates.RemoveAt(i);
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			if (TargetMonster != null && TargetMonster == collision.gameObject)
			{
				TargetMonster = null;
				_targetStatus = null;
			}
			else
			{
				_targetCandidates.Remove(collision.gameObject);
			}
		}
	}
}
