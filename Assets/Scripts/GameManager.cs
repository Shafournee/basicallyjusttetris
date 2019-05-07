using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject platformerPlayer;
    [SerializeField] GameObject tetrisPlayer;
    [SerializeField] int playersConnected = 0;

    // Used to keep a singleton
	public static GameManager Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton behavior
		if(Instance == null) 
		{
			Instance = this;

            // On start we want to tell the master client that we've loaded the scene
            photonView.RPC("PlayerHasJoined", RpcTarget.MasterClient);
        } 
		else 
		{
			Destroy(gameObject);
		}
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    [PunRPC]
    void InstantiatePlatformingPlayers()
    {
        PhotonNetwork.Instantiate(platformerPlayer.name, new Vector3(2f, 3f, 0f), Quaternion.identity);
    }

    [PunRPC]
    void InstantiateTetrisPlayer()
    {
        PhotonNetwork.Instantiate(tetrisPlayer.name, new Vector3(2.56f, 8.78f, 0f), Quaternion.identity);
    }

    [PunRPC]
    void PlayerHasJoined()
    {
        playersConnected++;

        // When a player joins, check if everyone has joined
        if(PhotonNetwork.IsMasterClient)
        {
            CheckIfAllPlayersHaveJoined();
        }
    }

    void CheckIfAllPlayersHaveJoined()
    {
        // If the number of players in the room equal the number of players who have connected, we want to instantiate players
        // This also implies that we're the master client, since the master client is the only one who tracks this info, but
        // we can include a check for safety
        if (playersConnected == PhotonNetwork.PlayerList.Length)
        {
            // First, let's find in our list who will be the tetris player
            // We'll do this by generating a random number
            int rand = Random.Range(0, PhotonNetwork.PlayerList.Length);

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                // this is the tetris player we're instantiating, so do an RPC call for that function
                if (i == rand)
                {
                    photonView.RPC("InstantiateTetrisPlayer", PhotonNetwork.PlayerList[i]);
                }
                else
                {
                    photonView.RPC("InstantiatePlatformingPlayers", PhotonNetwork.PlayerList[i]);
                }
            }
        }
    }
}
