using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour {

	private CharacterStatus _status;
	private Image _panelImage;
	private ItemPanel[] _itemPanels;
	private int _prevItemPanelIdx = 0;
	private int _currItemPanelIdx = 0;
	private bool _isPanelOpened = false;
	

	void Start ()
	{
		_status = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		_panelImage = GetComponent<Image>();
		_itemPanels = GetComponentsInChildren<ItemPanel>();

		_itemPanels[_currItemPanelIdx].Focus();

		HidePanel();
	}
	
	void Update ()
	{
		if(_isPanelOpened)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				MoveToUpperItem();
				UpdateGlimmeringItemPanel();
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				MoveToLowerItem();
				UpdateGlimmeringItemPanel();
			}

			if (Input.GetButtonDown("Submit"))
				BuyItem();
		}
	}

	private void MoveToUpperItem()
	{
		_prevItemPanelIdx = _currItemPanelIdx;

		if (_currItemPanelIdx == 0)
		{
			_currItemPanelIdx = _itemPanels.Length - 1;
			return;
		}

		_currItemPanelIdx--;
	}
	private void MoveToLowerItem()
	{
		_prevItemPanelIdx = _currItemPanelIdx;

		if (_currItemPanelIdx >= _itemPanels.Length - 1)
		{
			_currItemPanelIdx = 0;
			return;
		}

		_currItemPanelIdx++;
	}
	private void UpdateGlimmeringItemPanel()
	{
		_itemPanels[_prevItemPanelIdx].Unfocus();
		_itemPanels[_currItemPanelIdx].Focus();
	}

	private void BuyItem()
	{
		switch (_itemPanels[_currItemPanelIdx].Type)
		{
			case Item.ItemType.Hp:
				if (_status.CoinCount.Value >= ItemInfo.HpItemPrice)
				{
					SoundManager.instance.PlaySelectSound1();

					_status.AddHpItem();
					_status.CoinCount.Value -= ItemInfo.HpItemPrice;
				}
				else
					SoundManager.instance.PlayWrongSelectSound();
				break;

			case Item.ItemType.Mp:
				if (_status.CoinCount.Value >= ItemInfo.MpItemPrice)
				{
					SoundManager.instance.PlaySelectSound1();

					_status.AddMpItem();
					_status.CoinCount.Value -= ItemInfo.MpItemPrice;
				}
				else
					SoundManager.instance.PlayWrongSelectSound();
				break;
		}
	}

	public void ShowPanel()
	{
		_isPanelOpened = true;

		_panelImage.enabled = true;

		_prevItemPanelIdx = 0;
		_currItemPanelIdx = 0;

		foreach (ItemPanel panel in _itemPanels)
		{
			panel.enabled = true;
			panel.ShowPanel();
		}
	}
	public void HidePanel()
	{
		_isPanelOpened = false;

		_panelImage.enabled = false;

		foreach (ItemPanel panel in _itemPanels)
		{
			panel.HidePanel();
			panel.enabled = false;
		}
	}
}
