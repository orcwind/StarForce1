using UnityEngine;
using GameFramework;

namespace StarForce
{
    public class DamageTextData : EntityData
    {
        public float Damage { get; private set; }
        public bool IsCritical { get; private set; }
        public Vector3 Position { get; private set; }

        public DamageTextData(int entityId, int typeId, Vector3 position, float damage, bool isCritical) 
            : base(entityId, typeId)
        {
            Position = position;
            Damage = damage;
            IsCritical = isCritical;
        }
    }
}