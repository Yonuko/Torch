using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{

    public int chunkID, nbColones;

    public Material charged, lowCharhed, almostUncharged;

    ActualChunk playerChunk;

    // Start is called before the first frame update
    void Start()
    {
        playerChunk = GameObject.FindWithTag("Player").GetComponent<ActualChunk>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerChunk.actualChunk == 0) { return; } //Si le chunk est 0, signifie que le joueur n'est dans aucun chunk, donc on ne fait rien

        if (playerChunk.actualChunk == chunkID)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().material = charged; //Charge entièrement le chunk
        }
        else if(playerChunk.actualChunk == chunkID - 1 ||

                playerChunk.actualChunk == chunkID + 1 ||

                playerChunk.actualChunk == chunkID - nbColones ||

                playerChunk.actualChunk == chunkID + nbColones ||

                playerChunk.actualChunk == chunkID + nbColones + 1 ||
                playerChunk.actualChunk == chunkID + nbColones - 1 ||

                playerChunk.actualChunk == chunkID - nbColones + 1 ||
                playerChunk.actualChunk == chunkID - nbColones - 1 )
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().material = lowCharhed; //Charge partiellement le chunk
        }
        else if (playerChunk.actualChunk == chunkID - 2 ||
                playerChunk.actualChunk == chunkID - 2 + nbColones||
                playerChunk.actualChunk == chunkID - 2 - nbColones||

                playerChunk.actualChunk == chunkID + 2 ||
                playerChunk.actualChunk == chunkID + 2 + nbColones||
                playerChunk.actualChunk == chunkID + 2 - nbColones||

                playerChunk.actualChunk == chunkID - nbColones*2 ||
                playerChunk.actualChunk == chunkID + nbColones * 2 + 1 ||
                playerChunk.actualChunk == chunkID + nbColones * 2 + 2 ||
                playerChunk.actualChunk == chunkID + nbColones * 2 - 1 ||
                playerChunk.actualChunk == chunkID + nbColones * 2 - 2 ||

                playerChunk.actualChunk == chunkID + nbColones * 2 ||
                playerChunk.actualChunk == chunkID - nbColones * 2 + 1 ||
                playerChunk.actualChunk == chunkID - nbColones * 2 + 2 ||
                playerChunk.actualChunk == chunkID - nbColones * 2 - 1 ||
                playerChunk.actualChunk == chunkID - nbColones * 2 - 2 
                )
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().material = almostUncharged; //Charge très peu le chunk
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            playerChunk.actualChunk = chunkID;
        }
    }
}
