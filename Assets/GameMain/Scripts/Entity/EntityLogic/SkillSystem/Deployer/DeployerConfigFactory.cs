using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce.Skill
{
/// <summary>
/// 配置工厂
/// </summary>
    public class DeployerConfigFactory
    {
        private static Dictionary<string, object> cache;
        private static string projectName = "StarForce";
        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }
        public static IAttackSelector CreateAttackSelector(AttackData data)
        {
           string className = string.Format("{0}.Skill.{1}AttackSelector",projectName, data.SelectorType);
            return CreateObject<IAttackSelector>(className);
        }

        public static IImpactEffect[] CreateImpactEffects(AttackData data)
        {
             IImpactEffect[] impactArray = new IImpactEffect[data.ImpactType.Length];
            for (int i = 0; i < data.ImpactType.Length; i++)
            {
                // string impactName
                string impactName = string.Format("{0}.Skill.{1}Impact",projectName, data.ImpactType[i]);
                impactArray[i] = CreateObject<IImpactEffect>(impactName);
            }
            return impactArray;
        }
        public static T CreateObject<T>(string className) where T : class
        {
            try
            {
                if (!cache.ContainsKey(className))
                {
                    Type type = Type.GetType(className);
                    if (type == null)
                    {
                        Log.Error($"Type not found: {className}");
                        return null;
                    }
                    
                    object instance = Activator.CreateInstance(type);
                    cache.Add(className, instance);
                  
                }
              
                
                return cache[className] as T;
            }
            catch (Exception e)
            {
                Log.Error($"Error creating object of type {className}: {e.Message}");
                return null;
            }
        }
    }

}