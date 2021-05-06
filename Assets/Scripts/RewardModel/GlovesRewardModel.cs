public class GlovesRewardModel : IGetReward
{
    private GlovesSkinModel _skin;
    public GlovesSkinModel Skin => _skin;

    public GlovesRewardModel(GlovesSkinModel skin)
    {
        _skin = skin;
    }

    public void RewardPlayer()
    {
        GameEvents.Current.UnlockGloves(_skin);
    }

    public string EventName()
    {
        return "Skin gloves";
    }
}