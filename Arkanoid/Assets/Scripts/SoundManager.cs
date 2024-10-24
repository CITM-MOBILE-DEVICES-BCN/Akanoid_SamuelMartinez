using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource pointFX;
    public AudioSource deathFX;
    public AudioSource winFX;
    public AudioSource pauseFX;
    public AudioSource ballDead;
    public AudioSource menuMusic;
    public AudioSource gameplayMusic;
    public AudioSource bounceFX;
    public AudioSource powerUp;

    public void PlayScoreEfect()
    {
        pointFX.Play();
    }
    public void PlayDeathSoundEffect()
    {
        deathFX.Play();
    }
    public void PlayWinSoundEffect()
    {
        winFX.Play();
    }
    public void PlayPauseSoundEffect()
    {
        pauseFX.Play();
    }
    public void PlayBallDeadSoundEffect()
    {
        ballDead.Play();
    }
    public void PlayMenuMusic()
    {
        menuMusic.Play();
    }
    public void PlayGameplayMusic()
    {
        gameplayMusic.Play();
    }
    public void PlayBounceSoundEffect()
    {
        bounceFX.Play();
    }
    public void PlayPowerUpSoundEffect()
    {
        powerUp.Play();
    }
    public void StopMenuMusic()
    {
        menuMusic.Stop();
    }
    public void StopGameplayMusic()
    {
        gameplayMusic.Stop();
    }


}
