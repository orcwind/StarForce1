using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace StarForce.Skill
{
    [RequireComponent(typeof(CharacterSKillManager))]
/// <summary>
/// ��װ����ϵͳ���ṩ�򵥵ļ����ͷŹ���
/// </summary>
public class CharacterSkillSystem : MonoBehaviour
{
        private CharacterSKillManager skillManager;
        private Animator anim;
        private AttackData skill;
        private IAttackSelector selector;
        private AnimationEventBehaviour animEventBehaviour;

        private void Start()
        {
            skillManager = GetComponent<CharacterSKillManager>();
            anim = GetComponentInChildren<Animator>();
            animEventBehaviour = GetComponentInChildren<AnimationEventBehaviour>();
            animEventBehaviour.AttackHandler += DeploySkill;
        }

        private void DeploySkill()
        {
            Debug.Log("skill assetNmae is " + skill.AssetName);
            StartCoroutine( skillManager.GenerateSkill(skill));
        }

        /// <summary>
        /// Ϊ����ṩ
        /// </summary>
        public void AttackUseSkill(int skillID,bool isBatter=false)
        {
            if (skill!=null && isBatter)
                skillID = skill.NextBatterID;          

            //׼������
            skill = skillManager.PrepareSkill(skillID);
            if (skill == null) return;
              //���Ŷ���

            anim.SetBool(skill.AnimationName, true);

           // Transform tf = transform.FindChildByName("AttackArea");

            //***********����������е�Ŀ��
           // tf.GetComponent<AttackCollider>()?.targets.Clear();
            //����Ŀ��
           // Transform targetTF = SelectTarget();
                       
            //�������
           // if (skill.attackType != SkillAttackType.Single || targetTF==null) return ;

            #region ����Ŀ�� ѡ��Ŀ��
            //����Ŀ�� ѡ��Ŀ��
           // ����Ŀ��
           // transform.LookAt(targetTF);
            //����Ŀ��
            //ѡ��Ŀ��
            //1. ѡ��Ŀ�꣬���ָ��ʱ���ȡ��ѡ��
            //2. ѡ��AĿ�꣬���Զ�ȡ��ǰ����ѡ��BĿ�꣬����Ҫ�ֶ���Aȡ��
            // ����˼�룺 �洢�ϴ�ѡ���Ŀ��
            //��ȡ���ϴ�ѡ�е�����
            //SetSelectedActiveFx(false);
            //selectedTarget = targetTF;
            //SetSelectedActiveFx(true);
            #endregion
        }
        [HideInInspector]
        public Transform selectedTarget; 
        private Transform SelectTarget()
        {
            Transform[] target = new SectorAttackSelector().SelectTarget(skill, transform);
          //  return target.Length != 0 ? target[0] : null;
            return target !=null ? target[0] : null;
        }

        private void SetSelectedActiveFx(bool state)
        {   if (selectedTarget == null) return;
            var selected = selectedTarget.GetComponent<CharacterSelected>();
            if (selected) selected.SetSelectedActive(state);
        }
        /// <summary>
        /// ʹ��������ܣ�ΪNPC�ṩ��
        /// </summary>
        public void UseRandomSkill()
        {
           // ���󣺴ӹ���������ѡ������ļ���
             //��ɸѡ�����п����ͷŵļ��ܣ��ٲ��������
             var usableSkills = skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.Id) != null);
            if (usableSkills.Length <= 0) return;
            int randomValue = Random.Range(0, usableSkills.Length);
            AttackUseSkill(usableSkills[randomValue].Id);
        }
      


}
}