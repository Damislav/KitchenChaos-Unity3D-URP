using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject
{

  public KitchenObjectSO input;
  public KitchenObjectSO output;

  public float burninTimerMax;

}