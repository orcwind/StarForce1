using UnityEngine;

namespace StarForce
{
    [RequireComponent(typeof(Animator))]
    public class WeaponAnimationController : MonoBehaviour
    {
        private Animator m_Animator;
        private Player m_Player;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Player = GetComponentInParent<Player>();
        }

        public void UpdateWeaponAnimations()
        {
            if (m_Player == null || m_Animator == null)
                return;

               //  var playerData = m_Player.m_CharacterData;
             int weaponId = m_Player.m_CharacterData.WeaponId;

           // int weaponId = m_Player.PlayerData.WeaponId;

            // 更新普通攻击动画
            for (int i = 1; i <= 4; i++)
            {
                string attackClipName = string.Format(AssetPaths.AnimationPaths.NormalAttackPath, 
                    weaponId, i.ToString("D2"));
                UpdateAnimationClip($"attack{i.ToString("D2")}", attackClipName);
            }

            // 更新蓄力攻击动画
            string chargeClipName = string.Format(AssetPaths.AnimationPaths.ChargeAttackPath, weaponId);
            UpdateAnimationClip("attackCharging", chargeClipName);

            // ��新蓄力释放动画
            string releaseClipName = string.Format(AssetPaths.AnimationPaths.ChargeReleasePath, weaponId);
            UpdateAnimationClip("attackChargeRelease", releaseClipName);
        }

        private void UpdateAnimationClip(string stateName, string clipPath)
        {
            AnimationClip newClip = Resources.Load<AnimationClip>(clipPath);
            if (newClip == null)
            {
                Debug.LogError($"找不到动画片段: {clipPath}");
                return;
            }

            var controller = m_Animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            if (controller == null)
                return;

            foreach (var layer in controller.layers)
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.name == stateName)
                    {
                        state.state.motion = newClip;
                        break;
                    }
                }
            }
        }
    }
} 