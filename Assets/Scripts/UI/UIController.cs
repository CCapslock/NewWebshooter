using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

	public TextMeshProUGUI CoinsText;
	public GameObject InGamePanel;
	public GameObject WinPanel;
	public GameObject LosePanel;
	public Image[] Gradients;
	public TextMeshProUGUI CoinsTextInWinnerPanel;

	private MainMenu _mainMenu;
	private ShopMenu _shopMenu;
	private InGameUI _inGameUI;
	private PauseMenu _pauseMenu;
	private LoseMenu _loseMenu;
	private WinMenu _winMenu;

	private CoinsController _coinsController;
	private Color _tempColor;
	private float _timeOfAddingCoins = 0.5f;
	private int _addingCoinsAmount;


	private void Awake()
	{
		InGamePanel.SetActive(true);
		_coinsController = FindObjectOfType<CoinsController>();
		CoinsText.text = _coinsController.GetCoinsAmount().ToString();
		SetGradientsAlpha(1, 1);

		_mainMenu = GetComponentInChildren<MainMenu>();
		_shopMenu = GetComponentInChildren<ShopMenu>();
		_inGameUI = GetComponentInChildren<InGameUI>();
		_pauseMenu = GetComponentInChildren<PauseMenu>();
		_loseMenu = GetComponentInChildren<LoseMenu>();
		_winMenu = GetComponentInChildren<WinMenu>();
		SwitchUI(UIState.MainMenu);
		//Costyl, need to be changed:
		Time.timeScale = 0;
	}
	public void PauseGame()
	{
		Time.timeScale = 0f;
	}
	public void ContinueGame()
	{
		Time.timeScale = 1f;
	}
	public void NextLVL()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void LoseGame()
	{
		InGamePanel.SetActive(false);
		LosePanel.SetActive(true);
		Time.timeScale = 0f;
	}
	public void ActivateWinPanel(int coinsNum)
	{
		InGamePanel.SetActive(false);
		WinPanel.SetActive(true);
		CoinsTextInWinnerPanel.text = "+0";
		AddMoreCoinsInUI(coinsNum);
	}
	private void AddMoreCoinsInUI(int Amount)
	{
		for (int i = 0; i < Amount; i++)
		{
			Invoke("AddSingleCoin", 0.5f + _timeOfAddingCoins / Amount * i);
		}
	}
	public void SetGradientsAlpha(float maxHP, float currentHP)
	{
		for (int i = 0; i < Gradients.Length; i++)
		{
			_tempColor = Gradients[i].color;
			_tempColor.a = maxHP - currentHP;
			Gradients[i].color = _tempColor;
		}
	}
	private void AddSingleCoin()
	{
		_addingCoinsAmount++;
		CoinsTextInWinnerPanel.text = "+" + _addingCoinsAmount;
	}

	private void SwitchUI(UIState state)
    {
		switch (state)
        {
			case UIState.MainMenu:
				_mainMenu.Show();
				_shopMenu.Hide();
				_inGameUI.Hide();
				_pauseMenu.Hide();
				_loseMenu.Hide();
				_winMenu.Hide();
				break;
			case UIState.Shop:
				_mainMenu.Hide();
				_shopMenu.Show();
				_inGameUI.Hide();
				_pauseMenu.Hide();
				_loseMenu.Hide();
				_winMenu.Hide();
				break;
			case UIState.InGame:
				_mainMenu.Hide();
				_shopMenu.Hide();
				_inGameUI.Show();
				_pauseMenu.Hide();
				_loseMenu.Hide();
				_winMenu.Hide();
				break;
			case UIState.Pause:
				_mainMenu.Hide();
				_shopMenu.Hide();
				_inGameUI.Hide();
				_pauseMenu.Show();
				_loseMenu.Hide();
				_winMenu.Hide();
				break;
			case UIState.Lose:
				_mainMenu.Hide();
				_shopMenu.Hide();
				_inGameUI.Hide();
				_pauseMenu.Hide();
				_loseMenu.Show();
				_winMenu.Hide();
				break;
			case UIState.Win:
				_mainMenu.Hide();
				_shopMenu.Hide();
				_inGameUI.Hide();
				_pauseMenu.Hide();
				_loseMenu.Hide();
				_winMenu.Show();
				break;
		}
    }
}