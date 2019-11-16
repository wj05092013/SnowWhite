using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {

	public Item.ItemType Type;
	
	private Image[] _imageComps;
	private Text[] _textComps;

	private bool _isFocused = false;
	private float _timeElapsed = 0.0f;
	public byte BaseChroma;
	public float PanelGlimmeringTime;


	void Start ()
	{
		_imageComps = GetComponentsInChildren<Image>();
		_textComps = GetComponentsInChildren<Text>();

		HidePanel();
	}
	
	void Update ()
	{
		if (_isFocused)
			UpdateGlimmeringPanel();
	}

	void UpdateGlimmeringPanel()
	{
		_timeElapsed += Time.deltaTime;
		if (_timeElapsed > PanelGlimmeringTime)
			_timeElapsed -= PanelGlimmeringTime;

		float x = _timeElapsed - 1.0f;
		float chromaRatio = -x * x + 1.0f;

		byte resultChroma = (byte)(chromaRatio * (255 - BaseChroma) + BaseChroma);

		_imageComps[0].color = new Color32(resultChroma, resultChroma, resultChroma, 255);
	}

	public void Focus()
	{
		_isFocused = true;
		_timeElapsed = 0.0f;
		
	}
	public void Unfocus()
	{
		_isFocused = false;

		_imageComps[0].color = new Color32(255, 255, 255, 255);
	}

	public void ShowPanel()
	{
		foreach (Image img in _imageComps)
			img.enabled = true;
		foreach (Text txt in _textComps)
			txt.enabled = true;
	}
	public void HidePanel()
	{
		foreach (Image img in _imageComps)
			img.enabled = false;
		foreach (Text txt in _textComps)
			txt.enabled = false;
	}
}
