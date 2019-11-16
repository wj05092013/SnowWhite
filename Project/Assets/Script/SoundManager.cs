using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	private AudioSource _audioSource;

	// Player
	public AudioClip JumpSound;
	public AudioClip AttackSound;
	public AudioClip Skill1Sound;
	public AudioClip Skill2Sound;
	public AudioClip LevelUpSound;
	public AudioClip ItemGetSound;
	public AudioClip EatSound;
	public AudioClip DrinkSound;

	// AICharacter
	public AudioClip AIAttackSound;

	// NPC
	public AudioClip VoiceSound1;
	public AudioClip VoiceSound2;
	public AudioClip VoiceSound3;

	// Monster
	public AudioClip HitSound;
	public AudioClip MushroomSound;
	public AudioClip MagicSphereSound;
	public AudioClip MagicRockSound;
	public AudioClip CrowSound;

	// Etc
	public AudioClip ChangeMenuSound;
	public AudioClip SelectSound1;
	public AudioClip SelectSound2;
	public AudioClip WrongSelectSound;
	public AudioClip NotEnoughSound;


	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayJumpSound()
	{
		_audioSource.PlayOneShot(JumpSound);
	}
	public void PlayAttackSound()
	{
		_audioSource.PlayOneShot(AttackSound);
	}
	public void PlaySkill1Sound()
	{
		_audioSource.PlayOneShot(Skill1Sound, 0.2f);
	}
	public void PlaySkill2Sound()
	{
		_audioSource.PlayOneShot(Skill2Sound);
	}
	public void PlayLevelUpSound()
	{
		_audioSource.PlayOneShot(LevelUpSound, 0.5f);
	}
	public void PlayItemGetSound()
	{
		_audioSource.PlayOneShot(ItemGetSound, 0.5f);
	}
	public void PlayEatSound()
	{
		_audioSource.PlayOneShot(EatSound);
	}
	public void PlayDrinkSound()
	{
		_audioSource.PlayOneShot(DrinkSound);
	}

	public void PlayAIAttackSound()
	{
		_audioSource.PlayOneShot(AIAttackSound, 0.2f);
	}

	public void PlayVoiceSound1()
	{
		_audioSource.PlayOneShot(VoiceSound1, 0.5f);
	}
	public void PlayVoiceSound2()
	{
		_audioSource.PlayOneShot(VoiceSound2, 0.5f);
	}
	public void PlayVoiceSound3()
	{
		_audioSource.PlayOneShot(VoiceSound3);
	}

	public void PlayHitSound()
	{
		_audioSource.PlayOneShot(HitSound, 0.3f);
	}
	public void PlayMushroomSound()
	{
		_audioSource.PlayOneShot(MushroomSound, 2.0f);
	}
	public void PlayMagicSphereSound()
	{
		_audioSource.PlayOneShot(MagicSphereSound, 10.0f);
	}
	public void PlayMagicRockSound()
	{
		_audioSource.PlayOneShot(MagicRockSound, 3.0f);
	}
	public void PlayCrowSound()
	{
		_audioSource.PlayOneShot(CrowSound, 2.0f);
	}

	public void PlayChangeMenuSound()
	{
		_audioSource.PlayOneShot(ChangeMenuSound);
	}
	public void PlaySelectSound1()
	{
		_audioSource.PlayOneShot(SelectSound1);
	}
	public void PlaySelectSound2()
	{
		_audioSource.PlayOneShot(SelectSound2);
	}
	public void PlayWrongSelectSound()
	{
		_audioSource.PlayOneShot(WrongSelectSound, 0.5f);
	}
	public void PlayNotEnoughSound()
	{
		_audioSource.PlayOneShot(NotEnoughSound, 2.0f);
	}
}
