using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Voice;

namespace FridayNightTaggin.Scripts
{
    internal class SongManager : MonoBehaviour
    {
        [PunRPC]
        public void SpawnNote(int noteID)
        {
            if(noteID == 0) { } // im gonna write this later
        }
    }
}
