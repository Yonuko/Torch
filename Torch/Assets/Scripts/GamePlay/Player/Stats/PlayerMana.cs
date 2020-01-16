using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    Animator anim;
    float timer = 0.5f;

    //Temporaire
    Image manaBar;

    public float maxMana, playerMana;

    public bool NoMana = false;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

        if (PlayerSaveLoader.IsPlayerLoader())
        {
            playerMana = GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>().datas.playerLife;
        }

        //Temporaire
        manaBar = GameObject.Find("ManaBar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        

        

        //Temporaire
        manaBar.fillAmount = playerMana / maxMana;

        if (playerMana <= 0)
        {
            playerMana = 0;
            NoMana = true;

        }
        
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("test");
            LossMana(1.5f);
            
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            RestoreMana(1.5f);
            timer = 2.0f;
        }

    }



    public void LossMana(float cost)
    {
        playerMana -= cost;
    }

    public void RestoreMana(float value)
    {
        playerMana += value;
    }

}
