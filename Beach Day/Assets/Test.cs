using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 force = new Vector3(0, 0, 1) * 10;
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
