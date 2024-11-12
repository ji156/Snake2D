using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 4f;

    private Rigidbody2D snakeRb;

	void Start()
	{
		// Asigna el componente Rigidbody2D del objeto actual a snakeRb
		snakeRb = GetComponent<Rigidbody2D>();

		if (snakeRb == null)
		{
			Debug.LogError("No se encontró Rigidbody2D en el objeto Snake.");
		}
	}
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
			// Asignamos valor 0 en X y valor de la variable en Y
            snakeRb.velocity = new Vector2 (0, initialVelocity);
        }
		else if (Input.GetKeyDown(KeyCode.S))
		{
			snakeRb.velocity = new Vector2 (0,-initialVelocity);
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			snakeRb.velocity = new Vector2 (initialVelocity, 0);
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			snakeRb.velocity = new Vector2 (-initialVelocity, 0);
		}
        
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
			initialVelocity = 0;
		}
	}
}
