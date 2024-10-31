//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 人物类
    /// </summary>
    public abstract class Character : TargetableObject
    {
        [SerializeField]
        public CharacterData m_CharacterData = null;

        [SerializeField]
        private WeaponInfo m_WeaponInfo;

        [SerializeField]
        public SpriteRenderer m_SpriteRenderer = null;
       
        [SerializeField]
        private Color m_OriginalColor;


#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

            m_CharacterData = userData as CharacterData;
             
            if (m_CharacterData == null)
            {
                Log.Error("Character data is invalid.");
                return;
            }
           
            Name = Utility.Text.Format("Character ({0})", Id);
       
            //记录初始颜色
            m_SpriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
            m_OriginalColor = m_SpriteRenderer.color;
                //HitFxPos=FindChildByName("HitFxPos");
                //HitFxPos = .FindChild(transform, "HitFxPos");
           
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnHide(bool isShutdown, object userData)
#else
        protected internal override void OnHide(bool isShutdown, object userData)
#endif
        {
            base.OnHide(isShutdown, userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#else
        protected internal override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#endif
        {
            base.OnAttached(childEntity, parentTransform, userData);
            #region
            //if (childEntity is Thruster)
            //{
            //    m_Thruster = (Thruster)childEntity;
            //    return;
            //}

            //if (childEntity is Weapon)
            //{
            //    m_Weapons.Add((Weapon)childEntity);
            //    return;
            //}

            //if (childEntity is Armor)
            //{
            //    m_Armors.Add((Armor)childEntity);
            //    return;
            //}
            #endregion
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnDetached(EntityLogic childEntity, object userData)
#else
        protected internal override void OnDetached(EntityLogic childEntity, object userData)
#endif
        {
            base.OnDetached(childEntity, userData);

            #region
            //if (childEntity is Thruster)
            //{
            //    m_Thruster = null;
            //    return;
            //}

            //if (childEntity is Weapon)
            //{
            //    m_Weapons.Remove((Weapon)childEntity);
            //    return;
            //}

            //if (childEntity is Armor)
            //{
            //    m_Armors.Remove((Armor)childEntity);
            //    return;
            //}
            #endregion
        }

        protected override void OnDead(Entity attacker)
        {
            base.OnDead(attacker);

            GameEntry.Entity.ShowEffect(new EffectData(GameEntry.Entity.GenerateSerialId(), m_CharacterData.DeadEffectId)
            {
                Position = CachedTransform.localPosition,
            });
            GameEntry.Sound.PlaySound(m_CharacterData.DeadSoundId);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(m_CharacterData.Camp, m_CharacterData.HP, 0, m_CharacterData.Defense);
        }

        /// <summary>
        /// 属性攻击变色
        /// </summary>
        /// <param name="time">属性持续时间</param>
        /// <param name="color">属性类型对应颜色</param>
        public void FlashColor(float time, Color color)
        {
            m_SpriteRenderer.color = color;
            Invoke("ResetColor", time);

        }


        //恢复成原有颜色
        public void ResetColor()
        {
            m_SpriteRenderer.color = m_OriginalColor;
        }

        /// <summary>
        /// 死亡
        /// </summary>
        abstract public void Death();

        virtual public void Damage(float damageVal)
        {
            //写所有受到伤害是共性的表现 HP减少！
            //受击者 有防御能力
            damageVal = damageVal - m_CharacterData.Defense;

            //GameObject ob= GameObjectPool.Instance.CreateObject(damageValue, damageDisplayPre, this.transform.position, Quaternion.identity);
            //ob.GetComponent<DamageDisplay>().damageValue = damageVal;
            if (damageVal <= 0) return;
            m_CharacterData.HP -= damageVal;


            //受击后变颜色，暂时是红色，可根据伤害元素类型改变
            FlashColor(m_CharacterData.FlashTime, Color.red);
            if (m_CharacterData.HP <= 0) Death();
            //子类可以再加上个性的表现
        }
        //受击 同时播放受击 特效：需要找到 受击特效挂载点
        public Transform HitFxPos;

        public void EquipWeapon(int weaponId)
        {
            Log.Info($"开始装备武器: 当前WeaponId={m_CharacterData.WeaponId}, 新WeaponId={weaponId}");
            
            // 更新 CharacterData 中的 WeaponId
            m_CharacterData.WeaponId = weaponId;
            
            // 更新武器信息
            UpdateWeaponInfo();
            
            Log.Info($"武器装备完成: 当前WeaponId={m_CharacterData.WeaponId}");
        }

        private void UpdateWeaponInfo()
        {
            if (m_CharacterData.WeaponId == 0)
            {
                m_WeaponInfo = null;
                Log.Info("清除武器信息");
                return;
            }
            
            Log.Info($"更新武器信息: WeaponId={m_CharacterData.WeaponId}");
            m_WeaponInfo = new WeaponInfo(m_CharacterData.WeaponId);
        }

        public bool CanAttack()
        {
            return m_CharacterData.WeaponId != 0 && m_WeaponInfo != null;
        }

        public void Attack()
        {
            if (!CanAttack())
            {
                Log.Warning("Cannot attack without a weapon.");
                return;
            }

            // 执行攻击逻辑
            // 使用 m_WeaponInfo 中的数据进行攻击
            var attackData = m_WeaponInfo.GetAttackData(1); // 这里的1是默认攻击ID，你可能需要根据具体攻击类型传入不同的ID
            if (attackData != null)
            {
                Log.Info($"执行攻击: WeaponId={m_CharacterData.WeaponId}, AttackId={attackData.Id}");
                // 执行具体的攻击逻辑
            }
        }

        // 添加获取武器信息的方法
        public WeaponInfo GetWeaponInfo()
        {
            return m_WeaponInfo;
        }

        // 添加获取当前攻击数据的方法
        public AttackData GetCurrentAttackData(int attackId)
        {
            return m_WeaponInfo?.GetAttackData(attackId);
        }
    }
}
