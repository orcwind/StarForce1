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

        public override void Death()
        {

            GetComponentInChildren<Animator>().SetBool(m_EnemyData. death, true);
            print("Enemy is Dead ");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
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
                Log.Error("Player data is invalid.");
                return;
            }
            #region
            //ScrollableBackground sceneBackground = FindObjectOfType<ScrollableBackground>();
            //if (sceneBackground == null)
            //{
            //    Log.Warning("Can not find scene background.");
            //    return;
            //}

            //m_PlayerMoveBoundary = new Rect(sceneBackground.PlayerMoveBoundary.bounds.min.x, sceneBackground.PlayerMoveBoundary.bounds.min.z,
            //    sceneBackground.PlayerMoveBoundary.bounds.size.x, sceneBackground.PlayerMoveBoundary.bounds.size.z);
            #endregion
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

            GetComponentInChildren<Animator>().SetTrigger(m_EnemyData. hurt);

            //设置初始伤害显示位置偏移
            Vector3 offset = new Vector3(0, 1f, 0);

            //生成伤害显示
            GameObject ob = GameObjectPool.Instance.CreateObject("damageValue", m_EnemyData.damageDisplayPre, this.transform.position + offset, Quaternion.identity);
            //文本内容用伤害值替换
            // ob.GetComponent<DamageDisplay>().text.text= damageVal.ToString();
            //print("MonsterStatus OnDamage ");
        }
    }
}
