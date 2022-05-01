using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; // 引用 PUN API
using Photon.Realtime; // 引用 即時 API

/// <summary>
/// 大廳管理器
/// 玩家按下對戰按鈕後開始配對房間
/// </summary>
// MonoBehaviourPunCallbacks >> 連線功能回乎類別
// 例如 登入大廳後回乎你指定的程式
public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("連線中畫面")]
    [SerializeField] GameObject goConnectView;

    [Header("對戰按鈕")]
    [SerializeField] Button btnBattle;

    [Header("連線人數")]
    [SerializeField] Text textCountPlayer;
    //=====|讓按鈕跟程式溝通的流程|=====//
    //1.提供公開的方法 Public Method
    //2.按鈕在點擊後設定呼叫此方法。
    //==========//

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // 連線到控制台 在 ConnectUsingSettings 執行後自動連線
    // override允許複寫繼承的類別
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=green>1. 已經進入控制台</color>");

        // Photon 連線.加入大廳
        PhotonNetwork.JoinLobby();
    }

    // 加入大廳後自動執行
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=green>2. 已經進入大廳</color>");

        // 開啟連線按鈕互動功能
        btnBattle.interactable = true;
    }

    public void StartConnect() 
    {
        print("<color=yellow>3. 連線開始，加入隨機房間</color>");

        // 設定物件開關(布林)
        goConnectView.SetActive(true);

        // 連線加入隨機房間
        PhotonNetwork.JoinRandomRoom();
    }

    // 加入隨機房間失敗
    // 1. 連線品質過差
    // 2. 沒有房間
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>3-5. 加入隨機房間失敗</color>");

        // 房間設定
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 5;
        // 建立房間
        PhotonNetwork.CreateRoom("", ro);
    }

    // 進入房間
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=green>4. 加入房間成功</color>");
        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount; // 當前房間人數
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers; // 當前房間最大人數

        textCountPlayer.text = currentCount + " / " + maxCount;
    }
}
