using UnityEngine;

public class CoinsController : MonoBehaviour
{
	private string _coinsString = "Coins";
	[SerializeField] private int _curentCoinsAmount;


    private void Awake()
    {
		_curentCoinsAmount = PlayerPrefs.GetInt(_coinsString);
    }

    public int GetCoinsAmount()
	{
		_curentCoinsAmount = PlayerPrefs.GetInt(_coinsString);
		return _curentCoinsAmount;
	}

	public void AddCoins(int coinsAmount)
	{
		_curentCoinsAmount += coinsAmount;
		PlayerPrefs.SetInt(_coinsString, _curentCoinsAmount);
	}

	public void RemoveCoins(int coins)
    {
		_curentCoinsAmount -= coins;
		PlayerPrefs.SetInt(_coinsString, _curentCoinsAmount);
	}
}