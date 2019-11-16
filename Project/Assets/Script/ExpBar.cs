using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour {

	private CharacterStatus _status;
	private Image _image;
	private Text _text;
	private Sprite[] _sprites;
	private int _expPercentage;

	private const string _spritePath = "Sprite/UI/exp_bar_enlarged";


	// Use this for initialization
	void Start () {
		_status = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		_image = GetComponent<Image>();
		_text = GetComponentInChildren<Text>();

		_sprites = Resources.LoadAll<Sprite>(_spritePath);
	}
	
	// Update is called once per frame
	void Update () {

		UpdateExpText();
		UpdateExpBar();
	}

	private void UpdateExpText()
	{
		_text.text = _status.CurrentExp.ToString() + "/" + _status.CurrentLvlMaxExp.ToString();
	}
	private void UpdateExpBar()
	{
		_expPercentage = (int)((_status.CurrentExp - _status.PreviousLvlMaxExp) / (_status.CurrentLvlMaxExp - _status.PreviousLvlMaxExp) * 100.0f);
		if (_expPercentage > 100)
			_expPercentage = 100;

		_image.sprite = _sprites[_expPercentage];
	}
}
