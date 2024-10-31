using GameFramework.DataTable;
using System;
using UnityEngine;

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

        [SerializeField]
        private int m_WeaponId = 0;


//新结构 用weaponid 代替typeid获取数据
    public WeaponData(int entityId, int typeId, int weaponId, int ownerId, CampType ownerCamp) 
        : base(entityId, typeId, ownerId, ownerCamp)
    {
        IDataTable<DRWeapon> dtWeapon = GameEntry.DataTable.GetDataTable<DRWeapon>();
        DRWeapon drWeapon = dtWeapon.GetDataRow(weaponId);
        if (drWeapon != null)
        {
            m_WeaponName = drWeapon.WeaponName;
            m_WeaponDescription = drWeapon.WeaponDescription;
            m_WeaponType = drWeapon.WeaponType;
            m_WeaponId = weaponId;
        }
        else
        {
            Debug.LogError($"Cannot find weapon data for weaponId: {weaponId}");
        }
    }



        public string WeaponName
        {
            get { return m_WeaponName; }
        }

        /// <summary>
        /// 武器描述。
        /// </summary>
        public string WeaponDescription
        {
            get { return m_WeaponDescription; }
        }

        /// <summary>
        /// 武器类型（0:近战, 1:远程）。
        /// </summary>
        public int WeaponType
        {
            get { return m_WeaponType; }
        }

        public int WeaponId
        {
            get { return m_WeaponId; }
        }

        /// <summary>
        /// 获取武器类型的字符串表示。
        /// </summary>
        public string WeaponTypeString
        {
            get { return m_WeaponType == 0 ? "近战" : "远程"; }
        }
    }
}
