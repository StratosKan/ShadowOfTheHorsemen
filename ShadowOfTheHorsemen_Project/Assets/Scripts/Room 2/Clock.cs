using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    //A script for the grandfather's clock. It checks if player is in the room of the puzzle and starts ticking in a random range between 3 duration options.
    // When the player enters it starts Ticking, when the Ticking stops, it starts Hiting for a couple of seconds, in which the statue of the room looks towards the player
    //The player can move as long as the state is Ticking and must stand still when the state is Hiting

    private List<int> Timer;

    public GameObject area;
    protected IsPlayerInRoom isIn;

    private float timeLeft;

    //Sound effects
    public AudioSource ClockTickingSource;
    public AudioClip ClockTickingClip;
    public AudioSource ClockHitingSource;
    public AudioClip ClockHitingClip;

    public float ClockDebug;
    public bool IsHiting = false;
    public bool IsTicking = true;

    public float timeForAngelToLookAtPlayer = 4f;

    void Awake()
    {
        Timer = new List<int> { 4, 5, 6 };

        ClockTickingSource.GetComponent<AudioSource>();
        ClockHitingSource.GetComponent<AudioSource>();

        isIn = area.GetComponent<IsPlayerInRoom>();

        timeLeft = Timer[Random.Range(0,3)]; // a random duration for the first time the player enters, so they will rarely notice a motif in the puzzle
    }


    void FixedUpdate()
    {
        ClockDebug = timeLeft;

        if (isIn.InTheRoom)
        {

            if (IsTicking)
            {

                if (!ClockTickingSource.isPlaying) // A needed checking in order to never play the sound effect in a frame when the audio is already playing
                {
                    Ticking();
                }
                IsHiting = false;
                timeLeft -= Time.fixedDeltaTime;

                if (timeLeft <= 0)
                {
                    IsTicking = false;
                }
            }
            if (!IsTicking) // If the state is not Ticking, then it chages to Hiting and vice versa
            {
                if (!ClockHitingSource.isPlaying) {

                    Hiting();
                }

                IsHiting = true;
                timeLeft += Time.fixedDeltaTime;

                if (timeLeft >= timeForAngelToLookAtPlayer)
                {
                    IsTicking = true;

                    timeLeft = Timer[Random.Range(0, 3)];
                }
            }
        }
        else //Basic Reset
        {
            if (!ClockHitingSource.isPlaying)
            {
                ClockHitingSource.Stop();
                ClockTickingSource.Stop();
            }

            IsTicking = true;
            IsHiting = false;

            timeLeft = Timer[Random.Range(0, 3)];

        }
    }

    public void Ticking()
    {
        ClockHitingSource.Stop();
        ClockTickingSource.Play();
    }

    public void Hiting()
    {
        ClockTickingSource.Stop();
        ClockHitingSource.Play();
    }
}
