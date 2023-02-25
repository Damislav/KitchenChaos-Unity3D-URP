using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
  public KitchenObjectSO input;
  public KitchenObjectSO output;
  public float fryingTimerMax;

}