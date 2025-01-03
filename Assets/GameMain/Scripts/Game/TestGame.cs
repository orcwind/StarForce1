﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using GameFramework.Debugger;
using UnityEngine;

namespace StarForce
{
    public class TestGame : GameBase
    {
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get { return GameMode.Test; }
        }

        public override void Initialize()
        {
            Debug.Log("初始化TestGame");
            base.Initialize();
            
            // 生成武器
            WeaponManager.Instance.SpawnWeaponOnGround(10101, new Vector3(-2f, 0f, 0f));
            WeaponManager.Instance.SpawnWeaponOnGround(10102, new Vector3(2f, 0f, 0f));

            // 使用EnemyManager生成敌人
            EnemyManager.Instance.SpawnEnemy(20001, new Vector3(-3f, 0f, 0f));
            EnemyManager.Instance.SpawnEnemy(20001, new Vector3(3f, 0f, 0f));
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }
    }
}
