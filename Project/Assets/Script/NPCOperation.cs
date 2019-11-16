using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOperation : MonoBehaviour {

	public GameObject[] ConversationsPrefab;
	private GameObject _currentConversation;
	private int _conversationIdx = 0;


	private Camera _camera;
	private bool _isInteractionEnabled = false;


	// Use this for initialization
	void Start () {
		_camera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_isInteractionEnabled)
		{
			if (Input.GetButtonDown("Interaction"))
			{
				ShowConversation();
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			_isInteractionEnabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			_isInteractionEnabled = false;
			ExitConversation();
		}
	}


	private void ShowConversation()
	{
		// 대화 종료
		if (_conversationIdx >= ConversationsPrefab.Length)
		{
			ExitConversation();
			return;
		}

		if (ConversationsPrefab[_conversationIdx] == null)
		{
			ExitConversation();
			return;
		}


		// 이전 대화창 삭제후, 다음 대화창 생성
		if (_currentConversation != null)
			Destroy(_currentConversation);
		else
			GetComponent<VoicePlayer>().PlayVoiceSound();

		_currentConversation = Instantiate(ConversationsPrefab[_conversationIdx], _camera.transform);
		_currentConversation.transform.parent = _camera.transform;

		// 화면 바로 앞에 생성
		_currentConversation.transform.position += new Vector3(0.0f, 0.0f, 0.5f);

		_conversationIdx++;
	}

	private void ExitConversation()
	{
		_conversationIdx = 0;

		Destroy(_currentConversation);
		_currentConversation = null;
	}
}
