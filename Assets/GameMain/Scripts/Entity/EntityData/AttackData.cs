//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityEngine;

namespace StarForce
{
    [Serializable]
    public class AttackData : EntityData
    {
        [SerializeField]
        private GameObject m_SkillOwner;
        [SerializeField]
        private GameObject m_SkillPrefab;
        [SerializeField]
        private float m_DurationTime;

        [SerializeField]
        private int m_BackgroundId;
        [SerializeField]
        private string m_AssetName;
        [SerializeField]
        private int m_NextBatterID;
        [SerializeField]
        private float m_AtkDamage;
        [SerializeField]
        private float m_AttackAngle;
        [SerializeField]
        private float m_AttackDistance;
        [SerializeField]
        private float m_CriticalRate;
        [SerializeField]
        private float m_CriticalDamage;
        [SerializeField]
        private float m_KnockBackDistance;
        [SerializeField]
        private float m_KnockBackSpeed;
        [SerializeField]
        private int m_SelectorType;
        [SerializeField]
        private int m_SkillAttackType;
        [SerializeField]
        private Transform[] m_AttackTargets;

        [SerializeField]
        private string[] m_AttackTargetTags;
        [SerializeField]
        private string[] m_AttackTargetLayers;

        [SerializeField]
        private string[] m_ImpactType;
        [SerializeField]
        private string m_AnimationName;
        [SerializeField]
        private string m_AnimParaName;

        [SerializeField]
        private int m_CoolRemain;
        [SerializeField]
        
        private int m_CoolTime;
     
        [SerializeField]
        private float m_AtkInterval;

        [SerializeField]
        private float m_AttackWidth;
    
    
        [SerializeField]
        private string m_WeaponName;
        [SerializeField]
        private string m_WeaponDescription;
        [SerializeField]
        private string m_WeaponType;
       
        [SerializeField]
        private int m_AttackId;

             [SerializeField]
        private int m_WeaponId;

        public AttackData(int entityId, int typeId) : 
            base(entityId, typeId)
        {
            IDataTable<DRAttack> dtAttacks = GameEntry.DataTable.GetDataTable<DRAttack>();
            DRAttack drAttack = dtAttacks.GetDataRow(typeId);
            if (drAttack != null)
            {              
                m_BackgroundId = drAttack.BackgroundId;
                m_AssetName = drAttack.AssetName;
                m_NextBatterID = drAttack.NextBatterID;
                m_AtkDamage = drAttack.AtkDamage;
                m_AttackAngle = drAttack.AttackAngle;
                m_CriticalDamage = drAttack.CriticalDamage;
                m_CriticalRate = drAttack.CriticalRate;
                m_KnockBackDistance = drAttack.KnockBackDistance;
                m_KnockBackSpeed = drAttack.KnockBackSpeed;
                m_SelectorType = drAttack.SelectorType;
                m_SkillAttackType =drAttack.SkillAttackType;
                m_DurationTime = drAttack.DurationTime;
                m_CoolRemain = drAttack.CoolRemain;
                m_CoolTime = drAttack.CoolTime;
                m_AnimationName = drAttack.AnimationName;
                m_AnimParaName = drAttack.AnimParaName;
                m_AttackWidth = drAttack.AttackWidth;
               m_AttackDistance = drAttack.AttackDistance;
                m_WeaponName = drAttack.WeaponName;
                m_WeaponDescription = drAttack.WeaponDescription;
                m_WeaponType = drAttack.WeaponType;                
                m_AttackId = drAttack.AttackId;
                m_AtkInterval = drAttack.AtkInterval;
                m_WeaponId = drAttack.WeaponId;
            }
        }

        public string[] ImpactType
        {
            get { return m_ImpactType; }
            set { m_ImpactType = value; }
        }


        /// <summary>
        /// 被攻击目标层
        /// </summary>
        public string[] AttackTargetLayers
        {
            get
            { return m_AttackTargetLayers; }
            set
            {
                m_AttackTargetLayers = value;
            }
        }
              
        /// <summary>
        /// 被攻击目标tag
        /// </summary>
        public string[] AttackTargetTags 
        {
            get
            { return m_AttackTargetTags; }
            set
            {
                m_AttackTargetTags=value;
            } }



        [HideInInspector]
        /// <summary>
        /// 被攻击目标。
        /// </summary>
        public Transform[] AttackTargets
        {
            get { return m_AttackTargets; }
            set { m_AttackTargets = value; }
        }

