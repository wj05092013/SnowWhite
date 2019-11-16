using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkill : Skill
{
	public float Speed;
	public float LastingDistance;

	private Vector2 Direction;
	private Vector2 _initPos;


	private void Start()
	{
		SoundManager.instance.PlayAttackSound();

		if (FindObjectOfType<PlayerController>().IsLookingPositiveDir)
			Direction = Vector2.right;
		else
			Direction = Vector2.left;

		_initPos = transform.position;
	}

	private void Update()
	{
		if (CurrentDistanceFormInitPos() > LastingDistance)
			Destroy(gameObject);
	}

	private void FixedUpdate()
	{
		Vector3 variation = Direction * Speed * Time.fixedDeltaTime;
		transform.position += variation;
	}

	private float CurrentDistanceFormInitPos()
	{
		return Vector2.Distance(_initPos, transform.position);
	}
}
