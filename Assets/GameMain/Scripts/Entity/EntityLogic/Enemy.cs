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

        private Vector3 m_TargetPosition = Vector3.zero;

        private BoxCollider2D bodyCollider;

        public override void Death()
        {
            if (m_EnemyData != null)
            {
                var animator = GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    animator.SetBool(m_EnemyData.death, true);
                }
                Log.Info($"Enemy {gameObject.name} is Dead");
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

            m_EnemyData = userData as EnemyData;
            if (m_EnemyData == null)
            {
                Log.Error($"Enemy data is invalid for {gameObject.name}");
                return;
            }

            // 确保碰撞器处于激活状态
            if (bodyCollider != null)
            {
                bodyCollider.enabled = true;
            }
        
            // 注册到EnemyManager
            EnemyManager.Instance.RegisterEnemy(this);
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
            base.Damage(damageVal);

            if (m_EnemyData != null)
            {
                var animator = GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger(m_EnemyData.hurt);
                }

                // 设置初始伤害显示位置偏移
                Vector3 offset = new Vector3(0, 1f, 0);

                // 生成伤害显示
                if (m_EnemyData.damageDisplayPre != null)
                {
                    GameObject ob = GameObjectPool.Instance.CreateObject("damageValue", 
                        m_EnemyData.damageDisplayPre, 
                        transform.position + offset, 
                        Quaternion.identity);
                }
            }
        }
    }
}
