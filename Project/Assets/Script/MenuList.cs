using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuList : MonoBehaviour {

	private enum MenuItem
	{
		Start = 0,
		Exit,
		// Add elements here.
		MenuItemNum
	}
	private MenuItem _currentMenu;

	private string[] _menuItemName;

	private Text _text;
	private GameManager _gameMgr;

	private float _cumulativeTime = 0.0f;

	public byte BaseAlpha;
	public float TextGlimmeringTime;


	void Start ()
	{
		_menuItemName = new string[(int)MenuItem.MenuItemNum];
		_menuItemName[0] = "START";
		_menuItemName[1] = "EXIT";
		// Add elements here.

		_text = GetComponent<Text>();
		_gameMgr = FindObjectOfType<GameManager>();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
			MoveToRightMenuItem();
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			MoveToLeftMenuItem();

		_text.text = _menuItemName[(int)_currentMenu];

		UpdateGlimmeringText();

		if (Input.GetButtonDown("Submit"))
			CallGameMgr();
	}

	private void UpdateGlimmeringText()
	{
		_cumulativeTime += Time.deltaTime;
		if (_cumulativeTime > TextGlimmeringTime)
			_cumulativeTime -= TextGlimmeringTime;

		float x = _cumulativeTime - 1.0f;
		float alphaRatio = -x * x + 1.0f;

		byte resultAlpha = (byte)(alphaRatio * (255 - BaseAlpha) + BaseAlpha);

		_text.color = new Color32(0, 0, 0, resultAlpha);
	}

	private void MoveToRightMenuItem()
	{
		SoundManager.instance.PlayChangeMenuSound();

		if (_currentMenu + 1 >= MenuItem.MenuItemNum)
		{
			_currentMenu = 0;
			return;
		}
		_currentMenu++;
	}

	private void MoveToLeftMenuItem()
	{
		SoundManager.instance.PlayChangeMenuSound();

		if(_currentMenu == 0)
		{
			_currentMenu = MenuItem.MenuItemNum - 1;
			return;
		}
		_currentMenu--;
	}

	private void CallGameMgr()
	{
		SoundManager.instance.PlaySelectSound1();

		switch(_currentMenu)
		{
			case MenuItem.Start:
				_gameMgr.StartGame();
				break;

			case MenuItem.Exit:
				_gameMgr.ExitGame();
				break;

				// Add elements here.

			default:
				return;
		}
	}
}
