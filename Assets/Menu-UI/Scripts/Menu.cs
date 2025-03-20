using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage Prototype");
    }
    public void Quit()
    {
        Application.Quit();

    }
}
