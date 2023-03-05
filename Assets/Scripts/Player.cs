using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour, IKitchenObjectParents
{
  // only this class can set  
  public static Player Instance { get; private set; }

  public event EventHandler OnPickedSomething;
  public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

  public class OnSelectedCounterChangedEventArgs : EventArgs
  {
    public BaseCounter selectedCounter;
  }

  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private GameInput gameInput;
  [SerializeField] private LayerMask countersLayerMask;
  [SerializeField] private Transform kitchenObjectHoldPoint;

  private bool isWalking;
  private Vector3 lastInteractDir;
  private BaseCounter selectedCounter;
  private KitchenObject kitchenObject;

  private void Awake()
  {
    if (Instance != null)
    {
      Debug.LogError("There is more than one Player instance");
    }
    Instance = this;
  }

  private void Start()
  {
    gameInput.OnInteractAction += GameInput_OnInteractAction;
    gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
  }

  // pressing F key subscriber to gameinput 
  private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
  {
    if (!KitchenGameManager.Instance.IsGamePlaying()) return;

    if (selectedCounter != null)
    {
      selectedCounter.InteractAlternate(this);
    }
  }

  // pressing E key subscriber to gameinput 
  private void GameInput_OnInteractAction(object sender, System.EventArgs e)
  {
    if (!KitchenGameManager.Instance.IsGamePlaying()) return;

    if (selectedCounter != null)
    {
      selectedCounter.Interact(this);
    }
  }

  private void Update()
  {
    if (!KitchenGameManager.Instance.IsGamePlaying()) return;
    HandleMovement();
    HandleInteractions();
  }

  public bool IsWalking()
  {
    return isWalking;
  }

  private void HandleInteractions()
  {
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    if (moveDir != Vector3.zero)
    {
      lastInteractDir = moveDir;
    }

    float interactDistance = 2f;
    if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
    {
      if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
      {
        // Has baseCounter
        if (baseCounter != selectedCounter)
        {
          SetSelectedCounter(baseCounter);
        }
      }
      else
      {
        SetSelectedCounter(null);

      }
    }
    else
    {
      SetSelectedCounter(null);
    }
  }

  private void HandleMovement()
  {
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    float moveDistance = moveSpeed * Time.deltaTime;
    float playerRadius = .7f;
    float playerHeight = 2f;
    //if its true you can move if its not you cant
    bool canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

    if (!canMove)
    {
      // cannot move towards moveDir

      // Attempt only X movement
      Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
      canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position +
      Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
      if (canMove)
      {
        // can move only on the x
        moveDir = moveDirX;
      }
      else
      {
        // cannot move only on the x
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);


        //Attempt only Z movement
        if (canMove)
        {
          // can move only on the z
          moveDir = moveDirZ;
        }
        else
        {
          // cannot move in andy direction
        }
      }

    }
    if (canMove)
    {
      transform.position += moveDir * moveDistance;
    }


    // if we are walking animate
    isWalking = moveDir != Vector3.zero;

    // smooth rotation
    float rotateSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
  }

  private void SetSelectedCounter(BaseCounter selectedCounter)
  {
    this.selectedCounter = selectedCounter;

    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
    {
      selectedCounter = selectedCounter
    });
  }

  public Transform GetKitchenObjectFollowTransform()
  {
    return kitchenObjectHoldPoint;
  }

  public void SetKitchenObject(KitchenObject kitchenObject)
  {
    // when player picks up something
    this.kitchenObject = kitchenObject;
    if (kitchenObject != null)
    {
      OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }
  }

  public KitchenObject GetKitchenObject()
  {
    return kitchenObject;
  }

  public void ClearKitchenObject()
  {
    kitchenObject = null;
  }

  public bool HasKitchenObject()
  {
    return kitchenObject != null;
  }
}
