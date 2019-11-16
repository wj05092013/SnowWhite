using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterMovement : MonsterMovement
{
	private enum MovementType
	{
		Idle = 0,
		Left,
		Right,
		Max
	}
	private MovementType _movementFlag = MovementType.Idle;
	private Vector3 _moveDir;


	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();


		Vector3 pos = transform.position;

		// Tracing 타입의 몬스터의 경우 플레이어를 추적 중이라면,
		//  플레이어가 있는 방향으로 이동해야한다.
		if (TraceTarget != null)
		{
			Vector3 targetPos = TraceTarget.transform.position;

			if (pos.x > targetPos.x)
				_movementFlag = MovementType.Left;
			else if (pos.x < targetPos.x)
				_movementFlag = MovementType.Right;
		}


		_moveDir = Vector3.zero;

		// 몬스터의 좌우 이동에 따른 처리
		if (_movementFlag == MovementType.Left)
		{
			_moveDir = Vector3.left;
			_renderer.flipX = false;                     // 스프라이트 이미지의 좌우를 뒤집는다
		}
		else if (_movementFlag == MovementType.Right)
		{
			_moveDir = Vector3.right;
			_renderer.flipX = true;                    // 좌우를 뒤집지 않는다.
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	protected override void Move()
	{
		transform.position += _moveDir * MovePower * Time.fixedDeltaTime;
	}

	protected override IEnumerator ChangeMovement()
	{
		_movementFlag = (MovementType)Random.Range(0, (int)MovementType.Max);	// [0, 2] 사이 랜덤한 정수 생성

		if(_movementFlag == MovementType.Idle)
		{
			Animator.SetBool("isMoving", false);
		}
		else
		{
			Animator.SetBool("isMoving", true);
		}
		
		yield return new WaitForSeconds(MovementTime);		// 해당 시간만큼 같은 행동 반복

		StartCoroutine("ChangeMovement");				// 코루틴 재실행
	}
}
