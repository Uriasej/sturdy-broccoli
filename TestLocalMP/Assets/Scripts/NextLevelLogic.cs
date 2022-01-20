using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLogic : MonoBehaviour
{

    int currentLevel;
    int nextLevel;
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        nextLevel = currentLevel + 1;
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
