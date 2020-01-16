using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{

    public float lastPositionY, fallDistance, pourcentageLost;
    Transform player;

    public int distanceForDamage;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        pourcentageLost /= 100;
    }

    // Update is called once per frame
    void Update()
    {

        if (lastPositionY > transform.position.y)
        {
            fallDistance += lastPositionY - transform.position.y;
        }

        lastPositionY = transform.position.y;

        if (fallDistance >= distanceForDamage && controller.isGrounded)
        {
            GetComponent<PlayerLife>().TakeDamage(fallDistance * pourcentageLost);
            ResetValue();
        }

        if (fallDistance < distanceForDamage && controller.isGrounded)
        {
            ResetValue();
        }

    }

    void ResetValue()
    {
        fallDistance = 0;
        lastPositionY = 0;
    }
}
