using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionChecker : MonoBehaviour
{
    float raycastLength = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ObjectBelowCube()
    {
        // Let's raycast down the length that we'll be moving down
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - raycastLength/2), Vector3.down, raycastLength/2);

        if (hit.transform == null)
            return false;

        // First, let's check if any of the things we hit are children of the parent (or siblings)
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            // If this is the case, we don't want to do anything
            if (transform.parent.GetChild(i).gameObject == hit.transform.gameObject)
                return false;
        }

        // Otherwise, we want to stop the piece from moving
        return true;
    }


}
