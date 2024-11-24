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
    [System.Serializable]
    public class EnemyData : CharacterData
    {
        [SerializeField]
        private int m_GiveExp = 100;

        public int GiveExp
        {
            get { return m_GiveExp; }
            private set { m_GiveExp = value; }
        }

        public EnemyData(int entityId, int typeId) : base(entityId, typeId, CampType.Enemy)
        {

        }


        override public void Death()
        {  
            base.Death();
            Debug.Log("enemy is dead");
            //TODO 死亡表现
        }

        

    }
}
