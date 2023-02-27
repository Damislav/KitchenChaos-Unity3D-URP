using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player player)
  {
    // pick up and drop items
    if (!HasKitchenObject())
    {//there is no kitchenobject here}
      if (player.HasKitchenObject())
      {
        // Player is carrying something
        player.GetKitchenObject().SetKitchenObjectParent(this);
      }
      else
      {
        // Player not carrying anything
      }
    }
    else
    {
      // there is kitchen object
      if (player.HasKitchenObject())
      {
        //player cayyring something
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
          //Player is holding palte
          if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
          {
            GetKitchenObject().DestroySelf();
          }
        }
        else
        {
          // Player is not carrying Plate but something else
          if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
          {
            // counter is holding Plate
            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
            {
              player.GetKitchenObject().DestroySelf();
            }
          }
        }
      }
      else
      {
        // player is not carrying anything 
        GetKitchenObject().SetKitchenObjectParent(player);
      }

    }
  }
}
