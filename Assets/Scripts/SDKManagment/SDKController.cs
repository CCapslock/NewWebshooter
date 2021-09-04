using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using Facebook.Unity;
using System.Threading.Tasks;
using Firebase;

public class SDKController : MonoBehaviour
{
    public static SDKController Current;
    public static BaseTenjin InstanceTenjin;
    public static int ConversationValue = 0;
    [SerializeField] private string _ISIOSAppKey = "fff1b061";
    [SerializeField] private string _ISAndroidAppKey = null;
    [SerializeField] private string _currentAppKey = null;
    [SerializeField] private string _tenjinApiKey = "3F1TZPVMAKATNQFEJHB3NSD995ZA41QG";
    [SerializeField] private float _interstitialDelay = 30f;
    private bool _isISInitialised = false;
    private float _lastInterstitialTime;
    public static IGetReward RewardInstance = null;

    private int _currentLevelNumber = 1;
    private int _currentOverallLevelNumber = 0;
    private string _currentOverallLevelNumberString = "0";
    private string _currentLevelNumberString = "1";
    private int _previousLevelNumber = -1;
    private float _startLevelTime = 0f;
    private string _tempText;

    private FirebaseApp _app;
    private IronSourceImpressionData _impressionData;


    private void Start()
    {
        //Для создания IronSourceImpressionData требуется json файл, который можно получить по веб запросу (вроде бы)
        //_impressionData = new IronSourceImpressionData(need json file here);

        ConversationValue = 0;
        GameAnalyticsInitialize();

        SubscribeInterstitialEvents();
        SubscribeRewardedEvents();
        SubscribeGameAnalyticEvent();
        FacebookInitialize();
        CheckAttFacebook();
        InitializeIronSource();
        TenjinConnect();

        //Firebase
        FirebaseCheckAndFixDependencies();
        ImpressionSuccessEvent(_impressionData);

    }

    private async void CheckAttFacebook()
    {
        await FaseboockSuckTask();
    }

