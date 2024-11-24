using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce.Skill
{
/// <summary>
/// 
/// </summary>
    public class DamageImpact:IImpactEffect
    {
        //private AttackData attackData;
        //private SkillData data;
        public void Execute(SkillDeployer deployer)
        {
            if (deployer == null)
            {
                Log.Error("DamageImpact: deployer is null");
                return;
            }

            AttackData attackData = deployer.AttackData;
            if (attackData == null)
            {
                Log.Error("DamageImpact: AttackData is null");
                return;
            }

            if (attackData.SkillOwner == null)
            {
                Log.Error("DamageImpact: SkillOwner is null");
                return;
            }      
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        { 
            AttackData attackData = deployer.AttackData;
           
            float atkTime = 0;
            do
            {             
               OnceDamage(attackData);             

                yield return new WaitForSeconds(attackData.AtkInterval);
                atkTime += attackData.AtkInterval;
                deployer.CalculateTargets();
            } while (atkTime < attackData.DurationTime);
        }
        private void OnceDamage(AttackData attackData)
        {
            if (attackData.AttackTargets == null) return;
            
            // 计算基础伤害
            float atkDamage = attackData.AtkDamage;         
            
            // 判断是否暴击
            bool isCritical = Random.value <= attackData.CriticalRate / 100;
            if (isCritical)
            {
                atkDamage *= (1 + attackData.CriticalDamage);
                Log.Info($"Critical hit! Damage increased to: {atkDamage}");
            }

            for (int i = 0; i < attackData.AttackTargets.Length; i++)
            {
                Transform target = attackData.AttackTargets[i];
                if (target == null) continue;

                // 直接获取Enemy组件并调用Damage方法
                var enemy = target.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Damage(atkDamage);
                    Log.Info($"Dealing {atkDamage} damage to enemy {target.name}, IsCritical: {isCritical}");
                }
            }
        }
    }

}