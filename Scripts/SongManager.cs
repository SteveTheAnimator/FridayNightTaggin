using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Voice;
using System.Collections;
using GorillaLocomotion;
using ExitGames.Client.Photon;
using System.Runtime.InteropServices.ComTypes;

namespace FridayNightTaggin.Scripts
{
    internal class SongManager : MonoBehaviour, IOnEventCallback
    {
        public float SongSpeed = 1;
        public FNTManager manager = null;
        public bool isInSong = false;
        private void Start() // im gonna kill myself if this shit does not work istg
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDestroy()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == 73)
            {
                object[] data = (object[])photonEvent.CustomData;
                Photon.Realtime.Player player = (Photon.Realtime.Player)data[1];
                int noteid = (int)data[0];

                GameObject obj = manager.bundle.LoadAsset<GameObject>("ArrowPrefab");
                GameObject arrow;
                arrow = Instantiate(obj);
                if (player.CustomProperties.ContainsKey("FNTIsSelected1") && player.CustomProperties.ContainsValue("true"))
                {
                    arrow.transform.position = manager.FNTManagerObject.transform.GetChild(0).transform.GetChild(noteid - 1 + 4).transform.position;
                }
                if (player.CustomProperties.ContainsKey("FNTIsSelected2") && player.CustomProperties.ContainsValue("true"))
                {
                    arrow.transform.position = manager.FNTManagerObject.transform.GetChild(1).transform.GetChild(noteid - 1 + 4).transform.position;
                }
            }
        }

        public void Update()
        {
            // sigma
        }

        public IEnumerator PlaySong(int songID)
        {
            if(songID == -1) // test song, make not accessable when mod is fully public (yes you can access this right now you stinky skid)
            {
                if(!isInSong)
                {
                    isInSong = true;
                }
                Debug.Log("stink");
                SpawnNote(1);
                yield return new WaitForSeconds(1);
                SpawnNote(2);
                yield return new WaitForSeconds(1);
                SpawnNote(3);
                yield return new WaitForSeconds(1);
                SpawnNote(1);
                yield return new WaitForSeconds(1);
                SpawnNote(2);
                yield return new WaitForSeconds(1);
                SpawnNote(3);
                yield return new WaitForSeconds(1);
                SpawnNote(1);
                yield return new WaitForSeconds(1);
                SpawnNote(2);
                yield return new WaitForSeconds(1);
                SpawnNote(3);
                yield return new WaitForSeconds(1);
                SpawnNote(1);
                yield return new WaitForSeconds(1);
                SpawnNote(2);
                yield return new WaitForSeconds(1);
                SpawnNote(3);
                yield return new WaitForSeconds(1);
                SpawnNote(1);
                yield return new WaitForSeconds(1);
                SpawnNote(2);
                yield return new WaitForSeconds(1);
                SpawnNote(3);
                yield return new WaitForSeconds(1);
            }
        }

        public void SpawnNote(int noteid)
        {
            object[] content = new object[] { noteid, PhotonNetwork.LocalPlayer};
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(73, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public int WhatIDAmI()
        {
            if (manager == null)
            {
                Debug.LogError("Manager is not assigned!");
                return 0;
            }

            int returnthis = 0;

            if (manager.PlayerOne)
            {
                returnthis = 0;
            }
            else if (manager.PlayerTwo)
            {
                returnthis = 1;
            }

            return returnthis;
        }
    }
}
