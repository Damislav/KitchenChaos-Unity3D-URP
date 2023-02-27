using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : BaseCounter
{
  [SerializeField] private StoveCounter stoveCounter;
  [SerializeField] private GameObject particleGameObject;
  [SerializeField] private GameObject stoveOnGameObject;

  private void Start()
  {
    stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
  }

  private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
  {
    bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
    stoveOnGameObject.SetActive(showVisual);
    particleGameObject.SetActive(showVisual);
  }
}
