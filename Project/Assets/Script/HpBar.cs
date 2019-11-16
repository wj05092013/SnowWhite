using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

	private CharacterStatus _status;
	private Image _image;
	private Text _text;
	private Sprite[] _sprites;
	private int _hpPercentage;

	private const string _spritePath = "Sprite/UI/hp_bar_enlarged";


	// Use this for initialization
	void Start () {

		_status = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		_image = GetComponent<Image>();
		_text = GetComponentInChildren<Text>();

		_sprites = Resources.LoadAll<Sprite>(_spritePath);
	}
	
	// Update is called once per frame
	void Update () {

		UpdateHpText();
		UpdateHpBar();
	}

	private void UpdateHpText()
	{
		_text.text = _status.CurrentHp.ToString() + "/" + _status.MaxHp.ToString();
	}
	private void UpdateHpBar()
	{
		_hpPercentage = (int)((float)_status.CurrentHp / _status.MaxHp * 100.0f);

		_image.sprite = _sprites[_hpPercentage];
	}
}
