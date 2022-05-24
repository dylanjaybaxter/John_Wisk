using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossHealthbar : MonoBehaviour
{
    [SerializeField] private EnemyHealth bossHealth;
    public Slider slider;
    
    private void Start()
    {
        slider.value = bossHealth.currentHealth;
    }


    private void Update()
    {
        slider.value = bossHealth.currentHealth; //changes fill amount
    }
}
