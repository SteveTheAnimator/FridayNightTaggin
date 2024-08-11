using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Voice;
using System.Collections;

namespace FridayNightTaggin.Scripts
{
    internal class SongManager : MonoBehaviour
    {
        public List<GameObject> notes = new List<GameObject>();
        public float SongSpeed = 1;

        [PunRPC]
        public void SpawnNote(int noteID, int playerID)
        {
            GameObject obj = FNTManager.instance.bundle.LoadAsset<GameObject>("ArrowPrefab");
            GameObject arrow;
            arrow = Instantiate(obj);

            arrow.transform.position = FNTManager.instance.FNTManagerObject.transform.GetChild(playerID).transform.GetChild(noteID + 4).transform.position;
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
                Debug.Log("stink");
                GorillaTagger.Instance.myVRRig.RPC("SpawnNote", RpcTarget.All, new object[]
                {
                    "2",
                    WhatIDAmI()
                });
                yield return new WaitForSeconds(6);
                GorillaTagger.Instance.myVRRig.RPC("SpawnNote", RpcTarget.All, new object[]
                {
                    "3",
                    WhatIDAmI()
                });
                yield return new WaitForSeconds(6);
                GorillaTagger.Instance.myVRRig.RPC("SpawnNote", RpcTarget.All, new object[]
                {
                    "2",
                    WhatIDAmI()
                });
                yield return new WaitForSeconds(6);
                GorillaTagger.Instance.myVRRig.RPC("SpawnNote", RpcTarget.All, new object[]
                {
                    "4",
                    WhatIDAmI()
                });
                yield return new WaitForSeconds(6);
                GorillaTagger.Instance.myVRRig.RPC("SpawnNote", RpcTarget.All, new object[]
                {
                    "1",
                    WhatIDAmI()
                });
                yield return new WaitForSeconds(6);
                GorillaTagger.Instance.myVRRig.RPC("SpawnNote", RpcTarget.All, new object[]
                {
                    "3",
                    WhatIDAmI()
                });
                yield return new WaitForSeconds(6);
            }
        }

        public int WhatIDAmI()
        {
            int returnthis = 0;

            if(FNTManager.instance.PlayerOne)
            {
                returnthis= 0;
            }
            else
            {
                if(FNTManager.instance.PlayerTwo)
                {
                    returnthis= 1;
                }
            }

            return returnthis;
        }
    }
}
