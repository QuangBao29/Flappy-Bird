using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandlers : MonoBehaviour
{
    public static GameHandlers instance;
    public bool IsDead;

    private int Score = 0;

    [SerializeField]
    private GameObject TScore;
    [SerializeField]
    private GameObject HighScore;
    [SerializeField]
    private GameObject PanelEndgame;
    [SerializeField]
    private Text EndgameScore;
    [SerializeField]
    private GameObject SureOrNot;
    
    private AudioSource AudioButton;

    [SerializeField]
    private GameObject BirdObj;
    [SerializeField]
    private GameObject BGMove;
    
    private GameObject[] WallMove;

    private void Start()
    {
        Time.timeScale = 1;
        IsDead = false;
        PanelEndgame.SetActive(false);
        SureOrNot.SetActive(false);
        TScore.SetActive(true);
        AudioButton = gameObject.GetComponent<AudioSource>();
        BGMove.GetComponent<BGMovement>().enabled = false;
        WallMove = GameObject.FindGameObjectsWithTag("Wall Controller");
        for (int i = 0; i < WallMove.Length; i++) {
            WallMove[i].GetComponent<WallMovement>().enabled = false;
        }
        HighScore.GetComponent<Text>().text = "High Score\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    private void Update()
    {
        if (instance == null) instance = this;
        if (!IsDead)
        {
            if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            {
                BGMove.GetComponent<BGMovement>().enabled = true;
                for (int i = 0; i < WallMove.Length; i++)
                {
                    WallMove[i].GetComponent<WallMovement>().enabled = true;
                }
            }
        }
    }
    public void RestartButton()
    {
        AudioButton.Play();
        SceneManager.LoadScene(0);
    }
    public void QuitButton()
    {
        AudioButton.Play();
        PanelEndgame.SetActive(false);
        SureOrNot.SetActive(true);
    }
    public void YesButton()
    {
        AudioButton.Play();
        Application.Quit();
    }
    public void NoButton()
    {
        AudioButton.Play();
        SureOrNot.SetActive(false);
        PanelEndgame.SetActive(true);
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
    public void Scoring()
    {
        Score += 1;
        TScore.GetComponent<Text>().text = "Score: " + Score.ToString();
        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
        HighScore.GetComponent<Text>().text = "High Score\n" + PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void EndGame(GameObject obj)
    {
        Time.timeScale = 0;
        IsDead = true;
        EndgameScore.text = "Your Score\n" + Score.ToString();
        PanelEndgame.SetActive(true);
        TScore.SetActive(false);
    }

}
