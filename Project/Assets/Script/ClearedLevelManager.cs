using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedLevelManager : MonoBehaviour {

	private GameManager _gameMgr;


	private void Start()
	{
		_gameMgr = FindObjectOfType<GameManager>();
		_gameMgr.IsPlaying = false;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Submit"))
			_gameMgr.GoToMainMenu();
	}
}
