using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("GameManager Start Called");
		if(Platformer.localPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(player.name, new Vector3(0f, 3f, 0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
