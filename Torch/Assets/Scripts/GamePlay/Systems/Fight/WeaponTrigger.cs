using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    private bool isAttacking = false;
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking)
        {
            if(other.GetComponent<EnnemyBattleScript>() != false)
            {
                Debug.Log("Attack successful on "+other.name);
                other.GetComponent<EnnemyBattleScript>().TakeDamage(GameObject.FindWithTag("Player").GetComponent<PlayerBattleScript>().attackDamage);
            }
        }
    }

    public void startAttack()
    {
        isAttacking = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    public void endAttack()
    {
        isAttacking = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
