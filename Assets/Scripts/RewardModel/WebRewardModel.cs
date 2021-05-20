public class WebRewardModel : IGetReward
{
    private WebSkinModel _skin;
    public WebSkinModel Skin => _skin;

    public WebRewardModel(WebSkinModel skin)
    {
        _skin = skin;
    }

    public string EventName()
    {
        return "Skin web";
    }

    public void RewardPlayer()
    {
        GameEvents.Current.UnlockWeb(_skin);
        GameEvents.Current.SelectWeb(_skin);
    }
}