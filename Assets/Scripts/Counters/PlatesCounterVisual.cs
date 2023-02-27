using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
  [SerializeField] private PlatesCounter platesCounter;
  [SerializeField] private Transform counterTopVisual;
  [SerializeField] private Transform plateVisualPrefab;

  private List<GameObject> plateVisualGameobjectList;

  private void Awake()
  {
    plateVisualGameobjectList = new List<GameObject>();

  }
  private void Start()
  {
    platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
    platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
  }
  private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
  {
    GameObject plateGameObject = plateVisualGameobjectList[plateVisualGameobjectList.Count - 1];
    plateVisualGameobjectList.Remove(plateGameObject);
    Destroy(plateGameObject);
  }
  private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
  {
    // spawn plates at different heights
    Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopVisual);
    float plateOffsetY = .1f;
    plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameobjectList.Count, 0);
    plateVisualGameobjectList.Add(plateVisualTransform.gameObject);
  }

}
