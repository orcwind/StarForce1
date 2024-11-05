using GameFramework;
using GameFramework.Event;

namespace StarForce
{
    public class WeaponChangedEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(WeaponChangedEventArgs).GetHashCode();
        public override int Id => EventId;
        public int WeaponId { get; private set; }
        
        public static WeaponChangedEventArgs Create(int weaponId)
        {
            WeaponChangedEventArgs e = ReferencePool.Acquire<WeaponChangedEventArgs>();
            e.WeaponId = weaponId;
            return e;
        }

        public override void Clear()
        {
            WeaponId = 0;
        }
    }
} 