using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;
	[SerializeField] AudioClip BulletFire;
	[SerializeField] AudioClip BulletHit;

	public void FireNow(Vector3 dire)
	{
        rb.velocity = dire * speed;
		transform.right = dire;
		ManagerMain.Instance.PlayAudioClip(BulletFire);
    }

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		BossHealth enemy = hitInfo.GetComponent<BossHealth>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);

            ManagerMain.Instance.PlayAudioClip(BulletHit);
        }

		Instantiate(impactEffect, transform.position, transform.rotation);

		Destroy(gameObject);
	}
	
}
