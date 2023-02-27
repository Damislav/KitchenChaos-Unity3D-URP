using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
  // this was done because unity does not show interface like gameobject

  [SerializeField] private GameObject hasProgressGameObject;
  [SerializeField] private Image barImage;

  //interface
  private IHasProgress hasProgress;

  private void Start()
  {
    hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
    if (hasProgress == null)
    {
      Debug.Log("Gameobject " + hasProgressGameObject + "doest not have gameobject that impelments the interface");
    }
    hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

    barImage.fillAmount = 0f;

    Hide();
  }

  private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
  {
    barImage.fillAmount = e.progressNormalized;

    if (e.progressNormalized == 0f || e.progressNormalized == 1f)
    {
      Hide();
    }
    else
    {
      Show();
    }
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }

}
