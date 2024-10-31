using GameFramework.DataTable;
using System;

namespace StarForce
{
    [Serializable]
  public class GroundWeaponData : WeaponData
    {
        public GroundWeaponData(int entityId, int typeId, int weaponId, CampType ownerCamp) 
            : base(entityId, typeId, weaponId, 0, ownerCamp)
        {
        }
    }

    // public class GroundWeaponData : WeaponData
    // {
    //     public GroundWeaponData(int entityId, int typeId, int weaponId, CampType ownerCamp) : 
    //     base(entityId, typeId, weaponId, ownerCamp)
    //     {
    //     }
    // }
}