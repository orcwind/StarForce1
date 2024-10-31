using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using StarForce.Skill;
using UnityGameFramework.Runtime; 





namespace StarForce
{
    /// <summary>
    /// 马达
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        private int characterID = 10011;

        private Rigidbody2D rb;
        
        [SerializeField]
        private CharacterData m_characterData;
        private PlayerController controller;
        private Animator anim; //动画控制器
        private Transform childTransform; //
        private string playerName = "HeroKnight";//prefab暂时输入名称
        private CharacterSkillSystem skillSystem;
       private Animation animation_m;

        [Header("移动")]
        public float targetSpeed = 3f; //移动速度 
        public float jumpHeight = 0.7f; //跳跃高度
        public float aSpeed = -15f; //重力加速度     
        private float velocity_Y=0;
        public float rateY = 0.5f; //Y方向移动速率
        public float rollSpeed = 3f; //翻滚速度
        public float rollDistance = 2.5f;//翻滚距离
        public float atkMoveSpeed = 0.2f;//攻击时位移距离
        private float faceDirection;//控制朝向
        private int avaiableJumpCount ;
        public  int defaultJumpCount = 1;
        private float chargingSpeedRate=0.5f;

        public bool  isGround=true; //是否在地面
        public bool isrolling=false; //是否翻滚状态
        private bool isAttack; //是否攻击
        public bool isCharging;
        public bool isChargingRelease;
        [Header("连击")]
        public float cancelAttackTime = 0.3f; //取消攻击指令间隔（避免太快输入）
        public float batterAttackTime = 3f; //连击间隔
        //public float chargingduration = 1f; 蓄力时间，暂时只能在unity中修改，暂时取的默认值0.5s
        [Header("射击")]
        public GameObject circleProjectilePrefab;
        //投射物初始高度修正（离人物脚步距离）
        public float projectileHeight = 0.4f;


        //private bool isHurt;
        //private bool dashPressed; //Ƿ
        //public float dashDistance = 5f;//־
        //public int dashDefaultCount = 2;//ִ
        //private int dashCount; //ִ
        //public int jumpDefaultCount=2; //Ԯ
        //public int jumpCount; //Ԯ
        //private bool jumpPressed; //ǷԮ
        //[SerializeField]
        //private LayerMask dashLayerMask; //߲
        //private Vector3 rollDir;//
        //public bool rollPressed;

