using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    

    public void IncraseScore()
    {
        Score.IncraseScore();
        _scoreText.text = "Подбито: " + Score.GetScore();
    }


    public int GetScore()
    {
        return Score.GetScore();
    }

    private void ExitToMenu()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
    }


    private void Update()
    {
        ExitToMenu();
    }

    

}
