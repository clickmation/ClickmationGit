using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class ADManager : MonoBehaviour
{
    public static ADManager adManager;

    private string appID = "ca-app-pub-3575537359490368~8192022467";

    private BannerView bannerView;
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";

    private RewardedAd rewardedReviveAd;
    private RewardedAd rewardedTokenAd;
    private RewardedAd rewardedCoinAd;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (adManager == null)
        {
            adManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        MobileAds.Initialize(appID);

        this.rewardedReviveAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");
        this.rewardedTokenAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");
        this.rewardedCoinAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");

        this.rewardedReviveAd.OnUserEarnedReward += HandleUserEarnedReviveReward;
        this.rewardedTokenAd.OnUserEarnedReward += HandleUserEarnedTokenReward;
        this.rewardedCoinAd.OnUserEarnedReward += HandleUserEarnedCoinReward;
    }

    public RewardedAd CreateAndLoadRewardedAd (string adUnitId)
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");

        this.rewardedReviveAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");
        this.rewardedTokenAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");
        this.rewardedCoinAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");

        this.rewardedReviveAd.OnUserEarnedReward += HandleUserEarnedReviveReward;
        this.rewardedTokenAd.OnUserEarnedReward += HandleUserEarnedTokenReward;
        this.rewardedCoinAd.OnUserEarnedReward += HandleUserEarnedCoinReward;
    }

    public void HandleUserEarnedReviveReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
        Debug.Log(args.Type);

        GameMaster.gameMaster.Revive();
        AdRequest request = new AdRequest.Builder().Build();
        rewardedReviveAd.LoadAd(request);
    }
    public void HandleUserEarnedTokenReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
        Debug.Log(args.Type);

        SaveLoad.saveload.dr.GetReward();

        AdRequest request = new AdRequest.Builder().Build();
        rewardedTokenAd.LoadAd(request);
    }
    public void HandleUserEarnedCoinReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
        Debug.Log(args.Type);

        SaveLoad.saveload.rr.GetReward();

        AdRequest request = new AdRequest.Builder().Build();
        rewardedCoinAd.LoadAd(request);
    }

    public void ShowReviveRewardedAd()
    {
        if (this.rewardedReviveAd.IsLoaded())
        {
            this.rewardedReviveAd.Show();
        }
    }

    public void ShowTokenRewardedAd()
    {
        if (this.rewardedTokenAd.IsLoaded())
        {
            this.rewardedTokenAd.Show();
        }
    }

    public void ShowCoinRewardedAd()
    {
        if (this.rewardedCoinAd.IsLoaded())
        {
            this.rewardedCoinAd.Show();
        }
    }

    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();
    }

    public void HideBanner()
    {
        if (bannerView != null) bannerView.Hide();
    }
}
