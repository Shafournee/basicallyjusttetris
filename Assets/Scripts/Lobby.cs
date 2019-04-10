using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lobby : MonoBehaviourPun
{
    // Instantiate the object that shows the players in the lobby
    [SerializeField] GameObject playerLobbyObject;

    int playerReady;

    [SerializeField] GameObject playersReadyText;

    bool haveReadiedUp;

    GameObject heldPlayerObject;

    // Start is called before the first frame update
    void Start()
    {
        heldPlayerObject =  PhotonNetwork.Instantiate(playerLobbyObject.name, Vector3.zero, Quaternion.identity);
        Debug.Log("This is my instantiated Object " + heldPlayerObject);
    }

    // Update is called once per frame
    void Update()
    {
        playersReadyText.GetComponent<Text>().text = playerReady.ToString();
    }


    public void ReadyUpButton()
    {
        haveReadiedUp = !haveReadiedUp;

        if (haveReadiedUp == true)
        {
            photonView.RPC("checkReadiedPlayers", RpcTarget.AllBuffered, 1);
        }
        else
        {
            photonView.RPC("checkReadiedPlayers", RpcTarget.AllBuffered, -1);
        }
    }

    [PunRPC]
    void checkReadiedPlayers(int increment)
    {
        playerReady += increment;
        if(increment > 0)
        {
            heldPlayerObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "y";
        }
        else
        {
            heldPlayerObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "n";
        }
    }
}
