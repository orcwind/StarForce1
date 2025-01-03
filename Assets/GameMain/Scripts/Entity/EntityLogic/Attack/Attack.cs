﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;
using StarForce.Skill;

namespace StarForce
{
    /// <summary>
    /// 特效类。
    /// </summary>
    public class Attack : Entity
    {
        [SerializeField]
        public AttackData m_AttackData = null;

        private SkillDeployer m_Deployer = null;
        private float m_ElapseSeconds = 0f;
        private bool m_IsDeployed = false;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            Log.Info($"Attack.OnInit called: EntityId={Entity.Id}");
            
            // 获取或添加 SkillDeployer 组件
            m_Deployer = GetComponent<SkillDeployer>();
            if (m_Deployer == null)
            {
                Log.Info("Adding SkillDeployer component");
                m_Deployer = gameObject.AddComponent<SkillDeployer>();
            }

        
        
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_AttackData = userData as AttackData;
            if (m_AttackData == null)
            {
                Log.Error("Attack data is invalid.");
                return;
            }

            // 记录详细日志以便调试
            Log.Info($"------Attack OnShow: EntityId={Entity.Id}, TypeId={m_AttackData.TypeId}, " +
                     $"AttackId={m_AttackData.AttackId}, Owner={m_AttackData.SkillOwner?.name}");

            // 设置攻击实体的位置和旋转
            if (m_AttackData.SkillOwner != null)
            {
                transform.position = m_AttackData.SkillOwner.transform.position;
                transform.rotation = m_AttackData.SkillOwner.transform.rotation;
                
            }

            // 初始化状态
            m_ElapseSeconds = 0f;
            m_IsDeployed = false;

            // 初始化并部署技能
            InitializeAndDeploySkill();

         
        }

         protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

        if (m_AttackData == null)
        {
            return;
        }

        m_ElapseSeconds += elapseSeconds;
        
        // 检查是否需要隐藏实体
        if (m_AttackData.DurationTime <= 0)
        {
            // 立即隐藏
            GameEntry.Entity.HideEntity(this);
        }
        else if (m_ElapseSeconds >= m_AttackData.DurationTime)
        {
            // 持续时间结束后隐藏
            GameEntry.Entity.HideEntity(this);
        }
    }

     

        protected override void OnHide(bool isShutdown, object userData)
        {
            // m_AttackData = null;
            // m_IsDeployed = false;
            // m_ElapseSeconds = 0f;
            base.OnHide(isShutdown, userData);
        }

        private void InitializeAndDeploySkill()
        {
            if (m_IsDeployed || m_Deployer == null || m_AttackData == null)
            {
                return;
            }

            try
            {
                // 设置技能部署器的数据
                m_Deployer.AttackData = m_AttackData;
                // 部署技能
                //m_Deployer.DeploySkill();
               // m_IsDeployed = true;
                
                
            }
            catch (System.Exception e)
            {
                Log.Error($"Error deploying skill: {e.Message}");
                GameEntry.Entity.HideEntity(this);
            }
        }
    }
}
