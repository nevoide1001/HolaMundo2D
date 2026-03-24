using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Objetos;
    [SerializeField] private GameObject Items;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject Historia;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject EndParticles;

    private bool gameStarted = false;
    private float recordTime = 0f;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        Canvas.SetActive(true);
        Player.SetActive(false);
        Objetos.SetActive(false);
        Items.SetActive(false);
        Historia.SetActive(false);
        EndParticles.SetActive(false);

        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    void Update()
    {
        if (gameStarted)
        {
            recordTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    #endregion

    #region Public Methods

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            recordTime = 0f;
            Canvas.SetActive(false);
            Player.SetActive(true);
            Objetos.SetActive(true);
            Items.SetActive(true);
            if (musicSource != null)
            {
                musicSource.Play();
            }
        }
    }
    public void HistoriaButton()
    {
       if (Historia.activeSelf == false)
       {
           Historia.SetActive(true);
       }
       else
       {
           Historia.SetActive(false);
        }

    }

    public void EndGame()
    {
        Canvas.SetActive(true);
        Player.SetActive(false);
        Objetos.SetActive(false);
        Items.SetActive(false);
        EndParticles.SetActive(true);
        if (musicSource != null)
        {
            musicSource.Stop();
        }

           timeText.text = $"Tiempo: {recordTime:F2} segundos";
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }   

    
    #endregion


}
