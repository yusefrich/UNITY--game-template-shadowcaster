using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLineRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float distance = 0;
        Vector3[] positions  = new Vector3[gameObject.GetComponent<LineRenderer>().positionCount];
        gameObject.GetComponent<LineRenderer>().GetPositions(positions);
        
        for (int i = 0; i < positions.Length; i++)
        {
            print("position "+i+" = "+positions[i]);
            if (i + 1 < positions.Length)
            {
                distance += Vector3.Distance(positions[i], positions[i + 1]);
                
            }
        }
        
        gameObject.GetComponent<LineRenderer>().material.SetTextureScale ("_MainTex", new Vector2 (distance * 2, 1));﻿
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
