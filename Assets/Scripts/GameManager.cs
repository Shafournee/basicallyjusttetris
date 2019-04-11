using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void OnEnable()
    {
		Debug.Log("GameManager Start Called");
		PhotonNetwork.Instantiate(player.name, new Vector3(0f, 3f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
