using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpBar : MonoBehaviour {

	private CharacterStatus _status;
	private Image _image;
	private Text _text;
	private Sprite[] _sprites;
	private int _mpPercentage;

	private const string _spritePath = "Sprite/UI/mp_bar_enlarged";


	// Use this for initialization
	void Start () {

		_status = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		_image = GetComponent<Image>();
		_text = GetComponentInChildren<Text>();

		_sprites = Resources.LoadAll<Sprite>(_spritePath);
	}
	
	// Update is called once per frame
	void Update () {

		UpdateMpText();
		UpdateMpBar();
	}

	private void UpdateMpText()
	{
		_text.text = _status.CurrentMp.ToString() + "/" + _status.MaxMp.ToString();
	}
	private void UpdateMpBar()
	{
		_mpPercentage = (int)((float)_status.CurrentMp / _status.MaxMp * 100.0f);

		_image.sprite = _sprites[_mpPercentage];
	}
}
