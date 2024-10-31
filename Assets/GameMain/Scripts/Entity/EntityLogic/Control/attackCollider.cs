using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace StarForce

{
	///<summary>
	///´ò»÷Åö×²¼ì²â
	///<summary>

	public class AttackCollider : MonoBehaviour
	{
       public Animator animator;
       public List<Transform> targets = new List<Transform>();
        //
        public float pauseTime=2f;
        private float ffTimer;
        private float ffTimerTotal;
          

        private void Awake()
        {
            animator=transform.parent.GetComponent<Animator>();
           
        }

        private void FixedUpdate()
        {
            if (ffTimer > 0)
            {
                ffTimer -= Time.deltaTime;
                animator.speed = Mathf.Lerp(0.4f, 1f, (1 - (ffTimer / ffTimerTotal)));
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {           
            if (collision.transform.gameObject.layer == 12)
            {               
                if (!targets.Contains(collision.transform.parent.parent))
                    targets.Add(collision.transform.parent.parent);              
                    FrameFozen(pauseTime);
                // Invoke("resetTimeScale", 0.1f);
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
            ffTimer = 0;
            animator.speed = 1f;
          targets.Clear();
        }
        public void FrameFozen(float time)
        {
            ffTimer = time;
            ffTimerTotal = time;
        }
                

    }
}