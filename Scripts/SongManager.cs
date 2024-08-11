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

namespace FridayNightTaggin.Scripts
{
    internal class SongManager : MonoBehaviour
    {
        public List<GameObject> notes = new List<GameObject>();
        public float SongSpeed = 1;
        public FNTManager manager = null;
        public bool isInSong = false;

        [PunRPC]
        public void SpawnNote(int noteID, int playerID)
        {
            GameObject obj = manager.bundle.LoadAsset<GameObject>("ArrowPrefab");
            GameObject arrow;
            arrow = Instantiate(obj);

            arrow.transform.position = manager.FNTManagerObject.transform.GetChild(playerID).transform.GetChild(noteID + 4).transform.position;
            notes.Add(arrow);
        }

        public void Update()
        {
            foreach(GameObject note in notes) 
            {
                Vector3 v3 = new Vector3(note.transform.position.x, note.transform.position.y + SongSpeed, note.transform.position.z);
                note.transform.position = v3;
            }
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
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 2, WhatIDAmI());
                yield return new WaitForSeconds(1);
                manager.photonView.RPC("SpawnNote", RpcTarget.All, 4, WhatIDAmI());
                yield return new WaitForSeconds(1);
            }
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
