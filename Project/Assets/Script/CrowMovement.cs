using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovement : MonsterMovement
{
	private enum MovementType
	{
		Idle = 0,
		Move,
		Max
	}
	private MovementType _movementFlag = MovementType.Idle;
	
	private Vector3 _moveDirection;


	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	private void OnEnable()
	{
		GetComponent<AudioSource>().enabled = true;
	}
	private void OnDisable()
	{
		GetComponent<AudioSource>().enabled = false;
	}

	protected override void Move()
	{
		// 추적중인 상태인 경우
		if (TraceTarget != null)
		{
			// 이동 방향을 추적 대상의 위치 쪽으로 한다.
			_moveDirection = TraceTarget.transform.position - transform.position;
			_moveDirection = _moveDirection.normalized;     // 벡터 정규화
		}
		
		// 스프라이트 이미지 좌우 반전
		if (_moveDirection.x < 0)
			_renderer.flipX = false;
		else if (_moveDirection.x > 0)
			_renderer.flipX = true;

		// 이동 후 위치 적용
		transform.position += _moveDirection * MovePower * Time.fixedDeltaTime;
	}

	protected override IEnumerator ChangeMovement()
	{
		// 움직임 상태 랜덤하게 선택
		_movementFlag = (MovementType)Random.Range(0, (int)MovementType.Max);


		if(_movementFlag == MovementType.Idle)
		{
			_moveDirection = Vector3.zero;
		}
		else if(_movementFlag == MovementType.Move)
		{
			Vector2 direction;
			float vecLen;

			do
			{
				// 한 변의 길이가 2인 정사각형 내에서 한 점(벡터) 선택
				direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

				vecLen = Vector2.Distance(Vector2.zero, direction);

			} while (vecLen > 1);           // 정사각형 범위 내에서 단위원 안에 있는 경우를 선택

			_moveDirection = new Vector3(direction.x, direction.y, 0.0f);   // 방향 변경
		}

		yield return new WaitForSeconds(MovementTime);      // 해당 시간만큼 같은 행동 반복

		StartCoroutine("ChangeMovement");
	}
}
