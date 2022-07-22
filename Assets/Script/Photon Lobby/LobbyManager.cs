using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField RoomName, RoomPerson;
    public Button RoomCreate, RoomJoin;

    public GameObject RoomPrefab;
    public Transform RoomContent;

    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomName.text.Length > 0)
            RoomJoin.interactable = true;
        else
            RoomJoin.interactable = false;

        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
            RoomCreate.interactable = true;
        else
            RoomCreate.interactable = false;
    }

    public void OnClickCreateRoom()
    {
        RoomOptions Room = new RoomOptions();

        Room.MaxPlayers = byte.Parse(RoomPerson.text);

        Room.IsOpen = true;

        Room.IsVisible = true;

        PhotonNetwork.CreateRoom(RoomName.text, Room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    public void AllDelteRoom()
    {
        //Transform 오브젝트에 있는 하위 오브젝트에 접근하여 전체 삭제를 시도합니다.
        foreach(Transform trans in RoomContent)
        {
            //transform이 가지고 있는 게임 오브젝트를 삭제합니다.
            Destroy(trans.gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        foreach(RoomInfo info in RoomCatalog.Values)
        {
            GameObject room = Instantiate(RoomPrefab);

            room.transform.SetParent(RoomContent);

            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    void UpdateRoom(List<RoomInfo>roomList)
    {
        for(int i=0;i<roomList.Count;i++)
        {
            if(RoomCatalog.ContainsKey(roomList[i].Name))
            {
                if(roomList[i].RemovedFromList)
                {
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }

            RoomCatalog[roomList[i].Name] = roomList[i];
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRoom Filed {returnCode}:{message}");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDelteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }
}
