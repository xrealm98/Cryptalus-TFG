using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAudio : MonoBehaviour
{
    public List<AudioClip> sonidosPasos;
    public List<AudioClip> sonidosAtaque;
    public List<AudioClip> sonidosRecibirGolpe;
    public List<AudioClip> sonidosMuerte;

    [Range(0f, 1f)] public float volumenPasos = 0.2f;
    [Range(0.1f, 3f)] public float pitchPasos = 1f;

    [Range(0f, 1f)] public float volumenAtaque = 0.5f;
    [Range(0.1f, 3f)] public float pitchAtaque = 1f;

    [Range(0f, 1f)] public float volumenRecibirGolpe = 0.5f;
    [Range(0.1f, 3f)] public float pitchRecibirGolpe = 1f;

    [Range(0f, 1f)] public float volumenMuerte = 0.5f;
    [Range(0.1f, 3f)] public float pitchMuerte = 1f;


    private AudioSource fuentePasos, fuenteAtaque, fuenteRecibirGolpe, fuenteMuerte;

    void Start() {

        fuentePasos = GetComponents<AudioSource>()[0];
        fuenteAtaque = GetComponents<AudioSource>()[1];
        fuenteRecibirGolpe = GetComponents<AudioSource>()[2];
        fuenteMuerte = GetComponents<AudioSource>()[3];

    }


    public void ReproducirSonidoPasos()
    {
        AudioClip clip = sonidosPasos[Random.Range(0, sonidosPasos.Count)];
        fuentePasos.clip = clip;
        fuentePasos.volume = volumenPasos;
        fuentePasos.pitch = pitchPasos;
        fuentePasos.Play(); 
    }

    public void ReproducirSonidoAtaque()
    {
        AudioClip clip = sonidosAtaque[Random.Range(0, sonidosAtaque.Count)];
        fuentePasos.clip = clip;
        fuentePasos.volume = volumenAtaque;
        fuentePasos.pitch = pitchAtaque;
        fuentePasos.PlayOneShot(clip,volumenAtaque);
    }

    public void ReproducirSonidoRecibirGolpe()
    {
        if (fuenteRecibirGolpe.isPlaying) return;
        AudioClip clip = sonidosRecibirGolpe[0];
        fuenteRecibirGolpe.clip = clip;
        fuenteRecibirGolpe.volume = volumenRecibirGolpe;
        fuenteRecibirGolpe.pitch = pitchRecibirGolpe;
        fuenteRecibirGolpe.Play();
    }
    public void ReproducirSonidoMuerte()
    {
        AudioClip clip = sonidosMuerte[0];
        fuenteMuerte.clip = clip;
        fuenteMuerte.volume = volumenMuerte;
        fuenteMuerte.pitch = pitchMuerte;
        fuenteMuerte.Play();
    }
}
