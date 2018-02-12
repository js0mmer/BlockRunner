using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public EventSystem es;
    public GameObject startButton;

    void Update()
    {
        if(Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Submit") > 0f)
        {
            es.SetSelectedGameObject(startButton);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
