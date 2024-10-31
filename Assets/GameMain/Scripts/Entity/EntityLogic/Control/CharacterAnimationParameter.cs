using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarForce
{
    [System.Serializable]
    public class CharacterAnimationParameter
    {
        public string run = "run";
        public string idle = "idle";
        public string walk = "walk";     
        public string attack01 = "attack01";     
        public string attack02 = "attack02";      
        public string attack03 = "attack03";
        //普攻三段
        public string meleeAttack01 = "meleeAttack01";
        public string meleeAttack02 = "meleeAttack02";
        public string meleeAttack03 = "meleeAttack03";
        //蓄力攻击中
        public string attackCharging = "attackCharging";
        //蓄力攻击释放
        public string attackChargeDown = "attackChargeDown";
        //蓄力攻击上
        public string meleeAttackUpCharge = "meleeAttackUpCharge";
        //空中攻击
        public string attackAir = "attackAir";
        //空中攻击下
        public string attackAirDown = "attackAirDown";
        //蓄力攻击下
        public string meleeAttackDownCharge = "meleeAttackDownCharge";
        //射击
        public string shoot = "shoot";
        //蓄力射击
        public string shootCharge = "shootCharge";
        //反击
        public string counterStrike = "counterStrike";
        //使用怒气
        public string burst = "burst";

        public string death = "death";
        public string jump = "jump";
        public string isGround = "isGround";
        public string speed = "speed";
        public string roll = "roll";
        public string hurt = "hurt";
    }
}
