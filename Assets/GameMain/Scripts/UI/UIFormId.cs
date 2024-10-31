//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace StarForce
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 开始界面。
        /// </summary>
        UIStartMenu = 1,

        /// <summary>
        /// 设置。
        /// </summary>
        UISettingForm =2,

        /// <summary>
        /// 背包界面
        /// </summary>
        UIInventory = 3,

        /// <summary>
        /// 装备栏界面
        /// </summary>
        UIEquipmentSlot = 4,

        /// <summary>
        /// 角色参数界面
        /// </summary>
        UICharacterDetailStatus = 5,

        /// <summary>
        /// 死亡界面
        /// </summary>
        UIGameOver = 6,

        /// <summary>
        /// 关于
        /// </summary>
        UIAboutForm=7,

        /// <summary>
        /// 对话界面
        /// </summary>
        UIDialogForm=8,

        

        /// <summary>
        /// 交互提示
        /// </summary>
        UIInteractionPrompt = 9,

        /// <summary>
        /// 武器信息
        /// </summary>
        UIItemInfo = 10,
    }
}
