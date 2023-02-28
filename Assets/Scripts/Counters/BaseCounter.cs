using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParents
{

  public static event EventHandler OnAnyObjectPlacedHere;
  [SerializeField] protected Transform counterTopPoint;

  private KitchenObject kitchenObject;

  public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

  public void SetKitchenObject(KitchenObject kitchenObject)
  {
    this.kitchenObject = kitchenObject;
    if (kitchenObject != null)
    {
      OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }
  }

  public KitchenObject GetKitchenObject() => kitchenObject;

  public void ClearKitchenObject() => kitchenObject = null;

  public bool HasKitchenObject() => kitchenObject != null;

  // cause we will override it always
  public virtual void Interact(Player player)
  {
    // Debug.Log("Base counter.interact() should not be triggered");
  }

  public virtual void InteractAlternate(Player player)
  {
    // Debug.Log("Base counter.interact() should not be triggered");
  }


}
