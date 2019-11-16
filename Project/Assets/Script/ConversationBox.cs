using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationBox : MonoBehaviour
{
	private PlayerController _playerController;
	private SpriteRenderer _renderer;

	private void Start()
	{
		_playerController = FindObjectOfType<PlayerController>();
		_renderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if (_playerController.IsLookingPositiveDir)
			_renderer.flipX = false;
		else
			_renderer.flipX = true;
	}
}
