using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    float time = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Destruct()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
