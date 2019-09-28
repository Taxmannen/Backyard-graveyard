using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ParticleSoundPlayer : MonoBehaviour
{
    //public var OnBirthSound : AudioClip;
    //public var OnDeathSound : AudioClip;

    [SerializeField] private PlaySound particleBirthSound;
    //[SerializeField] private PlaySound particleDeathSound;

    private int numberOfParticles = 0;

    [SerializeField] private ParticleSystem particleSystem;
 
 //private var _numberOfParticles : int = 0;
 
 //function Update()
 //   {
 //       if (!OnBirthSound && !OnDeathSound) { return; }
 //       var count = particleSystem.particleCount;
 //       if (count < _numberOfParticles)
 //       { //particle has died
 //           SoundManager.Play(audio, OnDeathSound);
 //       }
 //       else if (count > _numberOfParticles)
 //       { //particle has been born
 //           SoundManager.Play(audio, OnBirthSound);
 //       }
 //       _numberOfParticles = count;
 //   }

    private void Update()
    {
        if(particleBirthSound == null) { return; }
        int count = particleSystem.particleCount;
        
        if(count > numberOfParticles)
        {
            //Particle is born
            particleBirthSound.Play();
        }
        numberOfParticles = count;

    }
}
