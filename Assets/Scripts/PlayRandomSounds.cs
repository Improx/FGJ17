using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSounds : MonoBehaviour {

    [SerializeField]
    public AudioClip[] audios;
    private AudioSource myAudioSource;
    [SerializeField]
    private float minSoundCooldown = 6f;
    [SerializeField]
    private float maxSoundCooldown = 12f;

    void Start () {
        myAudioSource = GetComponent<AudioSource>();
        int randomSound = Random.Range(0, audios.Length);
        float newCooldown = Random.Range(minSoundCooldown, maxSoundCooldown);
        StartCoroutine(playRandomSound(randomSound, newCooldown));
    }


    private IEnumerator playRandomSound(int sound, float cooldown) {
        myAudioSource.PlayOneShot(audios[sound]);
        yield return new WaitForSeconds(cooldown);
        int randomSound = Random.Range(0, audios.Length);
        float newCooldown = Random.Range(0,5f);
        StartCoroutine(playRandomSound(randomSound,newCooldown));
    }
}
