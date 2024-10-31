
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace StarForce.Skill
{
/// <summary>
/// 
/// </summary>
    public class DamageFireImpact:IImpactEffect
    {
        //private SkillData data;
        public void Execute(SkillDeployer deployer)
        {
            //data = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            //    float atkTime=0;
            //    AttackData data = deployer.SkillData;
            //    do
            //{ 
            //     OnceDamage(data);

            //    yield return new WaitForSeconds(data.atkInterval);
            //    atkTime += data.atkInterval;
            //    deployer.CalculateTargets();
            //} while (atkTime < data.durationTime);
            return null;
        }
        private void OnceDamage(AttackData data)
        {
            //技能实际攻击力

            //CharacterData m_characterData = new PlayerData(GameEntry.Entity.GenerateSerialId(), 10000);
            //float atk = data.atkRatio * data.Owner.GetComponent<CharacterStatus>().baseATK;
            //if (data.attackTargets == null) return;  
            //for (int i = 0; i < data.attackTargets.Length; i++)
            //{              
            //  var status=  data.attackTargets[i].GetComponent<CharacterStatus>();
            //      status.Damage(atk);
            //}
            //创建攻击特性
        }
    }

}