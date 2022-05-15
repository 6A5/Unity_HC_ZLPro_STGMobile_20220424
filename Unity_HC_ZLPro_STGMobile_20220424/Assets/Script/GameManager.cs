using Photon.Pun;
using System.Collections;
using System.Collections.Generic; // 引用 系統及一般(資料結構 List ArrayList)
using System.Linq; // 引用 系統查詢語言(資料結構轉換)
using UnityEngine;


namespace NkE1 
{
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField, Header("角色物件")]
        private GameObject goCharacter;

        [SerializeField, Header("玩家生成座標物件")]
        private Transform[] traSpawnPoint;

        /// <summary>
        /// 儲存座標生成清單
        /// </summary>
        private List<Transform> traSpawnPointList;

        private void Awake()
        {
            traSpawnPointList = new List<Transform>();
            traSpawnPointList = traSpawnPoint.ToList();

            // 如果是連線進入的玩家 生成物件
            if (photonView.IsMine)
            {
                // 取得清單中其中一個Index (0 , 清單長度) -- Unity的API不會到最大值
                int index = Random.Range(0, traSpawnPointList.Count);
                // 根據Index取出Tra
                Transform tra = traSpawnPointList[index];

                PhotonNetwork.Instantiate(goCharacter.name, Vector3.zero, Quaternion.identity);

                // 移除List中的Index的Tra
                traSpawnPointList.RemoveAt(index);
            }
        }
    }
}
