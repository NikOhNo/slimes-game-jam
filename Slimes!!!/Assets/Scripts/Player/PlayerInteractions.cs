using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    CapsuleCollider2D interactArea;

    IInteractable interactor = null;

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (interactor != null)
            {
                interactor.DoInteraction();
            }
        }
    }

    public void SetInteractor(IInteractable givenInteractor)
    {
        interactor = givenInteractor;
    }
}
