using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace StarForce.Skill
{
    [RequireComponent(typeof(CharacterSKillManager))]
/// <summary>
/// 封装技能系统，提供技能的生成和使用
/// </summary>
public class CharacterSkillSystem : MonoBehaviour
{
        private CharacterSKillManager skillManager;
        private Animator anim;
        public AttackData attack;
        private IAttackSelector selector;
        private AnimationEventBehaviour animEventBehaviour;

        private void Start()
        {
            skillManager = GetComponent<CharacterSKillManager>();
            anim = GetComponentInChildren<Animator>();
            animEventBehaviour = GetComponentInChildren<AnimationEventBehaviour>();
            animEventBehaviour.AttackHandler += DeploySkill;
        }

        /// <summary>
        /// 部署技能
        /// </summary>
        private void DeploySkill()
        {
            Debug.Log("skill assetNmae is " + attack.AssetName);
            StartCoroutine( skillManager.GenerateSkill(attack));
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        public void AttackUseSkill(int attackID,bool isBatter=false)
        {
            if (attack!=null && isBatter)
                attackID = attack.NextBatterID;          

            //准备技能
            attack = skillManager.PrepareSkill(attackID);
            if (attack == null) return;
              //���Ŷ���

             anim.SetBool(attack.AnimParaName, true);
    Debug.Log($"Setting animation parameter: {attack.AnimParaName} to true");
    
    bool isAttack = anim.GetBool(attack.AnimParaName);
    Debug.Log($"Animation parameter {attack.AnimParaName} is set to: {isAttack}");
            

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
            Transform[] target = new SectorAttackSelector().SelectTarget(attack, transform);
          //  return target.Length != 0 ? target[0] : null;
            return target !=null ? target[0] : null;
        }

        private void SetSelectedActiveFx(bool state)
        {   if (selectedTarget == null) return;
            var selected = selectedTarget.GetComponent<CharacterSelected>();
            if (selected) selected.SetSelectedActive(state);
        }
        /// <summary>
        /// 使用随机技能，为NPC提供
        /// </summary>
        public void UseRandomSkill()
        {
       //从技能管理器中找到所有可用的技能，并存储在usableSkills数组中，
       //产生一个随机数，随机数范围为0到usableSkills数组的长度，然后使用AttackUseSkill方法使用随机数对应的技能
             var usableSkills = skillManager.attacks.FindAll(s => 
             skillManager.PrepareSkill(s.Id) != null);
            if (usableSkills.Length <= 0) return;
            int randomValue = Random.Range(0, usableSkills.Length);
            AttackUseSkill(usableSkills[randomValue].Id);
        }
      


}
}