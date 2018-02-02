using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float distance;
    [SerializeField] private float offsetY;
    private Vector3 offset;
    private Vector3 position;
    private float rotationZ;
    private PlayerMovement _PlayerMovement;
    //moet  dit script fixe
    //moet weten of hij is  omgedraait of niet

    void Start ()
    {
        _PlayerMovement = player.GetComponent<PlayerMovement>();
    }
    void Update ()//fixe
    {
        transform.rotation = Quaternion.Euler(_PlayerMovement.Rotation());
        position = player.transform.position - (transform.forward * distance);
        //offset.x = _PlayerMovement.rotationZ() / 10;
        /*
         if (!_PlayerMovement.UpsideDown())
         {

             offset.y = -offsetY;
         }
         else
         {
             offset.y = offsetY;
         }*/
        transform.position = position;
    }
}
