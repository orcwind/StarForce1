using UnityEngine;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using Common;

namespace StarForce
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {
        private Dictionary<int, EnemyInfo> m_EnemyInfoDict;
        private List<Enemy> m_ActiveEnemies;

        protected override void Init()
        {
            base.Init();
            m_EnemyInfoDict = new Dictionary<int, EnemyInfo>();
            m_ActiveEnemies = new List<Enemy>();
            LoadAllEnemyData();
        }

        private void LoadAllEnemyData()
        {
            IDataTable<DRCharacter> dtCharacter = GameEntry.DataTable.GetDataTable<DRCharacter>();
            
            foreach (var drCharacter in dtCharacter)
            {
                // 只加载敌人类型的数据（假设TypeId >= 20000 为敌人）
                if (drCharacter.Id >= 20000)
                {
                    EnemyInfo enemyInfo = new EnemyInfo(drCharacter.Id);
                    m_EnemyInfoDict.Add(drCharacter.Id, enemyInfo);
                }
            }
        }

        public void SpawnEnemy(int enemyId, Vector3 position, Quaternion rotation = default)
        {
            if (!m_EnemyInfoDict.ContainsKey(enemyId))
            {
                Log.Warning($"尝试生成不存在的敌人ID: {enemyId}");
                return;
            }

            EnemyData enemyData = new EnemyData(GameEntry.Entity.GenerateSerialId(), enemyId)
            {
                Position = position,
                Rotation = rotation
            };

            GameEntry.Entity.ShowEnemy(enemyData);
        }

        // 注册活跃的敌人实例
        public void RegisterEnemy(Enemy enemy)
        {
            if (!m_ActiveEnemies.Contains(enemy))
            {
                m_ActiveEnemies.Add(enemy);
            }
        }

        // 注销敌人实例
        public void UnregisterEnemy(Enemy enemy)
        {
            m_ActiveEnemies.Remove(enemy);
        }

        // 获取所有活跃的敌人
        public List<Enemy> GetAllActiveEnemies()
        {
            return m_ActiveEnemies;
        }

        // 获取指定范围内的敌人
        public List<Enemy> GetEnemiesInRange(Vector2 position, float radius)
        {
            List<Enemy> enemiesInRange = new List<Enemy>();
            
            foreach (var enemy in m_ActiveEnemies)
            {
                if (Vector2.Distance(position, enemy.transform.position) <= radius)
                {
                    enemiesInRange.Add(enemy);
                }
            }

            return enemiesInRange;
        }

        // 清除所有敌人
        public void ClearAllEnemies()
        {
            foreach (var enemy in m_ActiveEnemies.ToArray())
            {
                GameEntry.Entity.HideEntity(enemy.Entity);
            }
            m_ActiveEnemies.Clear();
        }
    }
} 