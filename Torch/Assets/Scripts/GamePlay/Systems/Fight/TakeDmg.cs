using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakeDmg : MonoBehaviour
{
    public int curHP = 100;
    public int maxHP = 100;
    public int attackDamage = 0;

    public TakeDmgEnnemy target;

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        Debug.Log("Ca commence");
    }

    // Update is called once per frame
    void Update()
    {

       

        if (Input.GetKeyDown("w"))  // A SUPPRIMER, SERT POUR LE TEST
        {
            TakeHeal(14);
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
        Debug.Log("Oui");
        if (other.gameObject.CompareTag("Ennemy"))
        {
            Debug.Log("non");
            if (Input.GetKeyDown(KeyCode.R))
            {
                Attack();
            }
        }
    }

}