using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce

{
	///<summary>
	///角色动画数据
	///<summary>
    [System.Serializable]
    public class CharacterAnimationData
    {
        // 基础动作
        public string idle = "idle";
        public string run = "run";
        public string walk = "walk";
        public string jump = "jump";
        public string roll = "roll";
        public string hurt = "hurt";
        public string death = "death";

        // 普通攻击系列
        public string attack01 = "attack01";
        public string attack02 = "attack02";
        public string attack03 = "attack03";
        public string attack04 = "attack04";

        // 蓄力攻击系列
        public string attackCharging = "attackCharging";
        public string attackChargeDown = "attackChargeDown";
        public string meleeAttackUpCharge = "meleeAttackUpCharge";
        public string meleeAttackDownCharge = "meleeAttackDownCharge";

        // 空中攻击系列
        public string attackAir = "attackAir";
        public string attackAirDown = "attackAirDown";

        // 射击系列
        public string shoot = "shoot";
        public string shootCharge = "shootCharge";

        // 特殊技能
        public string counterStrike = "counterStrike";
        public string burst = "burst";

        // 状态参数
        public string isGround = "isGround";
        public string speed = "speed";
    }
}