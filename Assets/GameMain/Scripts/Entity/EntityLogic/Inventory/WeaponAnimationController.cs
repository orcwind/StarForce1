using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.Animations;
using UnityEditor.Animations;

namespace StarForce
{
    public class WeaponAnimationController : MonoBehaviour
    {
        private Animator m_Animator;
        private RuntimeAnimatorController m_BaseController;
        
        private void Awake()
        {
            // 获取当前物体上的 Animator 组件
            m_Animator = GetComponent<Animator>();
            if (m_Animator == null)
            {
                Log.Error("WeaponAnimationController: Animator component not found on the model object");
                return;
            }
            
            // 保存基础控制器的引用
            m_BaseController = m_Animator.runtimeAnimatorController;
            Log.Info("WeaponAnimationController: Base controller saved");
        }

        public void UpdateAnimationController(int weaponId)
        {
            if (m_Animator == null)
            {
                Log.Error("WeaponAnimationController: Animator component not found");
                return;
            }

            // 创建新的AnimatorController的副本
            AnimatorController newController = UnityEngine.Object.Instantiate(m_BaseController) as AnimatorController;
            if (newController == null)
            {
                Log.Error("WeaponAnimationController: Failed to create new AnimatorController");
                return;
            }

            Log.Info($"WeaponAnimationController: Creating new controller for weapon {weaponId}");

            // 遍历所有层
            foreach (AnimatorControllerLayer layer in newController.layers)
            {
                // 获取状态机
                AnimatorStateMachine stateMachine = layer.stateMachine;

                // 遍历所有状态
                foreach (ChildAnimatorState state in stateMachine.states)
                {
                    // 根据状态名称替换动画片段
                    switch (state.state.name.ToLower())
                    {
                         case "attack01":
                    ReplaceAnimation(state.state, string.Format(AssetPaths.AnimationPaths.NormalAttackPath, weaponId, "01"));
                    break;
                case "attack02":
                    ReplaceAnimation(state.state, string.Format(AssetPaths.AnimationPaths.NormalAttackPath, weaponId, "02"));
                    break;
                case "attack03":
                    ReplaceAnimation(state.state, string.Format(AssetPaths.AnimationPaths.NormalAttackPath, weaponId, "03"));
                    break;
                case "attack04":
                    ReplaceAnimation(state.state, string.Format(AssetPaths.AnimationPaths.NormalAttackPath, weaponId, "04"));
                    break;
                case "attackc":
                    ReplaceAnimation(state.state, string.Format(AssetPaths.AnimationPaths.ChargeAttackPath, weaponId));
                    break;
                case "attackcr":
                    ReplaceAnimation(state.state, string.Format(AssetPaths.AnimationPaths.ChargeReleasePath, weaponId));
                    break;
                    }
                }
            }

            // 应用新的控制器
            m_Animator.runtimeAnimatorController = newController;
            Log.Info($"WeaponAnimationController: Animation controller updated for weapon {weaponId}");
        }

        private void ReplaceAnimation(AnimatorState state, string animationPath)
        {
            // 加载新的动画片段
            AnimationClip newClip = Resources.Load<AnimationClip>(animationPath);
            if (newClip != null)
            {
                state.motion = newClip;
               
            }
            else
            {
                Log.Warning($"Failed to load animation: {animationPath}");
            }
        }

        public void ResetToBaseController()
        {
            if (m_Animator != null && m_BaseController != null)
            {
                m_Animator.runtimeAnimatorController = m_BaseController;
            }
        }
    }
} 