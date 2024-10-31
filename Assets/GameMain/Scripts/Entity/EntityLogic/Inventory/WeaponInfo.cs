using System.Collections.Generic;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class WeaponInfo
    {
        public WeaponData WeaponData { get; private set; }
        private Dictionary<int, AttackData> m_AttackDataDict;

        public WeaponInfo(int weaponId)
        {
            LoadWeaponData(weaponId);
            LoadAttackData(weaponId);
        }

        private void LoadWeaponData(int weaponId)
        {
            // 这里修改，TypeId使用固定值100000（武器类型的EntityId）
            const int WEAPON_TYPE_ID = 100000;
            WeaponData = new WeaponData(
                GameEntry.Entity.GenerateSerialId(), // entityId
                WEAPON_TYPE_ID,                      // typeId (固定值100000)
                weaponId,                           // weaponId (具体武器的ID，如101010)
                0,                                  // ownerId
                CampType.Player                     // ownerCamp
            );
            
            if (WeaponData.WeaponName == string.Empty)
            {
                Log.Warning($"无法找到武器ID为 {weaponId} 的数据");
            }
        }

        private void LoadAttackData(int weaponId)
        {
            m_AttackDataDict = new Dictionary<int, AttackData>();
            IDataTable<DRAttack> dtAttack = GameEntry.DataTable.GetDataTable<DRAttack>();
            var drAttacks = dtAttack.GetDataRows(a => a.WeaponId == weaponId);
            foreach (var drAttack in drAttacks)
            {
                AttackData attackData = new AttackData(GameEntry.Entity.GenerateSerialId(), drAttack.Id);
                m_AttackDataDict.Add(drAttack.AttackId, attackData);
            }
        }

        public AttackData GetAttackData(int attackId)
        {
            return m_AttackDataDict.TryGetValue(attackId, out AttackData attackData) ? attackData : null;
        }

        public IEnumerable<AttackData> GetAllAttacks()
        {
            return m_AttackDataDict.Values;
        }
    }
}
