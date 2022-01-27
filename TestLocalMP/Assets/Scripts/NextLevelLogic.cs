using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
public class NextLevelLogic : MonoBehaviour
{

    int currentLevel;
    int nextLevel;
    void Start()
    {
        #if UNITY_ADS
        Advertisement.Initialize('4582527');
        #endif

        currentLevel = SceneManager.GetActiveScene().buildIndex;

        nextLevel = currentLevel + 1;
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene(nextLevel);

        #if UNITY_ADS
        if (Advertisement.IsReady('Interstitial_Android'))
        {
            Advertisement.Show ('Interstitial_Android')
        }
        #endif
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 100, 30), "Restart"))
        SceneManager.LoadScene(currentLevel);
    }
}
