using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon;

namespace Com.ME.MEGAME
{
    public class Launcher : PunBehaviour
    {
        #region Private Serializable Fields

        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;
        [Tooltip("The UI Label to inform the user that they are connected")]
        [SerializeField]
        private GameObject connectedLabel;

        #endregion

        #region Private Fields

        string gameVersion = "1"; //This client's version number. Users are separated from each other by gameVersion which allows you to make breaking changes

        #endregion

        #region PunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnectedFromPhoton()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

            Debug.Log("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            //Failed to join Random lobby none exist or all are full so creating a new one
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom}, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            progressLabel.SetActive(false);
            connectedLabel.SetActive(true);
        }

        #endregion

        #region MonoBehavior Callbacks

        private void Awake()
        {
            //This makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.automaticallySyncScene = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            //connect();

            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            connectedLabel.SetActive(false);

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the connection process
        /// -If already connected Join a random room
        /// -If not yet connected, connect this applicaiton instance to Photon Cloud Network
        /// </summary>
        public void connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(gameVersion);
                //PhotonNetwork.gameVersion = gameVersion;
            }
        }

        #endregion
    }
}
