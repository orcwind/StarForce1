//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;
using Common;

namespace StarForce
{
    public class Enemy : Character
    {
        [SerializeField]
        private EnemyData m_EnemyData = null;
        public EnemyData EnemyData
        {
            get 
            { 
                if (m_EnemyData == null && m_CharacterData != null)
                {
                    m_EnemyData = m_CharacterData as EnemyData;
                }
                return m_EnemyData; 
            }
            private set 
            { 
                m_CharacterData = value;
                m_EnemyData = value; 
            }
        }

        private Vector3 m_TargetPosition = Vector3.zero;

        private BoxCollider2D bodyCollider;

        private bool isDead = false;

        private const string DEAD_LAYER_NAME = "DeadEnemy"; // 确保在Unity中创建这个Layer
        private const int DEAD_LAYER = 15; // 设置为DeadEnemy层的索引

        private int deadLayer;

        private void Awake()
        {
            deadLayer = LayerMask.NameToLayer(DEAD_LAYER_NAME);
        }

        public override void Death()
        {
            if (isDead) return;
            
            isDead = true;
            
            // 递归设置所有子物体的Layer
            SetLayerRecursively(gameObject, deadLayer);
            
            // 播放死亡动画
            var animator = GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetBool(m_EnemyData.AnimParams.death, true);
            }

            // 禁用所有碰撞器（包括子物体）
            DisableCollidersRecursively(gameObject);

            Log.Info($"Enemy {gameObject.name} is dead");
        }

        // 递归设置Layer的辅助方法
        private void SetLayerRecursively(GameObject obj, int newLayer)
        {
            if (obj == null) return;

            obj.layer = newLayer;
            
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }

        // 递归禁用所有碰撞器的辅助方法
        private void DisableCollidersRecursively(GameObject obj)
        {
            if (obj == null) return;

            // 禁用所有类型的碰撞器
            var colliders2D = obj.GetComponents<Collider2D>();
            foreach (var collider in colliders2D)
            {
                collider.enabled = false;
            }

            var colliders = obj.GetComponents<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            // 递归处理所有子物体
            foreach (Transform child in obj.transform)
            {
                DisableCollidersRecursively(child.gameObject);
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            Log.Info($"Enemy OnInit Start: {(userData != null ? "userData valid" : "userData null")}");
            
            base.OnInit(userData);

            Log.Info($"Enemy GameObject name: {gameObject.name}");
           // Log.Info($"Enemy Transform hierarchy: {transform.GetHierarchyPath()}");

            // 延迟获取碰撞器，等待预制件完全加载
            if (bodyCollider == null)
            {
                // 先检查当前物体
                bodyCollider = GetComponent<BoxCollider2D>();
                
                // 如果当前物体没有，再查找子物体
                if (bodyCollider == null)
                {
                    Transform colliderTransform = transform.Find("BodyCollider");
                    if (colliderTransform != null)
                    {
                        bodyCollider = colliderTransform.GetComponent<BoxCollider2D>();
                    }
                }
            }

            // 记录错误但不中断执行
            if (bodyCollider == null)
            {
                Log.Warning($"Cannot find BodyCollider on enemy {gameObject.name}");
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

               
            if (m_CharacterData == null)
            {
                Log.Error("Character data is null after base.OnShow");
                return;
            }

            // 设置数据
            EnemyData = m_CharacterData as EnemyData;
            if (EnemyData == null)
            {
                Log.Error("Invalid enemy data type");
                return;
            }
    if (EnemyData != null)
    {
        EnemyData.OnDeath += HandleDeath;
    }
            // 确保序列化字段也被更新
            m_EnemyData = EnemyData;

          

            // 确保碰撞器处于激活状态
            if (bodyCollider != null)
            {
                bodyCollider.enabled = true;
            }
        
            // 注册到EnemyManager
            EnemyManager.Instance.RegisterEnemy(this);
        }

private void HandleDeath()
{
    var animator = GetComponentInChildren<Animator>();
    if (animator != null)
    {
        animator.SetBool(EnemyData.AnimParams.death, true);
    }
}
#if UNITY_2017_3_OR_NEWER
        protected override void OnHide(bool isShutdown, object userData)
#else
        protected internal override void OnHide(bool isShutdown, object userData)
#endif
        {
            // 从EnemyManager注销
            EnemyManager.Instance.UnregisterEnemy(this);
            
            if (bodyCollider != null)
            {
                bodyCollider.enabled = false;
            }
 if (EnemyData != null)
    {
        EnemyData.OnDeath -= HandleDeath;
    }
            base.OnHide(isShutdown, userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            #region
            //if (Input.GetMouseButton(0))
            //{
            //    Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    m_TargetPosition = new Vector3(point.x, 0f, point.z);

            //    for (int i = 0; i < m_Weapons.Count; i++)
            //    {
            //        m_Weapons[i].TryAttack();
            //    }
            //}

            //Vector3 direction = m_TargetPosition - CachedTransform.localPosition;
            //if (direction.sqrMagnitude <= Vector3.kEpsilon)
            //{
            //    return;
            //}

            //Vector3 speed = Vector3.ClampMagnitude(direction.normalized * m_MyAircraftData.Speed * elapseSeconds, direction.magnitude);
            //CachedTransform.localPosition = new Vector3
            //(
            //    Mathf.Clamp(CachedTransform.localPosition.x + speed.x, m_PlayerMoveBoundary.xMin, m_PlayerMoveBoundary.xMax),
            //    0f,
            //    Mathf.Clamp(CachedTransform.localPosition.z + speed.z, m_PlayerMoveBoundary.yMin, m_PlayerMoveBoundary.yMax)
            //);
            #endregion
        }

        public override void Damage(float damageVal)
        {
            // 如果已经死亡，则不再受到伤害
            if (isDead)
            {
                Log.Info($"Enemy {gameObject.name} is already dead, cannot take damage");
                return;
            }

            base.Damage(damageVal);
            Log.Info($"Enemy {gameObject.name} is taking damage: {damageVal}");
            if (m_EnemyData != null)
            {
                // 播放受伤动画
                var animator = GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger(m_EnemyData.AnimParams.hurt);
                }

                              
                // 创建伤害显示
                DamageTextData damageTextData = new DamageTextData(
                    GameEntry.Entity.GenerateSerialId(),
                    80001,
                    transform.position,
                    damageVal - m_EnemyData.Defense,
                    false
                );

                GameEntry.Entity.ShowDamageText(damageTextData);
            }
        }
    }
}
