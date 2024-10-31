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
        public int GiveExp;

        public EnemyData(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
        {
        }

      


        // public float beAttackedSpeed;//被攻击后移动距离
        //private Vector2 direction;
        //private bool isHit;






    }
}
