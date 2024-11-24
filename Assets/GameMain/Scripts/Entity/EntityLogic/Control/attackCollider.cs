using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce

{
	///<summary>
	//攻击碰撞器，用于检测攻击范围内的目标，并减慢时间
	///<summary>

	public class AttackCollider : MonoBehaviour
	{
       private const string DEAD_LAYER_NAME = "DeadEnemy";
       private int deadLayer;

       public Animator animator;
       public List<Transform> targets = new List<Transform>();
        //
        public float pauseTime=2f;
        private float ffTimer;
        private float ffTimerTotal;
          
        // 添加一个标记来追踪当前碰撞是否已经处理
        private bool isProcessingCollision = false;

        private void Awake()
        {
            animator=transform.parent.GetComponent<Animator>();
            if(animator==null) 
            {
                Debug.LogError("Animator not found in AttackCollider");
            }
           
            // 缓存死亡层的索引
            deadLayer = LayerMask.NameToLayer(DEAD_LAYER_NAME);
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
            // 如果正在处理碰撞，直接返回
            if (isProcessingCollision) return;

            // 检查碰撞对象是否在死亡层
            if (collision.gameObject.layer == deadLayer)
            {
                Log.Info($"Collision with dead enemy: {collision.gameObject.name}");
                return;
            }

            int layer = collision.transform.gameObject.layer;
     

            
            if (collision.transform.gameObject.layer == 14)
            {               
                isProcessingCollision = true;

                if (!targets.Contains(collision.transform.parent.parent))
                    targets.Add(collision.transform.parent.parent);              
                    FrameFozen(pauseTime);
                    Log.Info("frame frozen");
                Log.Info($"Collision with enemy: {collision.gameObject.name}");
                // Invoke("resetTimeScale", 0.1f);
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
            ffTimer = 0;
            animator.speed = 1f;
          targets.Clear();
          // 重置碰撞处理标记
          isProcessingCollision = false;
        }
        public void FrameFozen(float time)
        {
            ffTimer = time;
            ffTimerTotal = time;
        }
                

    }
}