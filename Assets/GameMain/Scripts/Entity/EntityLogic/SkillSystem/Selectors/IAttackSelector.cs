using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce.Skill
{
/// <summary>
/// 
/// </summary>
    public interface IAttackSelector
    {
        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <param name="技能数据"></param>
        /// <param name="技能所在物体变化组件"></param>
        /// <returns></returns>
        Transform[] SelectTarget(AttackData data,Transform skillTF);
    }

}