using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject platformerPlayer;
    [SerializeField] GameObject tetrisPlayer;


    // Used to keep a singleton
	public static GameManager Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton behavior
		if(Instance == null) 
		{
			Instance = this;
			Debug.Log("Instantiated new player");
            // TODO: Make the tetris player get instantiated as well
            PhotonNetwork.Instantiate(platformerPlayer.name, new Vector3(0f, 3f, 0f), Quaternion.identity);
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
}
