using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePetalasMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetInteger("type", Random.Range(1,6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
