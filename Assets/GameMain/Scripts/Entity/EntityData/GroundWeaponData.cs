using GameFramework.DataTable;
using System;
using UnityEngine;

namespace StarForce
{
    [Serializable]
    public class GroundWeaponData : EntityData
    {
        [SerializeField]
        private int m_WeaponId = 0;
        [SerializeField]
        private CampType m_OwnerCamp = CampType.Unknown;
        
        public GroundWeaponData(int entityId, int typeId, int weaponId, CampType ownerCamp) 
            : base(entityId, typeId)  // typeId 固定为 100000
        {
            m_WeaponId = weaponId;
            m_OwnerCamp = ownerCamp;
        }

        public int WeaponId => m_WeaponId;
        public CampType OwnerCamp => m_OwnerCamp;
    }
}