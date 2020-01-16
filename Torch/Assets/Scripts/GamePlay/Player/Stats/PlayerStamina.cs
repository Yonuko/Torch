using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    Animator anim;
     float timer = 0.5f;

   
    //Temporaire
    Image staminaBar;

    public float maxStamina, playerStamina;

    public bool NoStamina = false;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

        if (PlayerSaveLoader.IsPlayerLoader())
        {
            playerStamina = GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>().datas.playerLife;
        }

        //Temporaire
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        

        //Temporaire
        staminaBar.fillAmount = playerStamina / maxStamina;

        if (playerStamina <= 0)
        {
            playerStamina = 0;
            NoStamina = true;
            
        }
        if (Input.GetKey(KeyCode.Y))
        {
            Debug.Log("test");
            LossStamina(1.5f);

        }
       

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            RestoreStamina(1.5f);
            timer = 2.0f;
        }


    }

    public void LossStamina(float cost)
    {
        playerStamina -= cost;
    }

    public void RestoreStamina(float value)
    {
        playerStamina += value;
    }
}
