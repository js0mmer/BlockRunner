using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float spawnDelay = 2f;
    public bool stop = false;
    public GameObject difficultyMenu;
    public GameObject gameOver;
    public GameObject pauseMenu;
    public Text gameOverScore;
    public Text score;
    public Text highscore;
    public GameObject obstacle;
    public GameObject player;
    public EventSystem es;
    public GameObject restartButton;
    public GameObject easyButton;
    public GameObject resumeButton;
    public GameObject pauseButton;
    public Vector3 spawnLocation;
    public int[] spawnRange;
    public int spawnAmount;
    public float endDelay = 2f;

    float timeBefore = 0;
    bool gameEnded = false;
    bool isPaused = false;
    ObjectPooler objectPooler;

    public void StartGame(int spawnAmount)
    {
        this.spawnAmount = spawnAmount;
        StartCoroutine(WaitSpawn());
        player.SetActive(true);
        difficultyMenu.SetActive(false);
        score.gameObject.SetActive(true);
        timeBefore = Time.time;

        if(Application.platform == RuntimePlatform.Android) pauseButton.SetActive(true);
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        isPaused = false;
        Time.timeScale = 1f;
        gameEnded = false;
        stop = false;
        if (Input.GetJoystickNames().Length > 0)
        {
            es.SetSelectedGameObject(easyButton);
        }
    }

    void Update()
    {
        if (!stop) {
            score.text = ((Time.time - timeBefore) * 10).ToString("0");
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                Resume();
            }
            else if (!difficultyMenu.activeInHierarchy && !gameOver.activeInHierarchy)
            {
                Pause();
            }
        }

        if (es.currentSelectedGameObject == null && Input.GetJoystickNames().Length > 0)
        {
            if(difficultyMenu.activeInHierarchy)
            {
                es.SetSelectedGameObject(easyButton);
            } else if(gameOver.activeInHierarchy)
            {
                es.SetSelectedGameObject(restartButton);
            } else if(pauseMenu.activeInHierarchy)
            {
                es.SetSelectedGameObject(resumeButton);
            }
        }
    }

    IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);

        while(!stop)
        {
            float[] previousRand = new float[3];

            for (int i = 0; i < spawnAmount; i++)
            {
                float rand = Random.Range(spawnRange[0], spawnRange[1] + 1);

                if(i == 1)
                {
                    if(previousRand[0] == rand)
                    {
                        i--;
                        continue;
                    }
                }

                if(i == 2)
                {
                    if (previousRand[0] == rand)
                    {
                        i--;
                        continue;
                    }

                    if (previousRand[1] == rand)
                    {
                        i--;
                        continue;
                    }
                }

                previousRand[i] = rand;
                objectPooler.SpawnFromPool("Obstacle", spawnLocation + new Vector3(rand * 2, 0, 0), obstacle.transform.rotation);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void EndGame()
    {
        if (!gameEnded)
        {
            stop = true;
            gameEnded = true;
            gameOverScore.text = "Score: " + score.text;

            if (int.Parse(score.text) > PlayerPrefs.GetInt("highscore" + spawnAmount, 0))
            {
                highscore.text = "Record: " + score.text;
                PlayerPrefs.SetInt("highscore" + spawnAmount, int.Parse(score.text));
            } else
            {
                highscore.text = "Record: " + PlayerPrefs.GetInt("highscore" + spawnAmount);
            }

            Invoke("GameOver", endDelay);
        }
    }

    void GameOver()
    {
        pauseButton.SetActive(false);
        gameOver.SetActive(true);
        if (Input.GetJoystickNames().Length > 0)
        {
            es.SetSelectedGameObject(restartButton);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
        if (Application.platform == RuntimePlatform.Android) pauseButton.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        if (Input.GetJoystickNames().Length > 0)
        {
            es.SetSelectedGameObject(resumeButton);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

