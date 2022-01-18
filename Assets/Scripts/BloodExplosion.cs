using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BloodExplosion : MonoBehaviour
{

    public float duration = 20;
    private float startTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startTime += Time.deltaTime;
        if (startTime >= duration){
            Destroy(gameObject);
        }
    }
}
