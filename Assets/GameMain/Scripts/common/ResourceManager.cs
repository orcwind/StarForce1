using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
/// <summary>
/// 读取配置文件：configmap.txt
/// </summary>
    public class ResourceManager
    {
        private static Dictionary<string, string> configMap;
        static ResourceManager()
        {
            string fileContent = GetConfigFile("ConfigMap.txt");
            BuildMap(fileContent);
        } 
        //读取文件    
        public static string GetConfigFile(string fileName)
        {
            //string url = "file://"+Application.streamingAssetsPath + "/"+ fileName;
            string url;
            //如果在编译器下
            //if(Application.platform== RuntimePlatform.WindowsEditor)
#if UNITY_EDITOR ||UNITY_STANDALONE
            url = "file://"+Application.dataPath + "/StreamingAssets/"+fileName;
            //否则在Iphone下...
#elif UNITY_IPHONE
             url = "file://"+Application.dataPath + "/Raw/"+fileName; 
            //否则在Android下...
#elif UNITY_ANDROID
             url = "jar:file://"+Application.dataPath + "!/assets/"+fileName;
#endif
            WWW www = new WWW(url);
            while (true)
            {
                if (www.isDone)
                    return www.text;
            }
        }
         
        public static  void BuildMap(string fileContent)
        {
            configMap = new Dictionary<string, string>();
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine())!= null)
                {
                    string[] keyValue = line.Split('=');
                    configMap.Add(keyValue[0], keyValue[1]);
                 }
            };
       }
        public static T Load<T>(string prefabName) where T:Object
        {
            string prefabPath = configMap[prefabName];
            return Resources.Load<T>(prefabPath);
        }
    }

}