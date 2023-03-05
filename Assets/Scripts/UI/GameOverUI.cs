using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI recipesDeliveredText;


  private void Start()
  {
    Hide();
    KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

  }

  private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
  {
    if (KitchenGameManager.Instance.IsGameOver())
    {
      Show();
      recipesDeliveredText.text = DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();
    }
    else
    {
      Hide();
    }
  }

  void Show()
  {
    gameObject.SetActive(true);
  }

  void Hide()
  {

    gameObject.SetActive(false);
  }
}
