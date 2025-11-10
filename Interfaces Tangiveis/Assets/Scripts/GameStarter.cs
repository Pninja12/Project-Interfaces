using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    int button = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnMessageArrived(string msg)
    {
        button = int.Parse(msg);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("First Choice") || button == 1)
        {
            GameData.rounds = 3;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetButtonDown("Second Choice") || button == 2)
        {
            GameData.rounds = 5;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetButtonDown("Third Choice") || button == 3)
        {
            GameData.rounds = 10;
            SceneManager.LoadScene("Game");
        }

        button = 0;
    }
    
    public static class GameData
    {
        public static int rounds;
    }
}
