using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour {

	private GameManager _gameMgr;

	private Image _panelImage;
	private MirrorAnim _mirrorAnim;

	private bool _isPaused = false;


	public enum MenuType
	{
		Resume = 0,
		Mainmenu,
		// Add elements here.
		MenuTypeNum
	}
	private MenuType _currentMenuType;

	private GlimmeringText[] _glmTexts;


	void Start ()
	{
		_gameMgr = FindObjectOfType<GameManager>();

		_panelImage = GetComponent<Image>();
		_mirrorAnim = GetComponentInChildren<MirrorAnim>();

		_panelImage.enabled = false;

		_glmTexts = GetComponentsInChildren<GlimmeringText>();
	}

	private void Update()
	{
		if(_isPaused)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				MoveToUpperMenuItem();
				UpdateTexts();
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				MoveToLowerMenuItem();
				UpdateTexts();
			}

			if (Input.GetButton("Submit"))
				CallGameMgr();
		}
	}

	public void ShowPanel()
	{
		_isPaused = true;
		
		_panelImage.enabled = true;
		_mirrorAnim.ShowAnim();

		foreach (GlimmeringText glmText in _glmTexts)
		{
			glmText.enabled = true;
			glmText.ShowText();
		}

		_currentMenuType = MenuType.Resume;
		UpdateTexts();
	}

	public void HidePanel()
	{
		_isPaused = false;
		
		_panelImage.enabled = false;
		_mirrorAnim.HideAnim();

		foreach (GlimmeringText glmText in _glmTexts)
		{
			glmText.enabled = false;
			glmText.HideText();
		}
	}

	private void MoveToUpperMenuItem()
	{
		if(_currentMenuType == 0)
		{
			_currentMenuType = MenuType.MenuTypeNum - 1;
			return;
		}
		_currentMenuType--;
	}

	private void MoveToLowerMenuItem()
	{
		if (_currentMenuType + 1 >= MenuType.MenuTypeNum)
		{
			_currentMenuType = 0;
			return;
		}
		_currentMenuType++;
	}

	private void UpdateTexts()
	{
		foreach (GlimmeringText glmText in _glmTexts)
		{
			if (glmText.Type == _currentMenuType)
				glmText.Focus();
			else
				glmText.Unfocus();
		}
	}

	private void CallGameMgr()
	{
		switch(_currentMenuType)
		{
			case MenuType.Resume:
				_gameMgr.ResumeGame();
				break;

			case MenuType.Mainmenu:
				_gameMgr.GoToMainMenu();
				break;

				// Add elements here.

			default:
				break;
		}
	}
}
