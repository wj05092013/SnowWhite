using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorAnim : MonoBehaviour
{
	private Sprite[] _sprites;
	private int _currSpriteIdx = 0;
	private const string _Sprite_Path = "Sprite/UI/pausemenu_sprite_enlarged";

	private Image _panelImage;

	private int _baseTime;
	private int _prevTime;
	private int _currTime;
	private int _elapsedTime = 0;

	public int SpriteIntervalTimeInMsec;
	public int AnimDelayInMsec;

	
	void Start ()
	{
		_sprites = Resources.LoadAll<Sprite>(_Sprite_Path);

		_panelImage = GetComponent<Image>();
		_panelImage.enabled = false;

		_baseTime = DateTime.Now.Millisecond;
		_currTime = _baseTime;	
	}

	private void Update()
	{
		if(_panelImage.enabled)
		{
			UpdateAnim();
		}
	}

	public void ShowAnim()
	{
		_panelImage.enabled = true;

		_currSpriteIdx = 0;
	}

	public void HideAnim()
	{
		_panelImage.enabled = false;
	}

	private void UpdateAnim()
	{
		_prevTime = _currTime;
		_currTime = DateTime.Now.Millisecond;
		if (_currTime <= _prevTime)
			return;

		int deltaTime = _currTime - _prevTime;
		_elapsedTime += deltaTime;
		
		if (_currSpriteIdx >= _sprites.Length)
		{
			if (_elapsedTime < AnimDelayInMsec)
				return;

			_currSpriteIdx = 0;
			_elapsedTime = 0;
		}

		if (_elapsedTime >= SpriteIntervalTimeInMsec)
		{

			_panelImage.sprite = _sprites[_currSpriteIdx];
			_currSpriteIdx++;

			_elapsedTime = 0;
		}
	}
}
