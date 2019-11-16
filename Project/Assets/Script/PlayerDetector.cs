using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour {

	private MonsterMovement _movement;
	private LevelManager _lvlMgr;


	// Use this for initialization
	void Start () {

		_movement = gameObject.GetComponentInParent<MonsterMovement>();
		_lvlMgr = FindObjectOfType<LevelManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (_lvlMgr.IsPlayerDead)
				return;

			_movement.TraceTarget = collision.gameObject;

			_movement.StopCoroutine("ChangeMovement");
			_movement.QueryCoroutineActivated = false;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			// 플레이어가 죽은 경우 추적 중지
			if (_lvlMgr.IsPlayerDead)
			{
				_movement.TraceTarget = null;

				if (!_movement.QueryCoroutineActivated)
				{
					_movement.StartCoroutine("ChangeMovement");
					_movement.QueryCoroutineActivated = true;
				}
				return;
			}

			_movement.TraceTarget = collision.gameObject;
			_movement.Animator.SetBool("isMoving", true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			_movement.TraceTarget = null;

			if (!_lvlMgr.IsPlayerDead)
			{
				_movement.StartCoroutine("ChangeMovement");
				_movement.QueryCoroutineActivated = true;
			}
		}
	}
}
