using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce.Skill
{
/// <summary>
/// �ͷ������ù���
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
            //�����㷨����
            //����ѡ��������������
            //ARPGTest.Skill.+ö����+AttackSelector
            //��������ѡ����ARPGTest.Skill.SectorAttackSelector
           string className = string.Format("{0}.Skill.{1}AttackSelector",projectName, data.SelectorType);
            return CreateObject<IAttackSelector>(className);
        }

        public static IImpactEffect[] CreateImpactEffects(AttackData data)
        {
            //����Ӱ��Ч����������
            //ARPGTest.Skill.+impactType[?]+Impact
            //����costSPѡ����ARPGTest.Skill.CostSPImpact
         
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
            if (!cache.ContainsKey(className))
            {
                Type type = Type.GetType(className);
                object instance = Activator.CreateInstance(type);
                cache.Add(className, instance);                
            }
            return cache[className] as T;
        }
    }

}