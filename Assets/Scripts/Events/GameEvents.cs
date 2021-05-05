using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;

    public void Awake()
    {
        if (Current != null)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Current = this;        
    }

    #region GeneralEvents



    #endregion

    #region LevelEvents

    public Action OnLevelLoaded;
    public void LevelLoaded()
    {
        OnLevelLoaded?.Invoke();
    }

    public Action<int> OnLevelStart;
    public void LevelStart(int levelNumber) //������ ����� �� ������ ������
    {
        OnLevelStart?.Invoke(levelNumber);
    }

    public Action OnLevelEnd; //����� ������ ��� ����� ������/���������
    public void LevelEnd()
    {
        OnLevelEnd?.Invoke();
    }

    public Action OnLevelComplete; //������ ��������� ��������� ui
    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public Action OnLevelFailed; //������ ��������� ��������� ui
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }

    #endregion
    #region SkinShop
    public Action<GlovesSkinModel> OnUnlockGloves;
    public void UnlockGloves(GlovesSkinModel skin)
    {
        OnUnlockGloves?.Invoke(skin);
    }

    public Action<GlovesSkinModel> OnSelectGloves;
    public void SelectGloves(GlovesSkinModel skin)
    {
        OnSelectGloves?.Invoke(skin);
    }

    public Action<WebSkinModel> OnUnlockWeb;
    public void UnlockWeb(WebSkinModel skin)
    {
        OnUnlockWeb?.Invoke(skin);
    }

    public Action<WebSkinModel> OnSelectWeb;
    public void SelectWeb(WebSkinModel skin)
    {
        OnSelectWeb?.Invoke(skin);
    }

    public Action<IGetReward> OnAskingRewardedVideo;
    public void AskingRewardedVideo(IGetReward reward)
    {
        OnAskingRewardedVideo?.Invoke(reward);
    }
    #endregion

    #region GoblinEvents
    public Action<GameObject> OnThrowingBomb;
    public void ThrowingBomb(GameObject bomb)
    {
        OnThrowingBomb?.Invoke(bomb);
    }

    public Action OnGoblinTakeDamage;
    public void GoblinTakeDamage()
    {
        OnGoblinTakeDamage?.Invoke();
    }
    #endregion
}
