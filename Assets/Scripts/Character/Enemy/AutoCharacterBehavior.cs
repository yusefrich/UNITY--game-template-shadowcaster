using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random = System.Random;

[RequireComponent(typeof(CustomCharacterController))]
public class AutoCharacterBehavior : MonoBehaviour
{
    [Header("Movement behavior")] 
    public GameObject movementPath;
    private Vector3 movementPathLocatiionLock;
    public GameObject[] movementPathPoints;
    private int currentMovementPathPoint = 0;
    private CustomCharacterController customCharacterController;

    [Header("character overwall behavior references")]
    public BehaviorType behaviorType;

    //here goes the attack reference
    [Header("Attack Behavior")] 
    public bool aggressiveCharacter;
    private CustomAttackController customAttackController;
    public GameObject attackType;
    //attack direction references
    [Tooltip("the default false value mean the character will aways attack in the direction of the player," +
             "if value is set to true than the character will attack on random directions")]
    [ConditionalField("aggressiveCharacter")]public bool randomizeAttackDirection;
    [ConditionalField("randomizeAttackDirection")]public GameObject[] attackDirectionPoints;
    
    //attack intervals reference
    [Tooltip("the default false value mean the character will aways attack only when close to the player," +
             "if the value is set to true than the character will attack constantly given a cooldown value")]
    [ConditionalField("aggressiveCharacter")]public bool constantAttacking;
    [ConditionalField("constantAttacking")] public float intervalBetweenAttacks;
    private float attackCoowdown = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        movementPathLocatiionLock = new Vector3(
            movementPath.transform.position.x,
            movementPath.transform.position.y,
            movementPath.transform.position.z);
        customCharacterController = GetComponent<CustomCharacterController>();
        customAttackController = GetComponent<CustomAttackController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementPath.transform.position = movementPathLocatiionLock;
        switch (behaviorType)
        {
            case BehaviorType.formlessEnemy:
                FormlessBehavior();
                break;
            case BehaviorType.walkingEnemy:
                print("behavior not yet implemented");
                break;
            case BehaviorType.walkingNpcCharacter:
                print("behavior not yet implemented");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void FormlessBehavior()
    {
        //print("im a formless enemy character");
        //movement behavior
        Vector2 movementDirection = (movementPathPoints[currentMovementPathPoint].transform.position -
                                    gameObject.transform.position).normalized;
        //print(movementDirection);
        //moving the player
        customCharacterController.Move(movementDirection);
        
        //gets next movementPathPoints point when player is close to its destination
        if (Vector3.Distance(gameObject.transform.position,
                movementPathPoints[currentMovementPathPoint].transform.position) < .1f)
        {
            currentMovementPathPoint++;
            if (currentMovementPathPoint >= movementPathPoints.Length)
            {
                currentMovementPathPoint = 0;
            }
        }

        if (!aggressiveCharacter)
        {
            return;
        }
        //attack behavuior
        if (attackCoowdown <= 0)
        {
            
            int attackDirection = Random.Range(0,attackDirectionPoints.Length);
            customAttackController.Attack(attackDirectionPoints[attackDirection].transform.position, attackType);
            attackCoowdown = intervalBetweenAttacks;
        }
        else
        {
            attackCoowdown -= Time.fixedDeltaTime;
        }
        
    }

    public enum BehaviorType
    {
        formlessEnemy, walkingEnemy, walkingNpcCharacter
    }

    
}
