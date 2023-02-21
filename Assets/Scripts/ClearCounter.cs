using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParents
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [SerializeField] private Transform counterTopPoint;
  [SerializeField] private ClearCounter secondClearCounter;
  [SerializeField] private bool testing;

  private KitchenObject kitchenObject;


  private void Update()
  {
    if (testing && Input.GetKeyDown(KeyCode.T))
    {
      if (kitchenObject != null)
      {
        //  kitchenObject.SetClearCounter(secondClearCounter);
      }
    }
  }

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
      // kitchenObject.SetClearCounter(player);

    }

  }

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



}
