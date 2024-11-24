using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
using System;

namespace StarForce.Skill
{
    /// <summary>
    /// 碰撞体攻击选择器
    /// </summary>
    public class ColliderAttackSelector : IAttackSelector
    {
        private const string DEAD_LAYER_NAME = "DeadEnemy";
        
        /// <summary>
        /// 选择目标
        /// </summary>
        /// <param name="攻击数据"></param>
        /// <param name="技能位置"></param>
        /// <returns></returns>

        public Transform[] SelectTarget(AttackData data, Transform attackTF)
        {
            if (data.SkillOwner == null)
            {
                Debug.LogError("SkillOwner is null in ColliderAttackSelector");
                return null;
            }

            // 获取 AttackCollider 组件
            AttackCollider attackCollider = data.SkillOwner.GetComponentInChildren<AttackCollider>();
            if (attackCollider == null)
            {
                Debug.LogError($"AttackCollider component not found on {data.SkillOwner.name}");
                return null;
            }

            // 过滤掉死亡单位
            List<Transform> validTargets = new List<Transform>();
            foreach (Transform target in attackCollider.targets)
            {
                if (target != null && target.gameObject.layer != LayerMask.NameToLayer(DEAD_LAYER_NAME))
                {
                    validTargets.Add(target);
                }
            }

            // 根据技能攻击类型返回目标
            if (data.SkillAttackType == SkillAttackType.Group)
            {
                return validTargets.ToArray();
            }
            else
            {
                // 单体攻击时返回最近的目标
                if (validTargets.Count == 0) return null;
                Transform min = validTargets.ToArray().GetMin(s => Vector3.Distance(s.position, attackTF.position));
                return new Transform[] { min };
            }
        }
      
        
    }
}


