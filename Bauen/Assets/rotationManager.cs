using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationManager : MonoBehaviour
{
    [SerializeField] public GameObject wood;

    [SerializeField] public GameObject eraser;

    [SerializeField] public GameObject Spot;

    public Quaternion woodRotation;

    public Quaternion eraserRotation;

    public Vector2 woodPos;

    void Start()
    {
        woodPos = wood.transform.position;

        woodRotation = wood.transform.rotation;

        eraserRotation = eraser.transform.rotation;
    }

    void Update()
    {
        woodPos = wood.transform.position;
        woodRotation = wood.transform.rotation;
        eraser.transform.rotation = woodRotation;
        eraser.transform.position = Spot.transform.position;
    }

    private void FixedUpdate()
    {
        if (woodPos.x > 11)
        {
            wood.transform.position = new Vector2(0, 0);
            woodRotation = Quaternion.Euler(0, 0, 0);
        }

        if (woodPos.x < -11)
        {
            wood.transform.position = new Vector2(0, 0);
            woodRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
