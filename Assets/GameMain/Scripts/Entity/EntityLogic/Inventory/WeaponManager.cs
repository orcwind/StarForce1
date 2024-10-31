using UnityEngine;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using Common;

namespace StarForce
{
    public class WeaponManager : MonoSingleton<WeaponManager>
    {
        private Dictionary<int, WeaponInfo> m_WeaponInfoDict;
        private List<GroundWeapon> m_GroundWeapons;

        protected override void Init()
        {
            base.Init();
            m_WeaponInfoDict = new Dictionary<int, WeaponInfo>();
            m_GroundWeapons = new List<GroundWeapon>();
            LoadAllWeaponData();
        }

        private void LoadAllWeaponData()
        {
            IDataTable<DRWeapon> dtWeapon = GameEntry.DataTable.GetDataTable<DRWeapon>();

            foreach (var drWeapon in dtWeapon)
            {
                WeaponInfo weaponInfo = new WeaponInfo(drWeapon.Id);
                m_WeaponInfoDict.Add(drWeapon.Id, weaponInfo);
            }
        }

        public WeaponInfo GetWeaponInfo(int weaponId)
        {
            if (m_WeaponInfoDict.TryGetValue(weaponId, out WeaponInfo weaponInfo))
            {
                return weaponInfo;
            }
            else
            {
                Log.Warning($"无法找到武器ID为 {weaponId} 的WeaponInfo");
                return null;
            }
        }

        // 添加新方法：生成地面武器
        public void SpawnWeaponOnGround(int weaponId, Vector3 position)
        {
            Log.Info($"尝试生成地面武器: WeaponId={weaponId}, Position={position}");
            
            if (!m_WeaponInfoDict.ContainsKey(weaponId))
            {
                Log.Warning($"尝试生成不存在的武器ID: {weaponId}");
                return;
            }

            GameEntry.Entity.ShowGroundWeapon(new GroundWeaponData(
                GameEntry.Entity.GenerateSerialId(),
                100000,  // GroundWeapon的typeId
                weaponId,
                CampType.Player)
            {
                Position = position,
                Rotation = Quaternion.identity
            });
        }

        // 注册地面武器实例
        public void RegisterGroundWeapon(GroundWeapon groundWeapon)
        {
            if (!m_GroundWeapons.Contains(groundWeapon))
            {
                m_GroundWeapons.Add(groundWeapon);
            }
        }

        // 注销地面武器实例
        public void UnregisterGroundWeapon(GroundWeapon groundWeapon)
        {
            m_GroundWeapons.Remove(groundWeapon);
        }

        // 获取所有地面武器
        public List<GroundWeapon> GetAllGroundWeapons()
        {
            return m_GroundWeapons;
        }
    }
}
