using System;
using UnityEngine;

public class SaveController : MonoBehaviour
{
	public int AmountOfLvlsBeforeBonus;
	public bool IsRandom;

	private MainGameController _gameController;
	private string _roundForPlaying = "LvlNum";
	private string _allRoundsForPlaying = "LvlBase";
	private string _notBonusLvlsCounter = "BasicLvls";
	private string _bonusLvlsCounter = "BonusLvls";
	private char[] _currentLvlBase;
	private int _currentLvlNum;

	private void Awake()
	{
		_gameController = FindObjectOfType<MainGameController>();
		if (PlayerPrefs.GetString(_allRoundsForPlaying).Length != _gameController.AvailableLevels.Length)
		{
			char[] lvlBaseNumbers = new char[_gameController.AvailableLevels.Length];
			for (int i = 0; i < lvlBaseNumbers.Length; i++)
			{
				lvlBaseNumbers[i] = Convert.ToChar("0");
			}
			string LVLBase = new string(lvlBaseNumbers);
			PlayerPrefs.SetString(_allRoundsForPlaying, LVLBase);
			PlayerPrefs.SetInt(_roundForPlaying, 0);
			PlayerPrefs.SetInt(_notBonusLvlsCounter, 0);
		}
		if (PlayerPrefs.GetInt(_roundForPlaying) != _gameController.AvailableLevels.Length)
		{
			IsRandom = false;
		}
		else
		{
			IsRandom = true;
		}
	}
	public int GetNextLvlNum()
	{
		_gameController = FindObjectOfType<MainGameController>();
		if (!IsRandom)
		{
			if (PlayerPrefs.GetInt(_roundForPlaying) != _gameController.AvailableLevels.Length)
			{
				_currentLvlNum = PlayerPrefs.GetInt(_roundForPlaying);
				return PlayerPrefs.GetInt(_roundForPlaying);
			}
			else
			{
				IsRandom = true;
				return GetRandomLvlNum();
			}
		}
		else
		{
			return GetRandomLvlNum();
		}
	}
	private int GetRandomLvlNum()
	{
		_currentLvlBase = PlayerPrefs.GetString(_allRoundsForPlaying).ToCharArray();
		bool AllLvlsPlayed = true;
		for (int i = 0; i < _currentLvlBase.Length; i++)
		{
			if (_currentLvlBase[i] == Convert.ToChar("0"))
			{
				AllLvlsPlayed = false;
			}
		}
		if (AllLvlsPlayed)
		{
			char[] lvlBaseNumbers = new char[_gameController.AvailableLevels.Length];
			for (int i = 0; i < lvlBaseNumbers.Length; i++)
			{
				lvlBaseNumbers[i] = Convert.ToChar("0");
			}
			string LVLBase = new string(lvlBaseNumbers);
			PlayerPrefs.SetString(_allRoundsForPlaying, LVLBase);
		}
		_currentLvlBase = PlayerPrefs.GetString(_allRoundsForPlaying).ToCharArray();
		for (int i = 0; i < 10000; i++)
		{
			int num = UnityEngine.Random.Range(0, _currentLvlBase.Length);
			if (_currentLvlBase[num] == Convert.ToChar("0"))
			{
				_currentLvlNum = num;
				return num;
			}
		}
		_currentLvlNum = 0;
		return 0;
	}
	public int GetCurrentLvlNum()
	{
		return _currentLvlNum;
	}
	public void SaveCurrentLvl(bool isBonusLvl)
	{
		if (isBonusLvl)
		{
			PlayerPrefs.SetInt(_notBonusLvlsCounter, 0);
		}
		else
		{
			PlayerPrefs.SetInt(_notBonusLvlsCounter, PlayerPrefs.GetInt(_notBonusLvlsCounter) + 1);
			if (IsRandom)
			{
				_currentLvlBase[_currentLvlNum] = Convert.ToChar("1");
				string LVLBase = new string(_currentLvlBase);
				PlayerPrefs.SetString(_allRoundsForPlaying, LVLBase);
			}
			else
			{
				PlayerPrefs.SetInt(_roundForPlaying, PlayerPrefs.GetInt(_roundForPlaying) + 1);
			}
		}
	}
	public bool IsItTimeForBonusLvl()
	{
		if (PlayerPrefs.GetInt(_notBonusLvlsCounter) > AmountOfLvlsBeforeBonus)
		{
			return true;
		}
		return false;
	}
}
