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

namespace StarForce.Skill
{
    /// <summary>
    /// ܹȡбɼ
    /// </summary>
    public class CharacterSKillManager : MonoBehaviour
    {
        [SerializeField]
        public AttackData[] attacks;
        //[SerializeField]
       // public CharacterData m_characterData;
        [SerializeField]
        public Attack m_Attack;
        [SerializeField]
        private int m_WeaponId;

        [SerializeField]
        private SkillDeployer deployer;

        private IObjectPool<AttackItemObject> m_AttackObjectPool = null;
        private const int DefaultCapacity = 16;

        private void Start()
        {   
            // m_characterData = GetComponent<Player>().PlayerData; 
            // if (m_characterData != null)
            // {
            //     UpdateAttacks(m_characterData.WeaponId);
            // }
            
            // 订阅事件
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowAttackSuccess);
            GameEntry.Event.Subscribe(WeaponChangedEventArgs.EventId, OnWeaponChanged);

            // 创建Attack对象池
            m_AttackObjectPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<AttackItemObject>("AttackPool", DefaultCapacity);
        }

        private void OnDestroy()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowAttackSuccess);
            GameEntry.Event.Unsubscribe(WeaponChangedEventArgs.EventId, OnWeaponChanged);
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
            WeaponInfo weaponInfo = new WeaponInfo(m_WeaponId);
            attacks = weaponInfo.GetAllAttacks().ToArray();
            
            if (attacks == null || attacks.Length == 0)
            {
                Log.Warning($"No attacks found for weapon {m_WeaponId}");
                return;
            }

            foreach(AttackData data in attacks)
            {
                Log.Info($"Updated attack id: {data.TypeId} for weapon {m_WeaponId}");
                InitSkill(data);
            }
        }

        //ʼ
        private void InitSkill(AttackData data)
        {          
         // data.skillPrefab=Resources.Load<GameObject>("Skill/"+data.prefabName);
       
       // data.SkillPrefab = ResourceManager.Load<GameObject>(data.name);
           data.SkillOwner = gameObject;
         Debug.Log("skillowner name is "+ data.SkillOwner);
           
        } 

        //׼ܣжǷͷ(ȴ
        public AttackData PrepareSkill(int id)
        {            
           AttackData attackGo = attacks.Find(s => s.TypeId == id);
        
         if (attackGo == null)
            {
                Debug.Log("attackGo is null");
                return null;
            }
            else
         {            
              
                if (attackGo != null && attackGo.CoolRemain <= 0)
               { Debug.Log("!!!attackgo is ready :"+ attackGo.TypeId);
                    return attackGo;}
                else
                {
                    Debug.Log("attackgo cannot cast");
                    return null; }
            }
        }

        //ɼ
        public void GenerateSkill(AttackData data)
        {
            if (data == null)
            {
                Log.Error("AttackData is null in GenerateSkill");
                return;
            }

            try 
            {
                // 创建新的AttackData，使用GenerateSerialId生成唯一ID
                AttackData newData = new AttackData(GameEntry.Entity.GenerateSerialId(), data.TypeId);
                
                // 直接使用GameEntry.Entity显示Attack实体
                GameEntry.Entity.ShowAttack(newData);
                
                Log.Info($"Generated attack: Id={newData.Id}, TypeId={newData.TypeId}");
            }
            catch (Exception e)
            {
                Log.Error($"Generate skill failed: {e.Message}");
            }
        }

        // 回收Attack对象
        public void RecycleAttack(Attack attack)
        {
            if (attack == null)
                return;

            AttackItemObject attackObject = attack.GetComponent<AttackItemObject>();
            if (attackObject != null)
            {
                m_AttackObjectPool.Unspawn(attackObject);
            }
        }

        private void OnShowAttackSuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            Debug.Log("ne.entitylogicType is "+ne.EntityLogicType);
            if (ne.EntityLogicType == typeof(Attack))
            {
                Log.Info($"OnShowAttackSuccess triggered for Attack entity: ID={ne.Entity.Id}");
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
                Log.Info($"Skill deployed: TypeId={m_Attack.m_AttackData.TypeId}, AttackId={m_Attack.m_AttackData.AttackId}");
                
                Log.Info("Attack entity successfully initialized");
            }
            Debug.Log("ne.entitylogicType is "+ne.EntityLogicType);
        }   
    }
}