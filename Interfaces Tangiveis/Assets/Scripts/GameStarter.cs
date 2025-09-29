using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("First Choice"))
        {
            GameData.rounds = 3;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetButtonDown("Second Choice"))
        {
            GameData.rounds = 5;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetButtonDown("Third Choice"))
        {
            GameData.rounds = 10;
            SceneManager.LoadScene("Game");
        }
    }
    
    public static class GameData
    {
        public static int rounds;
    }
}
