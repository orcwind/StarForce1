using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace SunnyLandTest

{
    ///<summary>
    ///
    ///<summary>

    public class DamageDisplay : MonoBehaviour, IResetable
    {
        public TMP_Text text;
        //private float damageValue=5;
        private Rigidbody2D body;
        private Transform tf;        
        private float alpha;

        //�������ϼ�ˮƽ����ϵ��
        private float horizoneForce = 1f;

        private void FixedUpdate()
        {
            changeAlpha();
        }

        //͸���Ƚ��ͣ�Ȼ����ã����ڿɸ����˺�Ԫ�����͸�����ɫ
        private void changeAlpha()
        {
            if(alpha<=0)
            {
                GameObjectPool.Instance.CollectObject(gameObject);
            }
            alpha -= 0.01f;
            text.color=new Color(text.color.r,text.color.g,text.color.b,alpha);
        }

        public void OnReset()
        {
            alpha = 1;
                            
            body = GetComponentInChildren<Rigidbody2D>();
            body.transform.position = GetComponentInParent<Transform>().position;
            
            //�����ˮƽ������������ʹ�˺�ֵ���Ϸ��������������
            body.AddForce(new Vector2(UnityEngine.Random.Range(-1, 1) * horizoneForce, 1),ForceMode2D.Impulse);
            
            //body.AddForce(Vector3.up * upForce, ForceMode2D.Impulse);            
            //body.AddForce(new Vector3(UnityEngine.Random.Range(0,1)*horizoneForce,0), ForceMode2D.Impulse);

            
            // body.gameObject.transform.position = tf.position;
            //body.transform.position =tf.position;
        }
        
     
    }
}