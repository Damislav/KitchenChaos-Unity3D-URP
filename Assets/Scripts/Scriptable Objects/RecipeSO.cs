using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
  public List<KitchenObjectSO> kitchenObjectSOList;
  public string recipeName; 
}