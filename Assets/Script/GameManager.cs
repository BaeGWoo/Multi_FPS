using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Character", new Vector3
            (
                Random.Range(0,5),
                1,
                Random.Range(0,5)
            ),
        Quaternion.identity
        );
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
