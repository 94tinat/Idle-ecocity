using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{

    public void BackToMain(string main)
    {
        SceneManager.LoadScene(main);
    }

}
