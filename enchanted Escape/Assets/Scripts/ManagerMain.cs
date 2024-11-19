using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ManagerMain : MonoBehaviour
{
    public static ManagerMain Instance;
    [SerializeField] AudioSource AS;
    [SerializeField] GameObject RBSLogo;
    [SerializeField] Image Overlay;
    [SerializeField] AudioClip Credits;
    [SerializeField] AudioClip WinCool;
    [SerializeField] AudioClip Bead;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        RBSLogo.transform.DOShakeScale(1, 0.1f, 9, 55, true, ShakeRandomnessMode.Harmonic).OnComplete(() => {

            DOVirtual.Float(0, 1, .9f, null).OnComplete(() => {
                LoadScene(1);
            });
        });
    }

    public void PlayAudioClip(AudioClip AC)
    {
        AS.PlayOneShot(AC);
    }

    public void LoadScene(int index)
    {
        Overlay.gameObject.SetActive(true);
        Overlay.DOFade(1, 0.18f).OnComplete(() =>
        {
            SceneManager.LoadScene(index);
            if (index == 5)
            {
                PlayAudioClip(Credits);
            }
            Overlay.DOFade(0, 0.18f).SetDelay(0.25f).OnComplete(() => { Overlay.gameObject.SetActive(false); });
        });
    }

    public void BossDead()
    {
        Invoke(nameof(GameDone), 2.5f);
    }

    void GameDone()
    {
        PlayAudioClip(WinCool);
        PlayAudioClip(Bead);
        LoadScene(5);
    }
}
