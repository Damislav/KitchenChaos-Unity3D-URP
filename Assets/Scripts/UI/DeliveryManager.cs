
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeliveryManager : MonoBehaviour
{
  public event EventHandler OnRecipeSpawn;
  public event EventHandler OnRecipeCompleted;
  public event EventHandler OnRecipeSuccess;
  public event EventHandler OnRecipeFailed;

  public static DeliveryManager Instance { get; private set; }
  [SerializeField] private RecipeListSO recipeListSo;
  private List<RecipeSO> waitingRecipeSOList;

  private float spawnRecipeTimer;
  private float spawnRecipeTimerMax = 4f;
  [SerializeField] private float waitingRecipeMax = 4;

  private void Awake()
  {
    Instance = this;
    waitingRecipeSOList = new List<RecipeSO>();
  }

  private void Update()
  {
    spawnRecipeTimer -= Time.deltaTime;
    if (spawnRecipeTimer <= 0f)
    {
      spawnRecipeTimer = spawnRecipeTimerMax;
      if (waitingRecipeSOList.Count < waitingRecipeMax)
      {
        RecipeSO waitingRecipeSO = recipeListSo.recipeSOList[UnityEngine.Random.Range(0, recipeListSo.recipeSOList.Count)];
        // Debug.Log(waitingRecipeSO.name);
        waitingRecipeSOList.Add(waitingRecipeSO);

        OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
      }

    }
  }

  public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
  {
    for (int i = 0; i < waitingRecipeSOList.Count; i++)
    {
      RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
      if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
      {
        // has the same number of ingriidents
        bool plateContentsMatchesRecipe = true;
        foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
        {
          // cycle trough each ingridient
          bool ingredientFound = false;
          foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
          {
            if (plateKitchenObjectSO == recipeKitchenObjectSO)
            {
              // ingredient matches!
              ingredientFound = true;
              break;
            }
          }
          if (!ingredientFound)
          {
            // this recipe ingredient was not found on plate
            plateContentsMatchesRecipe = false;
          }
        }
        if (plateContentsMatchesRecipe)
        {
          // Player delivered the correct recipe!
          // Debug.Log("Player develiverd corrrect recipe");
          waitingRecipeSOList.RemoveAt(i);

          OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
          OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
          return;
        }
      }
    }
    // no matches found
    // player did not deliver a correct recipe
    OnRecipeFailed?.Invoke(this, EventArgs.Empty);
  }

  public List<RecipeSO> GetWaitingRecipeSOList()
  {
    return waitingRecipeSOList;
  }

}
