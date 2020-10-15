﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SelfDestruct());
        
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
