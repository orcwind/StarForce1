//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace StarForce
{
    [StructLayout(LayoutKind.Auto)]
    public struct ImpactData
    {
        private readonly CampType m_Camp;
        private readonly float m_HP;
        private readonly float m_Attack;
        private readonly float m_Defense;

        public ImpactData(CampType camp, float hp, float attack, float defense)
        {
            m_Camp = camp;
            m_HP = hp;
            m_Attack = attack;
            m_Defense = defense;
        }

        public CampType Camp
        {
            get
            {
                return m_Camp;
            }
        }

        public float HP
        {
            get
            {
                return m_HP;
            }
        }

        public float Attack
        {
            get
            {
                return m_Attack;
            }
        }

        public float Defense
        {
            get
            {
                return m_Defense;
            }
        }
    }
}
