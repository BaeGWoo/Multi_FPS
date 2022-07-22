using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Photon.Voice;
using Photon.Pun;
using Photon.Realtime;


public class Billboard : MonoBehaviourPun
{

    public Text nickName;
    // Start is called before the first frame update
    void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
