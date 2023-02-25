using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectSO", menuName = "Food Items")]
public class KitchenObjectSO : ScriptableObject
{
  public Transform prefab;
  public Sprite sprite;
  public string objectName;
}