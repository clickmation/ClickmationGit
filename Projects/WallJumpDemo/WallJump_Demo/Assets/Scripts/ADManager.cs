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

    private RewardedAd rewardedAd;

    //private RewardedAd rewardedReviveAd;
    //private RewardedAd rewardedTokenAd;
    //private RewardedAd rewardedCoinAd;

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

        rewardedAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");

        //this.rewardedReviveAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");
        //this.rewardedTokenAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");
        //this.rewardedCoinAd = CreateAndLoadRewardedAd("ca-app-pub-3940256099942544/5224354917");

        //this.rewardedReviveAd.OnUserEarnedReward += HandleUserEarnedReviveReward;
        //this.rewardedTokenAd.OnUserEarnedReward += HandleUserEarnedTokenReward;
        //this.rewardedCoinAd.OnUserEarnedReward += HandleUserEarnedCoinReward;

        RequestBanner();
    }

    public RewardedAd CreateAndLoadRewardedAd (string adUnitId)
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
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
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
        Debug.Log(args.Type);

        switch (rewardType)
        {
            case "Revive":
                GameMaster.gameMaster.Revive();
                break;
            case "Token":
                SaveLoad.saveload.dr.GetReward();
                break;
            case "Coin":
                SaveLoad.saveload.rr.GetReward();
                break;
        }
    }

    //public void HandleUserEarnedReviveReward(object sender, Reward args)
    //{
    //    string type = args.Type;
    //    double amount = args.Amount;
    //    MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
    //    Debug.Log(args.Type);


    //    GameMaster.gameMaster.Revive();
    //}
    //public void HandleUserEarnedTokenReward(object sender, Reward args)
    //{
    //    string type = args.Type;
    //    double amount = args.Amount;
    //    MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
    //    Debug.Log(args.Type);


    //    SaveLoad.saveload.dr.GetReward();
    //}
    //public void HandleUserEarnedCoinReward(object sender, Reward args)
    //{
    //    string type = args.Type;
    //    double amount = args.Amount;
    //    MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
    //    Debug.Log(args.Type);


    //    SaveLoad.saveload.rr.GetReward();
    //}

    string rewardType;
    public void ShowRewardedAd(string rt)
    {
        if (this.rewardedAd.IsLoaded())
        {
            rewardType = rt;
            this.rewardedAd.Show();
        }
    }

    //public void ShowReviveRewardedAd()
    //{
    //    if (this.rewardedReviveAd.IsLoaded())
    //    {
    //        this.rewardedReviveAd.Show();
    //    }
    //}

    //public void ShowTokenRewardedAd()
    //{
    //    if (this.rewardedTokenAd.IsLoaded())
    //    {
    //        this.rewardedTokenAd.Show();
    //    }
    //}

    //public void ShowCoinRewardedAd()
    //{
    //    if (this.rewardedCoinAd.IsLoaded())
    //    {
    //        this.rewardedCoinAd.Show();
    //    }
    //}

    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }
}
