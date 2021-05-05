using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController Current;

	public Image[] Gradients;

	private MainMenu _mainMenu;
	private ShopMenu _shopMenu;
	private InGameUI _inGameUI;
	private PauseMenu _pauseMenu;
	private LoseMenu _loseMenu;
	private WinMenu _winMenu;

	private CoinsController _coinsController;
	private SaveController _saveController;
	private Color _tempColor;


	private void Awake()
	{
		if (Current != null)
		{
			Destroy(this.gameObject);
			return;
		}

		DontDestroyOnLoad(this.gameObject);
		Current = this;

		ResetUI();



		_mainMenu = GetComponentInChildren<MainMenu>();
		_shopMenu = GetComponentInChildren<ShopMenu>();
		_inGameUI = GetComponentInChildren<InGameUI>();
		_pauseMenu = GetComponentInChildren<PauseMenu>();
		_loseMenu = GetComponentInChildren<LoseMenu>();
		_winMenu = GetComponentInChildren<WinMenu>();
		OpenMainMenu();
		//Costyl, need to be changed:
		Time.timeScale = 0;
	}

    private void Start()
    {
		UIEvents.Current.OnButtonLevelStart += StartLevel;
		UIEvents.Current.OnButtonPause += PauseGame;
		UIEvents.Current.OnButtonResume += ContinueGame;
		UIEvents.Current.OnButtonNextLevel += NextLVL;
		UIEvents.Current.OnButtonRestart += NextLVL;
		UIEvents.Current.OnButtonShop += OpenShop;
		UIEvents.Current.OnButtonMainMenu += OpenMainMenu;
		UIEvents.Current.OnButtonBuySkinGloves += BuyGloves;
		UIEvents.Current.OnButtonBuySkinNet += BuyWeb;
		UIEvents.Current.OnButtonGetSkinGloves += GetGloves;
		UIEvents.Current.OnButtonGetSkinNet += GetWeb;        
    }

    private void ResetUI()
    {
		_coinsController = FindObjectOfType<CoinsController>();
		_saveController = FindObjectOfType<SaveController>();
		SetGradientsAlpha(1, 1);
	}

	private void OpenShop()
    {
		SwitchUI(UIState.Shop);
    }
	private void OpenMainMenu()
    {
		SwitchUI(UIState.MainMenu);
    }
	private void StartLevel()
    {
		Time.timeScale = 1f;
		SwitchUI(UIState.InGame);
		GameEvents.Current.LevelStart(_saveController.GetCurrentLvlNum());
    }
	private void PauseGame()
	{
		Time.timeScale = 0f;
		SwitchUI(UIState.Pause);
	}
	private void ContinueGame()
	{
		Time.timeScale = 1f;
		SwitchUI(UIState.InGame);
	}
	public void NextLVL()
	{
		SwitchUI(UIState.MainMenu);
		Time.timeScale = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		ResetUI();
	}
	public void LoseGame()
	{
		Time.timeScale = 0f;
		SwitchUI(UIState.Lose);
		GameEvents.Current.LevelEnd();
		GameEvents.Current.LevelFailed();
	}
	public void ActivateWinPanel(int coinsNum)
	{
		SwitchUI(UIState.Win);
		_winMenu.ActivatePanel(coinsNum);
		GameEvents.Current.LevelEnd();
		GameEvents.Current.LevelComplete();
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

	public int GetCurrentCoins()
    {
		return _coinsController.GetCoinsAmount();
    }

	private void BuyGloves(GlovesSkinModel skin)
    {
		if (_coinsController.GetCoinsAmount() >= _shopMenu.SkinPrice)
        {
			_coinsController.RemoveCoins(_shopMenu.SkinPrice);
			GameEvents.Current.UnlockGloves(skin);
        }
    }
	private void BuyWeb(WebSkinModel skin)
    {
		if (_coinsController.GetCoinsAmount() >= _shopMenu.SkinPrice)
        {
			_coinsController.RemoveCoins(_shopMenu.SkinPrice);
			GameEvents.Current.UnlockWeb(skin);
		}
	}
	private void GetGloves(GlovesSkinModel skin)
    {
		GameEvents.Current.AskingRewardedVideo(new GlovesRewardModel(skin));
    }
	private void GetWeb(WebSkinModel skin)
    {
		GameEvents.Current.AskingRewardedVideo(new WebRewardModel(skin));
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