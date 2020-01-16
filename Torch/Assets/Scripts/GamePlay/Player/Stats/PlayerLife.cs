using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    Animator anim;

    //Temporaire
    Image lifeBar;

    public float maxLife, playerLife;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

        if (PlayerSaveLoader.IsPlayerLoader())
        {
            playerLife = GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>().datas.playerLife;
        }

        //Temporaire
        lifeBar = GameObject.Find("LifeBar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        // Set animator Parametres
        anim.SetBool("Dead", dead);

        //Temporaire
        lifeBar.fillAmount = playerLife / maxLife;

        if (playerLife <= 0)
        {
            playerLife = 0;
            dead = true;
            GetComponent<PlayerMouvement>().stopMoving = true;
        }

    }

    public void TakeDamage(float damage)
    {
        playerLife -= damage;
    }

    public void HealPlayer(float value)
    {
        playerLife += value;
    }
}
