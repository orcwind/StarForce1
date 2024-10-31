using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;

namespace StarForce
{
    public static class ItemIconLoader
    {

        public static Sprite LoadIcon(string itemName, ItemType itemType)
        {
            string path = GetIconPath(itemName, itemType);         
            
            Sprite icon = Resources.Load<Sprite>(path);
            
            if (icon == null)
            {
                Log.Error($"无法加载物品图标：Resources/{path}");            
            }
                       
            return icon;
        }

        private static string GetIconPath(string itemName, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    return AssetPaths.ItemIcons.GetWeaponIconPath(itemName);
                case ItemType.Consumable:
                    return AssetPaths.ItemIcons.GetConsumableIconPath(itemName);
                case ItemType.Equipment:
                    return AssetPaths.ItemIcons.GetEquipmentIconPath(itemName);
                default:
                    Log.Error($"未知的物品类型：{itemType}");
                    return string.Empty;
            }
        }
    }
}
