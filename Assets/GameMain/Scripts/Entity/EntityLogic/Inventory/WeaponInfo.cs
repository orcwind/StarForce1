using System.Collections.Generic;
using System.Linq;
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
            WeaponData = new WeaponData(
                GameEntry.Entity.GenerateSerialId(),
                weaponId,
                0,
                CampType.Player
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
            
            // 获取武器类型和编号
            int weaponType = (weaponId / 100) % 100;  // xx部分
            int weaponNumber = weaponId % 100;        // yy部分
            
            // 构建基础攻击ID
            int baseAttackId = 900000 + (weaponType * 100) + weaponNumber * 10;
            
            // 加载所有攻击数据
            var drAttacks = dtAttack.GetDataRows(a => a.WeaponId == weaponId);
            foreach (var drAttack in drAttacks)
            {
                // 生成攻击实体ID
                int entityId = GameEntry.Entity.GenerateSerialId();
                
                // 根据攻击类型选择正确的预制体类型ID
                int attackTypeId;
                if (weaponType == 1)
                    attackTypeId = 901000; // 近战攻击
                else if (weaponType == 2)
                    attackTypeId = 902000; // 远程攻击
                else
                    attackTypeId = 903000; // 特殊攻击
                    
                // 创建攻击数据
                AttackData attackData = new AttackData(entityId, drAttack.Id);
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

        public AttackData[] GetAttackDataArray()
        {
            return m_AttackDataDict.Values.ToArray();
        }
    }
}
