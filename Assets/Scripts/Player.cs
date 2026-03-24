using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Referencia al componente Jetpack;
    [SerializeField] private Jetpack jetpack;
    [SerializeField] private ParticleSystem jetpackParticles;

    // Referencia al SpriteRenderer;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Obtiene el componente SpriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Forma de asignar el jetpack en caso de que no lo este ya;
        if (jetpack == null)
        {
            jetpack = GetComponent<Jetpack>();
            if (jetpackParticles != null)
            {
                jetpackParticles.Stop();
            }
                

            // Mensaje de error en la consola en caso de no encontrat jetpack;
            if (jetpack == null)
            {
                Debug.LogError("No se encontró el Jetpack en el Player.");
            }
        }
    }

    void Update()
    {
        // Return en caso de que el jetpack no este asignado;
        if (jetpack == null) return;

        // Obtiene los Axis del teclado para movimiento horizontal y vertical;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        #region Movimiento
        // Maneja el movimiento horizontal y la orientación del sprite;
        if (horizontal > 0)
        {
            jetpack.flyHorizontal(Jetpack.Direction.Right);
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            jetpack.flyHorizontal(Jetpack.Direction.Left);
            spriteRenderer.flipX = true;
        }

        // Maneja el movimiento vertical;
        if (vertical > 0)
        {
            jetpack.flyUp();
            if (!jetpackParticles.isPlaying)
            {
                jetpackParticles.Play();
            }
        }
        else
        {
            jetpack.stopFlying();
            if (jetpackParticles.isPlaying)
            {
                jetpackParticles.Stop();
            }
        }
        #endregion

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colision");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            Finish();
        }
    }

    private void Finish()
    {
        Debug.Log("¡Has llegado al final del nivel!");
        FindObjectOfType<GameManager>()?.EndGame();
    }
}
