using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	private GameManager _gameMgr;

	public string NextSceneName;


	private void Start()
	{
		_gameMgr = FindObjectOfType<GameManager>();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				_gameMgr.MoveToNextLevel(NextSceneName);
			}
		}			
	}
}
