using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    float xRand;

    [SerializeField] GameObject Square;
    
    public int cubeCount;

    void Start()
    {
        cubeCount = 0;

        ChooseCubeSpawn();
    }

    public void ChooseCubeSpawn()
    {
        Vector2 cubeSpawn = new Vector2(Random.Range(-5f, 5f), 6);
        Instantiate(Square, cubeSpawn, Quaternion.identity);

        cubeCount = cubeCount + 1;
    }
}
