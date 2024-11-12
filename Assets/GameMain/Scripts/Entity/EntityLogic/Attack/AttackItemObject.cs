using GameFramework.ObjectPool;
using UnityEngine;
using GameFramework;

namespace StarForce
{
    public class AttackItemObject : ObjectBase
    {
        public static AttackItemObject Create(Attack attack)
        {
            AttackItemObject attackItemObject = ReferencePool.Acquire<AttackItemObject>();
            attackItemObject.Initialize(attack);
            return attackItemObject;
        }

        private Attack m_Attack = null;

        protected override void Release(bool isShutdown)
        {
            if (m_Attack != null)
            {
                m_Attack = null;
            }
        }

        public void Initialize(Attack attack)
        {
            m_Attack = attack;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
        }

        protected override void OnUnspawn()
        {
            base.OnUnspawn();
        }
    }
} 