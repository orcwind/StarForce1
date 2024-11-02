//------------------------------------------------------------
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
    public class Player : Character
    {
       
        private Vector3 m_TargetPosition = Vector3.zero;

       
        private PlayerController m_Controller;

      
        private CharacterMotor m_CharacterMotor;

   
        private CharacterSKillManager m_CharacterSKillManager;

    
        private CharacterSkillSystem m_SkillSystem;

        [SerializeField]
        private PlayerData m_PlayerData;
       
        public PlayerData PlayerData
        {
            get { return m_CharacterData as PlayerData; }
            private set 
            { 
                m_CharacterData = value;
                m_PlayerData = value;
            }
        }

        public override void Death()
        {
            GetComponentInChildren<Animator>().SetBool(PlayerData.death, true);
            print("PlayerStatus Dead ");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            
            Log.Info("Player OnInit开始");
            
            // 确保WeaponAnimationController在正确的位置
            var animController = GetComponentInChildren<WeaponAnimationController>();
            if (animController == null)
            {
                Log.Error("未找到WeaponAnimationController组件，请检查预制体结构");
            }
            else
            {
                Log.Info("成功找到WeaponAnimationController组件");
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);
            
            if (m_CharacterData == null)
            {
                Log.Error("Character data is null after base.OnShow");
                return;
            }

            // 设置数据
            PlayerData = m_CharacterData as PlayerData;
            if (PlayerData == null)
            {
                Log.Error("Invalid player data type");
                return;
            }

            // 确保序列化字段也被更新
            m_PlayerData = PlayerData;

            if (m_Controller != null)
            {
                m_Controller.SetPlayerData(PlayerData);
                Log.Debug($"Set player data to controller: {PlayerData.Name}");
            }

            Log.Debug($"Player OnShow - PlayerData: {PlayerData.Name}, Controller: {m_Controller != null}");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);


            #region
            //if (Input.GetMouseButton(0))
            //{
            //    Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    m_TargetPosition = new Vector3(point.x, 0f, point.z);

            //    for (int i = 0; i < m_Weapons.Count; i++)
            //    {
            //        m_Weapons[i].TryAttack();
            //    }
            //}

            //Vector3 direction = m_TargetPosition - CachedTransform.localPosition;
            //if (direction.sqrMagnitude <= Vector3.kEpsilon)
            //{
            //    return;
            //}

            //Vector3 speed = Vector3.ClampMagnitude(direction.normalized * m_MyAircraftData.Speed * elapseSeconds, direction.magnitude);
            //CachedTransform.localPosition = new Vector3
            //(
            //    Mathf.Clamp(CachedTransform.localPosition.x + speed.x, m_PlayerMoveBoundary.xMin, m_PlayerMoveBoundary.xMax),
            //    0f,
            //    Mathf.Clamp(CachedTransform.localPosition.z + speed.z, m_PlayerMoveBoundary.yMin, m_PlayerMoveBoundary.yMax)
            //);
            #endregion
        }
    }
}
