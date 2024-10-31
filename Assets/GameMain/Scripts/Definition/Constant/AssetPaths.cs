using UnityEngine;

namespace StarForce
{
    public static class AssetPaths
    {
        public static class ItemIcons
        {
            private const string BaseFolder = "ItemIcons/";
            private const string WeaponFolder = BaseFolder + "Weapons/";
            private const string ConsumableFolder = BaseFolder + "Consumables/";
            private const string EquipmentFolder = BaseFolder + "Equipment/";

            public static string GetWeaponIconPath(string weaponName)
            {
                if (string.IsNullOrEmpty(weaponName))
                {
                    throw new System.ArgumentException("武器名称不能为空", nameof(weaponName));
                }
                
                string path = $"{WeaponFolder}{weaponName}";
              
                return path;
            }

            public static string GetConsumableIconPath(string consumableName)
            {
                if (string.IsNullOrEmpty(consumableName))
                {
                    throw new System.ArgumentException("消耗品名称不能为空", nameof(consumableName));
                }
                
                return $"{ConsumableFolder}{consumableName}";
            }

            public static string GetEquipmentIconPath(string equipmentName)
            {
                if (string.IsNullOrEmpty(equipmentName))
                {
                    throw new System.ArgumentException("装备名称不能为空", nameof(equipmentName));
                }
                
                return $"{EquipmentFolder}{equipmentName}";
            }
        }

        public static class AnimationPaths
        {
            private const string BaseFolder = "Animation/Attack/";
            public const string NormalAttackPath = BaseFolder + "{0}_Attack{1}";     // 例如: Animation/Attack/101010_Attack01
            public const string ChargeAttackPath = BaseFolder + "{0}_AttackC";       // 例如: Animation/Attack/101010_AttackC
            public const string ChargeReleasePath = BaseFolder + "{0}_AttackCR";     // 例如: Animation/Attack/101010_AttackCR
        }

        // 可以添加其他资源类型的路径
        // public static class Characters { ... }
        // public static class Environments { ... }
        // 等等
    }
}
