using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements; // 引用廣告API

namespace NkE1
{
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener // Ads介面
    {
        #region 資料
        [SerializeField, Header("看完的金幣"), Range(0, 1000)]
        private int addCoinValue = 100;

        private int coinPlayer;
        /// <summary>
        /// 添加金幣的廣告按鈕
        /// </summary>
        private Button btnAdsAddCoin;

        // 後臺數據ID
        private string gameIdAndroid = "4783699";
        private string gameIdIos = "4783698";
        private string gameId;

        // 後臺廣告編號
        private string adsIdAndroid = "AddCoin";
        private string adsIdIos = "AddCoin";
        private string adsId;
        #endregion

        /// <summary>
        /// 玩家金幣
        /// </summary>
        private Text txtCoin;

        private void Awake()
        {
            txtCoin = GameObject.Find("PlayerCoin").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("ADS_GetCoin").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);

            InitializeAds();

            #region 初始化廣告ID
#if UNITY_IOS
            // 蘋果
            adsId = adsIdIos;
#elif UNITY_ANDROID
            // 安卓
            adsId = adsIdAndroid;
#elif UNITY_STANDALONE
            // PC 測試
            adsId = adsIdAndroid;
#endif
            #endregion
        }

        #region 初始化與載入廣告 -介面-
        /// <summary>
        /// 初始化成功
        /// </summary>
        public void OnInitializationComplete()
        {
            print("<color=green>廣告初始化成功</color>");
        }

        /// <summary>
        /// 初始化失敗
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message">失敗訊息</param>
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>廣告初始化失敗 原因: " + message + "</color>");
        }

        /// <summary>
        /// 載入成功
        /// </summary>
        /// <param name="placementId">廣告載入的ID</param>
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>廣告載入成功" + placementId +  "</color>");
        }

        /// <summary>
        /// 載入失敗
        /// </summary>
        /// <param name="placementId">載入的ID</param>
        /// <param name="error"></param>
        /// <param name="message">失敗訊息</param>
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>廣告載入失敗 原因: " + message + "</color>");
        }

        // 顯示失敗
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>廣告顯示失敗 原因: " + message + "</color>");
        }
        // 顯示開始
        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>廣告顯示開始" + placementId + "</color>");
        }
        // 顯示點擊
        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>廣告顯示點擊" + placementId + "</color>");
        }
        // 顯示完成
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>廣告顯示完成" + placementId + "</color>");

            coinPlayer += addCoinValue;
            txtCoin.text = coinPlayer.ToString();
        }
        #endregion

        private void InitializeAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true, this);
        }

        /// <summary>
        /// 載入廣告
        /// </summary>
        private void LoadAds()
        {
            print("載入廣告ID: " + adsId);
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        /// <summary>
        /// 顯示廣告
        /// </summary>
        private void ShowAds()
        {
            Advertisement.Show(adsId, this);
        }

    }
}
