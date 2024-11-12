using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityGameFramework.Runtime;


namespace StarForce.Skill
{
    [RequireComponent(typeof(CharacterSKillManager))]
/// <summary>
/// 封装技能系统，提供技能的生成和使用
/// </summary>
public class CharacterSkillSystem : MonoBehaviour
{
        public CharacterSKillManager skillManager;
        private Animator anim;
        private AttackData attack;
        private IAttackSelector selector;
       // private AnimationEventBehaviour animEventBehaviour;
       
        public Player player;

        private AttackData currentAttackData = null; // 保存当前/上一次的攻击数据

        private void Start()
        {
            skillManager = GetComponent<CharacterSKillManager>();
            anim = GetComponentInChildren<Animator>();
            GetComponentInChildren<AnimationEventBehaviour>().AttackHandler += DeploySkill;
            player = GetComponent<Player>();
        }

        /// <summary>
        /// 部署技能
        /// </summary>
        private void DeploySkill()
        {  
            if (currentAttackData != null)
        {
            skillManager.GenerateSkill(currentAttackData);
        }
        }

        /// <summary>
        /// 使用技能
        /// </summary>
   
        [HideInInspector]
        public Transform selectedTarget; 
        private Transform SelectTarget()
        {
            Transform[] target = new SectorAttackSelector().SelectTarget(attack, transform);
          //  return target.Length != 0 ? target[0] : null;
            return target !=null ? target[0] : null;
        }

        private void SetSelectedActiveFx(bool state)
        {   if (selectedTarget == null) return;
            var selected = selectedTarget.GetComponent<CharacterSelected>();
            if (selected) selected.SetSelectedActive(state);
        }
        /// <summary>
        /// 使用随机技能，为NPC提供
        /// </summary>
        public void UseRandomSkill()
        {
       //从技能管理器中找到所有可用的技能，并存储在usableSkills数组中，
       //产生一个随机数，随机数范围为0到usableSkills数组的长度，然后使用AttackUseSkill方法使用随机数对应的技能
             var usableSkills = skillManager.attacks.FindAll(s => 
             skillManager.PrepareSkill(s.Id) != null);
            if (usableSkills.Length <= 0) return;
            int randomValue = Random.Range(0, usableSkills.Length);
            AttackUseSkill(usableSkills[randomValue].Id);
        }
      

        private void OnDestroy()
        {
            // 确保在销毁时清理所有技能实体
            if (skillManager != null && skillManager.m_Attack != null)
            {
                GameEntry.Entity.HideEntity(skillManager.m_Attack.Entity);
            }
        }

        public void AttackUseSkill(int weaponId, bool isBatter = false)
        {
            AttackData newAttack;
            
            if (!isBatter || currentAttackData == null)
            {
                // 第一次攻击，使用weaponId
                newAttack = skillManager.PrepareSkill(weaponId);
                Log.Info($"First attack with WeaponId: {weaponId}");
            }
            else 
            {
                // 连击，使用上一次攻击的NextBatterID
                int nextAttackId = currentAttackData.NextBatterID;
                if (nextAttackId <= 0)
                {
                    Log.Warning($"No next batter attack for AttackId: {currentAttackData.AttackId}");
                    return;
                }
                newAttack = skillManager.PrepareSkill(nextAttackId);
                Log.Info($"Batter attack: {currentAttackData.AttackId} -> {nextAttackId}");
            }

            if (newAttack == null)
            {
                Log.Error("Failed to prepare attack data");
                return;
            }

            // 保存这次的攻击数据，供下次连击使用
            currentAttackData = newAttack;
            
            // 播放动画
            if (anim != null)
            {
                anim.SetBool(currentAttackData.AnimParaName, true);
            }
        }

        // 在适当时机重置攻击状态
        public void ResetAttackState()
        {
            currentAttackData = null;
           // lastPressTime = -1f;
        }
}
}