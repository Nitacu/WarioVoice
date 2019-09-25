using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicParticles : MonoBehaviour
{

    public ParticleSystem musicParticles;

    public void playParticles()
    {
        musicParticles.Play();
    }

    public void stopParticles()
    {
        musicParticles.Stop();
    }
}
