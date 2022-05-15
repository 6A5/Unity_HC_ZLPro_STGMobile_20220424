using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NkE1
{
    /// <summary>
    /// 控制系統
    /// 虛擬搖桿
    /// </summary>
    public class SystemControl : MonoBehaviourPun
    {
        [SerializeField, Header("移動速度"), Range(0, 100)]
        private float speed = 3.5f;
        [SerializeField, Header("角色方向圖示範圍"), Range(1f, 2.5f)]
        private float rangeDirectionIcon;
        [SerializeField, Header("角色旋轉速度"), Range(0, 50)]
        private float speedTurn;
        [SerializeField, Header("動畫參數")]
        private string parameterWalk = "Running";
        [SerializeField, Header("畫布")]
        private GameObject m_canvas;
        [SerializeField, Header("玩家資訊")]
        private GameObject m_info;
        [SerializeField, Header("玩家面向")]
        private GameObject m_direction;

        private Animator ani;
        private Rigidbody rig;
        private Joystick joystick;
        private Transform traDirectionIcon;
        private CinemachineVirtualCamera cvc;
        private SystemAttack systemAttack;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();
            systemAttack = GetComponent<SystemAttack>();

            if (photonView.IsMine)
            {
                PlayerUIFollow follow = Instantiate(m_info).GetComponent<PlayerUIFollow>();
                follow.traPlayer = transform;

                // 取得搖桿
                GameObject tempCanvas = Instantiate(m_canvas);
                joystick = tempCanvas.transform.Find("Floating Joystick").GetComponent<Joystick>();
                systemAttack.btnFire = tempCanvas.transform.Find("Fire").GetComponent<Button>();

                Instantiate(m_info);

                // 取得方向圖示
                traDirectionIcon = Instantiate(m_direction).transform;

                // 取得攝影機管理器
                cvc = GameObject.Find("CM Manager").GetComponent<CinemachineVirtualCamera>();
                // 指定追蹤物件
                cvc.Follow = transform;


            }
            // 避免控制到別的物件
            else 
            {
                enabled = false;
            }
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
