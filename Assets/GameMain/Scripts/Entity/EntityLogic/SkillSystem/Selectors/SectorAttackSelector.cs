using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace StarForce.Skill
{
/// <summary>
/// 
/// </summary>
    public class SectorAttackSelector:IAttackSelector
    {
        /// <summary>
        /// ���Ρ�Բ��ѡ��
        /// </summary>
        /// <param name="��������"></param>
        /// <param name="���������Transform"></param>
        /// <returns></returns>

        public Transform[] SelectTarget(AttackData data, Transform skillTF)
        {
            ////���ݼ��������еı�ǩ����ȡ����Ŀ��
            ////string[] ---> Transform[]
            //List<Transform> targets = new List<Transform>();
            //string[] tags = data.AttackTargetTags;
            //for (int i = 0; i < tags.Length; i++)
            //{
            //    GameObject[] tempGoArray = GameObject.FindGameObjectsWithTag(tags[i]);
            //    targets.AddRange(tempGoArray.Select(s => s.transform));
            //}
            ////�жϹ�����Χ������/Բ�Σ�
            ////targets = targets.FindAll(s =>
            ////      (Vector3.Distance(s.position, skillTF.position) <= data.attackDistance &&
            ////        Vector3.Angle(skillTF.forward, s.position - skillTF.position) <= data.attackAngle / 2) &&
            ////       s.GetComponent<CharacterStatus>().HP > 0);
            //targets = targets.FindAll(s =>
            //    (Vector3.Distance(s.position, skillTF.position) <= data.AttackDistance &&
            //      Vector3.Angle(skillTF.forward, s.position - skillTF.position) <= data.AttackAngle / 2));
            //if (targets.Count == 0) return null;
            //else
            //{
            //    if (data.attackType == SkillAttackType.Group)
            //        return targets.ToArray();
            //    Transform min = targets.ToArray().GetMin(s => Vector3.Distance(s.position, skillTF.position));
            //    return new Transform[] { min };
            //}
            return null;    
        }
        }
    }


//public GameObject[] SelectTarget(SkillData skillData, Transform skillTransform)
//        {  
//            //1 ��tag��� ͨ��tag�� ���ܸߣ�  ��ʱ��:ָ���뾶 
//            //   �ұ��Ϊ c.tag in attackTargetTags={"Enemy","Boss"}
//            //   string string[]   Array.Index(stringArr��string )>=0
//            List<GameObject> listTargets = new List<GameObject>();
//            for(int i=0;i<skillData.attackTargetTags.Length;i++)
//            {                
//                var targets=GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
               
//                if (targets != null && targets.Length > 0)
//                { listTargets.AddRange(targets); }
//            }
//            if (listTargets.Count == 0) return null;
//            //2  ���ˣ��ȽϾ��롾ָ���뾶�� ���е�����  ͬʱ ���ŵ� HP>0
//            var enemys = listTargets.FindAll(go =>
//                (Vector3.Distance(go.transform.position,
//                skillTransform.position)<skillData.attackDistance)
//                &&(go.GetComponent<CharacterStatus>().HP>0)
//                &&(Vector3.Angle(skillTransform.forward,
//                go.transform.position-skillTransform.position)
//                <=skillData.attackAngle*0.5f));
//            if (enemys == null || enemys.Count == 0) return null;
//            //3 ���ݼ��ܹ������� ȷ�����ص�������
//            switch (skillData.attackType)
//            {
//                case SkillAttackType.Group:
//                    return enemys.ToArray();
//                    break;
//                case SkillAttackType.Single:
//                    var go = ArrayHelper.Min(enemys.ToArray(),
//                        e => Vector3.Distance(skillTransform.position,
//                            e.transform.position));
//                    return new GameObject[] { go };
//                    break;
//            }
//           return null;
