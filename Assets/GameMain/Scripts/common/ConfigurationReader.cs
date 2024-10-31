using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
/// <summary>
/// 
/// </summary>
    public class ConfigurationReader
    {
        public static string GetConfigFile(string fileName)
        {
            //string url = "file://"+Application.streamingAssetsPath + "/"+ fileName;
            string url;
            //����ڱ�������
            //if(Application.platform== RuntimePlatform.WindowsEditor)
#if UNITY_EDITOR ||UNITY_STANDALONE
            url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
            //������Iphone��...
#elif UNITY_IPHONE
             url = "file://"+Application.dataPath + "/Raw/"+fileName; 
            //������Android��...
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

        public static void Reader(string fileContent,Action<string> handler)
        {
            
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    handler(line);
                }
            };
        }
    }

}