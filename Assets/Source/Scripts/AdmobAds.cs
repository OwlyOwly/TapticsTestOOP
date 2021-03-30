using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobAds : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string appID, rewardedAdID;

    [SerializeField] private Timer timer;
    [SerializeField] private GameController controller;


    private void Start()
    {
        appID = "ca-app-pub-8031020776871928~6802399658";
        rewardedAdID = "ca-app-pub-3940256099942544/5224354917";
        MobileAds.Initialize(initStatus => { });
        RequestRewardedAd();
    }

    public void RequestRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdID);
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        timer.AddBonusTime();
        controller.GiveBonusTry();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestRewardedAd();
    }
}
