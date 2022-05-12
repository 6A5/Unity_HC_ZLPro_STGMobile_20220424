using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NkE1
{
    /// <summary>
    /// 控制系統
    /// 虛擬搖桿
    /// </summary>
    public class SystemControl : MonoBehaviour
    {
        [SerializeField, Header("虛擬搖桿")]
        private Joystick joystick;
        [SerializeField, Header("移動速度"), Range(0, 100)]
        private float speed = 3.5f;
        [SerializeField, Header("角色方向圖示")]
        private Transform traDirectionIcon;
        [SerializeField, Header("角色方向圖示範圍"), Range(1f, 2.5f)]
        private float rangeDirectionIcon;
        [SerializeField, Header("角色旋轉速度"), Range(0, 50)]
        private float speedTurn;
        [SerializeField, Header("動畫參數")]
        private string parameterWalk = "Running";

        private Animator ani;
        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            // GetJoystickValue();
            UpdateDirectionIconPos();
            LookDirectionIcon();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// 取得虛擬搖桿
        /// </summary>
        private void GetJoystickValue()
        {
            print("<color=yellow>水平 : " + joystick.Horizontal + "</Color>");
        }

        /// <summary>
        /// 移動功能
        /// </summary>
        private void Move()
        {
            rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
        }

        /// <summary>
        /// 角色移動方向
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            // 圖示位置移動
            Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0.1f, joystick.Vertical) * rangeDirectionIcon;
            traDirectionIcon.position = pos;
        }

        /// <summary>
        /// 面向方向圖示
        /// </summary>
        private void LookDirectionIcon()
        {
            // 取得角度 = 四位元.面向角度(target - rotateObj) => 向量
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            // 插值平滑
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            // 歐拉角 = 0 Y歐拉角 0
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        /// <summary>
        /// 動畫改變
        /// </summary>
        private void UpdateAnimation()
        {
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            ani.SetBool(parameterWalk, run);
        }
    }
}
