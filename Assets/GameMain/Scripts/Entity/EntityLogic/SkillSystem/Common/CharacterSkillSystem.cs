using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityGameFramework.Runtime;
using System;
using System.Linq; 

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
       private CharacterMotor controller;
        public Player player;

        private AttackData currentAttackData = null; // 保存当前/上一次的攻击数据

        private float lastAttackTime = -1f;

        private void Start()
        {
            skillManager = GetComponent<CharacterSKillManager>();
            anim = GetComponentInChildren<Animator>();
            GetComponentInChildren<AnimationEventBehaviour>().AttackHandler += DeploySkill;
            player = GetComponent<Player>();
            controller = GetComponent<CharacterMotor>();
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
            int randomValue = UnityEngine.Random.Range(0, usableSkills.Length);
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

        public void AttackUseSkill(int attackId, bool isBatter = false)
        {
            
            
            if (skillManager == null)
            {
                Log.Error("SkillManager is null");
                return;
            }

            try 
            {
                // 检查武器ID是否有效
                if (attackId <= 0)
                {
                    Log.Warning($"Invalid attackId: {attackId}");
                    return;
                }
                if (isBatter) attackId = skillManager.m_Attack.m_AttackData.NextBatterID;
                // 准备攻击数据
                AttackData newAttack = skillManager.PrepareSkill(attackId);
                if (newAttack == null)
                {
                    Log.Error($"Failed to prepare attack data for attackId: {attackId}");
                    return;
                }
             
                // 更新当前攻击数据和时间
                currentAttackData = newAttack;
                lastAttackTime = Time.time;
                
                // 播放动画
                if (anim != null && !string.IsNullOrEmpty(currentAttackData.AnimParaName))
                {
                    anim.SetBool(currentAttackData.AnimParaName, true);
                    
                }
                else
                {
                    Log.Warning($"Animation setup failed - Animator: {anim != null}, AnimParaName: {currentAttackData.AnimParaName}");
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error in AttackUseSkill: {e.Message}\nStackTrace: {e.StackTrace}");
            }
        }

        // 在适当时机重置攻击状态
        public void ResetAttackState()
        {
            if (anim != null && currentAttackData != null)
            {
                anim.SetBool(currentAttackData.AnimParaName, false);
            }
            currentAttackData = null;
            lastAttackTime = -1f;
            Log.Info("Attack state reset due to timeout");
        }
}
}