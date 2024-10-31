using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace StarForce.Skill

{
	///<summary>
	///
	///<summary>

	public class CharacterMotorData : MonoBehaviour
	{
        private Rigidbody2D rb;
        private CharacterData m_characterData;
        private PlayerController controller;
        private Animator anim; //动画控制器
        private Transform childTransform; //子物体transform
      
        private CharacterSkillSystem skillSystem;
        private Animation animation_m;
       
        [SerializeField]
        private string m_playerName;

        /// <summary>
        /// prefab暂时输入名称
        /// </summary>
        public string PlayerName
        {
            get
            {
                return m_playerName;
            }
        }

        [Header("移动")]
        [SerializeField]
        private float m_moveSpeed ;

        /// <summary>
        /// 移动速度 
        /// </summary>
        public float MoveSpeed
        {
            get
            {
                return m_moveSpeed;
            }
        }

        [SerializeField]
        private float m_jumpHeight ; 

        /// <summary>
        /// 跳跃高度
        /// </summary>
        public float JumpHeight
        {
            get
            {
                return m_jumpHeight;
            }
        }

        [SerializeField]
        private float m_aSpeed; 

        /// <summary>
        /// 重力加速度  
        /// </summary>
        public float ASpeed
        {
            get
            {
                return m_aSpeed;
            }
        }


        [SerializeField]
        private float m_velocity_Y ;

        /// <summary>
        /// Y方向加速度
        /// </summary>
        public float Velocity_Y
        {
            get
            {
                return m_velocity_Y;
            }
        }

        [SerializeField]
        private float m_rateY; 

        /// <summary>
        /// Y方向移动速率
        /// </summary>
        public float RateY
        {
            get
            {
                return m_rateY;
            }
        }

        [SerializeField]
        private float m_rollSpeed; 

        /// <summary>
        /// 翻滚移动速度
        /// </summary>
        public float RollSpeed
        {
            get
            {
                return m_rollSpeed;
            }
        }

        [SerializeField]
        private float m_rollDistance;

        /// <summary>
        /// 翻滚距离
        /// </summary>
        public float RollDistance
        {
            get
            {
                return m_rollDistance;
            }
        }

        [SerializeField]
        private float m_atkMoveSpeed;

        /// <summary>
        /// 攻击时位移距离
        /// </summary>
        public float AtkMoveSpeed
        {
            get
            {
                return m_atkMoveSpeed;
            }
        }

        [SerializeField]
        private float m_faceDirection;//控制朝向

        [SerializeField]
        private int avaiableJumpCount;

        [SerializeField]
        private int m_defaultJumpCount;
        public int DefaultJumpCount 
        {
            get
            {
                return m_defaultJumpCount;
            }
        }

        [SerializeField]
        private float m_chargingSpeedRate;

        /// <summary>
        /// 蓄力速度
        /// </summary>
        public float ChargingSpeedRate
        {
            get
            {
                return m_chargingSpeedRate;
            }
        }

        [SerializeField]
        private bool isGround = true; //是否在地面

        [SerializeField]
        private bool isrolling = false; //是否翻滚状态

        [SerializeField]
        private bool isAttack; //是否攻击

        [SerializeField]
        private bool isCharging;

        [SerializeField]
        private bool isChargingRelease;

        [Header("连击")]
        [SerializeField]
        private float m_cancelAttackTime;

        /// <summary>
        /// 取消攻击指令间隔（避免太快输入）
        /// </summary>
        public float CancelAttackTime
        {
            get
            {
                return m_cancelAttackTime;
            }
        }

        [SerializeField]
        private float m_batterAttackTime; 

        /// <summary>
        /// 连击间隔
        /// </summary>
        public float BatterAttackTime
        {
            get
            {
                return m_batterAttackTime;
            }
        }

        //public float chargingduration = 1f; 蓄力时间，暂时只能在unity中修改，暂时取的默认值0.5s
        [Header("射击")]
        [SerializeField]
        private GameObject circleProjectilePrefab;

        [SerializeField]
        private float m_projectileHeight;   

        /// <summary>
        /// 投射物初始高度修正（离人物足部距离）
        /// </summary>
        public float ProjectileHeight
        {
            get
            {
                return m_projectileHeight;
            }
        }

        //private bool isHurt;
        //private bool dashPressed; //�Ƿ�����
        //public float dashDistance = 5f;//���־���
        //public int dashDefaultCount = 2;//������ִ���
        //private int dashCount; //�����ִ���
        //public int jumpDefaultCount=2; //�����Ծ����
        //public int jumpCount; //����Ծ����
        //private bool jumpPressed; //�Ƿ���Ծ
        //[SerializeField]
        //private LayerMask dashLayerMask; //�������߲�
        //private Vector3 rollDir;//��������
        //public bool rollPressed;

        private void Awake()
        {
            m_characterData = GetComponent<CharacterData>();
            rb = GetComponentInChildren<Rigidbody2D>();
            childTransform = transform.FindChildByName(m_playerName);
            anim = childTransform.GetComponent<Animator>();
            controller = GetComponent<PlayerController>();
            skillSystem = GetComponent<CharacterSkillSystem>();
            //animation_m=GetComponent<Animation>();

        }

        private void Update()
        {
            isGround = childTransform.position.y <= transform.position.y + 0.05f ? true : false;
            m_faceDirection = transform.rotation.y == 0 ? 1 : -1;
            isAttack = controller.isAttacking;
            isChargingRelease = anim.GetBool(m_characterData. attackChargeDown);
        }

        public void Roll()
        {
            //空中或翻滚中或攻击中取消翻滚
            if (!isGround || isrolling || isAttack || isCharging || isChargingRelease) return;
            anim.SetTrigger(m_characterData. roll);
            Vector3 target = new Vector3(transform.position.x + m_faceDirection * RollDistance, transform.position.y);
            StartCoroutine(rollForward(target, m_faceDirection));
            isrolling = false;

        }

        private Vector2 rollPosition;
        private IEnumerator rollForward(Vector3 target, float faceDirection)
        {
            //未攻击且未到达翻滚距离
            while (Vector2.Distance(transform.position, target) >= 0.1f && !isAttack)
            {
                rollPosition = new Vector2((transform.position.x + faceDirection * Time.deltaTime * RollSpeed), transform.position.y);//transform.position + dashDir * dashDistance;
                rb.MovePosition(rollPosition);
                isrolling = true;
                yield return new WaitForFixedUpdate();

            }
            isrolling = false;
        }

        public void Move(Vector2 moveDir)
        {
            moveDir = new Vector2(moveDir.normalized.x, moveDir.normalized.y * RateY);


            if (isrolling) return;

            //攻击时速度
            if (isAttack && isGround)
            {
                rb.velocity = moveDir * MoveSpeed * 50 * Time.fixedDeltaTime * AtkMoveSpeed; return;
            }

            //蓄力时速度
            if (isCharging && isGround)
            {
                rb.velocity = moveDir * MoveSpeed * 50 * Time.fixedDeltaTime * ChargingSpeedRate; return;
            }

            //蓄力释放状态下移动停止
            if (isChargingRelease) return;


            rb.velocity = moveDir * MoveSpeed * 50 * Time.fixedDeltaTime;
            //if (!isGround)
            //{ anim.SetBool(status. run, false); return; }
            if (Vector2.SqrMagnitude(rb.velocity) >= 0.1f)

                anim.SetBool(m_characterData. run, true);


            if (rb.velocity.x >= 0.01f)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }


        public void Jump()
        {
            // if(isCharging) { return; }
            m_velocity_Y += ASpeed * Time.fixedDeltaTime;
            if (childTransform.position.y <= transform.position.y + 0.05f && Velocity_Y < 0)
            {
                m_velocity_Y = 0;
                childTransform.position = transform.position;
            }
            childTransform.Translate(new Vector3(0, m_velocity_Y) * Time.fixedDeltaTime);
        }

        internal void prepareJump()
        {
            // 默认蓄力状态不能跳跃
            if (isCharging) { return; }
            if (isGround)
                //在地面重置可挑次数为默认跳跃次数
                avaiableJumpCount = DefaultJumpCount;
            //在地面或者可挑次数大于零
            if (isGround || (!isGround && avaiableJumpCount > 0))
            {
                m_velocity_Y = Mathf.Sqrt(JumpHeight * -2f * ASpeed);
                avaiableJumpCount -= 1;
            }
        }
        private float lastPressTime = -1;
        internal void attack()
        {
            //攻击中取消攻击
            if (isAttack) return;

            //暂定默认普通攻击id为1001
            if (!isGround)
            {
                skillSystem.AttackUseSkill(1001, false); return;
            }
            //翻滚后攻击暂定id为1001， 后期更改为翻滚攻击
            if (isrolling)
            {
                skillSystem.AttackUseSkill(1001, false); return;
            }

            float intervalTime = Time.time - lastPressTime;
            if (intervalTime < CancelAttackTime) return;
            bool isBatter = intervalTime < BatterAttackTime;
            skillSystem.AttackUseSkill(1001, isBatter);
            lastPressTime = Time.time;
        }

        //蓄力普攻，播放动画，设置蓄力状态
        internal void charging()
        {
            anim.SetBool(m_characterData. attackCharging, true);
            isCharging = true;
            //GetComponent<Animation>()["attackCharging"].wrapMode = WrapMode.ClampForever;
        }

        //蓄力普攻释放，播放动画，设置蓄力状态结束
        internal void attackChargeRelease()
        {
            anim.SetBool(m_characterData. attackCharging, false);
            anim.SetBool(m_characterData. attackChargeDown, true);
            isCharging = false;
        }

        //shoot 主要创建投射物，同时设置投射物高度，投射物表现由投射物上挂的脚本确定。
        internal void shoot()
        {
            GameObjectPool.Instance.CreateObject("CircleProjectile", 
                circleProjectilePrefab, new Vector2(transform.position.x, transform.position.y +m_projectileHeight), transform.rotation);
        }


    }
}