using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour
{
	private Rigidbody2D playerRb;
	private Vector2 moveInput;
	private bool isDead = false;
	private bool gameStarted = false;
	private int score = 0;

	// Objetos
	[SerializeField] private GameObject apple;

	// Audio
	

	// Texto
	[SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento ajustable
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private TMP_Text startGameText;
	[SerializeField] private TMP_Text keyStartText;
	[SerializeField] private TMP_Text gameOverText;
	[SerializeField] private TMP_Text restartText;

	void Start()
	{
		// Inicializar Rigidbody2D
		playerRb = GetComponent<Rigidbody2D>();
		if (playerRb == null)
		{
			Debug.LogError("No se encuentra el componente Rigidbody en el GameObject Snake");
		}

		UpdateScoreText();
		RelocateAppleAndActivate();
		apple.SetActive(false);
		gameOverText.gameObject.SetActive(false);
		StartCoroutine(BlinkText());
	}

	void Update()
	{
		// Iniciar juego
		if (!gameStarted)
		{
			if (Input.anyKeyDown)
			{
				StartGame();
			}
		}

		// Detectar entrada de movimiento solo si el jugador está vivo
		if (!isDead)
		{
			float moveX = Input.GetAxisRaw("Horizontal");
			float moveY = Input.GetAxisRaw("Vertical");

			moveInput = new Vector2(moveX, moveY).normalized;
		}
		else if (Input.anyKeyDown)
		{
			// Reiniciar el juego cuando el jugador está muerto y se presiona Espacio
			RestartGame();
		}
	}

	void FixedUpdate()
	{
		// Mover el Rigidbody en la dirección calculada
		if (!isDead)
		{
			playerRb.MovePosition(playerRb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
		}
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			moveSpeed = 0f;
			isDead = true;
			apple.SetActive(false);
			gameOverText.gameObject.SetActive(isDead);
		}
	}

	// Metodo para iniciar Juego
	private void StartGame()
	{
		gameStarted = true;
		apple.SetActive(true);
		startGameText.gameObject.SetActive(false);
		keyStartText.gameObject.SetActive(false);
		RelocateApple(apple);
	}

	// Método para reiniciar el juego
	public void RestartGame()
	{
		isDead = false;
		moveSpeed = 5f;
		transform.position = Vector3.zero;
		score = 0;
		UpdateScoreText();
		gameOverText.gameObject.SetActive(isDead);
		keyStartText.gameObject.SetActive(isDead);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Apple")
		{
			// sumar puntos
			score++;
			UpdateScoreText();
			// recolocar la manzana 
			RelocateApple(collision.gameObject);
		}
	}

	private void UpdateScoreText()
	{
		if (scoreText != null)
		{
			scoreText.text = $"SCORE: {score}";
		}
		else
		{
			Debug.LogError("scoreText no está asignado en el Inspector");
		}
	}

	private void RelocateAppleAndActivate()
	{
		RelocateApple(apple);
		apple.SetActive(true);
	}

	// Relocalizar la manzana en un lugar aleatorio
	private void RelocateApple(GameObject apple)
	{
		// Define los límites para las posiciones aleatorias en X y Y
		float minX = -7f, maxX = 7f;
		float minY = -3f, maxY = 3f;

		// Generar una nueva posición aleatoria dentro de los límites definidos
		float randomX = Random.Range(minX, maxX);
		float randomY = Random.Range(minY, maxY);

		// Asigna la nueva posición a la manzana
		apple.transform.position = new Vector2(randomX, randomY);
	}

	// Corrutina para hacer parpadear el texto
	private IEnumerator BlinkText()
	{
		while (true)
		{
			keyStartText.gameObject.SetActive(!keyStartText.gameObject.activeSelf);
			restartText.gameObject.SetActive(!restartText.gameObject.activeSelf);
			yield return new WaitForSeconds(0.2f);
		}
	}
}
