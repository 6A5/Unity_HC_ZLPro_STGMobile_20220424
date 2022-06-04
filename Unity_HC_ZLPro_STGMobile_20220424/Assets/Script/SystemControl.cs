using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NkE1
{
    /// <summary>
    /// ����t��
    /// �����n��
    /// </summary>
    public class SystemControl : MonoBehaviourPun
    {
        [SerializeField, Header("���ʳt��"), Range(0, 100)]
        private float speed = 3.5f;
        [SerializeField, Header("�����V�ϥܽd��"), Range(1f, 2.5f)]
        private float rangeDirectionIcon;
        [SerializeField, Header("�������t��"), Range(0, 50)]
        private float speedTurn;
        [SerializeField, Header("�ʵe�Ѽ�")]
        private string parameterWalk = "Running";
        [SerializeField, Header("�e��")]
        private GameObject m_canvas;
        [SerializeField, Header("���a��T")]
        private GameObject m_info;
        [SerializeField, Header("���a���V")]
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

                // ���o�n��
                GameObject tempCanvas = Instantiate(m_canvas);
                joystick = tempCanvas.transform.Find("Floating Joystick").GetComponent<Joystick>();
                systemAttack.btnFire = tempCanvas.transform.Find("Fire").GetComponent<Button>();

                Instantiate(m_info);

                // ���o��V�ϥ�
                traDirectionIcon = Instantiate(m_direction).transform;

                // ���o��v���޲z��
                cvc = GameObject.Find("CM Manager").GetComponent<CinemachineVirtualCamera>();
                // ���w�l�ܪ���
                cvc.Follow = transform;


            }
            // �קK�����O������
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
        /// ���o�����n��
        /// </summary>
        private void GetJoystickValue()
        {
            print("<color=yellow>���� : " + joystick.Horizontal + "</Color>");
        }

        /// <summary>
        /// ���ʥ\��
        /// </summary>
        private void Move()
        {
            rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
        }

        /// <summary>
        /// ���Ⲿ�ʤ�V
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            // �ϥܦ�m����
            Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0.1f, joystick.Vertical) * rangeDirectionIcon;
            traDirectionIcon.position = pos;
        }

        /// <summary>
        /// ���V��V�ϥ�
        /// </summary>
        private void LookDirectionIcon()
        {
            // ���o���� = �|�줸.���V����(target - rotateObj) => �V�q
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            // ���ȥ���
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            // �کԨ� = 0 Y�کԨ� 0
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        /// <summary>
        /// �ʵe����
        /// </summary>
        private void UpdateAnimation()
        {
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            ani.SetBool(parameterWalk, run);
        }
    }
}
