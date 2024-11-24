using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityGameFramework.Editor.ResourceTools;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using System;
using System.Linq;
using GameFramework.ObjectPool;
using GameFramework;
using GameFramework.Entity;
using StarForce;

namespace StarForce.Skill
{
    /// <summary>
    /// 角色技能管理器
    /// </summary>
    public class CharacterSKillManager : MonoBehaviour
    {
        [SerializeField]
        public AttackData[] attacks;
        [SerializeField]
        public Attack m_Attack;
        [SerializeField]
        private int m_WeaponId;
        [SerializeField]
        private int baseAttackId;
        [SerializeField]
        private SkillDeployer deployer;

        private void Start()
        {   
            // 订阅事件
            GameEntry.Event.Subscribe(
                UnityGameFramework.Runtime.ShowEntitySuccessEventArgs.EventId, 
                OnShowAttackSuccess);
            GameEntry.Event.Subscribe(
                WeaponChangedEventArgs.EventId, 
                OnWeaponChanged);
            GameEntry.Event.Subscribe(
                UnityGameFramework.Runtime.HideEntityCompleteEventArgs.EventId, 
                OnHideEntityComplete);
        }

        private void OnDestroy()
        {
            GameEntry.Event.Unsubscribe(
                UnityGameFramework.Runtime.ShowEntitySuccessEventArgs.EventId, 
                OnShowAttackSuccess);
            GameEntry.Event.Unsubscribe(
                WeaponChangedEventArgs.EventId, 
                OnWeaponChanged);
            GameEntry.Event.Unsubscribe(
                UnityGameFramework.Runtime.HideEntityCompleteEventArgs.EventId, 
                OnHideEntityComplete);
        }

        private void OnWeaponChanged(object sender, GameEventArgs e)
        {
            WeaponChangedEventArgs ne = (WeaponChangedEventArgs)e;
            UpdateAttacks(ne.WeaponId);
            Log.Info($"Weapon changed event received: WeaponId={ne.WeaponId}");
        }

        public void UpdateAttacks(int weaponId)
        {
            m_WeaponId = weaponId;

            baseAttackId = 900000 + (weaponId / 100) % 100 * 1000 + weaponId % 100 * 10 + 1;
            WeaponInfo weaponInfo = new WeaponInfo(m_WeaponId);
            attacks = weaponInfo.GetAllAttacks().ToArray();
            
            if (attacks == null || attacks.Length == 0)
            {
                Log.Warning($"No attacks found for weapon {m_WeaponId}");
                return;
            }

           
        }

        private void InitSkill(AttackData data)
        {          
            data.SkillOwner = gameObject;
           
        } 

        public bool HasWeaponEquipped()
        {
            return m_WeaponId > 0 && attacks != null && attacks.Length > 0;
        }
      
        public AttackData PrepareSkill(int attackId)
        {         
            if (!HasWeaponEquipped())
            {
                Log.Warning("No weapon equipped");
                return null;
            }   

            if (attacks == null || attacks.Length == 0)
            {
                Log.Error("No attacks data available");
                return null;
            }
                      
            AttackData attackData = Array.Find(attacks, s => s.TypeId == attackId);
            
            if (attackData == null)
            {
                Log.Error($"Could not find attack with TypeId: {attackId}");
                return null;
            }
            
            if (attackData.CoolRemain > 0)
            {
                Log.Info($"Attack {attackId} is on cooldown: {attackData.CoolRemain}");
                return null;
            }

            Log.Info($"Successfully prepared attack: TypeId={attackData.TypeId}, AnimParaName={attackData.AnimParaName}");
            return attackData;
        }

        public void GenerateSkill(AttackData data)
        {
            if (data == null)
            {
                Log.Error("AttackData is null in GenerateSkill");
                return;
            }

            try 
            {
                // 创建新的AttackData实例
                AttackData newData = new AttackData(
                    GameEntry.Entity.GenerateSerialId(),
                    data.TypeId
                );
                newData.SkillOwner = this.gameObject;
                newData.Position = transform.position;
                newData.Rotation = transform.rotation;
                
               // Log.Info($"Generating attack with data: TypeId={newData.TypeId}, Position={newData.Position}, EntityId={newData.Id}");
                
                // 显示Attack实体
                GameEntry.Entity.ShowAttack(newData);
                
                //Log.Info($"Generated attack with data: TypeId={newData.TypeId}, AttackId={newData.AttackId}, Owner={newData.SkillOwner.name}, EntityId={newData.Id}");
            }
            catch (Exception e)
            {
                Log.Error($"Generate skill failed: {e.Message}\nStackTrace: {e.StackTrace}");
            }
        }

        private void OnShowAttackSuccess(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.ShowEntitySuccessEventArgs ne = (UnityGameFramework.Runtime.ShowEntitySuccessEventArgs)e;           
            if (ne.EntityLogicType == typeof(Attack))
            {
               
                m_Attack = (Attack)ne.Entity.Logic;
                
                if (m_Attack == null)
                {
                    Log.Error("Failed to cast entity logic to Attack type");
                    return;
                }

                // 获取并初始化SkillDeployer
                deployer = m_Attack.GetComponent<SkillDeployer>();
                if (deployer == null)
                {
                    Log.Error("SkillDeployer component not found on Attack entity");
                    return;
                }

                deployer.AttackData = m_Attack.m_AttackData;
                deployer.DeploySkill();
                //Log.Info($"Skill deployed: TypeId={m_Attack.m_AttackData.TypeId}, AttackId={m_Attack.m_AttackData.AttackId}");
                
               
            }
            
        }   

        private void OnHideEntityComplete(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.HideEntityCompleteEventArgs ne = 
                (UnityGameFramework.Runtime.HideEntityCompleteEventArgs)e;
            
            try
            {
                // 记录实体隐藏的日志信息
               
            }
            catch (Exception ex)
            {
                Log.Error($"Error in OnHideEntityComplete: {ex.Message}");
            }
        }
    }
}