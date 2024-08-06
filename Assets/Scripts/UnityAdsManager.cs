using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidGameId = "5664269";
    [SerializeField] private string iosGameId = "5664268";
    [SerializeField] private string adUnitIdAndroid = "Rewarded_Android";
    [SerializeField] private string adUnitIdiOS = "Rewarded_iOS";
    private string gameId;

    private string adUnitId;
    private bool testMode = true;

    void Start()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iosGameId
            : androidGameId;
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void ShowRewardedVideo()

    {

        adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? adUnitIdiOS
            : adUnitIdAndroid;
        Advertisement.Load(adUnitId, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad Loaded: {adUnitId}");
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Unity Ads Rewarded Video Completed - give reward to the player");
            // Reward the user here

            Engine.engine.addExtraLife();
            Engine.engine.closeGameOverMenu();
        }
        else
        {
            Debug.Log("Unity Ads Rewarded Video not completed.");
        }
    }
}