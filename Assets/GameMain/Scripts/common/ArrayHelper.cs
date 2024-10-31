using UnityEngine;
using System.Collections.Generic ;
using System;

namespace Common
{
    //集合或数组的助手类
    public static class ArrayHelper
    {
        //升序排列      
        public static void OrderBy<T, TKey>(this T[] array, Func<T, TKey> condition)   where TKey:IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = i + 1; j < array.Length; j++)
                    if (condition(array[i]).CompareTo(condition(array[j])) > 0)
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
        }

        //降序排列
        public static void OrderByDescending<T, TKey>(this T[] array, Func<T, TKey> condition)   where TKey : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = i + 1; j < array.Length; j++)
                    if (condition(array[i]).CompareTo(condition(array[j])) < 0)
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
        }
                
        //查找
         public static T Find<T>(this T[] array, Func<T, bool> condition)
        {
            for (int i = 0; i < array.Length; i++)
            {//满足条件【调用者指定相应的条件】
                //if(array[i]==5)
                if (condition(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);
        }

        //查找全部
        public static T[] FindAll<T>(this T[] array, Func<T,bool> condition)
        {
            List<T> tempList = new List<T>();
            foreach (var item in array)
            {
                if (condition(item))
                    tempList.Add(item);
            }
            return tempList.Count > 0 ? tempList.ToArray() : null;
        }
     
        //提取数组中元素为单独数组
        public static TKey[] Select<T, TKey>(this T[] array,  Func<T, TKey> condition)
        {
            //存储筛选出来满足条件的元素
            TKey[] result = new TKey[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                //筛选的条件【满足筛选条件，就将该元素存到temparr】
                result[i] = condition(array[i]);
            }
            return result;
        }

        //根据条件找最大值对应元素
        public static T GetMax<T, TKey>(this T[] array, Func<T, TKey> condition)   where TKey : IComparable<TKey>
    {
        T temp =default(T);
        temp = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (condition(temp).CompareTo(condition(array[i])) < 0)
            {
               temp = array[i];               
            }
        }
        return temp;
    }
        
        //根据条件找最小值对应元素
        public static T GetMin<T, TKey>  (this T[] array, Func<T, TKey> condition)   where TKey : IComparable<TKey>
        {
            T temp = default(T);
            temp = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (condition(temp).CompareTo(condition(array[i])) > 0)
                {
                    temp = array[i];
                }
            }
            return temp;
        }
    }
}