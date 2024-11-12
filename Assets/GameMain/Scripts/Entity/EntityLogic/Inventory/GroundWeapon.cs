using UnityEngine;
using UnityGameFramework.Runtime;
//using GameFramework.UI;
using GameFramework.Event;
using GameFramework.DataTable;
using StarForce.Skill;

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
            
            // 确保 userData 是 GroundWeaponData 类型
            GroundWeaponData groundWeaponData = userData as GroundWeaponData;
            if (groundWeaponData == null)
            {
                Log.Error("无效的地面武器数据：userData 不是 GroundWeaponData 类型");
                return;
            }

            // 根据武器ID创建WeaponData
            m_WeaponData = new WeaponData(
                GameEntry.Entity.GenerateSerialId(), 
                groundWeaponData.WeaponId,  // 使用传入的具体武器ID
                0,  // 没有所有者
                groundWeaponData.OwnerCamp
            );
            
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

            PlayerData playerData = playerController.GetPlayerData();
            if (playerData == null)
            {
                Log.Error("PlayerData is null");
                return;
            }

            // 保存旧武器ID用于掉落
            int oldWeaponId = playerData.WeaponId;

            // 更新 PlayerData 中的 WeaponId
            playerData.WeaponId = m_WeaponData.WeaponId;
            playerController.GetComponent<Player>().PlayerData.WeaponId = m_WeaponData.WeaponId;

            // 获取并更新 CharacterSKillManager
            CharacterSKillManager skillManager = playerController.GetComponent<CharacterSKillManager>();
            if (skillManager != null)
            {
                skillManager.UpdateAttacks(m_WeaponData.WeaponId);
                Log.Info($"Updated attacks for weapon: {m_WeaponData.WeaponId}");
            }
            else
            {
                Log.Error("CharacterSKillManager not found on player");
            }

            // 触发武器更换事件
            GameEntry.Event.Fire(this, WeaponChangedEventArgs.Create(m_WeaponData.WeaponId));
            Log.Info($"Weapon changed event fired: WeaponId={m_WeaponData.WeaponId}");

            // 更新动画控制器
            WeaponAnimationController weaponAnimController = playerController.GetComponentInChildren<WeaponAnimationController>();
            if (weaponAnimController != null)
            {
                weaponAnimController.UpdateAnimationController(m_WeaponData.WeaponId);
            }

            // 处理旧武器掉落
            if (oldWeaponId != 0)
            {
                Vector3 dropPosition = playerController.transform.position + playerController.transform.right * 0.5f;
                WeaponManager.Instance.SpawnWeaponOnGround(oldWeaponId, dropPosition);
                Log.Info($"Old weapon dropped: WeaponId={oldWeaponId}, Position={dropPosition}");
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
