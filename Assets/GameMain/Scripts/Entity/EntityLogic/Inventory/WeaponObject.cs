using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework; 

namespace StarForce
{
    public class WeaponObject : Entity
    {
        [SerializeField]
        private WeaponData m_WeaponData = null;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        public void InitializeWeaponObject(object userData)
        {
            base.OnShow(userData); // 调用基类的protected OnShow方法

            m_WeaponData = userData as WeaponData;
            if (m_WeaponData == null)
            {
                Log.Error("Weapon data is invalid.");
                return;
            }

            // 执行WeaponObject特有的初始化逻辑
            Name = Utility.Text.Format("Weapon ({0})", m_WeaponData.Id.ToString());
            CachedTransform.localPosition = m_WeaponData.Position;
            CachedTransform.localRotation = m_WeaponData.Rotation;
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }

        public WeaponData GetWeaponData()
        {
            return m_WeaponData;
        }
    }
}
