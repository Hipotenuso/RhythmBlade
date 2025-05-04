using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public void LoadScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
