using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Common;

namespace Common
{ 
    /// <summary>
    /// 
    /// </summary>
    /// 
    public interface IResetable
    {
        void OnReset();
    }
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;
        protected override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// ͨԤƼλãǶȴƷ
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prefab">ҪʵԤƼ</param>
        /// <param name="pos">λ</param>
        /// <param name="rotate">ת</param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {         
            GameObject go = FindUsableObject(key);
            if (go == null)
            {
                   go = AddObject(key, prefab);
            }
            UseObject(pos, rotate, go);
                 return go;
            
        }


        //ʹö
        private void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);
            foreach (var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }
        //Ӷ
        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);
            if (!cache.ContainsKey(key))
                cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        //ѰҶ
        private GameObject FindUsableObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g=>!g.activeInHierarchy);
            return null;
        }
        /// <summary>
        /// ն
        /// </summary>
        /// <param name="go">ն</param>
        /// <param name="delay">ӳʱ䣬ĬΪ0</param>
        public void CollectObject(GameObject go, float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(go, delay));
        }
        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Clear(string key)
        { 
            //for (int i = 0; i < cache[key].Count; i++)
            //{
            // Destroy(cache[key][i]);
            //}
            for (int i = cache[key].Count-1; i>=0; i--)
            {
                 Destroy(cache[key][i]);
            }
            cache.Remove(key);
        }
        public void ClearAll()
        {
            List<string> keyList = new List<string>(cache.Keys);
            foreach (var key in keyList)
            {
                Clear(key);
             }
        }

    }
}

