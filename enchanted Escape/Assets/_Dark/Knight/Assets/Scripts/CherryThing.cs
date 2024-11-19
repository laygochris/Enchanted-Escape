using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CherryThing : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] AudioClip AC;
    [SerializeField] AudioClip GW;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().ChallangeKnight() ;
            text.text = "Challanged The Knight";
            ManagerMain.Instance.PlayAudioClip(AC);
            ManagerMain.Instance.PlayAudioClip(GW);
            transform.gameObject.SetActive(false);  
        }
    }
}
