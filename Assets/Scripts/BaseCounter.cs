using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParents
{
  [SerializeField] public KitchenObjectSO kitchenObjectSO;
  [SerializeField] public Transform counterTopPoint;

  public KitchenObject kitchenObject;

  public Transform GetKitchenObjectFollowTransform()
  {
    return counterTopPoint;
  }

  public void SetKitchenObject(KitchenObject kitchenObject)
  {

    this.kitchenObject = kitchenObject;
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


  public virtual void Interact(Player player)
  {
    // cause we will override it always
    Debug.Log("Base counter.interact() should not be triggered");
  }



}
