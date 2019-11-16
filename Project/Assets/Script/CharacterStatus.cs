using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour {
	
	public enum CharacterType
	{
		Player,
		AI
	}
	public CharacterType Type;

	public int MaxLevel;
	public int CurrentLevel;
	public GameObject ParticleOnLevelUp;

	public float CurrentExp;
	public float CurrentLvlMaxExp;
	public float PreviousLvlMaxExp = 0.0f;
	public float ExpIncrExtent;

	public int MaxHp;
	public int CurrentHp;
	public int HpIncrExtent;

	public int MaxMp;
	public int CurrentMp;
	public int MpIncrExtent;

	public int AttackPower;
	public float AttackPowerIncrExtent;

	public int UnbeatableTime;				// 피격시 플레이어가 무적이 되는 시간
	public bool IsUnbeatable = false;		// 플레이어 무적 플래그
	public const float BlinkTerm = 0.2f;    // 캐릭터가 깜빡이는 시간

	public IntWrapper HpItemCount = new IntWrapper();
	public IntWrapper MpItemCount = new IntWrapper();
	public IntWrapper CoinCount = new IntWrapper();

	private bool _coroutineOperatingUnbeatableStatus = false;
	private bool _isDead = false;


	private void Start()
	{
		if(Type == CharacterType.Player)
		{
			DoEverySecond();
		}
	}

	void Update ()
	{
		if (Type == CharacterType.Player)
		{
			if (CurrentExp >= CurrentLvlMaxExp)
				LevelUp();

			if (Input.GetButtonDown("UseHpItem"))
				UseHpItem();
			if (Input.GetButtonDown("UseMpItem"))
				UseMpItem();
		}

		if (IsUnbeatable && !_coroutineOperatingUnbeatableStatus)
			StartCoroutine("UnbeatableStatus");
	}


	public void OnDead()
	{
		_isDead = true;
		CancelInvoke("DoEverySecond");
	}
	public void OnRespawn()
	{
		_isDead = false;
		DoEverySecond();

		// 캐릭터 특성치 초기화
		CurrentHp = MaxHp;
		CurrentMp = MaxMp;
	}
	public void LevelUp()
	{
		if (CurrentLevel >= MaxLevel)
			return;

		SoundManager.instance.PlayLevelUpSound();

		GameObject particle = Instantiate(ParticleOnLevelUp, transform.position, transform.rotation);
		Destroy(particle, 3.0f);

		// 최대 경험치 증가
		float expIncr = (CurrentLvlMaxExp - PreviousLvlMaxExp) * ExpIncrExtent;
		PreviousLvlMaxExp = CurrentLvlMaxExp;
		CurrentLvlMaxExp += expIncr;

		// 최대 체력 증가
		MaxHp += CurrentLevel * HpIncrExtent;
		CurrentHp = MaxHp;

		// 최대 마력 증가
		MaxMp += CurrentLevel * MpIncrExtent;
		CurrentMp = MaxMp;

		
		if (Type == CharacterType.Player)
		{
			AttackPower = (int)(AttackPower * AttackPowerIncrExtent);
			FindObjectOfType<AICharacterMovement>().gameObject.GetComponent<CharacterStatus>().AttackPower = AttackPower;
		}

		// 레벨 증가
		++CurrentLevel;
		if (CurrentLevel > MaxLevel)
			CurrentLevel = MaxLevel;
	}

	IEnumerator UnbeatableStatus()
	{
		_coroutineOperatingUnbeatableStatus = true;

		int count = 0;

		SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

		int unbeatableCount = (int)(UnbeatableTime / BlinkTerm);

		while (count < unbeatableCount)
		{
			if(count % 2 == 0)
				renderer.color = new Color32(255, 255, 255, 65);	// 알파 값 변경
			else
				renderer.color = new Color32(255, 255, 255, 195);	// 알파 값 변경

			yield return new WaitForSeconds(BlinkTerm);				// 매 BlinkTime마다 깜빡임

			count++;
		}

		renderer.color = new Color32(255, 255, 255, 255);			// 알파 값 원상 복귀

		IsUnbeatable = false;           // 무적 상태 해제
		_coroutineOperatingUnbeatableStatus = false;

		yield return null;
	}


	public void GainExp(float exp)
	{
		CurrentExp += exp;
	}

	public void GainHp(int hp)
	{
		CurrentHp += hp;
		if (CurrentHp > MaxHp)
			CurrentHp = MaxHp;
	}
	public void LoseHp(int attackPower)
	{
		CurrentHp -= attackPower;
		if (CurrentHp <= 0)
		{
			CurrentHp = 0;
			FindObjectOfType<LevelManager>().OnPlayerIsDead();
		}
	}

	public void GainMp(int mp)
	{
		CurrentMp += mp;
		if (CurrentMp > MaxMp)
			CurrentMp = MaxMp;
	}
	public void LoseMp(int mp)
	{
		CurrentMp -= mp;
		if (CurrentMp <= 0)
			CurrentMp = 0;
	}


	public void AddHpItem()
	{
		HpItemCount.Value++;
	}
	private void UseHpItem()
	{
		if (HpItemCount.Value <= 0)
			return;

		SoundManager.instance.PlayEatSound();

		GainHp(ItemInfo.HpItemRestoreAmount);
		HpItemCount.Value--;
	}

	public void AddMpItem()
	{
		MpItemCount.Value++;
	}
	private void UseMpItem()
	{
		if (MpItemCount.Value <= 0)
			return;

		SoundManager.instance.PlayDrinkSound();

		GainMp(ItemInfo.MpItemRestoreAmount);
		MpItemCount.Value--;
	}

	public void AddCoin()
	{
		CoinCount.Value++;
	}
	public void LoseCoin()
	{
		if (CoinCount.Value <= 0)
			return;

		CoinCount.Value--;
	}

	private void DoEverySecond()
	{
		GainHp(1);
		GainMp(1);
		
		Invoke("DoEverySecond", 1.0f);
	}
}
