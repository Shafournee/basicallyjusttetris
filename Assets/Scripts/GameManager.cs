using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject player;
	public static GameManager Instance = null;

    // Start is called before the first frame update
    void Start()
    {
		if(Instance == null) 
		{
			Instance = this;
			Debug.Log("Instantiated new player");
			PhotonNetwork.Instantiate(player.name, new Vector3(0f, 3f, 0f), Quaternion.identity);
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