        private Character m_Character;
        private Player m_Player;

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            // 延迟初始化，等待所有组件都准备好
            StartCoroutine(InitializeComponents());
        }

        private IEnumerator InitializeComponents()
        {
            // 等待一帧，确保其他组件都已初始化
            yield return null;

            // 获取Character或Player组件
            m_Character = GetComponent<Character>();
            m_Player = GetComponent<Player>();

            if (m_Character == null && m_Player == null)
            {
                Log.Error("Neither Character nor Player component found!");
                yield break;
            }

            // 初始化
            rb = GetComponentInChildren<Rigidbody2D>();
            if (rb == null)
            {
                Log.Error("Rigidbody2D component is missing!");
                yield break;
            }

            childTransform = transform.FindChildByName(playerName);
            if (childTransform == null)
            {
                Log.Error($"Child transform '{playerName}' not found!");
                yield break;
            }

            anim = GetComponentInChildren<Animator>();
            if (anim == null)
            {
                Log.Error("Animator component is missing!");
                yield break;
            }

            controller = GetComponent<PlayerController>();
            if (controller == null)
            {
                Log.Error("PlayerController component is missing!");
                yield break;
            }

            skillSystem = GetComponent<CharacterSkillSystem>();
            if (skillSystem == null)
            {
                Log.Error("CharacterSkillSystem component is missing!");
                yield break;
            }

            animation_m = GetComponent<Animation>();
            
            // 初始化角色数据
            m_characterData = new PlayerData(GameEntry.Entity.GenerateSerialId(), characterID);
            
            // 通知PlayerController设置PlayerData
            controller.SetPlayerData((PlayerData)m_characterData);

            // 所有组件都初始化完成后
            IsInitialized = true;
            
            yield break;
        }
 
        private void Update()
        {   
            if (!gameObject.activeInHierarchy || childTransform == null)
                return;

            isGround = childTransform.position.y <= transform.position.y + 0.05f;
            faceDirection = transform.rotation.y == 0 ? 1 : -1;
            isAttack = controller != null && controller.isAttacking;
            
            if (anim != null)
            {
                isChargingRelease = anim.GetBool(m_characterData.attackChargeDown);
            }
        }
                
        public void Roll()
        {
            //空中或翻滚中或攻击中取消翻滚
            if (!isGround || isrolling || isAttack || isCharging || isChargingRelease) return;
            anim.SetTrigger(m_characterData. roll);
            Vector3 target = new Vector3(transform.position.x + faceDirection * rollDistance, 
            transform.position.y);
            StartCoroutine(rollForward(target, faceDirection));
            isrolling = false;
          
        }

        private Vector2 rollPosition;
        private IEnumerator rollForward(Vector3 target, float faceDirection)
        {
            //未攻击且未到达翻滚距离
            while(Vector2.Distance(transform.position,target)>=0.1f && !isAttack)
            {
                isrolling = true;
                rollPosition = new Vector2((transform.position.x + faceDirection * Time.deltaTime * rollSpeed), transform.position.y);//transform.position + dashDir * dashDistance;
                rb.MovePosition(rollPosition);  
                
                yield return new WaitForFixedUpdate();
              
            }
            isrolling = false;
            anim.SetBool(m_characterData. roll, false);
        }

        public void Move(Vector2 moveDir)
        {
            // 如果必要组件未初始化，直接返回
            if (rb == null || anim == null || m_characterData == null || controller == null)
            {
                return;
            }

            // 如果正在攻击或翻滚，不执行移动
            if (isAttack || isrolling)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            // 计算移动速度
            float moveSpeed = m_characterData.MoveSpeed;
            
            // 应用X和Y方向的移动
            Vector2 velocity = new Vector2(
                moveDir.x * moveSpeed,
                moveDir.y * moveSpeed * rateY  // Y轴移动使用rateY系数来调整速度
            );
            
            rb.velocity = velocity;

            // 更新动画状态
            if (anim != null)
            {
                bool isMoving = moveDir.magnitude > 0.1f;
                anim.SetBool(m_characterData.run, isMoving);
                anim.SetFloat(m_characterData.speed, moveDir.magnitude);
            }

            // 更新朝向（只根据X轴方向）
            if (Mathf.Abs(moveDir.x) > 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, moveDir.x > 0 ? 0 : 180, 0);
            }
        }

       
        public void Jump()
        {
           // if(isCharging) { return; }
            velocity_Y += aSpeed * Time.fixedDeltaTime;
          
            if (childTransform.position.y <= transform.position.y + 0.05f && velocity_Y < 0)
            {
                velocity_Y = 0;
                childTransform.position = transform.position;            
            }
            childTransform.Translate(new Vector3(0, velocity_Y) * Time.fixedDeltaTime);
        }

        internal void prepareJump()
        {
            // 默认蓄力状态不能跳跃
            if (isCharging) { return; }
            if (isGround) 
                //在地面重置可挑次数为默认跳跃次数
                avaiableJumpCount = defaultJumpCount; 
            //在地面或者可挑次数大于零
            if (isGround || (!isGround && avaiableJumpCount > 0))
            {
                velocity_Y = Mathf.Sqrt(jumpHeight * -2f * aSpeed);
                avaiableJumpCount-=1;
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
                skillSystem.AttackUseSkill(100110, false);
                Debug.Log("use attack skill");
                return;
            }
            //翻滚后攻击暂定id为1001， 后期更改为翻滚攻击
            if (isrolling)
            {
                skillSystem.AttackUseSkill(100110, false); return;
            }

            float intervalTime = Time.time - lastPressTime;
            if (intervalTime < cancelAttackTime) return;
            bool isBatter = intervalTime < batterAttackTime;
            skillSystem.AttackUseSkill(100110, isBatter);          
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
            Debug.Log("shooting");
           // GameObjectPool.Instance.CreateObject("CircleProjectile", circleProjectilePrefab, new Vector2(transform.position.x,transform.position.y+projectileHeight), transform.rotation);
        }

        public void EquipWeapon(int weaponId)
        {
            if (m_characterData is PlayerData playerData)
            {
                int oldWeaponId = playerData.WeaponId;
                if (oldWeaponId != 0)
                {
                    DropCurrentWeapon();
                }
                playerData.WeaponId = weaponId;
            }
        }

        private void DropCurrentWeapon()
        {
            if (m_characterData is PlayerData playerData && playerData.WeaponId != 0)
            {
                Vector3 dropPosition = transform.position + (transform.right * faceDirection * 0.5f);
                WeaponManager.Instance.SpawnWeaponOnGround(playerData.WeaponId, dropPosition);
                playerData.WeaponId = 0;
            }
        }
    }
}
