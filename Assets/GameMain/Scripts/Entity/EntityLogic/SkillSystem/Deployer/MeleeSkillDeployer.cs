using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce.Skill
{
/// <summary>
/// 
/// </summary>
public class MeleeSkillDeployer : SkillDeployer
{

    public override void DeploySkill()
    {
       //执行选区算法
        CalculateTargets();
        ImpactTarget();
             
    }
}
}