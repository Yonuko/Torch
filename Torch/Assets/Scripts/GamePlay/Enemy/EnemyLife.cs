using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    Animator anim;

    //Temporaire
    //Image EnemylifeBar;

    public float maxLife, enemyLife;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

        //Temporaire
        //EnemylifeBar = GameObject.Find("EnemyLifeBar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        // Set animator Parametres
        anim.SetBool("Dead", dead);

        //Temporaire
        //EnemylifeBar.fillAmount = enemyLife / maxLife;

        if (enemyLife <= 0)
        {
            enemyLife = 0;
            dead = true;
        }

    }

    public void TakeDamage(float damage)
    {
        enemyLife -= damage;
    }

    public void HealPlayer(float value)
    {
       enemyLife += value;
    }
}
