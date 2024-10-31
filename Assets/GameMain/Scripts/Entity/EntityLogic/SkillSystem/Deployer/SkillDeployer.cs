using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce.Skill
{
    /// <summary>
    /// 技能释放器
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        private AttackData skillData;
       // private string projectName= "SunnyLandTest";
        public AttackData SkillData
        {
            get { return skillData; }
            set
            {
                skillData = value;
                InitDeployer();
            }
        }
        //选区算法对象
       private IAttackSelector selector;
       private IImpactEffect[] impactArray;
        private void InitDeployer()
        {
           // string className = string.Format("{0}.Skill.{1}AttackSelector",projectName, skillData.selectorType);      
            selector = DeployerConfigFactory.CreateAttackSelector(SkillData);
            impactArray = DeployerConfigFactory.CreateImpactEffects(SkillData);
          

        }
        //选区 IAttackSelector
        public void CalculateTargets()
        {
        // skillData.attackTargets= selector.SelectTarget(skillData, transform);
            //if (skillData.attackTargets == null)
            //    Debug.Log("no target");
            //else
            //Debug.Log(SkillData.attackTargets.Length);
             
        }

        //释放方式
        //供技能管理器调用，由子类实现，定义具体策略
        public abstract void DeploySkill();
        
        public void ImpactTarget()
        {
            for (int i = 0; i < impactArray.Length; i++)
            {
                impactArray[i].Execute(this);               
            }
        }

    }
}
