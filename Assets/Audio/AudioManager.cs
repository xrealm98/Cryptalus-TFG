using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sonido[] sonidos;

    public static AudioManager instance;    
    void Awake() {
        if (instance == null) { 
            instance = this;
        
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sonido sonido in sonidos) {

            sonido.fuente = gameObject.AddComponent<AudioSource>();
            sonido.fuente.clip = sonido.clip;
            sonido.fuente.volume = sonido.volume;
            sonido.fuente.pitch = sonido.pitch;
            sonido.fuente.loop = sonido.loop;

        }
    
    }

    public void Play(string nombre) {

       Sonido s = Array.Find(sonidos, sound => sound.name == nombre);
        if (s == null) {
            Debug.LogWarning("Sonido: " + nombre + "no encontrado.");
            return;
        }
        s.fuente.Play();
    }

    public void PlayRandomTema(string[] temas) {
        
        if (temas == null || temas.Length == 0)
        {
            Debug.LogWarning("No se han proporcionado temas para reproducir.");
            return;
        }

        // Seleccionamos un tema aleatorio del array.
        string temaSeleccionado = temas[UnityEngine.Random.Range(0, temas.Length)];

        // Detenemos cualquier música previa antes de reproducir la siguiente.
        foreach (string tema in temas)
        {
            StopPlaying(tema);
        }

       
        Play(temaSeleccionado);

    }

    public void StopPlaying(string nombre)
    {
        Sonido s = Array.Find(sonidos, sound => sound.name == nombre);
        if (s == null)
        {
            Debug.LogWarning("Sonido: " + name + " no encontrado.");
            return;
        }
        s.fuente.Stop();
    }

}
