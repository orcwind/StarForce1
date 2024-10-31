//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 特效类。
    /// </summary>
    public class Attack : Entity
    {
        [SerializeField]
        public AttackData m_AttackData = null;

        private float m_ElapseSeconds = 0f;

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

            m_AttackData = userData as AttackData;
            if (m_AttackData == null)
            {
                Log.Error("Attack data is invalid.");
                return;
            }

            m_ElapseSeconds = 0f;
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            //m_ElapseSeconds += elapseSeconds;
            //if (m_ElapseSeconds >= m_AttackData.KeepTime)
            //{
            //    GameEntry.Entity.HideEntity(this);
            //}
        }
    }
}
