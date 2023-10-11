using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering;

public class LobbyUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;
    [SerializeField] private RoomButton _room;
    private Dictionary<string, RoomButton> roomDict = new Dictionary<string, RoomButton>();

    public void OnClickCreateRoom()
    {
        NetworkManager.instance.CreateRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RoomButton desRoom = null;
        foreach (RoomInfo info in roomList)
        {
            Debug.Log(info);
            Debug.Log(info.CustomProperties["RoomState"].ToString());
            if (info.RemovedFromList)
            {
                roomDict.TryGetValue(info.Name, out desRoom);
                Destroy(desRoom);
                roomDict.Remove(info.Name);
            }
            else
            {
                if (!roomDict.ContainsKey(info.Name) && info.CustomProperties["RoomState"].ToString() == "Waiting")
                {
                    RoomButton room = Instantiate(_room, _content);
                    room.RoomName = info.Name;
                    room.onClick.AddListener(OnClickJoinRoom);
                    roomDict.Add(info.Name, room);
                }
                else if (roomDict.ContainsKey(info.Name) && info.CustomProperties["RoomState"].ToString() == "Playing")
                {
                    roomDict.TryGetValue(info.Name, out desRoom);
                    Destroy(desRoom);
                    roomDict.Remove(info.Name);
                }
            }
        }
    }

    public void OnClickJoinRoom()
    {
        NetworkManager.instance.JoinRoom();
    }
}
