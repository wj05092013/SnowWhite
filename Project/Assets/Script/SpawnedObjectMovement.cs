using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObjectMovement : MonoBehaviour {

	public Vector3 InitDirection = Vector3.zero;
	public float InitVelocity = 0.0f;
	public float LastingDistance;

	private Vector3 _initPos;


	// Use this for initialization
	void Start () {
		_initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GetCurrentDistanceFormInitPos() > LastingDistance)
			Destroy(gameObject);
	}

	private float GetCurrentDistanceFormInitPos()
	{
		return Vector3.Distance(_initPos, transform.position);
	}

	private void FixedUpdate()
	{
		FixedMove();		// 매 fixed frame 마다 실행
	}

	public void FixedMove()
	{
		transform.position += InitDirection * InitVelocity * Time.fixedDeltaTime;		// 오브젝트 위치 변경(이동 수행)
	}
}
