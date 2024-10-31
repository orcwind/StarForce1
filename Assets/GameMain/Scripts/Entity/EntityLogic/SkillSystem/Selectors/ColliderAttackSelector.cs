using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
using System;

namespace StarForce.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class ColliderAttackSelector : IAttackSelector
    {
        /// <summary>
        /// ����ѡ��
        /// </summary>
        /// <param name="��������"></param>
        /// <param name="���������Transform"></param>
        /// <returns></returns>

        public Transform[] SelectTarget(AttackData data, Transform skillTF)
        {
            
            //���ݼ��������еı�ǩ����ȡ����Ŀ��
            //string[] ---> Transform[]
            List<Transform> targets = new List<Transform>();         
           Transform tf= data.SkillOwner.transform.FindChildByName("AttackArea");           
           targets = tf.GetComponentInChildren<AttackCollider>()?.targets;
                           
            if (targets.Count == 0) return null;
            else
            {
                if (data.SkillAttackType == SkillAttackType.Group)
                    return  targets.ToArray();
                Transform min = targets.ToArray().GetMin(s => Vector3.Distance(s.position, skillTF.position));
                return new Transform[] { min };
            }
                       
        }
      
        
    }
}


