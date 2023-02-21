using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


  public override void Interact(Player player)
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




}
