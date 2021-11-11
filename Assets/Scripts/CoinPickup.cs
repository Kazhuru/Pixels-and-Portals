using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] AudioClip coinSFX;
    [SerializeField] [Range(0f, 1f)] float coinSFXVolume = 1f;

    private bool picked = false;
    private GameSession session;

    void Start()
    {
        session = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!picked)
        {
            picked = true;
            PlayCoinSFX();
            gameObject.GetComponent<Animator>().SetTrigger("pickup");
            if (session != null)
                session.IncreaseScore();
        }
    }

    private void PlayCoinSFX()
    {
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
        audioSource.clip = coinSFX;
        audioSource.volume = coinSFXVolume;
        audioSource.Play();
    }

    public void coinPickUpEvent()
    {
        Destroy(gameObject);
    }
}
