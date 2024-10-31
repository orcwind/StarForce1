using System;
using UnityEngine;



namespace StarForce
{
    /// <summary>
    /// 技能数据
    /// </summary>
    [Serializable]
	public class SkillData
	{
        /// <summary>技能拥有者</summary>
        [HideInInspector]
         public GameObject Owner;
        /// <summary>技能编号</summary>
        public int skillID ;
        /// <summary>技能图标</summary>
      //  public string skillIcon ;
        /// <summary>描述</summary>
        public string description ;
        /// <summary>技能名称</summary>
        public string name ;
        /// <summary>持续时间</summary>
        public float durationTime ;
        /// <summary>在持续时间内，两次伤害之间的间隔时间</summary>
        public float atkInterval ;
        /// <summary>伤害</summary>
        public float atkDamage ;
          /// <summary>攻击比率</summary>
        public float atkRatio=1;
        
        /// <summary>冷却时间</summary>
        public int coolTime ;
        /// <summary>冷却剩余</summary>
        public int coolRemain;
        /// <summary>魔法消耗</summary>
        public int costSP ;
        /// <summary>攻击距离</summary>
        public float attackDistance ;
        /// <summary>攻击范围（上下距离）</summary>
        public float attackWidth;
        /// <summary>攻击目标</summary>
        [HideInInspector]
        public Transform[] attackTargets;
        /// <summary>攻击目标的TAG</summary>
        public string[] attackTargetTags ={"Enemy"};

        /// <summary>攻击目标的TAG</summary>
        public string[] attackTargetLayers = { "BodyCollider", "BrokenObject" };
        /// <summary>技能等级</summary>
        public int level ;
        /// <summary>技能预制对象</summary>
      [HideInInspector]
        public GameObject skillPrefab;
        /// <summary>预制文件名</summary>
        public string prefabName ;
        /// <summary>攻击范围 线形，矩形，扇形(圆形)</summary>
        public SelectorMode selectorType ;
        /// <summary>攻击类型，单攻，群攻</summary>
        public SkillAttackType attackType ;
        /// <summary>是否激活</summary>
        public bool Activated;
        /// <summary>技能对应的动画名称 </summary>
        public string animtionName ;
        /// <summary> 攻击范围角度</summary>
        public int attackAngle ;
        /// <summary>目标受击特效名称</summary>
        public string hitFxName;
        /// <summary>目标受击特效预制件</summary>
            [HideInInspector]
        public GameObject hitFxPrefab;
        /// <summary>下一个连击技能编号</summary>
        public int nextBatterId ;
        /// <summary>
        /// 击退距离
        /// </summary>
        public float knockBackDistance;
        /// <summary>
        /// 击退移动速度
        /// </summary>
        public float knockBackSpeed;
        public string[] impactType={"CostSP","Damage","KnockBack"};

      
  	}
}
