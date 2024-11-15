using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	// Referencia al AudioSource del GameObject Background
	public AudioSource backgroundAudioSource;

	void Start()
	{
		// Si no has asignado el AudioSource desde el Inspector, obt�nlo autom�ticamente
		if (backgroundAudioSource == null)
		{
			backgroundAudioSource = GetComponent<AudioSource>();
		}
	}
	void Update()
	{
		// Si presionas la tecla 'S', el sonido de fondo se detiene
		if (Input.anyKeyDown)
		{
			StopBackgroundMusic();
		}
	}


	// M�todo para detener el sonido de fondo
	public void StopBackgroundMusic()
	{
		// Detener el AudioSource del GameObject Background
		backgroundAudioSource.Stop();
	}

}
