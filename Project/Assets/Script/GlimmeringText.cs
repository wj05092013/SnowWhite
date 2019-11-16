using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlimmeringText : MonoBehaviour {

	private bool IsFocused = false;
	private Text _text;

	private int _baseTime;
	private int _prevTime;
	private int _currTime;
	private int _elapsedTime = 0;

	public PausePanel.MenuType Type;

	public byte BaseAlpha;
	public int TextGlimmeringTimeInMsec;


	void Start ()
	{
		_text = GetComponent<Text>();
		_text.enabled = false;
	}
	
	void Update ()
	{
		if(_text.enabled && IsFocused)
		{
			UpdateTextAlpha();
		}
	}

	private void UpdateTextAlpha()
	{
		_prevTime = _currTime;
		_currTime = DateTime.Now.Millisecond;
		if (_currTime <= _prevTime)
			return;

		int deltaTime = _currTime - _prevTime;
		_elapsedTime += deltaTime;

		if (_elapsedTime >= TextGlimmeringTimeInMsec)
			_elapsedTime = 0;

		float x = _elapsedTime / 1000.0f - 1.0f;
		float alphaRatio = -x * x + 1.0f;

		byte resultAlpha = (byte)(alphaRatio * (255 - BaseAlpha) + BaseAlpha);

		_text.color = new Color32(255, 255, 255, resultAlpha);
	}

	public void Focus()
	{
		IsFocused = true;

		_baseTime = DateTime.Now.Millisecond;
		_currTime = _baseTime;
		_elapsedTime = 0;

		_text.color = new Color32(255, 255, 255, BaseAlpha);
	}
	
	public void Unfocus()
	{
		IsFocused = false;

		_baseTime = 0;
		_currTime = 0;
		_elapsedTime = 0;

		_text.color = new Color32(150, 150, 150, 255);
	}

	public void ShowText()
	{
		_text.enabled = true;
	}
	public void HideText()
	{
		_text.enabled = false;
	}
}
