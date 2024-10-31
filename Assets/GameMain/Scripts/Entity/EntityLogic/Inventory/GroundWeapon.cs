using UnityEngine;
using UnityGameFramework.Runtime;
//using GameFramework.UI;
using GameFramework.Event;
using GameFramework.DataTable;

namespace StarForce
{
    public class GroundWeapon : Entity, IInteractable
    {
       [SerializeField]
        public WeaponData m_WeaponData;
        private SpriteRenderer m_SpriteRenderer;
        private Collider2D m_Collider;

        [SerializeField]
        private UIItemInfo m_UIItemInfo;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            
            m_SpriteRenderer =transform.GetChild(0).GetComponent<SpriteRenderer>();
            m_Collider = GetComponent<Collider2D>();
            
            if (m_Collider == null)
            {
                m_Collider = gameObject.AddComponent<CircleCollider2D>();
            }
            m_Collider.isTrigger = true;
            
            gameObject.layer = Constant.Layer.InteractionItemLayerId;
            Log.Info($"GroundWeapon initialized on layer: {LayerMask.LayerToName(gameObject.layer)}");

            // 获取UIItemInfo组件
            m_UIItemInfo = transform.GetChild(1).GetComponent<UIItemInfo>();
            if (m_UIItemInfo == null)
            {
                Log.Error("UIItemInfo component not found on the second child of GroundWeapon.");
            }
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            m_WeaponData = userData as WeaponData;
            if (m_WeaponData == null)
            {
                Log.Error("无效的武器数据。");
                return;
            }

            Log.Info($"地面武器显示: WeaponId={m_WeaponData.WeaponId}, WeaponName={m_WeaponData.WeaponName}");

            // 尝试加载武器图标
            Sprite weaponSprite = ItemIconLoader.LoadIcon(m_WeaponData.WeaponName, ItemType.Weapon);
            if (weaponSprite != null)
            {
                Log.Info($"成功加载武器图标：{m_WeaponData.WeaponName}");
                m_SpriteRenderer.sprite = weaponSprite;
            }
            else
            {
                Log.Warning($"无法加载武器图标：{m_WeaponData.WeaponName}，尝试加载默认图标");               
            }

            UpdateUIItemInfo();

            // 注册到WeaponManager
            WeaponManager.Instance.RegisterGroundWeapon(this);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            // 从WeaponManager中注销
            WeaponManager.Instance.UnregisterGroundWeapon(this);
            HideUI();
            base.OnHide(isShutdown, userData);
        }

        private void OnEnable()
        {
            // 仅用于调试
            Log.Info($"GroundWeapon {gameObject.name} enabled on layer: {LayerMask.LayerToName(gameObject.layer)}");
        }

        public void Interact(PlayerController playerController)
        {
            Log.Info($"GroundWeapon.Interact 被调用: WeaponId={m_WeaponData.WeaponId}");
            if (playerController == null)
            {
                Log.Error("PlayerController is null");
                return;
            }

            // 获取 PlayerData
            PlayerData playerData = playerController.GetPlayerData();
            if (playerData == null)
            {
                Log.Error("PlayerData is null");
                return;
            }

            Log.Info($"开始拾取武器: 地面武器ID={m_WeaponData.WeaponId}");
            Log.Info($"玩家当前武器ID={playerData.WeaponId}");

            // 保存旧武器ID
            int oldWeaponId = playerData.WeaponId;

            // 直接更新 PlayerData 中的 WeaponId
            playerData.WeaponId = m_WeaponData.WeaponId;
            Log.Info($"更新后的武器ID={playerData.WeaponId}");

            // 如果有旧武器，则掉落
            if (oldWeaponId != 0)
            {
                Vector3 dropPosition = playerController.transform.position + playerController.transform.right * 0.5f;
                WeaponManager.Instance.SpawnWeaponOnGround(oldWeaponId, dropPosition);
                Log.Info($"掉落旧武器: WeaponId={oldWeaponId}, Position={dropPosition}");
            }

            GameEntry.Entity.HideEntity(this);
        }

        public string GetInteractionText()
        {
            return $"拾取 {m_WeaponData.WeaponName}";
        }

        private void UpdateUIItemInfo()
        {
            if (m_UIItemInfo != null)
            {
                m_UIItemInfo.UpdateInfo(m_WeaponData);
                m_UIItemInfo.gameObject.SetActive(false);
            }
        }

        public void ShowUI()
        {
            if (m_UIItemInfo != null)
            {
                m_UIItemInfo.gameObject.SetActive(true);
                
            }
        }

        public void HideUI()
        {
            m_UIItemInfo?.gameObject.SetActive(false);
        }

        public WeaponData GetWeaponData()
        {
            return m_WeaponData;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }


      
    }
}