    private Task FaseboockSuckTask()
    {
        Task task = new Task(() =>
        {
#if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                FB.Mobile.SetAdvertiserTrackingEnabled(UnityEngine.iOS.Device.advertisingTrackingEnabled);
            }
            //Debug.Log("TASKENDED");
#endif
        });
        task.Start();
        return task;
    }

    #region GameAnalytics
    private void GameAnalyticsInitialize()
    {
        GameAnalytics.Initialize();
    }

    private void OnLevelStartEvent(int levelNumber)
    {
        _currentLevelNumber = levelNumber;
        _currentOverallLevelNumber = PlayerPrefs.GetInt("OverallLevels");
        _currentOverallLevelNumberString = _currentOverallLevelNumber.ToString();
        _currentLevelNumberString = _currentLevelNumber.ToString();
        if (_previousLevelNumber > 0)
        {
            GameAnalytics.NewDesignEvent($"LevelTime:{_previousLevelNumber}", Time.time - _startLevelTime);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "LevelTime", _currentOverallLevelNumberString, (int)(Time.time - _startLevelTime));
        }
        _previousLevelNumber = _currentLevelNumber - 1;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, _currentOverallLevelNumberString);
    }

    private void OnLevelFailedEvent()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, _currentOverallLevelNumberString);
    }

    private void OnLevelCompleteEvent()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _currentOverallLevelNumberString);
    }

    private void SubscribeGameAnalyticEvent()
    {
        GameEvents.Current.OnLevelStart += OnLevelStartEvent;
        GameEvents.Current.OnLevelComplete += OnLevelCompleteEvent;
        GameEvents.Current.OnLevelFailed += OnLevelFailedEvent;
    }


    #endregion
    #region FacebookSDK
    private void FacebookInitialize()
    {
        if (!FB.IsInitialized)
        {

            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.LogWarning("Failed to Initialize the Facebook SDK");
        }
    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    #endregion
    #region InitializeIronSourseSDK
    private void InitializeIronSource()
    {
        if (!_isISInitialised)
        {
#if UNITY_ANDROID
            _currentAppKey = _ISAndroidAppKey;
#elif UNITY_IPHONE
                _currentAppKey = _ISIOSAppKey;
#else
            _currentAppKey = _ISIOSAppKey;
            ;
#endif            
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(_currentAppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
            _isISInitialised = true;
            Invoke("LoadInterstitial", 5f);
        }
    }
    #endregion
    #region Interstitial
    private void StartInterstitialOnLevelEnding()
    {
        if (Time.unscaledTime - _lastInterstitialTime > _interstitialDelay)
        {
            ShowInterstitial();
        }
    }

    private void ShowInterstitialInvoke()
    {

    }
    private void LoadInterstitial()
    {
        IronSource.Agent.loadInterstitial();
    }

    private void ShowInterstitial()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            _lastInterstitialTime = Time.unscaledTime;
            IronSource.Agent.showInterstitial();
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "IronSource", _currentOverallLevelNumberString);
            GameAnalytics.NewDesignEvent($"ShowInterstitial:{_currentOverallLevelNumberString}");
        }
        else
        {
            LoadInterstitial();
        }
    }

    private void SubscribeInterstitialEvents()
    {
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        GameEvents.Current.OnInterstitialAsked += StartInterstitialOnLevelEnding;
    }

    private void InterstitialAdReadyEvent()
    {
        //Debug.Log("unity-script: I got InterstitialAdReadyEvent");
    }

    private void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        //Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }

    private void InterstitialAdShowSucceededEvent()
    {
        //Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
        LoadInterstitial();
    }

    private void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        //Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    private void InterstitialAdClickedEvent()
    {
        //Debug.Log("unity-script: I got InterstitialAdClickedEvent");

    }

    private void InterstitialAdOpenedEvent()
    {
        // Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
    }

    private void InterstitialAdClosedEvent()
    {
        //Debug.Log("unity-script: I got InterstitialAdClosedEvent");
        LoadInterstitial();
    }
    #endregion
    #region Rewarded
    private void SubscribeRewardedEvents()
    {
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

        GameEvents.Current.OnAskingRewardedVideo += AskingShowReward;
    }

    private void ShowRewardedVideo()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
            //GameAnalytics.NewDesignEvent($"Ad:Rewarded:{RewardInstance.EventName()}:{_currentLevelNumber}");
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "IronSource", _currentLevelNumberString);
            GameAnalytics.NewDesignEvent($"RewardedVideo:{_currentOverallLevelNumberString}:ClickShow");

        }
    }

    public void AskingShowReward(IGetReward instance)
    {
        SetRewardInstance(instance);
        ShowRewardedVideo();
    }

    private void SetRewardInstance(IGetReward instance)
    {
        RewardInstance = instance;
    }

    private void ClearRewardInstance()
    {
        RewardInstance = null;
    }

    private void RewardedVideoAdOpenedEvent()
    {

    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    private void RewardedVideoAdClosedEvent()
    {

    }
    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available.
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    private void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        UIEvents.Current.RewardedVideoAvailabilityChanged(available);
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    private void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        if (RewardInstance != null)
        {
            RewardInstance.RewardPlayer();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "IronSource", $"InLevel {_currentLevelNumber}, Reward:{RewardInstance.EventName()}");
            GameAnalytics.NewDesignEvent($"RewardedVideo:{_currentOverallLevelNumberString}:RewardReceive:{RewardInstance.EventName()}");
        }
        else
        {
            RewardInstance = null;
        }
        ClearRewardInstance();
    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    private void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        ClearRewardInstance();
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    private void RewardedVideoAdStartedEvent()
    {

    }
    //Invoked when the video ad finishes playing. 
    private void RewardedVideoAdEndedEvent()
    {

    }

    private void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.RewardedVideo, "IronSource", $"{RewardInstance.EventName()}");
        GameAnalytics.NewDesignEvent($"RewardedVideo:{_currentOverallLevelNumberString}:VideoClicked");
    }
    #endregion
    #region Tenjin

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            TenjinConnect();
        }
    }

    private void TenjinConnect()
    {
        InstanceTenjin = Tenjin.getInstance(_tenjinApiKey);
#if UNITY_IOS

      // Tenjin wrapper for requestTrackingAuthorization
      InstanceTenjin.RequestTrackingAuthorizationWithCompletionHandler((status) => {
        Debug.Log("===> App Tracking Transparency Authorization Status: " + status);



        // Sends install/open event to Tenjin
        InstanceTenjin.Connect();

        //UpdateConversationValue(0);

      });

#elif UNITY_ANDROID

        // Sends install/open event to Tenjin
        InstanceTenjin.Connect();

#endif

    }

    private void UpdateConversationValue(int value)
    {
        if (value <= 63 && value >= 0 && value > ConversationValue)
        {
            ConversationValue = value;
            InstanceTenjin.UpdateConversionValue(ConversationValue);
        }

    }

    public void IncreaseConversationValue()
    {
        if (ConversationValue >= 0 && ConversationValue <= 62)
        {
            ConversationValue++;
            InstanceTenjin.UpdateConversionValue(ConversationValue);
        }
    }
    #endregion
    #region Firebase
    //Step 5: Confirm Google Play services version requirements
    private void FirebaseCheckAndFixDependencies()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                _app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log("Firebase is ready to use by app");
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }

    private void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {

        if (impressionData != null)
        {
            Firebase.Analytics.Parameter[] AdParameters = 
            {
                new Firebase.Analytics.Parameter("ad_platform", "ironSource"),
                new Firebase.Analytics.Parameter("ad_source", impressionData.adNetwork),
                new Firebase.Analytics.Parameter("ad_unit_name", impressionData.adUnit),
                new Firebase.Analytics.Parameter("ad_format", impressionData.instanceName),
                new Firebase.Analytics.Parameter("currency","USD"),
                new Firebase.Analytics.Parameter("value", impressionData.revenue.ToString())
            };

            Firebase.Analytics.FirebaseAnalytics.LogEvent("custom_ad_impression", AdParameters);
        }
    }
    #endregion
}