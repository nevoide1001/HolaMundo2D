using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    #region AudioSource
    [Header("Audio Clips")]
    [SerializeField] private AudioClip cherrySound;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioClip spikesSound;

    private AudioSource audioSource;
    #endregion
    #region Constants

    const float SpykesDamage = 20;
    const float CherryHeal = 50;
    const float GemForce = 500;
    #endregion

    #region Enums
    // Enum de los items;
    public enum ItemTypes
    {
        Cherry,
        Spykes,
        Gem
    }

    // Con esto las properties aparecen en el inspector;
    [field: SerializeField]public ItemTypes type {get; set; }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Comprobar colision;
        //Debug.Log("Colisión detectada con: " + collision.gameObject); // Para ver si la colisión se detecta

        if(collision.CompareTag("Player"))
        {

            //Debug.Log("El ítem tocó al jugador"); // Para ver si colisiona con el jugador;

            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
            switch(type)
            {
                case ItemTypes.Cherry:
                jetpack.Regenerate(CherryHeal);
                    AudioSource.PlayClipAtPoint(cherrySound, transform.position);
                    break;

                case ItemTypes.Gem:
                jetpack.GetComponent<Rigidbody2D>().AddForce(Vector2.up * GemForce);
                    AudioSource.PlayClipAtPoint(gemSound, transform.position);
                    break;

                case ItemTypes.Spykes:
                jetpack.energy -= SpykesDamage;
                    PlayerAnimator playerAnim = collision.GetComponent<PlayerAnimator>();
                    if (playerAnim != null)
                        playerAnim.SpikesHurt();
                    AudioSource.PlayClipAtPoint(spikesSound, transform.position);
                    break;

            }

            Destroy(gameObject);
 
        }

        if(collision.CompareTag("Ground"))
        {

            Debug.Log("El ítem tocó el suelo"); // Para ver si colisiona con el Suelo;

            Destroy(gameObject);

        }
      
    }
    #endregion

}
