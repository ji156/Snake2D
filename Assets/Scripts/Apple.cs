using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	public AudioSource foodSound;  // Referencia al AudioSource

	// Start is called before the first frame update
	void Start()
	{
		// Si no has asignado el AudioSource desde el Inspector, obténlo automáticamente
		if (foodSound == null)
		{
			foodSound = GetComponent<AudioSource>();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Verificar si la colisión es con la serpiente
		if (collision.gameObject.CompareTag("Snake"))
		{
			if (foodSound != null)
			{
				foodSound.Play();  // Reproducir sonido cuando la serpiente come la manzana
			}
			else
			{
				Debug.LogError("El AudioSource es nulo, no se puede reproducir el sonido.");
			}
		}
	}
}
