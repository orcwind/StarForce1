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
        /// ����Ŀ��
        /// </summary>
        /// <param name="��������"></param>
        /// <param name="������������仯���"></param>
        /// <returns></returns>
        Transform[] SelectTarget(AttackData data,Transform skillTF);
    }

}