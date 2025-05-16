using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    private void Start()
    {
        Score.ResetScore();
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void ExitGame()
    {
        Application.Quit();
    }


    public void ViewScore()
    {
        SceneManager.LoadScene(2);
    }

}
