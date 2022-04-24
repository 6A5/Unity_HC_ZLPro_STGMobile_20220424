using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 大廳管理器
/// 玩家按下對戰按鈕後開始配對房間
/// </summary>
public class LobbyManager : MonoBehaviour
{

    [SerializeField] GameObject goConnectView;
    
    //=====|讓按鈕跟程式溝通的流程|=====//
    //1.提供公開的方法 Public Method
    //2.按鈕在點擊後設定呼叫此方法。
    //==========//

    public void StartConnect() 
    {
        print("開始連線......");

        // 設定物件開關(布林)
        goConnectView.SetActive(true);
    }
}
