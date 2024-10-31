//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2024-10-17 08:54:36.548
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
    /// 马达表。
    /// </summary>
    public class DRCharacterMotor : DataRowBase
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
        /// 获取移动速度。
        /// </summary>
        public float MoveSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取跳跃高度。
        /// </summary>
        public float JumpHeight
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取重力加速度。
        /// </summary>
        public float Aspeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取Y方向加速度。
        /// </summary>
        public float Velocity_Y
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取Y方向移动速率。
        /// </summary>
        public float RateY
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取翻滚速度。
        /// </summary>
        public float RollSPeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取翻滚距离。
        /// </summary>
        public float RollDIstance
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击时位移距离。
        /// </summary>
        public float AtkMoveSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取默认跳跃次数。
        /// </summary>
        public float DefaultJumpCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取充能速率。
        /// </summary>
        public float ChargingSpeedRate
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取取消攻击指令间隔。
        /// </summary>
        public float CancelAttackTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取连击间隔。
        /// </summary>
        public float BatterAttackTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取投射物初始高度修正（离人物足部距离）。
        /// </summary>
        public float ProjectileHeight
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
            MoveSpeed = float.Parse(columnStrings[index++]);
            JumpHeight = float.Parse(columnStrings[index++]);
            Aspeed = float.Parse(columnStrings[index++]);
            Velocity_Y = float.Parse(columnStrings[index++]);
            RateY = float.Parse(columnStrings[index++]);
            RollSPeed = float.Parse(columnStrings[index++]);
            RollDIstance = float.Parse(columnStrings[index++]);
            AtkMoveSpeed = float.Parse(columnStrings[index++]);
            DefaultJumpCount = float.Parse(columnStrings[index++]);
            ChargingSpeedRate = float.Parse(columnStrings[index++]);
            CancelAttackTime = float.Parse(columnStrings[index++]);
            BatterAttackTime = float.Parse(columnStrings[index++]);
            ProjectileHeight = float.Parse(columnStrings[index++]);

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
                    MoveSpeed = binaryReader.ReadSingle();
                    JumpHeight = binaryReader.ReadSingle();
                    Aspeed = binaryReader.ReadSingle();
                    Velocity_Y = binaryReader.ReadSingle();
                    RateY = binaryReader.ReadSingle();
                    RollSPeed = binaryReader.ReadSingle();
                    RollDIstance = binaryReader.ReadSingle();
                    AtkMoveSpeed = binaryReader.ReadSingle();
                    DefaultJumpCount = binaryReader.ReadSingle();
                    ChargingSpeedRate = binaryReader.ReadSingle();
                    CancelAttackTime = binaryReader.ReadSingle();
                    BatterAttackTime = binaryReader.ReadSingle();
                    ProjectileHeight = binaryReader.ReadSingle();
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
