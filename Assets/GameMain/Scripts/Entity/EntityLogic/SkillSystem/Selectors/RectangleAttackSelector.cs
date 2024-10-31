using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace StarForce.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class RectangleAttackSelector : IAttackSelector
    {
        /// <summary>
        /// 矩形选区
        /// </summary>
        /// <param name="技能数据"></param>
        /// <param name="技能组件的Transform"></param>
        /// <returns></returns>

        public Transform[] SelectTarget(AttackData data, Transform skillTF)
        {
            //根据技能数据中的标签，获取所有目标
            //string[] ---> Transform[]
            List<Transform> targets = new List<Transform>();
            string[] tags = data.AttackTargetTags;
            for (int i = 0; i < tags.Length; i++)
            {
                GameObject[] tempGoArray = GameObject.FindGameObjectsWithTag(tags[i]);
                targets.AddRange(tempGoArray.Select(s => s.transform));
            }
      
            //判断攻击范围（扇形/圆形）
            float skillOwnerRotationY = data.SkillOwner.transform.rotation.y;
            bool skillOwnerRotate = skillOwnerRotationY == 0 ? true : false;
            if (skillOwnerRotate)
            {
                //targets = targets.FindAll(s =>
                // s.position.x - skillTF.position.x <= data.attackDistance
                //&& s.position.x - skillTF.position.x >= 0
                //&& Mathf.Abs(s.position.y - skillTF.position.y) <= data.attackWidth / 2
                //    && s.GetComponent<CharacterData>().HP > 0);

            }
            else
            {
              //  targets = targets.FindAll(s => skillTF.position.x - s.position.x <= data.attackDistance
              //  && skillTF.position.x - s.position.x >= 0
              //&& Mathf.Abs(s.position.y - skillTF.position.y) <= data.attackWidth / 2
              //&& s.GetComponent<CharacterData>().HP > 0);
            }

            if (targets.Count == 0) return null;
            else
            {
                //if (data.attackType == SkillAttackType.Group)
                //    return targets.ToArray();
                //Transform min = targets.ToArray().GetMin(s => Vector3.Distance(s.position, skillTF.position));
                //return new Transform[] { min };
            }
            return null;
        }
    }
}


