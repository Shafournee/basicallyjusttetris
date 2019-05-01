using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lobby : MonoBehaviourPun
{
    // Instantiate the object that shows the players in the lobby
    [SerializeField] GameObject playerLobbyObject;

    int playersReady;

    [SerializeField] GameObject playersReadyText;

    bool haveReadiedUp;

	bool isLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerLobbyObject.name, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        playersReadyText.GetComponent<Text>().text = playersReady.ToString();
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (PhotonNetwork.IsMasterClient && !isLoading)
            {
				isLoading = true;
				PhotonNetwork.LoadLevel("Tetris");
            }
        }
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
    void checkReadiedPlayers(int increment, PhotonMessageInfo info)
    {
        playersReady += increment;
        if(increment > 0)
        {
            GameObject senderGameObject = (GameObject)info.Sender.TagObject;
            senderGameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "y";
        }
        else
        {
            GameObject senderGameObject = (GameObject)info.Sender.TagObject;
            senderGameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "n";
        }
    }
}
