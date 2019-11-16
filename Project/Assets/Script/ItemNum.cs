using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNum : MonoBehaviour {
	
	private CharacterStatus _status;
	private Text _textComp;

	public Item.ItemType Type;
	private IntWrapper _itemCount;


	void Start ()
	{
		Invoke("Init", 0.01f);
	}

	private void Init()
	{
		_status = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		if(_status == null)
		{
			Invoke("Init", 0.01f);
			return;
		}

		_textComp = GetComponent<Text>();

		switch (Type)
		{
			case Item.ItemType.Hp:
				_itemCount = _status.HpItemCount;
				break;

			case Item.ItemType.Mp:
				_itemCount = _status.MpItemCount;
				break;

			case Item.ItemType.Coin:
				_itemCount = _status.CoinCount;
				break;
		}
	}

	private void Update()
	{
		_textComp.text = string.Format("x " + _itemCount.Value.ToString());
	}
}
