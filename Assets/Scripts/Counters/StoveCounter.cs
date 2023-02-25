using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

  [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
  private float fryingTimer;

  private void Update()
  {
    if (HasKitchenObject())
    {
      fryingTimer += Time.deltaTime;
      FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
      if (fryingTimer > fryingRecipeSO.fryingTimerMax)
      {

      }
    }
  }
  public override void Interact(Player player)
  {
    // pick up and drop items
    if (!HasKitchenObject())
    {//there is no kitchenobject here}
      if (player.HasKitchenObject())
      {
        // Player is carrying something
        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
          // player carrying something can be fried
          player.GetKitchenObject().SetKitchenObjectParent(this);
        }
      }
      else
      {
        // player not carrying anything
      }
    }
    else
    {
      // there is kitchen object
      if (player.HasKitchenObject())
      {
        //player cayyring something
      }
      else
      {
        // player is not carrying anything
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }

  private bool HasRecipeWithInput(KitchenObjectSO inputkitchenObjectSO)
  {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO);
    return fryingRecipeSO != null;
  }

  private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
  {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO
    );
    if (fryingRecipeSO != null)
    {
      return fryingRecipeSO.output;
    }
    else
    {
      return null;
    }
  }

  private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
    {
      if (fryingRecipeSO.input == inputKitchenObjectSO)
      {
        return fryingRecipeSO;
      }
    }
    return null;
  }

}
