using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public float MovePower;
	public float JumpPower;
	
	private Rigidbody2D _rbody;
	private Animator _animator;

	public bool IsLookingPositiveDir = true;	// Positive direction : 오른쪽
	private bool _isInputJump = false;
	private bool _isInAir = false;	

	private float _prevFrameVelocityAxisY;



	// ----- [Override Method] -----
	// 오브젝트 초기화
	void Start () {
		// 현재 오브젝트의, 혹은 자식 오브젝트의 컴포넌트들을 획득
		_rbody = gameObject.GetComponent<Rigidbody2D>();
		_animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	// 매 프레임 마다 수행
	void Update () {
		if(Input.GetAxisRaw("Horizontal") == 0)
		{
			_animator.SetBool("isWalking", false);		// 좌우 움직임이 없는 상태(애니메이션 플래그)
		}
		else
		{
			_animator.SetBool("isWalking", true);		// 좌우 움직임이 있는 상태(애니메이션 플래그)
		}

		// 오브젝트가 가속도를 가지고 하강할 때 (떨어질 때)
		if(_rbody.velocity.y < _prevFrameVelocityAxisY)
		{
			_isInAir = true;							// 오브젝트가 공중에 있음
			_animator.SetBool("isFalling", true);		// 오브젝트가 떨어지고 있음(애니메이션 플래그)
		}
		_prevFrameVelocityAxisY = _rbody.velocity.y;	// 다음 프레임에서 현재 프레임의 오브젝트 Y축 속도를 알기 위해

		// 오브젝트가 공중에 있을 경우 점프할 수 없음
		if (Input.GetButtonDown("Jump") && !_isInAir)
		{
			_isInputJump = true;				// 점프키가 입력됨
			_isInAir = true;					// 오브젝트가 공중에 있음
			_animator.SetTrigger("doJump");		// 트리거 발생
		}
	}

	// 고정된 시간 간격의 프레임 마다 수행
	private void FixedUpdate()
	{
		Move();
		Jump();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// 캐릭터 아래의 Trigger가 플레이어 레이어 오브젝트와 충돌하고, 충돌을 유지할 경우
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			_animator.SetBool("isFalling", false);		// 캐릭터의 착지 (애니메이션 플래그)
			_isInAir = false;							// 캐릭터의 착지
		}
	}

	private void Move()
	{
		if(Input.GetAxisRaw("Horizontal") < 0)
		{
			Vector3 scale = gameObject.transform.localScale;
			gameObject.transform.localScale = new Vector3(-Math.Abs(scale.x), scale.y, scale.z);

			transform.position += Vector3.left * MovePower * Time.fixedDeltaTime;
			IsLookingPositiveDir = false;				// 왼쪽을 바라보는 중
		}
		else if(Input.GetAxisRaw("Horizontal") > 0)
		{
			Vector3 scale = gameObject.transform.localScale;
			gameObject.transform.localScale = new Vector3(Math.Abs(scale.x), scale.y, scale.z);

			transform.position += Vector3.right * MovePower * Time.fixedDeltaTime;
			IsLookingPositiveDir = true;				// 오른쪽을 바라보는 중
		}
	}

	private void Jump()
	{
		// 점프키의 입력이 있었다면
		if (!_isInputJump)
			return;

		SoundManager.instance.PlayJumpSound();

		// y축 양의 방향으로 힘을 가한다
		_rbody.AddForce(new Vector2(0.0f, JumpPower), ForceMode2D.Impulse);

		_isInputJump = false;		// 점프키 입력 상태 해제
	}
}
