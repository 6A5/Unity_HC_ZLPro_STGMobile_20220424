using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements; // �ޥμs�iAPI

namespace NkE1
{
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener // Ads����
    {
        #region ���
        [SerializeField, Header("�ݧ�������"), Range(0, 1000)]
        private int addCoinValue = 100;

        private int coinPlayer;
        /// <summary>
        /// �K�[�������s�i���s
        /// </summary>
        private Button btnAdsAddCoin;

        // ��O�ƾ�ID
        private string gameIdAndroid = "4783699";
        private string gameIdIos = "4783698";
        private string gameId;

        // ��O�s�i�s��
        private string adsIdAndroid = "AddCoin";
        private string adsIdIos = "AddCoin";
        private string adsId;
        #endregion

        /// <summary>
        /// ���a����
        /// </summary>
        private Text txtCoin;

        private void Awake()
        {
            txtCoin = GameObject.Find("PlayerCoin").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("ADS_GetCoin").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);

            InitializeAds();

            #region ��l�Ƽs�iID
#if UNITY_IOS
            // ī�G
            adsId = adsIdIos;
#elif UNITY_ANDROID
            // �w��
            adsId = adsIdAndroid;
#elif UNITY_STANDALONE
            // PC ����
            adsId = adsIdAndroid;
#endif
            #endregion
        }

        #region ��l�ƻP���J�s�i -����-
        /// <summary>
        /// ��l�Ʀ��\
        /// </summary>
        public void OnInitializationComplete()
        {
            print("<color=green>�s�i��l�Ʀ��\</color>");
        }

        /// <summary>
        /// ��l�ƥ���
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message">���ѰT��</param>
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>�s�i��l�ƥ��� ��]: " + message + "</color>");
        }

        /// <summary>
        /// ���J���\
        /// </summary>
        /// <param name="placementId">�s�i���J��ID</param>
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>�s�i���J���\" + placementId +  "</color>");
        }

        /// <summary>
        /// ���J����
        /// </summary>
        /// <param name="placementId">���J��ID</param>
        /// <param name="error"></param>
        /// <param name="message">���ѰT��</param>
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>�s�i���J���� ��]: " + message + "</color>");
        }

        // ��ܥ���
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>�s�i��ܥ��� ��]: " + message + "</color>");
        }
        // ��ܶ}�l
        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>�s�i��ܶ}�l" + placementId + "</color>");
        }
        // ����I��
        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>�s�i����I��" + placementId + "</color>");
        }
        // ��ܧ���
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>�s�i��ܧ���" + placementId + "</color>");

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
        /// ���J�s�i
        /// </summary>
        private void LoadAds()
        {
            print("���J�s�iID: " + adsId);
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        /// <summary>
        /// ��ܼs�i
        /// </summary>
        private void ShowAds()
        {
            Advertisement.Show(adsId, this);
        }

    }
}
