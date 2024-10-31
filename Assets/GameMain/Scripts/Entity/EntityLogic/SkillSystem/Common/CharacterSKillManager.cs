 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityGameFramework.Editor.ResourceTools;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using System;


namespace StarForce.Skill
{
    /// <summary>
    /// ���ܹ���������ȡ�����б������ɼ���
    /// </summary>
    public class CharacterSKillManager : MonoBehaviour
    {
        //��������
        public AttackData[] skills;
        public CharacterData m_characterData;
        public Attack m_Attack;
        
        private void Start()
        {
            AttackData AD01 = new AttackData(GameEntry.Entity.GenerateSerialId(), 100110);
            AttackData AD02 = new AttackData(GameEntry.Entity.GenerateSerialId(), 100111);
            AttackData AD03 = new AttackData(GameEntry.Entity.GenerateSerialId(), 100112);
            skills =new AttackData[3] {AD01,AD02,AD03 };
          
            for (int i = 0; i < skills.Length; i++)
			{
			 InitSkill(skills[i]);
			}

            //��ʱ��10011���ݲ���
            m_characterData = new PlayerData(GameEntry.Entity.GenerateSerialId(), 10011);           
           GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowAttackSuccess);

        }



        //��ʼ������
        private void InitSkill(AttackData data)
        {          
         // data.skillPrefab=Resources.Load<GameObject>("Skill/"+data.prefabName);
       
       // data.SkillPrefab = ResourceManager.Load<GameObject>(data.name);
           data.SkillOwner = gameObject;
         Debug.Log("skillowner name is "+ data.SkillOwner);
           
        } 

        //׼�����ܣ��ж��Ƿ�����ͷ�����(��ȴ��������
        public AttackData PrepareSkill(int id)
        {
           AttackData skillGo = skills.Find(s => s.TypeId == id);
        
         if (skillGo == null)
            {
                print("skillgo is null");
                return null;
            }
            else
         {            
              
                if (skillGo != null && skillGo.CoolRemain <= 0)
                    return skillGo;
                else
                {
                    Debug.Log("sillgo cannot cast");
                    return null; }
            }
        }

        //���ɼ���
        public IEnumerator GenerateSkill(AttackData data)
        {
            if (m_Attack == null) 
            {Debug.Log("attack is null");
             yield break;
            }
           // Debug.Log("3 m_attack name is " + m_Attack.name);
            //��������
            // GameObject skillGo= Instantiate(data.skillPrefab, transform.position, transform.rotation);
            // GameObject skillGo = GameObjectPool.Instance.CreateObject(data.prefabName, data.skillPrefab, transform.position, transform.rotation);
            AttackData TempattackData = new AttackData(GameEntry.Entity.GenerateSerialId(), data.TypeId);
           // GameEntry.Entity.ShowAttack(TempattackData);
           
            yield return new WaitUntil(() => m_Attack != null);

            if (m_Attack == null)
    {
        Debug.LogError("m_Attack 仍然是 null，可能是 ShowAttack 没有正确设置");
        yield break;
    }

    Debug.Log("2 m_attack name is " + m_Attack.name);
          
          
            //string m_name = GameEntry.Entity.GetEntity(data.TypeId).gameObject.name;
           // Debug.Log("m_name is " + m_name);
                    
            SkillDeployer deployer = m_Attack.GetComponent<SkillDeployer>();
            if (deployer == null) 
            
            {Debug.Log("deployer is null");
           yield break;
            }
             Debug.Log("deployer.name is "+ deployer.name);
            for (int i = 0;i < data.ImpactType.Length;i++)
            {
                Debug.Log(data.ImpactType[i]);
            }
            //���ݼ������ݣ�ͬʱ��ʼ��deployer
           deployer.SkillData = data;//�ڲ������㷨����
         deployer.DeploySkill();//�ڲ�ִ���㷨����

          //���ټ���  ----��ʱδʵ��
          // Destroy(skillGo, data.durationTime);
            //GameObjectPool.Instance.CollectObject(skillGo, data.durationTime);

            //����������ȴ
            StartCoroutine(CoolTimeDown(data));
        }
        //������ȴ����ʱ
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
                if (m_Attack == null)
                {
                    Debug.LogError("OnShowAttackSuccess: m_Attack 设置失败");
                }
            }
        }
    }
}