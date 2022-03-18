using UnityEngine;

public class prefabLogic : MonoBehaviour
{

    void Update()
    {
        if (gameObject.transform.position.y <= -11)
        {
            Destroy(gameObject);
        }
    }
}
