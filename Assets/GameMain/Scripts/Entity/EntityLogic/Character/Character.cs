﻿//------------------------------------------------------------
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
        protected CharacterData m_CharacterData;

        public CharacterData CharacterData
        {
            get { return m_CharacterData; }
            protected set { m_CharacterData = value; }
        }

        [SerializeField]
        private WeaponInfo m_WeaponInfo;

        [SerializeField]
        public SpriteRenderer m_SpriteRenderer = null;
       
        [SerializeField]
        private Color m_OriginalColor;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            m_OriginalColor = m_SpriteRenderer.color;
            Log.Debug($"Character OnInit - userData type: {userData?.GetType().Name ?? "null"}");
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            // 设置数据
            m_CharacterData = userData as CharacterData;
            if (m_CharacterData == null)
            {
                Log.Error($"Invalid character data type: {userData?.GetType().Name ?? "null"}");
                return;
            }

            Log.Debug($"Character data set successfully: {m_CharacterData.GetType().Name}");
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

            GameEntry.Entity.ShowEffect(new EffectData(GameEntry.Entity.GenerateSerialId(), CharacterData.DeadEffectId)
            {
                Position = CachedTransform.localPosition,
            });
            GameEntry.Sound.PlaySound(CharacterData.DeadSoundId);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(CharacterData.Camp, CharacterData.HP, 0, CharacterData.Defense);
        }

        /// <summary>
        /// 属性攻击变色
        /// </summary>
        /// <param name="time">属性持续时间</param>
        /// <param name="color">属性类型对应颜色</param>
        public void FlashColor(float time, Color color)
        {
            color.a = m_SpriteRenderer.color.a;
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

        public virtual void Damage(float damageVal)
        {
            if (m_CharacterData == null) return;

            // 应用防御力计算
            float finalDamage = damageVal - m_CharacterData.Defense;
            if (finalDamage <= 0) return;

            // 更新HP
            m_CharacterData.HP -= finalDamage;

            // 变色效果
            FlashColor(m_CharacterData.FlashTime, Color.red);

            // 检查死亡
            if (m_CharacterData.HP <= 0)
            {
                Death();
            }
        }

        //受击 同时播放受击 特效：需要找到 受击特效挂载点
        public Transform HitFxPos;

        public void EquipWeapon(int weaponId)
        {
            Log.Info($"开始装备武器: 当前WeaponId={CharacterData.WeaponId}, 新WeaponId={weaponId}");
            
            // 更新 CharacterData 中的 WeaponId
            CharacterData.WeaponId = weaponId;
            
            // 更新武器信息
            UpdateWeaponInfo();
            
            Log.Info($"武器装备完成: 当前WeaponId={CharacterData.WeaponId}");
        }

        private void UpdateWeaponInfo()
        {
            if (CharacterData.WeaponId == 0)
            {
                m_WeaponInfo = null;
                Log.Info("清除武器信息");
                return;
            }
            
            Log.Info($"更新武器信息: WeaponId={CharacterData.WeaponId}");
            m_WeaponInfo = new WeaponInfo(CharacterData.WeaponId);
        }

        public bool CanAttack()
        {
            return CharacterData.WeaponId != 0 && m_WeaponInfo != null;
        }

        public void Attack()
        {
            if (!CanAttack())
            {
                Log.Warning("Cannot attack without a weapon.");
                return;
            }

            // 添加调用栈日志
            Log.Debug($"Attack called from: {new System.Diagnostics.StackTrace().ToString()}");
            
            var attackData = m_WeaponInfo.GetAttackData(1);
            if (attackData != null)
            {
                Log.Info($"执行攻击: WeaponId={CharacterData.WeaponId}, AttackId={attackData.Id}");
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

        // 添加公共方法获取数据
        public CharacterData GetCharacterData()
        {
            return m_CharacterData;
        }
    }
}
