using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TetrisPlayer : MonoBehaviourPun
{

    [SerializeField] GameObject[] pieces;

    // This keeps track of whether or not spots are occupied
    bool[,] grid = new bool[10, 15];

    GameObject currentlyControlledPiece;

    float leftBound = 0f;
    float rightBound = 5.12f;

    float increment = 1.28f;

    float moveDownPause = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNextPiece();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        // Move the object right
        if(Input.GetKeyDown(KeyCode.RightArrow) && currentlyControlledPiece.transform.position.x < rightBound)
        {
            currentlyControlledPiece.transform.position = new Vector3(currentlyControlledPiece.transform.position.x + increment, 
                currentlyControlledPiece.transform.position.y, 0f);
        }
        // Move the object left
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentlyControlledPiece.transform.position.x > leftBound)
        {
            currentlyControlledPiece.transform.position = new Vector3(currentlyControlledPiece.transform.position.x - increment, 
                currentlyControlledPiece.transform.position.y, 0f);
        }
    }

    public void SpawnNextPiece()
    {
        //Set our current pieces parent to null

        if(currentlyControlledPiece != null)
            currentlyControlledPiece.transform.parent = null;

        // Pick a random piece from the pieces list
        int randomPiece = Random.Range(0, pieces.Length);

        // Instantiate that piece
        currentlyControlledPiece = PhotonNetwork.Instantiate(pieces[randomPiece].name, transform.position, Quaternion.identity);
        currentlyControlledPiece.transform.parent = gameObject.transform;
        StartCoroutine(MovePieceDown());
    }

    private void FixedUpdate()
    {
        
    }

    IEnumerator MovePieceDown()
    {
        bool loop = true;
        while (loop)
        {
            if (currentlyControlledPiece == null)
                yield return null;

            yield return new WaitForSeconds(moveDownPause);
            currentlyControlledPiece.transform.position = new Vector3(currentlyControlledPiece.transform.position.x,
                currentlyControlledPiece.transform.position.y - increment, 0f);

            // TODO FIX THIS, DOESN'T STOP THE BLOCK FROM GOING PAST THE BOTTOM
            if (currentlyControlledPiece.transform.position.y == -8.98)
            {
                loop = false;
            }

            // We're gonna call the function on the child objects that tell us if we've hit something below us
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                if(transform.GetChild(0).GetChild(i).GetComponent<BlockCollisionChecker>().ObjectBelowCube())
                {
                    // We want to exit the loop
                    loop = false;
                }
            }
        }
        SpawnNextPiece();
    }

    void CheckIfSlotIsOccupied()
    {

    }
}
