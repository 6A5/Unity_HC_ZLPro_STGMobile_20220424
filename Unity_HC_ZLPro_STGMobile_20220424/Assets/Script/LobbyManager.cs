using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �j�U�޲z��
/// ���a���U��ԫ��s��}�l�t��ж�
/// </summary>
public class LobbyManager : MonoBehaviour
{

    [SerializeField] GameObject goConnectView;
    
    //=====|�����s��{�����q���y�{|=====//
    //1.���Ѥ��}����k Public Method
    //2.���s�b�I����]�w�I�s����k�C
    //==========//

    public void StartConnect() 
    {
        print("�}�l�s�u......");

        // �]�w����}��(���L)
        goConnectView.SetActive(true);
    }
}
