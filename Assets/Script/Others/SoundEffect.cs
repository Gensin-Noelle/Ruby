using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip enemyInjuredMusic;
    public AudioClip playerInjuredMusic;
    public AudioClip playerShotMusic;
    private AudioSource play;
    // Start is called before the first frame update
    void Start()
    {
       play = GetComponent<AudioSource>();
    }
    public void EnemyInjuredPlay()
    {
        play.clip = enemyInjuredMusic;
        play.volume = 0.5f;
        play.Play();
    }

    public void PlayerInjuredPlay()
    {
        play.clip = playerInjuredMusic;
        play.volume = 0.5f;
        play.Play();
    }

        public void PlayerShotPlay()
    {
        play.clip = playerShotMusic;
        play.volume = 0.5f;
        play.Play();
    }
}
