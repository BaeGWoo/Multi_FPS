using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class PhotonControl : MonoBehaviourPun , IPunObservable
{
    public float speed = 5.0f;
    public float angleSpeed;

    public LayerMask layer;
    public Camera cam;

    public int health=100;
    RaycastHit hit;

    public Texture2D cursorImage;
    private void Start()
    {

        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
        //현재 플레이어가 나 자신이라면
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }

        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    // IPunObservable 인터페이스를 구현해야한다.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(health);
        }
        else
        {
            // Network player, receive data
            this.health = (int)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(dir.normalized * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3
            (
            0,
            Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime,
            0
            );


        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Damage();

        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(ray, out hit, Mathf.Infinity,layer))
            {
                photonView.RPC("Damage", RpcTarget.All);
            }
        }
       

       
    }

    [PunRPC]
    public void Damage()
    {
        if (health <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }


        hit.transform.GetComponent<PhotonControl>().health -= 10;
    }
}
