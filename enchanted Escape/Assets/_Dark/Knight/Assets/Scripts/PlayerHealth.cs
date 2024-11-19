using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerHealth : MonoBehaviour
{

	public int health = 100;
	public Transform[] xory;
	public GameObject deathEffect;
	public Boss Boss;
    [SerializeField] AudioClip PHurt;

    public void TakeDamage(int damage)
	{
		health -= damage;
		ManagerMain.Instance.PlayAudioClip(PHurt);

		StartCoroutine(DamageAnimation());

		if (health <= 0)
		{
			Die();
		}
	}

    void Die()
	{
        Instantiate(deathEffect, transform.position, Quaternion.identity);

		if (Boss)
		{
			foreach (Transform t in xory) {
				t.gameObject.tag = transform.tag;
            }
			Boss.player = xory[Random.Range(0, xory.Length)];
			Boss.Done();
        }

		transform.gameObject.SetActive(false);
    }

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}

	public void ChallangeKnight()
	{
        DOVirtual.Float(0, 1, 1.8f, null).OnComplete(() => {
            ManagerMain.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
	}

}
