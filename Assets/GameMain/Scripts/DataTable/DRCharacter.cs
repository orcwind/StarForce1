//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2024-10-17 08:54:36.547
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 角色状态表。
    /// </summary>
    public class DRCharacter : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取角色编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取攻击速度修正系数。
        /// </summary>
        public float AttackSpeedRate
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取伤害。
        /// </summary>
        public float BaseATK
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取防御。
        /// </summary>
        public float Defence
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取生命。
        /// </summary>
        public float HP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最大生命。
        /// </summary>
        public float MaxHP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取法力值。
        /// </summary>
        public float SP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最大法力值。
        /// </summary>
        public float MaxSP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取护盾。
        /// </summary>
        public float Shield
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取怒气值。
        /// </summary>
        public float Rage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最大怒气值。
        /// </summary>
        public float MaxRage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取移动速度。
        /// </summary>
        public float MoveSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取受伤闪烁时间。
        /// </summary>
        public float FlashTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取死亡效果。
        /// </summary>
        public int DeadEffectId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取死亡声音。
        /// </summary>
        public int DeadSoundId
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            AttackSpeedRate = float.Parse(columnStrings[index++]);
            BaseATK = float.Parse(columnStrings[index++]);
            Defence = float.Parse(columnStrings[index++]);
            HP = float.Parse(columnStrings[index++]);
            MaxHP = float.Parse(columnStrings[index++]);
            SP = float.Parse(columnStrings[index++]);
            MaxSP = float.Parse(columnStrings[index++]);
            Shield = float.Parse(columnStrings[index++]);
            Rage = float.Parse(columnStrings[index++]);
            MaxRage = float.Parse(columnStrings[index++]);
            MoveSpeed = float.Parse(columnStrings[index++]);
            FlashTime = float.Parse(columnStrings[index++]);
            DeadEffectId = int.Parse(columnStrings[index++]);
            DeadSoundId = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    AttackSpeedRate = binaryReader.ReadSingle();
                    BaseATK = binaryReader.ReadSingle();
                    Defence = binaryReader.ReadSingle();
                    HP = binaryReader.ReadSingle();
                    MaxHP = binaryReader.ReadSingle();
                    SP = binaryReader.ReadSingle();
                    MaxSP = binaryReader.ReadSingle();
                    Shield = binaryReader.ReadSingle();
                    Rage = binaryReader.ReadSingle();
                    MaxRage = binaryReader.ReadSingle();
                    MoveSpeed = binaryReader.ReadSingle();
                    FlashTime = binaryReader.ReadSingle();
                    DeadEffectId = binaryReader.Read7BitEncodedInt32();
                    DeadSoundId = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
