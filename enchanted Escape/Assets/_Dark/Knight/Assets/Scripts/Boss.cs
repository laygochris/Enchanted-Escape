using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{

	public Transform player;

	public bool isFlipped = false;

	[SerializeField] AudioClip BossEntry;
    private void Awake()
    {
		ManagerMain.Instance.PlayAudioClip(BossEntry);
    }
    public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	public void Done()
	{
        DOVirtual.Float(0, 1, 0.9f, null).OnComplete(() => {
            ManagerMain.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }


	public void Lauhg()
	{
        ManagerMain.Instance.PlayAudioClip(BossEntry);
    }
}
