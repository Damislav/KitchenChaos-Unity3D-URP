using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlateCompleteVisual : MonoBehaviour
{
  [Serializable]
  public struct KitchenObjectSo_GameObject
  {
    public KitchenObjectSO kitchenObjectSO;
    public GameObject gameObject;
  }

  [SerializeField] private PlateKitchenObject plateKitchenObject;
  [SerializeField] private List<KitchenObjectSo_GameObject> kitchenObjectSoGameObjectsList;

  private void Start()
  {
    plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    //   set inactive for start
    foreach (KitchenObjectSo_GameObject kitchenObjectSOGameObject in kitchenObjectSoGameObjectsList)
    {
      kitchenObjectSOGameObject.gameObject.SetActive(false);
    }
  }
  private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
  {
    foreach (KitchenObjectSo_GameObject kitchenObjectSOGameObject in kitchenObjectSoGameObjectsList)
    {
      if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
      {
        kitchenObjectSOGameObject.gameObject.SetActive(true);
      }
    }
  }

}
