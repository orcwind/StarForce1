//------------------------------------------------------------
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
            
            // 通过WeaponManager生成武器
            WeaponManager.Instance.SpawnWeaponOnGround(101010, new Vector3(-2f, 0f, 0f));
            WeaponManager.Instance.SpawnWeaponOnGround(101020, new Vector3(2f, 0f, 0f));
        }   

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }
    }
}
