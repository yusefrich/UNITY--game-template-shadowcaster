using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomAttackController : MonoBehaviour
{
    [Header("attack references")]
    public GameObject[] attackPoints;
    [FormerlySerializedAs("atackCoowdownValue")] [Header("Cooldown reference")]    
    public float attackCoowdownValue = .5f;
    private float attackCoowdown = 0f;
    private bool canAttack = true;
    

    public void Attack(Vector3 attackPointPosition, GameObject AttackType)
    {
        if (canAttack)
        {
            attackCoowdown = attackCoowdownValue;
            //check the clossest attack point
            float distance = Mathf.Infinity;
            GameObject closestPoint = null;
            
            foreach (GameObject point in attackPoints)
            {
                if (Vector3.Distance(point.transform.position, attackPointPosition) < distance)
                {
                    distance = Vector3.Distance(point.transform.position, attackPointPosition);
                    closestPoint = point;
                }
            }
            //instantiate the attack on the clossest point
            GameObject currentInstantiation = Instantiate(AttackType, closestPoint.transform);
            currentInstantiation.transform.localRotation = Quaternion.Euler(Vector3.zero);
            currentInstantiation.transform.localPosition = Vector3.zero;
            
            //attack with that attack point
            //closestPoint.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCoowdown <= 0)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
            attackCoowdown -= Time.deltaTime;
        }
    }
}
