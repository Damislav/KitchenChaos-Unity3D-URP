using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
  // only this class can set 
  public static Player Instance { get; private set; }

  public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;


  public class OnSelectedCounterChangedEventArgs : EventArgs
  {
    public ClearCounter selectedCounter;
  }


  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private GameInput gameInput;
  [SerializeField] private LayerMask countersLayerMask;

  private bool isWalking;
  private Vector3 lastInteractDir;
  [SerializeField] private ClearCounter selectedCounter;

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

  }

  // pressing E key subscriber to gameinput 
  private void GameInput_OnInteractAction(object sender, System.EventArgs e)
  {
    if (selectedCounter != null)
    {
      selectedCounter.Interact();
    }
  }

  private void Update()
  {
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
      if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
      {
        // Has ClearCounter
        if (clearCounter != selectedCounter)
        {
          SetSelectedCounter(clearCounter);
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
    bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

    if (!canMove)
    {
      // cannot move towards moveDir

      // Attempt only X movement
      Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
      canMove = !Physics.CapsuleCast(transform.position, transform.position +
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


  private void SetSelectedCounter(ClearCounter selectedCounter)
  {
    this.selectedCounter = selectedCounter;

    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
    {
      selectedCounter = selectedCounter
    });
  }


}
