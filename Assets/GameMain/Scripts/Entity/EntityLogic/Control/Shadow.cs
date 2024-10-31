using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace StarForce
{
    public class Shadow : MonoBehaviour
    {
        public Transform playerTransform; //父物体Transform
        public Transform bodyTransform;
        public float shadowSizeFloat = 0.5f;  //影子最小缩放比例（在原有基础上）

        public float heightDifference; //玩家跳跃高度差
        public Vector3 scale; //初始影子缩放大小
        private CharacterMotor playerMove; // 声明父物体移动脚本，主要是用于获取设置的玩家跳跃高度
        private string playerName = "HeroKnight_0";

        private void Start()
        {
            playerMove = GetComponentInParent<CharacterMotor>();
            playerTransform = transform.parent.GetComponent<Transform>();
            scale = transform.localScale;//将影子的初始缩放赋值给scale
           // bodyTransform = transform.parent.FindChildByName(playerName).GetComponent<Transform>();
        }

        private void Update()
        {
            heightDifference = bodyTransform.position.y - playerTransform.position.y; // 高度差计算：子物体y-父物体y
                                                                                      //按照最大跳跃高度和高度差的比例来缩放影子大小，限制影子最小缩放
                                                                                      //Mathf.Clamp() 三个参数，第一个参数是要限制的变量，第二个是最小值，第三个是最大值
                                                                                      //用scale.x-(heightDifference/playerMove.jumpHeight)

            transform.localScale = new Vector3(
                Mathf.Clamp(scale.x - (heightDifference / playerMove.jumpHeight) * scale.x, scale.x * shadowSizeFloat, scale.x),
                Mathf.Clamp(scale.y - (heightDifference / playerMove.jumpHeight) * scale.y, scale.y * shadowSizeFloat, scale.y)
                , scale.z);


        }
    }
}
