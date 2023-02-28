using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
  public static event EventHandler OnAnyCut;

  public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
  public event EventHandler OnCut;

  [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

  private int cuttingProgress;


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
          // player carrying something can be cut
          player.GetKitchenObject().SetKitchenObjectParent(this);
          cuttingProgress = 0;

          CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
          {
            progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
          });
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
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) { }
        //Player is holding palte
        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
        {
          GetKitchenObject().DestroySelf();
        }
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
    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputkitchenObjectSO
    );
    return cuttingRecipeSO != null;
  }

  public override void InteractAlternate(Player player)
  {
    if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
    {      // if there is kitchen object and it can be cut(has a recipe) 
      cuttingProgress++;

      OnCut?.Invoke(this, EventArgs.Empty);
      OnAnyCut?.Invoke(this, EventArgs.Empty); 

      CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().

      GetKitchenObjectSO());

      OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
      {
        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
      });



      if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
      {
        KitchenObjectSO outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

        GetKitchenObject().DestroySelf();

        KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
      }
    }
  }

  private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
  {
    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputkitchenObjectSO
    );
    if (cuttingRecipeSO != null)
    {
      return cuttingRecipeSO.output;
    }
    else
    {
      return null;
    }
  }

  private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
    {
      if (cuttingRecipeSO.input == inputKitchenObjectSO)
      {
        return cuttingRecipeSO;
      }
    }
    return null;
  }
}
