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

        private WeaponAnimationController m_WeaponAnimController;

        public PlayerData PlayerData
        {
            get { return (PlayerData)m_CharacterData; }
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
            //m_Controller= GetComponent<PlayerController>();
            //m_CharacterSKillManager = GetComponent<CharacterSKillManager>();
            //m_CharacterMotor=GetComponent<CharacterMotor>();
            //m_SkillSystem= GetComponent<CharacterSkillSystem>();

        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);
            if (!(m_CharacterData is PlayerData))
            {
                Log.Error("Player data is invalid.");
                return;
            }
         

            #region
            //ScrollableBackground sceneBackground = FindObjectOfType<ScrollableBackground>();
            //if (sceneBackground == null)
            //{
            //    Log.Warning("Can not find scene background.");
            //    return;
            //}

            //m_PlayerMoveBoundary = new Rect(sceneBackground.PlayerMoveBoundary.bounds.min.x, sceneBackground.PlayerMoveBoundary.bounds.min.z,
            //    sceneBackground.PlayerMoveBoundary.bounds.size.x, sceneBackground.PlayerMoveBoundary.bounds.size.z);
            #endregion
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

        private void Start()
        {
            m_WeaponAnimController = GetComponentInChildren<WeaponAnimationController>();
        }

        public void ChangeWeapon(int newWeaponId)
        {
            PlayerData.WeaponId = newWeaponId;
            if (m_WeaponAnimController != null)
            {
                m_WeaponAnimController.UpdateWeaponAnimations();
            }
        }
    }
}
