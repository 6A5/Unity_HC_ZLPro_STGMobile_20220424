using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NkE1
{
    /// <summary>
    /// ���a��TUI�l��
    /// </summary>
    public class PlayerUIFollow : MonoBehaviour
    {
        [SerializeField, Header("�첾")]
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
