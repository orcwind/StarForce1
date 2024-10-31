//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2024-10-17 08:54:36.549
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
    /// 动作表。
    /// </summary>
    public class DRAttack : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取模型编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取武器id。
        /// </summary>
        public int WeaponId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取武器描述。
        /// </summary>
        public string WeaponDescription
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取近战远程类型。
        /// </summary>
        public string WeaponType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取武器图标名字。
        /// </summary>
        public string WeaponName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击编号。
        /// </summary>
        public int AttackId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取下个连击id。
        /// </summary>
        public int NextBatterID
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取动画名称。
        /// </summary>
        public string AnimationName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取持续时间。
        /// </summary>
        public float DurationTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取在持续时间内两次伤害之间的间隔。
        /// </summary>
        public float AtkInterval
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取背景音乐编号（没有就设置为0）。
        /// </summary>
        public int BackgroundId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取特效预制件名称。
        /// </summary>
        public string AssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取伤害。
        /// </summary>
        public float AtkDamage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取冷却时间。
        /// </summary>
        public int CoolTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取冷却剩余。
        /// </summary>
        public int CoolRemain
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击距离。
        /// </summary>
        public float AttackDistance
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取扇形角度。
        /// </summary>
        public float AttackAngle
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击范围（上下距离）。
        /// </summary>
        public float AttackWidth
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取暴击率%。
        /// </summary>
        public float CriticalRate
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取暴击伤害增加倍率。
        /// </summary>
        public float CriticalDamage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取击退距离。
        /// </summary>
        public float KnockBackDistance
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取击退速度。
        /// </summary>
        public float KnockBackSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取选择器Sector=1Rectangle=2Collider=3。
        /// </summary>
        public int SelectorType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取单攻Single=1Group=2。
        /// </summary>
        public int SkillAttackType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能对应的动画参数。
        /// </summary>
        public string AnimParaName
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
            WeaponId = int.Parse(columnStrings[index++]);
            WeaponDescription = columnStrings[index++];
            WeaponType = columnStrings[index++];
            WeaponName = columnStrings[index++];
            AttackId = int.Parse(columnStrings[index++]);
            NextBatterID = int.Parse(columnStrings[index++]);
            AnimationName = columnStrings[index++];
            DurationTime = float.Parse(columnStrings[index++]);
            AtkInterval = float.Parse(columnStrings[index++]);
            BackgroundId = int.Parse(columnStrings[index++]);
            AssetName = columnStrings[index++];
            AtkDamage = float.Parse(columnStrings[index++]);
            CoolTime = int.Parse(columnStrings[index++]);
            CoolRemain = int.Parse(columnStrings[index++]);
            AttackDistance = float.Parse(columnStrings[index++]);
            AttackAngle = float.Parse(columnStrings[index++]);
            AttackWidth = float.Parse(columnStrings[index++]);
            CriticalRate = float.Parse(columnStrings[index++]);
            CriticalDamage = float.Parse(columnStrings[index++]);
            KnockBackDistance = float.Parse(columnStrings[index++]);
            KnockBackSpeed = float.Parse(columnStrings[index++]);
            SelectorType = int.Parse(columnStrings[index++]);
            SkillAttackType = int.Parse(columnStrings[index++]);
            AnimParaName = columnStrings[index++];

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
                    WeaponId = binaryReader.Read7BitEncodedInt32();
                    WeaponDescription = binaryReader.ReadString();
                    WeaponType = binaryReader.ReadString();
                    WeaponName = binaryReader.ReadString();
                    AttackId = binaryReader.Read7BitEncodedInt32();
                    NextBatterID = binaryReader.Read7BitEncodedInt32();
                    AnimationName = binaryReader.ReadString();
                    DurationTime = binaryReader.ReadSingle();
                    AtkInterval = binaryReader.ReadSingle();
                    BackgroundId = binaryReader.Read7BitEncodedInt32();
                    AssetName = binaryReader.ReadString();
                    AtkDamage = binaryReader.ReadSingle();
                    CoolTime = binaryReader.Read7BitEncodedInt32();
                    CoolRemain = binaryReader.Read7BitEncodedInt32();
                    AttackDistance = binaryReader.ReadSingle();
                    AttackAngle = binaryReader.ReadSingle();
                    AttackWidth = binaryReader.ReadSingle();
                    CriticalRate = binaryReader.ReadSingle();
                    CriticalDamage = binaryReader.ReadSingle();
                    KnockBackDistance = binaryReader.ReadSingle();
                    KnockBackSpeed = binaryReader.ReadSingle();
                    SelectorType = binaryReader.Read7BitEncodedInt32();
                    SkillAttackType = binaryReader.Read7BitEncodedInt32();
                    AnimParaName = binaryReader.ReadString();
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
