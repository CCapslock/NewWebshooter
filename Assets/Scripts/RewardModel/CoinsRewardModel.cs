public class CoinsRewardModel : IGetReward
{
    private int _coins;
    public int Coins => _coins;

    public CoinsRewardModel(int coins)
    {
        _coins = coins;
    }

    public string EventName()
    {
        return "Coins";
    }

    public void RewardPlayer()
    {
        GameEvents.Current.IncreaseCoins(_coins);
    }
}