using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleCollision : MonoBehaviour
{

    ParticleSystem particles;
    public GameObject splatPrefab;
    public Transform splatHolder;
    private List<ParticleCollisionEvent> collissionEvents = new List<ParticleCollisionEvent>();
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public float SoundCapResetSpeed = 0.55f;
    public int MaxSounds = 3;
    float timePassed;
    int soundsPlayed;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > SoundCapResetSpeed)
        {
            soundsPlayed = 0;
            timePassed = 0;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particles, other, collissionEvents);

        int count = collissionEvents.Count;

        for (int i = 0; i < count; i++)
        {
            Instantiate(splatPrefab, collissionEvents[i].intersection, Quaternion.Euler(0.0f, 0.09f, Random.Range(0.0f, 360.0f)), splatHolder);
            if (soundsPlayed < MaxSounds)
            {
                soundsPlayed += 1;
                audioSource.pitch = Random.Range(.9f, 1.1f);
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)], Random.Range(0.1f, 0.35f));
            }
        }
    }
}
