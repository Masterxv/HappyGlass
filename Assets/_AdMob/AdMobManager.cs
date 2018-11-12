using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

/// <summary>
/// Happy Glass. This Project was made in August 2018. If you like the project add me on Facebook and follow me on Instagram to never miss and Update. 
/// Credit: Satyam Parkhi
/// Email: satyamparkhi@gmail.com
/// Facebook : https://www.facebook.com/satyamparkhi
/// Instagram : https://www.instagram.com/satyamparkhi/
/// Whatsapp : +91 7050225661
/// </summary>
public class AdMobManager : MonoBehaviour
{
	public static AdMobManager _AdMobInstance;
	public string bannerAdId, interstitialAdId, rewardVideoAdId;
	public bool  isDebug;
	public  bool isOnTop;
	private static RewardBasedVideoAd rewardBasedVideo;
	private static BannerView bannerView;
	private static InterstitialAd interstitial ;
	// Use this for initialization

	void Awake ()
	{
		if (_AdMobInstance) {
			DestroyImmediate (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			_AdMobInstance = this;
		}
	}
	void Start ()
	{
		loadRewardVideo ();
		loadInterstitial ();
		showBannerAd ();
	}	
	// Update is called once per frame
	void OnGUI ()
	{
		if (isDebug) {
			if (GUI.Button (new Rect (20, 0, 100, 60), "Load Full")) {
				loadInterstitial ();
			}
			if (GUI.Button (new Rect (20, 80, 100, 60), "Load Video")) {
				loadRewardVideo ();
			}
			if (GUI.Button (new Rect (20, 160, 100, 60), "Show Banner")) {
				showBannerAd ();
			}
			if (GUI.Button (new Rect (200, 0, 100, 60), "Show Full")) {
				showInterstitial ();
			}
			if (GUI.Button (new Rect (200, 80, 100, 60), "Show Video")) {
				showRewardVideo ();
			}
			if (GUI.Button (new Rect (200, 160, 100, 60), "Hide Banner")) {
				hideBannerAd ();
			}	
		}
	}
    void onInterstitialEvent (object sender, System.EventArgs args)
	{
		print("OnAdLoaded event received.");
		// Handle the ad loaded event.
	}
	void onInterstitialCloseEvent (object sender, System.EventArgs args)
	{
		print("OnAdLoaded event received.");
		// Handle the ad loaded event.
	}

	void onBannerEvent (string eventName, string msg)
	{
		
	}

	void onRewardedVideoEvent (object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		print("User rewarded with: " + amount.ToString() + " " + type);
        if (Hint)
        {
            for (int i = 0; i < FindObjectOfType<GameManager>().Hint.Length; i++)
            {
                FindObjectOfType<GameManager>().Hint[i].SetActive(true);
            }
            Hint = false;
        }
    }


	public  void showBannerAd ()
	{
		if(isOnTop)
		{
			bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Top);
			AdRequest request = new AdRequest.Builder().Build();
			// Load the banner with the request.
			bannerView.LoadAd(request);
		}
		else
		{
			bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
			AdRequest request = new AdRequest.Builder().Build();
			// Load the banner with the request.
			bannerView.LoadAd(request);
		}
		// Create an empty ad request.

	}
		

	public  void loadInterstitial ()
	{
		interstitial = new InterstitialAd(interstitialAdId);
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}

	public  void showInterstitial ()
	{		
		if (interstitial.IsLoaded()) {            
            interstitial.Show();
			interstitial.OnAdOpening += onInterstitialEvent;
			interstitial.OnAdClosed += onInterstitialCloseEvent;
		}
		else
		{
			loadInterstitial ();
			interstitial.Show();
			interstitial.OnAdOpening += onInterstitialEvent;
			interstitial.OnAdClosed += onInterstitialCloseEvent;
		}
        if (Hint)
        {
            for (int i = 0; i < FindObjectOfType<GameManager>().Hint.Length; i++)
            {
                FindObjectOfType<GameManager>().Hint[i].SetActive(true);
            }
            Hint = false;
        }
    }
    [HideInInspector]
    public bool Hint;

    public  void loadRewardVideo ()
	{
		rewardBasedVideo = RewardBasedVideoAd.Instance;

		AdRequest request = new AdRequest.Builder().Build();
		rewardBasedVideo.LoadAd(request, rewardVideoAdId);
	}

	public  void showRewardVideo ()
	{		
		if (rewardBasedVideo.IsLoaded())
		{

			rewardBasedVideo.Show();
			rewardBasedVideo.OnAdRewarded += onRewardedVideoEvent;
			rewardBasedVideo.OnAdClosed += onRewardedVideoSkippedEvent;
			rewardBasedVideo.OnAdFailedToLoad += onRewardedVideoFailedEvent;
		}
		else
		{
			loadRewardVideo ();
		}
	}
	void onRewardedVideoSkippedEvent (System.Object sender,System.EventArgs args)
	{	

	}
	void onRewardedVideoFailedEvent (System.Object sender,System.EventArgs args)
	{	

	}

	public  void hideBannerAd ()
	{
		bannerView.Hide();
	}


}
