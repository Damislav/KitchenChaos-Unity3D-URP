using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : MonoBehaviour, IKitchenObjectParents
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [SerializeField] private Transform counterTopPoint;

  private KitchenObject kitchenObject;

  public void Interact(Player player)
  {
    if (kitchenObject == null)
    {//spawning object
      Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
      kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
    }
    else
    {
      // give the object to player
      kitchenObject.SetKitchenObjectParent(player);
    }
  }



  public void ClearKitchenObject()
  {
    throw new System.NotImplementedException();
  }

  public KitchenObject GetKitchenObject()
  {
    throw new System.NotImplementedException();
  }

  public Transform GetKitchenObjectFollowTransform()
  {
    throw new System.NotImplementedException();
  }

  public bool HasKitchenObject()
  {
    throw new System.NotImplementedException();
  }

  public void SetKitchenObject(KitchenObject kitchenObject)
  {
    throw new System.NotImplementedException();
  }
}
