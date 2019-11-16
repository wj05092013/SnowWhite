using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public GameObject HpItemPrefab;
	public GameObject MpItemPrefab;
	public GameObject CoinItemPrefab;

	public float GatherableDistance;

	public enum ItemType
	{
		Hp = 0,
		Mp,
		Coin,
		Count
	}
	public ItemType _itemType;

	private SpawnManager _spawnMgr;
	private GameObject _player;

	private bool _isFlyingToPlayer = false;
	private float _flyingTime = 0.0f;
	private const float FlyingPower = 50.0f;


	// Use this for initialization
	void Start ()
	{
		_player = FindObjectOfType<PlayerController>().gameObject;

		_spawnMgr = gameObject.GetComponent<SpawnManager>();

		_itemType = (ItemType)Random.Range(0, (int)ItemType.Count);

		if (_itemType == ItemType.Hp)
			_spawnMgr.SpawnObjectPrefab = HpItemPrefab;
		else if (_itemType == ItemType.Mp)
			_spawnMgr.SpawnObjectPrefab = MpItemPrefab;
		else if (_itemType == ItemType.Coin)
			_spawnMgr.SpawnObjectPrefab = CoinItemPrefab;

		GameObject spawnedObj = _spawnMgr.Spawn(new Vector2(transform.position.x, transform.position.y));
		spawnedObj.transform.SetParent(gameObject.transform);

		ForceOnStart();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float distX = Mathf.Abs(_player.transform.position.x - transform.position.x);

		if (distX < GatherableDistance)
		{
			//float distance = Vector2.Distance(_player.transform.position, transform.position);
			float distY = Mathf.Abs(_player.transform.position.y - transform.position.y);

			if (distY < GatherableDistance)
			{
				if (Input.GetButtonDown("GatherItem"))
				{
					_isFlyingToPlayer = true;
					gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 2.0f), ForceMode2D.Impulse);
				}

				if (_isFlyingToPlayer && distX < 0.5f)
					ItemGathered();
			}
		}		
	}

	private void FixedUpdate()
	{
		if(_isFlyingToPlayer)
		{
			FlyingToPlayer();
		}
	}


	private void ForceOnStart()
	{
		Vector2 forceDir = new Vector2(Random.Range(-3.0f, 3.0f), 3.0f);

		GetComponent<Rigidbody2D>().AddForce(forceDir, ForceMode2D.Impulse);
	}

	private void FlyingToPlayer()
	{
		_flyingTime += Time.fixedDeltaTime;

		Vector3 dir = _player.transform.position - gameObject.transform.position;

		transform.position += dir * _flyingTime * FlyingPower * Time.fixedDeltaTime;
	}

	private void ItemGathered()
	{
		SoundManager.instance.PlayItemGetSound();

		CharacterStatus charStatus = _player.GetComponent<CharacterStatus>();

		if (_itemType == ItemType.Hp)
			charStatus.AddHpItem();
		else if (_itemType == ItemType.Mp)
			charStatus.AddMpItem();
		else if (_itemType == ItemType.Coin)
			charStatus.AddCoin();

		Destroy(gameObject);
	}
}
