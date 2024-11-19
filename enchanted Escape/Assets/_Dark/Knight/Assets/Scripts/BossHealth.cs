using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossHealth : MonoBehaviour
{

	public int health = 500;
	[SerializeField] int EnragedHp;
	public GameObject deathEffect;
	public CinemachineVirtualCamera vcam;
	public Transform Player;
	bool anim = false;
	[SerializeField] AudioClip BHurt;
	[SerializeField] AudioClip BShout;

	public bool isInvulnerable = false;

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;
		ManagerMain.Instance.PlayAudioClip(BHurt);

		if (health <= EnragedHp)
		{
			if(!anim)
			{
				anim = true;
                vcam.Follow = transform;
				GetComponent<Boss>().Lauhg();
                ManagerMain.Instance.PlayAudioClip(BShout);
                Invoke(nameof(BackToPlayer), 1.8f);
            }


            GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
        {
            Die();

		}
	}

	void BackToPlayer()
	{
        vcam.Follow = Player;
    }

	void Die()
	{
        ManagerMain.Instance.PlayAudioClip(BShout);
		ManagerMain.Instance.BossDead();
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
