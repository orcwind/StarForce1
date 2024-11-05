using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityGameFramework.Editor.ResourceTools;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using System;
using System.Linq;


namespace StarForce.Skill
{
    /// <summary>
    /// ܹȡбɼ
    /// </summary>
    public class CharacterSKillManager : MonoBehaviour
    {
        [SerializeField]
        public AttackData[] attacks;
        [SerializeField]
        public CharacterData m_characterData;
        [SerializeField]
        public Attack m_Attack;
        [SerializeField]
        private int m_WeaponId;

        private void Start()
        {   
            m_characterData = GetComponent<Player>().PlayerData; 
            if (m_characterData != null)
            {
                UpdateAttacks(m_characterData.WeaponId);
            }
            
            // 订阅事件
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowAttackSuccess);
            GameEntry.Event.Subscribe(WeaponChangedEventArgs.EventId, OnWeaponChanged);
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

        private void UpdateAttacks(int weaponId)
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
            Debug.Log("prepare skill id is "+id);
            foreach(AttackData data in attacks)
            {
                Debug.Log("attack data id is "+data.TypeId);
            }

           AttackData attackGo = attacks.Find(s => s.TypeId == id);
        
         if (attackGo == null)
            {
                Debug.Log("attackGo is null");
                return null;
            }
            else
         {            
              
                if (attackGo != null && attackGo.CoolRemain <= 0)
                    return attackGo;
                else
                {
                    Debug.Log("sillgo cannot cast");
                    return null; }
            }
        }

        //ɼ
        public IEnumerator GenerateSkill(AttackData data)
        {
            if (data == null)
            {
                Log.Error("AttackData is null");
                yield break;
            }

            // 创建新的攻击数据
            AttackData tempAttackData = new AttackData(GameEntry.Entity.GenerateSerialId(), data.TypeId);
            
            // 显示攻击实体
            GameEntry.Entity.ShowAttack(tempAttackData);
            
            // 等待攻击实体创建完成
            float timeout = 5f;
            float elapsed = 0f;
            while (m_Attack == null && elapsed < timeout)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            if (m_Attack == null)
            {
                Log.Error($"Attack entity creation failed after {timeout} seconds");
                yield break;
            }

            Log.Info($"Attack entity created successfully: {m_Attack.name}");

            // 获取并配置技能部署器
            SkillDeployer deployer = m_Attack.GetComponent<SkillDeployer>();
            if (deployer == null)
            {
                Log.Error("SkillDeployer component not found on Attack entity");
                yield break;
            }

            // 配置技能部署器
            deployer.AttackData = data;
            deployer.DeploySkill();

            // 启动冷却计时
            StartCoroutine(CoolTimeDown(data));
        }
        //ȴʱ
        private IEnumerator CoolTimeDown(AttackData data)
        {
            data.CoolRemain = data.CoolTime;
            while (data.CoolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.CoolRemain--;
            }

        }


         private void OnShowAttackSuccess(object sender, GameEventArgs e)
         {
               ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.EntityLogicType == typeof(Attack))
         {
             m_Attack = (Attack)ne.Entity.Logic;
           Debug.Log($"Attack entity initialized: {m_Attack != null}");
            }
        }   
    }
}