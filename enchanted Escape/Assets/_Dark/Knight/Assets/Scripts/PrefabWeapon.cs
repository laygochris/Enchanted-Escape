using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public Transform Player;
	public GameObject bulletPrefab;
	[SerializeField] int reloadtime;
	bool isreloaded = true;
	public Vector3 Direction;
	[SerializeField] Image PB;
	[SerializeField] Image Bullet;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.E) && isreloaded)
		{
			Shoot();
            PB.DOFillAmount(0, 0.25f).OnComplete(() => {
                PB.DOFillAmount(1, reloadtime - 0.25f).OnComplete(() => {
                    Bullet.DOColor(Color.white, 0.25f);
                });
            });
			Bullet.DOColor(Color.gray,0.25f);
            Invoke(nameof(ReloadNow), reloadtime);

        }
	}

	void ReloadNow()
	{
        isreloaded = !isreloaded;
    }

	void Shoot ()
	{
        isreloaded =!isreloaded;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>().FireNow(firePoint.position - Player.position);
	}
}
