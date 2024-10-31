using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class CircleProjectile: MonoBehaviour,IResetable
{
    public Vector2 targetPos;
    public float speed=2;
    public float projectileDistance=2;    

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) >=0.1f)
            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position,targetPos) < 0.1f)
        {

            //Destroy(gameObject);
            GameObjectPool.Instance.CollectObject(gameObject);
        }
    }
    
    public void OnReset()
    {
        //targetPos = new Vector2(transform.position.x+projectileDistance,transform.position.y);
        targetPos = transform.TransformPoint(projectileDistance, 0, 0);
    }
}
