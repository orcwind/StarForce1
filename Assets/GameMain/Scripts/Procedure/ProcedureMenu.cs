//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace StarForce
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartNormalGame = false;
        private bool m_StartTest = false;
        private UIStartMenu m_UIStartMenu = null;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void StartNormalGame()
        {
            m_StartNormalGame = true;
        }


        public void StartTest()
        {
            m_StartTest = true;
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
      
            m_StartNormalGame = false;
            m_StartTest = false;
            GameEntry.UI.OpenUIForm(UIFormId.UIStartMenu, this);
            Debug.Log("打开UIStartMenu");
           
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if (m_UIStartMenu != null)
            {
                m_UIStartMenu.Close(isShutdown);
                m_UIStartMenu = null;
            }
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_StartNormalGame)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Main"));
             
                procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Normal);
                Debug.Log("开始Normal游戏");
              
                ChangeState<ProcedureChangeScene>(procedureOwner);
                
            }

            if (m_StartTest)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Test"));
             
                procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Test);
                Debug.Log("开始Test游戏");  
              
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_UIStartMenu = (UIStartMenu)ne.UIForm.Logic;
        }
    }
}
