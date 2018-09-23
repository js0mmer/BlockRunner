using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public EventSystem es;
    public GameObject startButton;

    void Update()
    {
        if(es.currentSelectedGameObject == null && Input.GetJoystickNames().Length > 0)
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
