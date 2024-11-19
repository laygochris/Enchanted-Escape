using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using DG.Tweening;

public class DoorLevelMaze : MonoBehaviour
{
    bool entred = false;
    [SerializeField] GameObject Pac;
    [SerializeField] Pacman pac;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!entred)
        {
            entred = true;
            pac.enabled = false;
            Pac.transform.DOScale(0, 0.5f).OnComplete(() => {
                Invoke(nameof(NextScene), 0.9f);
            });
            
        }
    }

    void NextScene()
    {
        GameManager.Instance.NextScene();
    }
}
