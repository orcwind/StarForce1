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
        private AttackData attackData;
      
        public AttackData AttackData
        {
            get { return attackData; }
            set
            {
                attackData = value;
                InitDeployer();
            }
        }
        //ѡ���㷨����
       private IAttackSelector selector;
       private IImpactEffect[] impactArray;
        private void InitDeployer()
        {
            selector = DeployerConfigFactory.CreateAttackSelector(attackData);
            impactArray = DeployerConfigFactory.CreateImpactEffects(attackData);
          
        }
        //ѡ�� IAttackSelector
        public void CalculateTargets()
        {
            attackData.AttackTargets = selector.SelectTarget(attackData, transform);          
             
        }
   
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
