using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCrossHairTarget : MonoBehaviour
{
    Camera main;

    RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(main.transform.position, main.transform.forward, out hit);
        transform.position = hit.point;
    }
}
