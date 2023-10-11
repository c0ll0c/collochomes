using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Text;
using ExitGames.Client.Photon;
using Photon.Pun.Demo.Cockpit;


public class PlayerData
{
    public string Nickname { get; set; }
    public int PlayerID { get; set; }
    //public bool IsReady { get; set; }
    public string PlayerStatus { get; set; }
    public float Speed { get; set; }
    public string PlayerCode { get; set; }
    public bool Vaccinated { get; set; }
}


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public bool isJoinRoom = false;

    public static NetworkManager s_instance;
    public static NetworkManager instance
    {
        get { return s_instance; }
    }

    // Photon View 컴포넌트
    public PhotonView PV;

    // 방에 들어올 수 있는 최대 플레이어 수
    private const int MaxPlayers = 6;

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    public void Connect(string nickname)
    {
        // 마스터 서버에 연결
        Debug.Log("Connect");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        SceneManager.LoadScene("LobbyScene");
    }

    public void CreateRoom()
    {
        // 방 이름을 설정하고 방에 참가 요청
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = (byte)MaxPlayers; // 최대 플레이어 수 설정
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "RoomState" };
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "RoomState", "Waiting" } };
        PhotonNetwork.CreateRoom("게임방", roomOptions, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("게임방");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties["RoomState"].ToString() == "Playing")
        {
            Debug.Log("Already Start");
            ExitRoom();
        }
        else
        {
            Debug.Log("Join Room");
            SceneManager.LoadScene("ReadyScene");
        }
    }

    private void CheckAllPlayersReady()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object isReady;
            if (player.CustomProperties.TryGetValue("isReady", out isReady))
            {
                if ((bool)isReady == false)
                {
                    return; // 한 명 이상의 플레이어가 아직 준비되지 않았으므로 리턴
                }
            }
            else
            {
                return; // 한 명 이상의 플레이어가 아직 준비 상태를 설정하지 않았으므로 리턴
            }
        }
        // 모든 플레이어가 준비되었으므로 게임을 시작
        if (PhotonNetwork.PlayerList.Length >= 1)
        {
            PlayerReady();
        }
    }
    public void PlayerReady()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add("RoomState", "Playing");
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }

        // 속성 업데이트를 기다린 후에 레벨을 로드
        StartCoroutine(LoadLevelAfterPropertiesUpdate());
    }
    
    private IEnumerator LoadLevelAfterPropertiesUpdate()
    {
        bool roomStateUpdated = false;

        // RoomState가 "Playing"으로 업데이트될 때까지 대기
        while (!roomStateUpdated)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomState", out object roomState))
            {
                if (roomState.ToString() == "Playing")
                {
                    roomStateUpdated = true;
                }
            }

            yield return null;
        }
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["RoomState"].ToString());
        // 업데이트 후 레벨 로드
        PhotonNetwork.LoadLevel("PlayScene");
    }

    public void ExitRoom()
    {
        // 방을 떠남
        isJoinRoom = false;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("LobbyScene");
    }

    public override void OnLeftRoom()
    {
        // 방을 떠난 후 실행할 코드를 여기에 작성합니다.
        // 예를 들어 메인 화면으로 씬을 전환하거나 필요한 초기화 작업을 수행할 수 있습니다.
        Destroy(gameObject);
        SceneManager.LoadScene("LobbyScene");
    }



    //--------------------------------- 게임 중 변경 properties

    public void SetPlayerReady(bool isReady)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("isReady", isReady);
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    public void SetPlayerStatus(string PlayerStatus)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("PlayerStatus", PlayerStatus);
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    public void SetPlayerCode(string code)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("PlayerCode", code);
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    public void SetPlayerSpeed(float Speed)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("Speed", Speed);
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    public void SetVaccinated(bool Vaccinated)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("Vaccinated", Vaccinated);
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    public void SetVirus(int virus)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("virusNo", virus);
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("PlayerStatus"))
        {
            UpdateInfo();
        }
    }

    private void UpdateInfo()
    {
        PhotonView[] playersInfo = UnityEngine.Object.FindObjectsOfType<PhotonView>();

        foreach (PhotonView p in playersInfo)
        {
            if (p.name == "NetworkManager" || p.name == "detox" || p.name == "detoxLayer2") continue;
            GameObject gamePlayer = p.gameObject;
            if (p.Owner.CustomProperties["PlayerStatus"] == null) return;
            gamePlayer.tag = (string)p.Owner.CustomProperties["PlayerStatus"];
        }

        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();
        foreach (PlayerData p in currentPlayersStatus)
        {
            Debug.Log(p.PlayerID + " " + p.Nickname + " " + p.PlayerStatus);
        }
    }

    public List<PlayerData> GetPlayersStatus()
    {
        List<PlayerData> playersStatus = new List<PlayerData>();

        // Check if CurrentRoom is not null
        if (PhotonNetwork.CurrentRoom != null)
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                // Check if player is not null
                if (player != null)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.Nickname = player.NickName;
                    playerData.PlayerID = player.ActorNumber;

                    // Check if CustomProperties is not null
                    /*
                    if (player.CustomProperties != null && player.CustomProperties.ContainsKey("isReady"))
                    {
                        playerData.IsReady = (bool)player.CustomProperties["isReady"];
                    }
                    else
                    {
                        playerData.IsReady = false;
                    }
                    */

                    if (player.CustomProperties != null && player.CustomProperties.ContainsKey("PlayerStatus"))
                    {
                        playerData.PlayerStatus = (string)player.CustomProperties["PlayerStatus"];
                    }
                    else
                    {
                        playerData.PlayerStatus = "Player";
                    }

                    if (player.CustomProperties.ContainsKey("Speed"))
                    {
                        playerData.Speed = (float)player.CustomProperties["Speed"];
                    }
                    else
                    {
                        playerData.Speed = 3;
                    }

                    if (player.CustomProperties != null && player.CustomProperties.ContainsKey("PlayerCode"))
                    {
                        playerData.PlayerCode = (string)player.CustomProperties["PlayerCode"];
                    }
                    else
                    {
                        playerData.PlayerCode = "00000"; // 기본값 설정
                    }

                    if (player.CustomProperties != null && player.CustomProperties.ContainsKey("Vaccinated"))
                    {
                        playerData.Vaccinated = (bool)player.CustomProperties["Vaccinated"];
                    }
                    else
                    {
                        playerData.Vaccinated = false; // 기본값 설정
                    }


                    playersStatus.Add(playerData);
                }
            }
        }

        return playersStatus;
    }

    public PlayerData GetMyStatus()
    {
        PlayerData myData = new PlayerData();

        // 현재 로컬 플레이어의 상태를 가져옵니다.
        Photon.Realtime.Player localPlayer = PhotonNetwork.LocalPlayer;

        // PlayerData 인스턴스에 닉네임과 ID를 설정합니다.
        myData.Nickname = localPlayer.NickName;
        myData.PlayerID = localPlayer.ActorNumber;

        /*
        // 플레이어의 "isReady" 상태를 확인하고 설정합니다.
        if (localPlayer.CustomProperties.ContainsKey("isReady"))
        {
            myData.IsReady = (bool)localPlayer.CustomProperties["isReady"];
        }
        else
        {
            myData.IsReady = false;
        }
        */

        if (localPlayer.CustomProperties.ContainsKey("PlayerStatus"))
        {
            myData.PlayerStatus = (string)localPlayer.CustomProperties["PlayerStatus"];
        }
        else
        {
            myData.PlayerStatus = "Player";
        }

        if (localPlayer.CustomProperties.ContainsKey("Speed"))
        {
            myData.Speed = (float)localPlayer.CustomProperties["Speed"];
        }
        else
        {
            myData.Speed = 3;
        }

        if (localPlayer.CustomProperties.ContainsKey("PlayerCode"))
        {
            myData.PlayerCode = (string)localPlayer.CustomProperties["PlayerCode"];
        }
        else
        {
            myData.PlayerCode = "000AA"; // 기본값 설정
        }

        if (localPlayer.CustomProperties.ContainsKey("Vaccinated"))
        {
            myData.Vaccinated = (bool)localPlayer.CustomProperties["Vaccinated"];
        }
        else
        {
            myData.Vaccinated = false; // 기본값 설정
        }


        return myData;
    }

}

