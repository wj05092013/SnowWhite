using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

	private float _alpha = 0.0f;
	private SpriteRenderer _renderer;
	private Animator _animator;


	// Use this for initialization
	void Start () {

		_renderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
		
		PlayAnim();
	}

	private void Update()
	{
		_alpha += Time.deltaTime * 0.5f;
		if (_alpha >= 1.0f)
			_alpha = 1.0f;
		
		_renderer.color = new Color32(255, 255, 255, (byte)(255 * _alpha));
	}

	void PlayAnim()
	{
		_animator.Play("");
		
		Invoke("PlayAnim", 2.0f);
	}
}
