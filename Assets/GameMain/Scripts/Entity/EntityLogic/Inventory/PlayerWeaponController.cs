using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Event;

namespace StarForce
{
    public class PlayerWeaponController : MonoBehaviour
    {
        public WeaponData CurrentWeaponData { get; private set; }

        private void Start()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        }

        private void OnDestroy()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        }

        public void PerformAttack()
        {
            if (CurrentWeaponData != null)
            {
                Debug.Log($"Performing attack with {CurrentWeaponData.WeaponName}");
                // 实现攻击逻辑，使用 CurrentWeaponData 的属性
            }
        }

        public void PerformChargedAttack()
        {
            if (CurrentWeaponData != null)
            {
                Debug.Log($"Performing charged attack with {CurrentWeaponData.WeaponName}");
                // 实现蓄力攻击逻辑，使用 CurrentWeaponData 的属性
            }
        }

        public void EquipWeapon(WeaponData weaponData)
        {
            if (CurrentWeaponData != null)
            {
                DropCurrentWeapon();
            }

            CurrentWeaponData = weaponData;
            // 可能需要更新玩家的外观或其他相关逻辑
            Debug.Log($"Equipped weapon: {CurrentWeaponData.WeaponName}");
        }

        public void DropCurrentWeapon()
        {
            if (CurrentWeaponData != null)
            {
               // WeaponManager.Instance.SpawnWeapon(CurrentWeaponData.TypeId, transform.position + transform.forward, Quaternion.identity);
                CurrentWeaponData = null;
                // 可能需要更新玩家的外观或其他相关逻辑
                Debug.Log("Weapon dropped");
            }
        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.EntityLogicType == typeof(GroundWeapon))
            {
                // 如果需要，可以在这里处理地面武器的生成
            }
        }
    }
}
