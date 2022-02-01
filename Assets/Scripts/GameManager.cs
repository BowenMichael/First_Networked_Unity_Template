using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon;
using Photon.Realtime;

namespace Com.ME.MEGAME
{
    public class GameManager : PunBehaviour
    {
        #region Public Variables

        public string launcherScene;

        #endregion

        #region Poton Callbacks

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene
        /// </summary>
        public override void OnLeftRoom()
        {
            if (string.IsNullOrEmpty(launcherScene))
                SceneManager.LoadScene(launcherScene);
            else
                SceneManager.LoadScene(0);
        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
    }
}
