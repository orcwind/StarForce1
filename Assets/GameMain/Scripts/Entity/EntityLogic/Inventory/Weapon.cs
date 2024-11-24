using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework;

namespace StarForce
{
    public class Weapon : Entity, IReference
    {
        [SerializeField]
        protected WeaponData m_WeaponData = null;

        private Transform m_ParentTransform = null;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        public void InitializeWeapon(object userData)
        {
            base.OnShow(userData); // 调用基类的protected OnShow方法

            m_WeaponData = userData as WeaponData;
            if (m_WeaponData == null)
            {
                Log.Error("Weapon data is invalid.");
                return;
            }

            Name = m_WeaponData.WeaponName;
            CachedTransform.localPosition = m_WeaponData.Position;
            CachedTransform.localRotation = m_WeaponData.Rotation;
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);

            Name = string.Format("Weapon of {0}", parentEntity.Name);
            CachedTransform.localPosition = Vector3.zero;
            m_ParentTransform = parentTransform;
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            m_WeaponData = null;
            base.OnHide(isShutdown, userData);
        }

        public int GetTypeId()
        {
            return m_WeaponData.TypeId;
        }

        public WeaponData WeaponData
        {
            get { return m_WeaponData; }
        }

        public static Weapon Create(object userData)
        {
            Weapon weapon = ReferencePool.Acquire<Weapon>();
            weapon.InitializeWeapon(userData);
            return weapon;
        }

        public void Clear()
        {
            m_WeaponData = null;
            m_ParentTransform = null;
        }

        public WeaponData GetWeaponData()
        {
            return m_WeaponData;
        }
    }
}
