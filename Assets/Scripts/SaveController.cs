using System;
using UnityEngine;

public class SaveController : MonoBehaviour
{
	public int AmountOfLvlsBeforeBonus;

	private MainGameController _gameController;
	private string _roundForPlaying = "LvlNum";
	private string _allRoundsForPlaying = "LvlBase";
	private string _notBonusLvlsCounter = "BasicLvls";
	private string _bonusLvlsCounter = "BonusLvls";
	private string _overallCompletedLevels = "OverallLevels";
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
	}
	public int GetNextLvlNum()
	{
		_gameController = FindObjectOfType<MainGameController>();
		if (PlayerPrefs.GetInt(_roundForPlaying) != _gameController.AvailableLevels.Length)
		{
			_currentLvlNum = PlayerPrefs.GetInt(_roundForPlaying);
			return PlayerPrefs.GetInt(_roundForPlaying);
		}
		else
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
			_currentLvlNum = PlayerPrefs.GetInt(_roundForPlaying);
			return PlayerPrefs.GetInt(_roundForPlaying);
		}
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
			PlayerPrefs.SetInt(_roundForPlaying, PlayerPrefs.GetInt(_roundForPlaying) + 1);
		}
		PlayerPrefs.SetInt(_overallCompletedLevels, PlayerPrefs.GetInt(_overallCompletedLevels) + 1);
	}
	public bool IsItTimeForBonusLvl()
	{
		if (PlayerPrefs.GetInt(_notBonusLvlsCounter) >= AmountOfLvlsBeforeBonus)
		{
			return true;
		}
		return false;
	}
}
