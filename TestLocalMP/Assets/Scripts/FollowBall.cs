using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FollowBall : MonoBehaviour
{

    private PostProcessVolume postProcessVolume;

    private Bloom bloom;

    private Vignette vignette;
    public GameObject ball;
    public Vector2 ballPos;

    private bool bloomEnabled;
    private bool vignetteEnabled;

    void Start()
    {
        bloomEnabled = true;

        vignetteEnabled = true;

        ball = GameObject.Find("Circle");

        postProcessVolume = gameObject.GetComponent<PostProcessVolume>();

        postProcessVolume.profile.TryGetSettings(out bloom);
        postProcessVolume.profile.TryGetSettings(out vignette);

    }

    void Update()
    {
        ballPos = ball.transform.position;

        gameObject.transform.position = new Vector3(ballPos.x, ballPos.y, -100);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(1000, 10, 200, 30), "Bloom Toggle"))
        {
            if (bloomEnabled == false)
            {
                bloom.active = true;
                bloomEnabled = true;
            }
            else if (bloomEnabled == true)
            {
                bloom.active = false;
                bloomEnabled = false;
            }
        }

        if (GUI.Button(new Rect(1000, 40, 200, 30), "Vignette Toggle"));
        {
            if (vignetteEnabled == false)
            {
                vignette.active = true;
                vignetteEnabled = true;
            }
            else if (vignetteEnabled == true)
            {
                vignette.active = false;
                vignetteEnabled = false;
            }
        }
    }
}
