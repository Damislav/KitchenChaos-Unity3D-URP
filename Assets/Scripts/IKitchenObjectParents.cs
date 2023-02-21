
using System;
using UnityEngine;
using System.Collections;

public interface IKitchenObjectParents
{
  public Transform GetKitchenObjectFollowTransform();

  public void SetKitchenObject(KitchenObject kitchenObject);

  public KitchenObject GetKitchenObject();

  public void ClearKitchenObject();

  public bool HasKitchenObject();



}

