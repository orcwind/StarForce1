/// <summary>
/// Generic Mono singleton.
/// </summary>
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

namespace Common
{
	public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
	{
		private static T s_Instance = null;
		private static readonly object s_Lock = new object();

		public static T Instance
		{
			get
			{
				if (s_Instance == null)
				{
					lock (s_Lock)
					{
						s_Instance = FindObjectOfType<T>();
						if (s_Instance == null)
						{
							GameObject go = new GameObject($"Singleton_{typeof(T).Name}");
							s_Instance = go.AddComponent<T>();
							DontDestroyOnLoad(go);
							s_Instance.Init();
						}
					}
				}
				return s_Instance;
			}
		}

		protected virtual void Awake()
		{
			if (s_Instance == null)
			{
				s_Instance = this as T;
				DontDestroyOnLoad(gameObject);
				Init();
			}
			else if (s_Instance != this)
			{
				Log.Warning($"检测到 {typeof(T).Name} 的重复实例。正");
				Destroy(gameObject);
			}
		}

		protected virtual void OnDestroy()
		{
			if (s_Instance == this)
			{
				s_Instance = null;
			}
		}

		protected virtual void Init() { }
	}
}
