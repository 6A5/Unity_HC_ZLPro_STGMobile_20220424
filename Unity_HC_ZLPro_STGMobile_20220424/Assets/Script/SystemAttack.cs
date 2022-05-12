using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NkE1
{
    /// <summary>
    /// �����t��
    /// </summary>
    public class SystemAttack : MonoBehaviour
    {
        [SerializeField, Header("�o�g�l�u")]
        private Button btnFire;
        [SerializeField, Header("�l�u")]
        private GameObject goProjectile;
        [SerializeField, Header("�̤j�ƶq")]
        private int projectileCountMax = 3;
        [SerializeField, Header("�l�u��m")]
        private Transform traFire;
        [SerializeField, Header("�l�u�o�g�t��"), Range(0, 3000)]
        private int speedFire = 500;

        private int projectileCountCurrent;

        private void Awake()
        {
            btnFire.onClick.AddListener(Fire);
        }

        /// <summary>
        /// �}�j
        /// </summary>
        private void Fire()
        {
            Instantiate(goProjectile, traFire.position, Quaternion.identity);
        }
    }
}
