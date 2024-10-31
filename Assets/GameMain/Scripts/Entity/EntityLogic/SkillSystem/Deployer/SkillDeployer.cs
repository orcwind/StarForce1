using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce.Skill
{
    /// <summary>
    /// �����ͷ���
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
        //ѡ���㷨����
       private IAttackSelector selector;
       private IImpactEffect[] impactArray;
        private void InitDeployer()
        {
           // string className = string.Format("{0}.Skill.{1}AttackSelector",projectName, skillData.selectorType);      
            selector = DeployerConfigFactory.CreateAttackSelector(SkillData);
            impactArray = DeployerConfigFactory.CreateImpactEffects(SkillData);
          

        }
        //ѡ�� IAttackSelector
        public void CalculateTargets()
        {
        // skillData.attackTargets= selector.SelectTarget(skillData, transform);
            //if (skillData.attackTargets == null)
            //    Debug.Log("no target");
            //else
            //Debug.Log(SkillData.attackTargets.Length);
             
        }

        //�ͷŷ�ʽ
        //�����ܹ��������ã�������ʵ�֣�����������
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
