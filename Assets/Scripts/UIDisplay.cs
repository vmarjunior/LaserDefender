using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Health playerHealth;
    [SerializeField] Slider healthBar;

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start() 
    {
        healthBar.maxValue = playerHealth.GetHealth();
    }

    void Update() 
    {
        if (scoreKeeper != null)
            scoreText.text = scoreKeeper.GetScore().ToString("0000000000");

        if (playerHealth != null)
            healthBar.value = playerHealth.GetHealth();
    }
}
