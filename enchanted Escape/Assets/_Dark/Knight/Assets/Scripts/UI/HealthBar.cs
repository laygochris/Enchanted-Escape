using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public BossHealth bossHealth;
	public PlayerHealth PlayerHealth;
	public Slider slider;

	void Start()
	{

        if (bossHealth)
            slider.maxValue = bossHealth.health;

        if (PlayerHealth)
            slider.maxValue = PlayerHealth.health;
	}

	// Update is called once per frame
	void Update()
    {
		if(bossHealth)
			slider.value = bossHealth.health;

		if(PlayerHealth)
            slider.value = PlayerHealth.health;
    }
}
