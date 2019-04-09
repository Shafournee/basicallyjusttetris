using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerLobbyObject : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = GameObject.Find("Layout Group").transform;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
