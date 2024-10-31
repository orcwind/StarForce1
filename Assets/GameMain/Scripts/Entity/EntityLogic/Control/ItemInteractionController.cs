using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
	///<summary>
	///
	///<summary>

	public class ItemInteractionController : MonoBehaviour
	{
		private List<IInteractable> m_NearbyItems = new List<IInteractable>();
		private PlayerController m_PlayerController;

		private void Start()
		{
			m_PlayerController = GetComponent<PlayerController>();
		}

		public void InteractWithNearestItem()
		{
			Log.Info("尝试与最近的物品交互");
			if (m_NearbyItems.Count > 0)
			{
				IInteractable nearestItem = GetNearestItem();
				if (nearestItem != null)
				{
					Log.Info($"找到可交互物品: {nearestItem.GetType().Name}");
					nearestItem.Interact(m_PlayerController);
				}
			}
			else
			{
				Log.Warning("附近没有可交互物品");
			}
		}

		private IInteractable GetNearestItem()
		{
			IInteractable nearest = null;
			float minDistance = float.MaxValue;

			foreach (var item in m_NearbyItems)
			{
				float distance = Vector3.Distance(transform.position, item.GetPosition());
				if (distance < minDistance)
				{
					minDistance = distance;
					nearest = item;
				}
			}

			return nearest;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			IInteractable item = other.GetComponent<IInteractable>();
			if (item != null && !m_NearbyItems.Contains(item))
			{
			
				m_NearbyItems.Add(item);
				item.ShowUI();
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			IInteractable item = other.GetComponent<IInteractable>();
			if (item != null)
			{
			
				m_NearbyItems.Remove(item);
				item.HideUI();
			}
		}
	}
}
