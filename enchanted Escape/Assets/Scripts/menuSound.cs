using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSound : MonoBehaviour
{
   public void PlaySound(AudioClip SC)
    {
        ManagerMain.Instance.PlayAudioClip(SC);
    }
}
