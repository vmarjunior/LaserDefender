using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField] [Range(0f, 1f)] float explosionVolume = 1f;

    public void PlayShootingClip()
    {
        if (shootingClip != null)
            PlayClip(shootingClip, shootingVolume);
    }

    public void PlayExplosionClip()
    {
        if (explosionClip != null)
            PlayClip(explosionClip, explosionVolume);
    }

    void PlayClip(AudioClip audioClip, float audioVolume)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(audioClip, cameraPos, audioVolume);
    }

}
