using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpikeDMG : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    bool dead = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
        collision.GetComponent<PlayerHealth>().TakeDamage(100);
            dead = true;
            text.text = "Press Any Key To Restart";
            collision.gameObject.SetActive(false);  
        }
    }

    private void Update()
    {
        if (dead)
        {
            if (Input.anyKeyDown)
            {
                ManagerMain.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}
