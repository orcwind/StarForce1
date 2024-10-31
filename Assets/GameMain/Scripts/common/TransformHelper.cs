using UnityEngine;
using System.Collections;

namespace Common
{
    public static class TransformHelper 
    {
        //未知层级查找后代指定名称
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            Transform childTF = currentTF.Find(childName);
            if (childTF != null) return childTF;
            for (int i = 0; i < currentTF.childCount; i++)
            {
                childTF = FindChildByName(currentTF.GetChild(i), childName);
                if (childTF != null) return childTF;
            }
            return null;
        }

    //    //找到FindChild 放在这里 递归！
    //    public static Transform FindChild(Transform trans, string goName)
    //    {
    //        Transform child = trans.Find(goName);
    //        if (child != null)
    //            return child;

    //        Transform go;
    //        for (int i = 0; i < trans.childCount; i++)
    //        {
    //            child = trans.GetChild(i);
    //            go = FindChild(child, goName);
    //            if (go != null)
    //                return go;
    //        }
    //        return null;
    //    }
        /// <summary>
        /// 转向
        /// </summary>
        public static void LookAtTarget(Vector3 target,
            Transform transform, float rotationSpeed)
        {
            if (target != Vector3.zero)
            {
                Quaternion dir = Quaternion.LookRotation(target);
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    dir, rotationSpeed);
            }
        }
    }
}