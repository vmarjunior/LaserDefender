using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int scoreOnDestroy = 100;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake = false;
    [SerializeField] Animator destroyAnimator;
    [SerializeField] RuntimeAnimatorController animatorController;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }    

    public int GetHealth() => health;

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void ShakeCamera()
    {
        if (applyCameraShake && cameraShake != null)
            cameraShake.Play();
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;

            if (animatorController != null)
                destroyAnimator.runtimeAnimatorController = animatorController;
            else
                Destroy(gameObject);

            audioPlayer.PlayExplosionClip();

            if (gameObject.tag != "Player")
            {
                scoreKeeper.ModifyScore(scoreOnDestroy);
                GetComponents<Collider2D>().ToList().ForEach(col => col.enabled = false);
            }
            else
            {
                levelManager.LoadGameOver();
            }
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            var damageEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(damageEffect.gameObject, damageEffect.main.duration + damageEffect.main.startLifetime.constantMax);
        }
    }

    void OnDeathAnimationFinished()
    {
        destroyAnimator.runtimeAnimatorController = null;
        Destroy(gameObject);
    }
}
