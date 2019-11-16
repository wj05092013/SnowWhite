using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public struct PlayerData
	{
		public int MaxLevel;
		public int CurrentLevel;

		public float CurrentExp;
		public float CurrentLvlMaxExp;
		public float PreviousLvlMaxExp;

		public int MaxHp;
		public int CurrentHp;

		public int MaxMp;
		public int CurrentMp;

		public int AttackPower;

		public int HpItemCount;
		public int MpItemCount;
		public int CoinCount;
	}
	private PlayerData _playerData;
	public bool SavedDataChanged = false;

	public string MainMenuSceneName;
	public string GameStartSceneName;
	public string LastSceneName;

	public bool IsPlaying = false;
	public bool IsPaused = false;


	private void Awake()
	{
		Screen.SetResolution(1600, 900, true);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		Application.targetFrameRate = 40;
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		if (IsPlaying && Input.GetButtonDown("Escape"))
		{
			if (!IsPaused)
				PauseGame();
			else
				ResumeGame();
		}
	}


	public void PauseGame()
	{
		Time.timeScale = 0.0f;
		IsPaused = true;

		FindObjectOfType<PausePanel>().ShowPanel();
	}

	public void ResumeGame()
	{
		if (!IsPaused)
			return;

		Time.timeScale = 1.0f;
		IsPaused = false;

		FindObjectOfType<PausePanel>().HidePanel();
	}

	public void StartGame()
	{
		SceneManager.LoadScene(GameStartSceneName);
		IsPlaying = true;
	}

	public void GoToMainMenu()
	{
		ResumeGame();
		IsPlaying = false;

		SceneManager.LoadScene(MainMenuSceneName);
	}

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void MoveToNextLevel(string sceneName)
	{
		SaveData();

		SceneManager.LoadScene(sceneName);
	}

	public void MoveToLastLevel()
	{
		SceneManager.LoadScene(LastSceneName);
	}

	public void SaveData()
	{
		CharacterStatus playerStatus = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();

		_playerData.MaxLevel = playerStatus.MaxLevel;
		_playerData.CurrentLevel = playerStatus.CurrentLevel;

		_playerData.CurrentExp = playerStatus.CurrentExp;
		_playerData.CurrentLvlMaxExp = playerStatus.CurrentLvlMaxExp;
		_playerData.PreviousLvlMaxExp = playerStatus.PreviousLvlMaxExp;

		_playerData.MaxHp = playerStatus.MaxHp;
		_playerData.CurrentHp = playerStatus.CurrentHp;

		_playerData.MaxMp = playerStatus.MaxMp;
		_playerData.CurrentMp = playerStatus.CurrentMp;

		_playerData.AttackPower = playerStatus.AttackPower;

		_playerData.HpItemCount = playerStatus.HpItemCount.Value;
		_playerData.MpItemCount = playerStatus.MpItemCount.Value;
		_playerData.CoinCount = playerStatus.CoinCount.Value;

		SavedDataChanged = true;
	}
	
	public void LoadData()
	{
		CharacterStatus playerStatus = FindObjectOfType<PlayerController>().GetComponent<CharacterStatus>();
		CharacterStatus aiStatus = FindObjectOfType<AICharacterMovement>().GetComponent<CharacterStatus>();
		if(playerStatus == null || aiStatus == null)
		{
			Invoke("LoadData", 0.01f);
			return;
		}

		playerStatus.MaxLevel = _playerData.MaxLevel;
		playerStatus.CurrentLevel = _playerData.CurrentLevel;

		playerStatus.CurrentExp = _playerData.CurrentExp;
		playerStatus.CurrentLvlMaxExp = _playerData.CurrentLvlMaxExp;
		playerStatus.PreviousLvlMaxExp = _playerData.PreviousLvlMaxExp;

		playerStatus.MaxHp = _playerData.MaxHp;
		playerStatus.CurrentHp = _playerData.CurrentHp;

		playerStatus.MaxMp = _playerData.MaxMp;
		playerStatus.CurrentMp = _playerData.CurrentMp;

		playerStatus.AttackPower = _playerData.AttackPower;

		playerStatus.HpItemCount.Value = _playerData.HpItemCount;
		playerStatus.MpItemCount.Value = _playerData.MpItemCount;
		playerStatus.CoinCount.Value = _playerData.CoinCount;

		aiStatus.AttackPower = _playerData.AttackPower;

		SavedDataChanged = false;
	}
}
