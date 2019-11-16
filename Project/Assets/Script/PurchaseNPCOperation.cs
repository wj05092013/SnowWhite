using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseNPCOperation : MonoBehaviour {

	private PurchasePanel _purchasePanelInst;

	private Camera _camera;

	private bool _isInteractionEnabled = false;


	private void Start()
	{
		Init();
	}
	private void Init()
	{
		_purchasePanelInst = FindObjectOfType<PurchasePanel>();

		if(_purchasePanelInst == null)
		{
			Invoke("Init", 0.01f);
			return;
		}
	}

	void Update ()
	{
		if(_isInteractionEnabled)
		{
			if (Input.GetButtonDown("Interaction"))
				_purchasePanelInst.ShowPanel();
		}
	}


	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			_purchasePanelInst.enabled = true;

			_isInteractionEnabled = true;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			_isInteractionEnabled = false;

			_purchasePanelInst.HidePanel();
			_purchasePanelInst.enabled = false;
		}
	}
}
