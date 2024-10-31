//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public abstract class GameBase
    {
        public abstract GameMode GameMode
        {
            get;
        }

        protected ScrollableBackground SceneBackground
        {
            get;
            private set;
        }

        public bool GameOver
        {
            get;
            protected set;
        }

        private Player m_Player = null;

        public int heroID = 10011;

       // public int weaponID = 101010;
        public string m_name = "RedHeroKnight_Sword";

        public virtual void Initialize()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

            //SceneBackground = Object.FindObjectOfType<ScrollableBackground>();
            //if (SceneBackground == null)
            //{
            //    Log.Warning("Can not find scene background.");
            //    return;
            //}

            // SceneBackground.VisibleBoundary.gameObject.GetOrAddComponent<HideByBoundary>();           
            var playerData = new PlayerData(GameEntry.Entity.GenerateSerialId(), heroID)
            {
                Name = m_name,
                Position = Vector3.zero,
            };
            playerData.WeaponId = 0;
            
            GameEntry.Entity.ShowPlayer(playerData);
            
            GameOver = false;
            m_Player = null;
        }

        public virtual void Shutdown()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_Player != null && m_Player.IsDead)
            {
                GameOver = false;
                // GameOver = true;
                return;
            }
        }

        protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.EntityLogicType == typeof(Player))
            {
                m_Player = (Player)ne.Entity.Logic;
                
                // 获取 PlayerController 并设置 PlayerData
                var playerController = m_Player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    PlayerData playerData = ne.UserData as PlayerData;
                    playerController.SetPlayerData(playerData);
                    Log.Info($"Set PlayerData to PlayerController: {playerData.Name}");
                }
                else
                {
                    Log.Error("PlayerController not found on player entity");
                }
            }
        }

        protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }
    }
}
