using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(8f * Time.deltaTime * Vector3.up);

        if (transform.position.y > 11f)
        {
            Destroy(this.gameObject);
        }
    }
}
