using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMovement : MonoBehaviour
{
	public enum MonsterType
	{
		FreeStyle = 0,
		Tracing
	}
	public MonsterType Type;
	
	protected SpriteRenderer _renderer;
	public Animator Animator;

	public float MovePower;			// 이동 크기
	public float MovementTime;      // 같은 이동을 반복하는 시간

	protected Vector3 _initPos;


	private bool IsHit = false;
	private const float _hitForcingTime = 1.0f;
	private float _currentHitForcingTime = 0.0f;
	private float _hitForceMagnitude;
	private Vector3 _hitForceDir;

	// Tracing 타입의 몬스터에만 해당되는 필드
	public GameObject TraceTarget;					// 추적 대상 오브젝트
	public bool QueryCoroutineActivated = false;	// ChangeMovement 코루틴이 실행중이면 true


	protected virtual void Start()
	{
		_renderer = gameObject.GetComponentInChildren<SpriteRenderer>();      // 현재 오브젝트의 SpriteRenderer 컴포넌트
		Animator = gameObject.GetComponentInChildren<Animator>();            // 현재 오브젝트의 Animator 컴포넌트

		_initPos = transform.position;

		StartCoroutine("ChangeMovement");       // 코루틴 시작
		QueryCoroutineActivated = true;
	}

	protected virtual void Update()
	{

	}

	protected virtual void FixedUpdate()
	{
		ThrustOnHit();
		Move();
	}

	private void ThrustOnHit()
	{
		if (IsHit)
		{
			if (_currentHitForcingTime > _hitForcingTime)
			{
				_currentHitForcingTime = 0.0f;
				IsHit = false;

				return;
			}

			_currentHitForcingTime += Time.fixedDeltaTime;

			transform.position += _hitForceDir * GetCurrentMovePowerByHit() * Time.fixedDeltaTime;
		}
	}

	private float GetCurrentMovePowerByHit()
	{
		return -_hitForceMagnitude / _hitForcingTime * _currentHitForcingTime + _hitForceMagnitude;
	}

	public void OnHit(float forceMagnitude, Vector3 forceDir)
	{
		IsHit = true;

		_currentHitForcingTime = 0.0f;
		_hitForceMagnitude = forceMagnitude;
		_hitForceDir = forceDir;
	}

	public virtual void OnDead()
	{
		StopCoroutine("ChangeMovement");        // 코루틴 종료
		QueryCoroutineActivated = false;
	}

	public virtual void OnRespawned()
	{
		IsHit = false;

		transform.position = _initPos;
		_currentHitForcingTime = 0.0f;

		StartCoroutine("ChangeMovement");
		QueryCoroutineActivated = true;
	}

	protected abstract void Move();						// 이동 메서드 오버라이드 필요
	protected abstract IEnumerator ChangeMovement();	// 코루틴 메서드 오버라이드 필요
}
