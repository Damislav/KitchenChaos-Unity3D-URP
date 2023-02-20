using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  public event EventHandler OnInteractAction;
  private PlayerInputActions playerInputActions;

  private void Awake()
  {
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();
    // event--- publisher --- subscribers listen to this event when fired
    playerInputActions.Player.Interact.performed += Interact_performed;
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
