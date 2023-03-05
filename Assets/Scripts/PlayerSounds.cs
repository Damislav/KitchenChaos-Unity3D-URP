using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

  private Player player;
  private float footstepTimer;
  private float footstepTimerMax;
  private float volume = 1f;

  private void Awake()
  {
    player = GetComponent<Player>();
  }

  private void Update()
  {
    footstepTimer -= Time.deltaTime;
    if (footstepTimer < 0f)
    {
      footstepTimer = footstepTimerMax;
      if (player.IsWalking())
      {
        SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
      }


    }
  }
}
