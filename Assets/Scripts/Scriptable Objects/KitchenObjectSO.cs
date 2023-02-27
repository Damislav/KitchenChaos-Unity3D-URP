using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
  public Transform prefab;
  public Sprite sprite;
  public string objectName;
}