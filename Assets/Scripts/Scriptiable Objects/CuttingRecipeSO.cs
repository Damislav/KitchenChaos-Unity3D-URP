using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipeSO")]
public class CuttingRecipeSO : ScriptableObject
{
  public KitchenObjectSO input;
  public KitchenObjectSO output;
  public int cuttingProgressMax;
}