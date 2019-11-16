using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AICharacterMovement : MonoBehaviour {

	private enum MoveDir
	{
		Idle,
		Left,
		Right
	}
	private MoveDir _moveDir = MoveDir.Idle;

	public float MovePower;
	private bool _doAttack = false;
	
	private Animator _animator;
	private MonsterDetector _detector;

	private enum AttackType
	{
		Attack01 = 0,
		Attack02,
		Count
	}


	void Start ()
	{
		_animator = gameObject.GetComponentInChildren<Animator>();
		_detector = gameObject.GetComponentInChildren<MonsterDetector>();
	}
	
	void Update ()
	{
		if (_detector.TargetMonster != null)
		{
			if (_detector.TargetMonster.transform.position.x > gameObject.transform.position.x)
				_moveDir = MoveDir.Right;
			else
				_moveDir = MoveDir.Left;
		}
		else
			_moveDir = MoveDir.Right;
	}

	private void FixedUpdate()
	{
		if (!_doAttack)
			Move();
	}

	private void Move()
	{
		if(_moveDir == MoveDir.Left)
		{
			Vector3 scale = gameObject.transform.localScale;
			gameObject.transform.localScale = new Vector3(-Math.Abs(scale.x), scale.y, scale.z);

			transform.position += Vector3.left * MovePower * Time.fixedDeltaTime;
		}
		else if (_moveDir == MoveDir.Right)
		{
			Vector3 scale = gameObject.transform.localScale;
			gameObject.transform.localScale = new Vector3(Math.Abs(scale.x), scale.y, scale.z);
			
			transform.position += Vector3.right * MovePower * Time.fixedDeltaTime;
		}
	}

	public void SetAttack(bool isAttacking)
	{
		_doAttack = isAttacking;

		if(_doAttack)
		{
			SoundManager.instance.PlayAIAttackSound();

			_animator.SetBool("IsWalking", false);

			AttackType type = (AttackType)UnityEngine.Random.Range((int)AttackType.Attack01, (int)AttackType.Count);

			if (type == AttackType.Attack01)
				_animator.SetBool("Attack01", true);
			else if (type == AttackType.Attack02)
				_animator.SetBool("Attack02", true);
		}
		else
		{
			_animator.SetBool("IsWalking", true);

			_animator.SetBool("Attack01", false);
			_animator.SetBool("Attack02", false);
		}
	}
}
