using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakeDmgEnnemy : MonoBehaviour
{
    public int curHP = 100;
    public int maxHP = 100;
    public int attackDamage = 0;

    private float time = 0.0f;

    private bool attackAvailable = true;

    public TakeDmg target;

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown("w"))  // A SUPPRIMER, SERT POUR LE TEST
        {
            TakeHeal(14);
        }

        if (attackAvailable == false)
        {
            time += Time.deltaTime;
            if (time >= 2f)
            {
                time = 0.0f;
                attackAvailable = true;
            }
        }
        

    }

    public void TakeDamage(int value)
    {
        curHP -= value;
        if (curHP < 0)
        {
            curHP = 0;
        }
    }

    void TakeHeal(int value)
    {
        curHP += value;
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
    }

    void Attack()
    {
        if (attackDamage < 1)
        {
            attackDamage = 1;
        }
        target.TakeDamage(attackDamage);

    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (attackAvailable)
            {
                Attack();
                attackAvailable = false;
            }
        }
    }

}