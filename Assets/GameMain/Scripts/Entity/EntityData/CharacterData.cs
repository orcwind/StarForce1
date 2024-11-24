//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime; 

namespace StarForce
{
    [Serializable]
    public abstract class CharacterData : TargetableObjectData
    {
        [SerializeField]
        protected CharacterAnimationData animParams = new CharacterAnimationData();

        // 获取动画参数的属性
        public CharacterAnimationData AnimParams => animParams;

        [SerializeField]
        private float m_AttackSpeedRate = 1;

        [SerializeField]
        private float m_BaseATK = 0;
      
        [SerializeField]
        private float m_Defense = 0;

        //[SerializeField]
        //private float m_HP = 0;

        [SerializeField]
        private float m_MaxHP = 0;

        [SerializeField]
        private float m_MaxSP = 0;

        [SerializeField]
        private float m_SP = 0;

        [SerializeField]
        private float m_Shield = 0;
     
        [SerializeField]
        private float m_Rage = 0;

        [SerializeField]
        private float m_MaxRage = 0;

        [SerializeField]
        private float m_MoveSpeed = 0;

        //闪烁时间
        [SerializeField]
        private float m_FlashTime = 0;

        [SerializeField]
        private int m_DeadEffectId = 0;

        [SerializeField]
        private int m_DeadSoundId = 0;

        [SerializeField]
        private int m_WeaponId = 0;

 

        public CharacterData(int entityId, int typeId, CampType camp)
            : base(entityId, typeId, camp)
        {
            if (GameEntry.DataTable == null)
            {
                Log.Error("GameEntry.DataTable is null!");
                return;
            }

            IDataTable<DRCharacter> dtCharacter = GameEntry.DataTable.GetDataTable<DRCharacter>();
            if (dtCharacter == null)
            {
                Log.Error("Can not get character data table.");
                return;
            }

            DRCharacter drCharacter = dtCharacter.GetDataRow(TypeId);
            if (drCharacter == null)
            {
                Log.Error($"Can not find character data row {TypeId}");
                return;
            }
            
            InitializeData(drCharacter);
        }

        private void InitializeData(DRCharacter drCharacter)
        {
            m_AttackSpeedRate = drCharacter.AttackSpeedRate;
            m_BaseATK = drCharacter.BaseATK;
            m_Defense = drCharacter.Defence;
            m_MaxHP = drCharacter.MaxHP;
            m_MaxSP = drCharacter.MaxSP;
            m_Shield = drCharacter.Shield;
            m_Rage = drCharacter.Rage;
            m_MaxRage = drCharacter.MaxRage; 
            m_MoveSpeed = drCharacter.MoveSpeed;
            m_FlashTime = drCharacter.FlashTime;
            m_DeadEffectId = drCharacter.DeadEffectId;
            m_DeadSoundId = drCharacter.DeadSoundId;         
            
            HP = m_MaxHP;
            m_SP = m_MaxSP;
            //m_WeaponId = 101020;
        }


        /// <summary>
        /// 显示伤害点
        /// </summary>
        public GameObject damageDisplayPre;

        /// <summary>
        /// 攻击速率
        /// </summary>
        public float AttackSpeedRate
        {
            get
            {
                return m_AttackSpeedRate;
            }
        }

        /// <summary>
        /// 基础攻击力
        /// </summary>
        public float BaseATK
        {
            get
            {
                return m_BaseATK;
            }
        }

        /// <summary>
        /// 护盾
        /// </summary>
        public float Shield
        {
            get
            {
                return m_Shield;
            }
        }

        /// <summary>
        /// 怒气
        /// </summary>
        public float Rage
        {
            get
            {
                return m_Rage;
            }
        }

        /// <summary>
        /// 最大怒气
        /// </summary>
        public float MaxRage
        {
            get
            {
                return m_MaxRage;
            }
        }
        /// <summary>
        /// 最大生命。
        /// </summary>
        public override float MaxHP
        {
            get
            {
                return m_MaxHP;
            }
        }

        /// <summary>
        /// 最大法力值。
        /// </summary>
        public float MaxSP
        {
            get
            {
                return m_MaxSP;
            }
        }

        /// <summary>
        /// 法力值。
        /// </summary>
        public float SP
        {
            get
            {
                return m_SP;
            }
        }


        /// <summary>
        /// 防御。
        /// </summary>
        public float Defense
        {
            get
            {
                return m_Defense;
            }
        }

        /// <summary>
        /// 移动速度。
        /// </summary>
        public float MoveSpeed
        {
            get
            {
                return m_MoveSpeed;
            }
        }

        /// <summary>
        /// 受伤闪烁时间
        /// </summary>
        public float FlashTime
        {
            get
            {
                return m_FlashTime;
            }
        }

        /// <summary>
        /// 死亡效果
        /// </summary>
        public int DeadEffectId
        {
            get
            {
                return m_DeadEffectId;
            }
        }

        /// <summary>
        /// 死亡声音
        /// </summary>
        public int DeadSoundId
        {
            get
            {
                return m_DeadSoundId;
            }
        }

        /// <summary>
        /// 武器id
        /// </summary>
        public int WeaponId
        {
            get
            {
                return m_WeaponId;
            }
            set
            {
                m_WeaponId = value;
            }
        }

        // 添加事件
        public event System.Action OnDeath;

      
        // private Color originalColor;
        // public SpriteRenderer spriteRenderer;
        // public IEnumerator FlashColor(float time, Color color)
        // {
        //    spriteRenderer.color = color;

        //    yield return new WaitForSeconds(time);
            
        //    ResetColor();

        // }

        // //恢复成原有颜色
        // public void ResetColor()
        // {
        //    spriteRenderer.color = originalColor;
        // }

       // 修改 Death 方法
        virtual public void Death()
        {
            OnDeath?.Invoke();
        }
     
    }
}
