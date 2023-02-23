using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;

  private PlayerInputActions playerInputActions;



  private void Awake()
  {
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();
    // event--- publisher --- subscribers listen to this event when fired
    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
  }
  private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
  }

  private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    // same as if its not null call it otheerwise wil lthrow null reference
    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }
  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

    inputVector = inputVector.normalized;

    return inputVector;
  }
}
