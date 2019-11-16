using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAp : MonoBehaviour {

	private CharacterStatus _status;
	private Text _text;


	// Use this for initialization
	void Start () {

		_status = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		_text.text = "AP\n" + _status.AttackPower.ToString();
	}
}
