using UnityEngine;

namespace StarForce
{
    public class GameSceneManager
    {
        private void EnsureAudioListener()
        {
            if (Object.FindObjectOfType<AudioListener>() == null)
            {
                Camera mainCamera = Camera.main;
                if (mainCamera != null && mainCamera.GetComponent<AudioListener>() == null)
                {
                    mainCamera.gameObject.AddComponent<AudioListener>();
                }
            }
        }
    }
} 