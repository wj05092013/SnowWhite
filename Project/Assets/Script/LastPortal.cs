using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPortal : MonoBehaviour {

	private GameManager _gameMgr;


	private void Awake()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Animator>().enabled = false;
		GetComponent<CircleCollider2D>().enabled = false;
	}

	private void Start()
	{
		_gameMgr = FindObjectOfType<GameManager>();
	}

	public void OnCrowDead()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<Animator>().enabled = true;
		GetComponent<CircleCollider2D>().enabled = true;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				_gameMgr.MoveToLastLevel();
			}
		}
	}
}
