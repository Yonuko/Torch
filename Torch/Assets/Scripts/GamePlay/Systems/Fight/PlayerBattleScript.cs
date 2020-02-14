using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBattleScript : MonoBehaviour
{
    public int curHP = 100;
    public int maxHP = 100;
    public int attackDamage = 4;

    public GameObject weapon;

    Animator anim;

    bool oui = false;

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        anim = GetComponent<Animator>();
        Debug.Log("Ca commence");
    }

    // Update is called once per frame
    void Update()
    {

       

        if (Input.GetKeyDown("w"))  // A SUPPRIMER, SERT POUR LE TEST
        {
            TakeHeal(14);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("Stable Sword Inward Slash");
        }
        if (Input.GetKeyDown("f"))
        {
            if (oui){
                anim.Play("Withdrawing Sword");
                oui = false;
            }
            else
            {
                anim.Play("Sheathing Sword");
                oui = true;
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

    void startAttack()
    {
        weapon.GetComponent<WeaponTrigger>().startAttack();
    }

    void endAttack()
    {
        weapon.GetComponent<WeaponTrigger>().endAttack();
    }





}