using GameFramework.DataTable;
using System;
using UnityEngine;  
using UnityGameFramework.Runtime;


namespace StarForce
{
    [Serializable]
    public class WeaponData : AccessoryObjectData
    {
        [SerializeField]
        private string m_WeaponName = string.Empty;

        [SerializeField]
        private string m_WeaponDescription = string.Empty;

        [SerializeField]
        private int m_WeaponType = 0;

        public WeaponData(int entityId, int typeId, int ownerId, CampType ownerCamp) 
            : base(entityId, typeId, ownerId, ownerCamp)
        {
            IDataTable<DRWeapon> dtWeapon = GameEntry.DataTable.GetDataTable<DRWeapon>();
            DRWeapon drWeapon = dtWeapon.GetDataRow(typeId);
            if (drWeapon != null)
            {
                m_WeaponName = drWeapon.WeaponName;
                m_WeaponDescription = drWeapon.WeaponDescription;
                m_WeaponType = drWeapon.WeaponType;
            }
            else
            {
                Log.Error($"Cannot find weapon data for typeId: {typeId}");
            }
        }

        public string WeaponName => m_WeaponName;
        public string WeaponDescription => m_WeaponDescription;
        public int WeaponType => m_WeaponType;
        public int WeaponId => TypeId;
        public string WeaponTypeString => m_WeaponType == 0 ? "近战" : "远程";
    }
}
