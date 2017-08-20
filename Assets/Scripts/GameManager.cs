using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    public float spawnDelay = 2f;
    public bool stop = false;
    public GameObject difficultyMenu;
    public GameObject gameOver;
    public Text gameOverScore;
    public Text score;
    public Text highscore;
    public GameObject obstacle;
    public GameObject player;
    public EventSystem es;
    public GameObject restartButton;
    public Vector3 spawnLocation;
    public int[] spawnRange;
    public int spawnAmount;
    public float endDelay = 2f;

    float timeBefore = 0;
    bool gameEnded = false;

    public void StartGame(int spawnAmount)
    {
        this.spawnAmount = spawnAmount;
        StartCoroutine(WaitSpawn());
        player.SetActive(true);
        difficultyMenu.SetActive(false);
        score.gameObject.SetActive(true);
        timeBefore = Time.time;
    }

    void Update()
    {
        if (!stop) {
            score.text = ((Time.time - timeBefore) * 10).ToString("0");
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
                float rand = Random.Range(spawnRange[0], spawnRange[1] + 1); ;

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
                Instantiate(obstacle, spawnLocation + new Vector3(rand * 2, 0, 0), obstacle.transform.rotation);
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
        gameOver.SetActive(true);
        es.SetSelectedGameObject(restartButton);
    }
}

