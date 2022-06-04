using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NkE1
{
    /// <summary>
    /// 玩家資訊UI追蹤
    /// </summary>
    public class PlayerUIFollow : MonoBehaviour
    {
        [SerializeField, Header("位移")]
        private Vector3 v3Offset;

        private string namePlayer = "Warrior";
        public Transform traPlayer;

        private void Awake()
        {
            // traPlayer = GameObject.Find(namePlayer).transform;
        }

        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            transform.position = traPlayer.position + v3Offset;
        }
    }
}
