using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; // �ޥ� PUN API
using Photon.Realtime; // �ޥ� �Y�� API

/// <summary>
/// �j�U�޲z��
/// ���a���U��ԫ��s��}�l�t��ж�
/// </summary>
// MonoBehaviourPunCallbacks >> �s�u�\��^�G���O
// �Ҧp �n�J�j�U��^�G�A���w���{��
public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("�s�u���e��")]
    [SerializeField] GameObject goConnectView;

    [Header("��ԫ��s")]
    [SerializeField] Button btnBattle;

    [Header("�s�u�H��")]
    [SerializeField] Text textCountPlayer;
    //=====|�����s��{�����q���y�{|=====//
    //1.���Ѥ��}����k Public Method
    //2.���s�b�I����]�w�I�s����k�C
    //==========//

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // �s�u�챱��x �b ConnectUsingSettings �����۰ʳs�u
    // override���\�Ƽg�~�Ӫ����O
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=green>1. �w�g�i�J����x</color>");

        // Photon �s�u.�[�J�j�U
        PhotonNetwork.JoinLobby();
    }

    // �[�J�j�U��۰ʰ���
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=green>2. �w�g�i�J�j�U</color>");

        // �}�ҳs�u���s���ʥ\��
        btnBattle.interactable = true;
    }

    public void StartConnect() 
    {
        print("<color=yellow>3. �s�u�}�l�A�[�J�H���ж�</color>");

        // �]�w����}��(���L)
        goConnectView.SetActive(true);

        // �s�u�[�J�H���ж�
        PhotonNetwork.JoinRandomRoom();
    }

    // �[�J�H���ж�����
    // 1. �s�u�~��L�t
    // 2. �S���ж�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>3-5. �[�J�H���ж�����</color>");

        // �ж��]�w
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 5;
        // �إߩж�
        PhotonNetwork.CreateRoom("", ro);
    }

    // �i�J�ж�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=green>4. �[�J�ж����\</color>");
        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount; // ��e�ж��H��
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers; // ��e�ж��̤j�H��

        textCountPlayer.text = currentCount + " / " + maxCount;
    }
}
