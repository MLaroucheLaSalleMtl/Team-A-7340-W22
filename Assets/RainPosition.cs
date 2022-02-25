using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = new Quaternion(0, -55.328f, 0, 0);
    }
}
