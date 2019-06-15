using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, _IsDamagable
{
    private CustomCharacterController customCharacterController;
    
    // Start is called before the first frame update
    void Start()
    {
        customCharacterController = GetComponent<CustomCharacterController>();
    }

    public void TakeHit()
    {
        EndLife();
    }

    public void EndLife()
    {
        customCharacterController.DestroyDeath();
    }
}
