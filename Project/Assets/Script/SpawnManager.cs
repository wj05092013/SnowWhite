using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public GameObject SpawnObjectPrefab;		// 스폰할 오브젝트


	public GameObject Spawn(Vector2 position)
	{
		GameObject tempObj = Instantiate(SpawnObjectPrefab);        // 해당 오브젝트 스폰
		tempObj.transform.position = Vector3.zero;
		tempObj.transform.position = position;						// 초기위치 설정(월드 좌표)
		
		return tempObj;
	}
	public GameObject Spawn(Vector2 position, Vector3 initDirection)
	{
		GameObject tempObj = Spawn(position);
		SpawnedObjectMovement objMovement = tempObj.GetComponent<SpawnedObjectMovement>();

		// 스폰된 오브젝트가 이동 기능(SpawnedObjectMovement Component)을 가지고 있다면, 방향을 설정한다.
		if(objMovement != null)
			objMovement.InitDirection = initDirection;

		return tempObj;
	}
}
