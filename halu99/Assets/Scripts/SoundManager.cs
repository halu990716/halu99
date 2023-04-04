using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public AudioClip PlayerDie;
    public AudioClip EnemyMissile;
    public AudioClip MissileAudio;
    public AudioClip getItem;
    public AudioClip Click;
    public AudioClip Skill;
    public AudioClip Clear;

    public void soundManager(string audioName)
    {

        if (audioName == "PlayerDie")
        {//explosion
            GetComponent<AudioSource>().PlayOneShot(PlayerDie, 0.3f);

        }

        if (audioName == "EnemyMissile")
        {//sparkexplosion
            GetComponent<AudioSource>().PlayOneShot(EnemyMissile, 0.3f);
        }

        if (audioName == "MissileAudio")
        {
            GetComponent<AudioSource>().PlayOneShot(MissileAudio, 0.3f);
        }

        if (audioName == "getItem")
        {
            GetComponent<AudioSource>().PlayOneShot(getItem, 0.3f);
        }

        if (audioName == "Click")
        {
            GetComponent<AudioSource>().PlayOneShot(Click);
        }

        if (audioName == "Skill")
        {
            GetComponent<AudioSource>().PlayOneShot(Skill);
        }

        if (audioName == "Clear")
        {
            GetComponent<AudioSource>().PlayOneShot(Clear);
        }
    }
}
