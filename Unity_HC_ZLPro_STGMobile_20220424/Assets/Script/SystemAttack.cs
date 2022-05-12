using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NkE1
{
    /// <summary>
    /// 攻擊系統
    /// </summary>
    public class SystemAttack : MonoBehaviour
    {
        [SerializeField, Header("發射子彈")]
        private Button btnFire;
        [SerializeField, Header("子彈")]
        private GameObject goProjectile;
        [SerializeField, Header("最大數量")]
        private int projectileCountMax = 3;
        [SerializeField, Header("子彈位置")]
        private Transform traFire;
        [SerializeField, Header("子彈發射速度"), Range(0, 3000)]
        private int speedFire = 500;

        private int projectileCountCurrent;

        private void Awake()
        {
            btnFire.onClick.AddListener(Fire);
        }

        /// <summary>
        /// 開槍
        /// </summary>
        private void Fire()
        {
            Instantiate(goProjectile, traFire.position, Quaternion.identity);
        }
    }
}
