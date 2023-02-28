using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DeliveryManagerUI : MonoBehaviour
{

  [SerializeField] private Transform container;
  [SerializeField] private Transform recipeTemplate;

  private void Awake()
  {
    recipeTemplate.gameObject.SetActive(false);

  }

  private void Start()
  {
    DeliveryManager.Instance.OnRecipeSpawn += DeliveryManager_OnRecipeSpawned;
    DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeSpawned_OnRecipeCompleted;
    UpdateVisual();
  }

  public void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
  {
    UpdateVisual();
  }
  public void DeliveryManager_OnRecipeSpawned_OnRecipeCompleted(object sender, System.EventArgs e)
  {
    UpdateVisual();
  }

  private void UpdateVisual()
  {
    foreach (Transform child in container)
    {
      if (child == recipeTemplate) continue;
      Destroy(child.gameObject);
    }
    foreach (RecipeSO recipeSo in DeliveryManager.Instance.GetWaitingRecipeSOList())
    {
      Transform recipeTransform = Instantiate(recipeTemplate, container);
      recipeTransform.gameObject.SetActive(true);
      recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSo);

    }
  }


}
