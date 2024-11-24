using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce

{
	///<summary>
	///±³¾°
	///<summary>

	public class Background : Entity
	{
	private BackgroundData m_BackgroundData = null;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
			m_BackgroundData = (BackgroundData)userData;
			CachedTransform.SetLocalPositionX(m_BackgroundData.Position.x);
        }


    }
}