using Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace StarForce
{
    /// <summary>
    /// 小怪状态
    /// </summary>
    public class EnemyData : CharacterData
    {
        /// <summary>
        /// 贡献经验值
        /// </summary>
         public int GiveExp { get; private set; }

        public EnemyData(int entityId, int typeId) : base(entityId, typeId, CampType.Enemy)
        {

            GiveExp = 100;
        }


    }
}
