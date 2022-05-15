using Photon.Pun;
using System.Collections;
using System.Collections.Generic; // �ޥ� �t�ΤΤ@��(��Ƶ��c List ArrayList)
using System.Linq; // �ޥ� �t�άd�߻y��(��Ƶ��c�ഫ)
using UnityEngine;


namespace NkE1 
{
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField, Header("���⪫��")]
        private GameObject goCharacter;

        [SerializeField, Header("���a�ͦ��y�Ъ���")]
        private Transform[] traSpawnPoint;

        /// <summary>
        /// �x�s�y�Хͦ��M��
        /// </summary>
        private List<Transform> traSpawnPointList;

        private void Awake()
        {
            traSpawnPointList = new List<Transform>();
            traSpawnPointList = traSpawnPoint.ToList();

            // �p�G�O�s�u�i�J�����a �ͦ�����
            if (photonView.IsMine)
            {
                // ���o�M�椤�䤤�@��Index (0 , �M�����) -- Unity��API���|��̤j��
                int index = Random.Range(0, traSpawnPointList.Count);
                // �ھ�Index���XTra
                Transform tra = traSpawnPointList[index];

                PhotonNetwork.Instantiate(goCharacter.name, Vector3.zero, Quaternion.identity);

                // ����List����Index��Tra
                traSpawnPointList.RemoveAt(index);
            }
        }
    }
}
