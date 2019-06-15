using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CustomCharacterController))]
public class CustomAnimationController : MonoBehaviour
{
    private Animator anim;
    private AnimationType animationType;
    private CustomCharacterController customCharacterController;
    MovementData movementData;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        customCharacterController = GetComponent<CustomCharacterController>();
        //get the type of character
        if (GetComponent<Player>() != null)
        {
            animationType = AnimationType.player;
        }
        else
        {
            switch (GetComponent<AutoCharacterBehavior>().behaviorType)
            {
                case AutoCharacterBehavior.BehaviorType.formlessEnemy:
                    animationType = AnimationType.formlessEnemy;
                    break;
                case AutoCharacterBehavior.BehaviorType.walkingEnemy:
                    animationType = AnimationType.walkingEnemy;
                    break;
                case AutoCharacterBehavior.BehaviorType.walkingNpcCharacter:
                    animationType = AnimationType.walkingNpcCharacter;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //this updates the current movement data for using with the animator
        movementData = JsonUtility.FromJson<MovementData>(customCharacterController.GetMovementOutputData());
        switch (animationType)
        {
            case AnimationType.player:
                PlayerAnimation();
                break;
            case AnimationType.formlessEnemy:
                FormlessEnemyAnimation();
                break;
            case AnimationType.walkingEnemy:
                break;
            case AnimationType.walkingNpcCharacter:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void PlayerAnimation()
    {
        anim.SetBool("IsJumping", movementData.IsJumping);
        anim.SetBool("IsMoving", movementData.IsMoving);
        anim.SetFloat("Xspeed", movementData.Xspeed);
        anim.SetFloat("Yspeed", movementData.Yspeed);
        anim.SetFloat("LastX", movementData.LastX);
        anim.SetFloat("LastY", movementData.LastY);
        
        anim.SetBool("Falling", movementData.Falling);
        anim.SetBool("Alive", movementData.Alive);
    }

    void FormlessEnemyAnimation()
    {
        
    }
    
    public enum AnimationType
    {
        player, formlessEnemy, walkingEnemy, walkingNpcCharacter
    }
}

public class MovementData
{
    public float Xspeed;
    public float Yspeed;
    public float LastX;
    public float LastY;
    public bool IsMoving;
    public bool IsJumping;
    
    public bool Falling;
    public bool Alive;


}