        /// <summary>
        /// 技能prefab。
        /// </summary>
        public GameObject SkillPrefab
        {
            get { return m_SkillPrefab; }
            set { m_SkillPrefab = value; }
        }

        /// <summary>
        /// 技能拥有者。
        /// </summary>
        public GameObject SkillOwner
        {
            get { return m_SkillOwner; }
            set { m_SkillOwner = value; }
        }

        ///// <summary>
        ///// 获取资源名称。
        ///// </summary>
        //public string AssetName
        //{
        //    get { return m_AssetName; }
        //}

        /// <summary>
        /// 获取背景音乐编号（没有就设置为0）。
        /// </summary>
        public int BackgroundId
        {
            get{ return m_BackgroundId; }
        }


        /// <summary>
        /// 连击数量。
        /// </summary>
        public int NextBatterID
        {
            get { return m_NextBatterID; }
        }


        /// <summary>
        /// 攻击力。
        /// </summary>
        public float AtkDamage
        {
            get { return m_AtkDamage; }
        }

        /// <summary>
        /// 持续时间。
        /// </summary>
        public float DurationTime
        {
            get { return m_DurationTime; }
        }

        ///// <summary>
        ///// 攻击角度。
        ///// </summary>
        //public float AttackAngle
        //{
        //    get { return m_AttackAngle; }
        //}

        /// <summary>
        /// 爆伤。
        /// </summary>
        public float CriticalDamage
        {
            get { return m_CriticalDamage; }
        }

        /// <summary>
        /// 暴击率。
        /// </summary>
        public float CriticalRate
        {
            get { return m_CriticalRate; }
        }

        /// <summary>
        /// 击退距离。
        /// </summary>
        public float KnockBackDistance
        {
            get { return m_KnockBackDistance; }
        }


        /// <summary>
        /// 击退速度。
        /// </summary>
        public float KnockBackSpeed
        {
            get { return m_KnockBackSpeed; }
        }

        /// <summary>
        /// 攻击范围类型（Rectangle，Sector, Collider）。
        /// </summary>
        public SelectorMode SelectorType
        {
            get { return (SelectorMode)m_SelectorType; }
        }
        /// <summary>
        /// 单数复数（Single, Group）
        /// </summary>
        public SkillAttackType SkillAttackType
        {
            get { return (SkillAttackType)m_SkillAttackType; }
        }

        /// <summary>
        /// 冷却剩余时间。
        /// </summary>
        public int CoolRemain
        {
            get { return m_CoolRemain; }
            set { m_CoolRemain = value; }
        }/// <summary>
         /// 冷却时间。
         /// </summary>
        public int CoolTime
        {
            get { return m_CoolTime; }
        }

        /// <summary>
        /// 攻击范围类型（Rectangle，Sector, Collider）。
        /// </summary>
        public string AnimationName
        {
            get { return m_AnimationName; }
        }

        /// <summary>
        /// 攻击伞形角度。
        /// </summary>
        public float AttackAngle
        {
            get { return m_AttackAngle; }
        }

        /// <summary>
        /// 攻击范围类型（Rectangle，Sector, Collider）。
        /// </summary>
        public string AssetName
        {
            get { return m_AssetName; }
        }

        /// <summary>
        /// 在持续时间内两次伤害之间的间隔。
        /// </summary>
        public float AtkInterval
        {
            get { return m_AtkInterval; }
        }

        /// <summary>
        /// 攻击宽度（上下距离）。
        /// </summary>
        public float AttackWidth
        {
            get { return m_AttackWidth; }
        }

        /// <summary>
        /// 攻击距离。
        /// </summary>
        public float AttackDistance
        {
            get { return m_AttackDistance; }
        }

        /// <summary>
        /// 武器名称。
        /// </summary>
        public string WeaponName
        {
            get { return m_WeaponName; }
        }

        /// <summary>
        /// 武器描述。
        /// </summary>
        public string WeaponDescription
        {
            get { return m_WeaponDescription; }
        }

        /// <summary>
        /// 武器类型。
        /// </summary>
        public string WeaponType
        {
            get { return m_WeaponType; }
        }
     
        /// <summary>
        /// 攻击ID。
        /// </summary>
        public int AttackId
        {
            get { return m_AttackId; }
        }

        /// <summary>
        /// 动画参数名称。
        /// </summary>
        public string AnimParaName
        {
            get { return m_AnimParaName; }
        }

        /// <summary>
        /// 武器ID。
        /// </summary>
        public int WeaponId
        {
            get { return m_WeaponId; }
        }

    }


}
