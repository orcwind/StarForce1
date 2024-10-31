using System.Collections;
using System.Collections.Generic;
using System.Linq;

//using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace StarForce.Skill
{
/// <summary>
/// »÷ÍËÐ§¹û
/// </summary>
    public class KnockBackImpact:IImpactEffect
    {
        //private SkillData data;
        //public bool isHit;
        public Vector2 direction;
        public void Execute(SkillDeployer deployer)
        {

            AttackData data = deployer.SkillData;

            float kbdistance = data.KnockBackDistance;
            if (data.AttackTargetTags == null) return;
            for (int i = 0; i < data.AttackTargets.Length; i++)
            {
                direction = data.SkillOwner.transform.rotation.y == 0 ? Vector2.right : Vector2.left;
               
                Rigidbody2D rb = data.AttackTargets[i].GetComponent<Rigidbody2D>();

                Vector3 target = new Vector3(rb.transform.position.x + direction.x * data.KnockBackDistance, rb.transform.position.y);
                MonoBehaviourHelper.StartCoroutine(KnockbackMove(data, target, rb.transform));
               
            }
        }


        public IEnumerator KnockbackMove(AttackData data, Vector3 target, Transform transform)
        {
            while (Vector2.Distance(transform.position, target) >= 0.1f)
            {  transform.GetComponent<Rigidbody2D>().velocity = direction * data.KnockBackSpeed;
                
                yield return new WaitForFixedUpdate(); }
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
        }

      
    }

}