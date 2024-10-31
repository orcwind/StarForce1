using UnityEngine;

namespace StarForce
{
    public interface IInteractable
    {
        void Interact(PlayerController player);
        string GetInteractionText();
        void ShowUI();
        void HideUI();
        Vector3 GetPosition();
    }
}
