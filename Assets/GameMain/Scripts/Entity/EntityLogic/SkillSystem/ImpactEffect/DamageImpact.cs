
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace StarForce.Skill
{
/// <summary>
/// 
/// </summary>
    public class DamageImpact:IImpactEffect
    {
        //private SkillData data;
        public void Execute(SkillDeployer deployer)
        {
            //data = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            float atkTime = 0;
            AttackData data = deployer.SkillData;
            do
            {
                OnceDamage(data);

                yield return new WaitForSeconds(data.AtkInterval);
                atkTime += data.AtkInterval;
                deployer.CalculateTargets();
            } while (atkTime < data.DurationTime);

        }
        private void OnceDamage(AttackData data)
        {
            //技能实际攻击力
            //  float atk = data.atkRatio * data.SkillOwner.GetComponent<CharacterStatus>().baseATK + data.atkDamage;
            AttackData tempAttackData = data.SkillOwner.GetComponent<CharacterSKillManager>().m_Attack.m_AttackData;
            float atk=tempAttackData.AtkDamage;
            if (data.AttackTargets == null) return;
            for (int i = 0; i < data.AttackTargets.Length; i++)
            {
                //var status = data.AttackTargets[i].GetComponent<CharacterStatus>();
                //status.Damage(atk);

                CharacterData tempData = data.AttackTargets[i].GetComponent<CharacterSKillManager>().m_characterData;
                
               // tempData.Damage(atk);
            }
            //创建攻击特性
        }
    }

}