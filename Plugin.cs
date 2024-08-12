using BepInEx;
using Cinemachine;
using ExitGames.Client.Photon;
using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;
using static NetworkSystem;

namespace FridayNightTaggin
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public bool inModdedRoom;
        public AssetBundle bundle = null;
        public GameObject FNTManager = null;
        public Material UnTakenMaterial = null;
        public Material TakenMaterial = null;
        public bool isPlayerOne = false;
        public bool isPlayerTwo = false;
        public CinemachineVirtualCamera Cinemachinecamera;
        public Camera ThirdPersonCamera = null;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            bundle = LoadAssetBundle("FridayNightTaggin.Assets.fridaynighttaggin");
            GameObject fntobject = bundle.LoadAsset<GameObject>("FNTManager");
            UnTakenMaterial = bundle.LoadAsset<Material>("SelectionMateral");
            TakenMaterial = bundle.LoadAsset<Material>("TakenMaterial");
            FNTManager = Instantiate(fntobject);
            FNTManager.SetActive(false);
            FNTManager.transform.position = new Vector3(-62.645f, 3.9253f, -68.426f);
            FNTManager.transform.rotation = Quaternion.Euler(0f, 5.66356087f, 0f);
            FNTManager.AddComponent<Scripts.FNTManager>();
            FNTManager.AddComponent<Scripts.SongManager>();
            FNTManager.GetComponent<Scripts.FNTManager>().FNTManagerObject = FNTManager;
            FNTManager.GetComponent<Scripts.FNTManager>().songManager = FNTManager.GetComponent<Scripts.SongManager>();
            FNTManager.GetComponent<Scripts.SongManager>().manager = FNTManager.GetComponent<Scripts.FNTManager>();
            FNTManager.GetComponent<Scripts.FNTManager>().bundle = bundle;
            FNTManager.GetComponent<Scripts.FNTManager>().photonView = FNTManager.AddComponent<PhotonView>();
            FNTManager.GetComponent<Scripts.FNTManager>().photonView.ViewID = 128;
            try
            {
                ThirdPersonCamera = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera")?.GetComponent<Camera>();
            }
            catch (Exception)
            {
                ThirdPersonCamera = GameObject.Find("Shoulder Camera")?.GetComponent<Camera>();
            }
        }
        AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        void Update()
        {
            /* Null Checking */
            Debug.Log(FNTManager == null ? "FNTManager is null" : "FNTManager is not null");
            Debug.Log(ThirdPersonCamera == null ? "ThirdPersonCamera is null" : "ThirdPersonCamera is not null");
            Debug.Log(Cinemachinecamera == null ? "Cinemachinecamera is null" : "Cinemachinecamera is not null");
            Debug.Log(FNTManager.GetComponent<Scripts.FNTManager>() == null ? "FNTManager is null" : "FNTManager is not null"); // MYY WAYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY

            /* Networked Stuff */
            if (inModdedRoom)
            {
                if (!isPlayerTwo)
                {
                    if (!HasAPlayerSelected(1))
                    {
                        if (Vector3.Distance(GorillaLocomotion.Player.Instance.transform.position, FNTManager.transform.GetChild(0).transform.position) < 3f)
                        {
                            Vector3 v3 = FNTManager.transform.GetChild(0).transform.position;
                            v3.y += 1;
                            GorillaLocomotion.Player.Instance.transform.position = v3;

                            isPlayerOne = true;

                            FNTManager.transform.GetChild(0).GetComponent<Renderer>().material = TakenMaterial;

                            FNTManager.GetComponent<Scripts.FNTManager>().PlayerOne = true;

                            ThirdPersonCamera.gameObject.transform.parent = FNTManager.transform.GetChild(2).GetChild(2).transform;
                            ThirdPersonCamera.gameObject.transform.localPosition = Vector3.zero;
                            ThirdPersonCamera.gameObject.transform.rotation = Quaternion.identity;

                            if (Cinemachinecamera != null)
                            {
                                Cinemachinecamera.enabled = false;
                            }
                            else
                            {
                                Cinemachinecamera = ThirdPersonCamera.gameObject.transform.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
                                if (Cinemachinecamera != null)
                                {
                                    Cinemachinecamera.enabled = false;
                                }
                            }

                            Hashtable hash = new Hashtable();
                            hash.Add("FNTIsSelected1", "true");
                            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                        }
                    }
                    else
                    {
                        FNTManager.transform.GetChild(0).GetComponent<Renderer>().material = TakenMaterial;
                    }
                }
                if (!isPlayerOne)
                {
                    if (!HasAPlayerSelected(2))
                    {
                        if (Vector3.Distance(GorillaLocomotion.Player.Instance.transform.position, FNTManager.transform.GetChild(1).transform.position) < 3f)
                        {
                            Vector3 v3 = FNTManager.transform.GetChild(1).transform.position;
                            v3.y += 1;
                            GorillaLocomotion.Player.Instance.transform.position = v3;

                            isPlayerTwo = true;

                            FNTManager.transform.GetChild(1).GetComponent<Renderer>().material = TakenMaterial;

                            FNTManager.GetComponent<Scripts.FNTManager>().PlayerTwo = true;

                            ThirdPersonCamera.gameObject.transform.parent = FNTManager.transform.GetChild(2).GetChild(2).transform;
                            ThirdPersonCamera.gameObject.transform.localPosition = Vector3.zero;
                            ThirdPersonCamera.gameObject.transform.rotation = Quaternion.identity;

                            if (Cinemachinecamera != null)
                            {
                                Cinemachinecamera.enabled = false;
                            }
                            else
                            {
                                Cinemachinecamera = ThirdPersonCamera.gameObject.transform.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
                                if (Cinemachinecamera != null)
                                {
                                    Cinemachinecamera.enabled = false;
                                }
                            }

                            Hashtable hash = new Hashtable();
                            hash.Add("FNTIsSelected2", "true");
                            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                        }
                    }
                    else
                    {
                        FNTManager.transform.GetChild(1).GetComponent<Renderer>().material = TakenMaterial;
                    }
                }
            }
            if(inModdedRoom)
            {
                if(isPlayerOne)
                {
                    Vector3 v3 = FNTManager.transform.GetChild(0).transform.position;
                    v3.y += 1;
                    GorillaLocomotion.Player.Instance.transform.position = v3;

                    ThirdPersonCamera.gameObject.transform.position = FNTManager.transform.GetChild(2).GetChild(2).transform.position;
                    ThirdPersonCamera.gameObject.transform.rotation = FNTManager.transform.GetChild(2).GetChild(2).transform.rotation;

                    if (Cinemachinecamera != null)
                    {
                        Cinemachinecamera.enabled = false;
                    }
                    else
                    {
                        Cinemachinecamera = ThirdPersonCamera.gameObject.transform.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
                        if (Cinemachinecamera != null)
                        {
                            Cinemachinecamera.enabled = false;
                        }
                    }
                    if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                    {if (FNTManager.GetComponent<Scripts.FNTManager>().songManager != null)
                        {
                            if (!FNTManager.GetComponent<Scripts.FNTManager>().songManager.isInSong)
                            { GorillaTagger.Instance.StartCoroutine(FNTManager.GetComponent<Scripts.FNTManager>().songManager.PlaySong(-1)); }
                        }
                        if (!FNTManager.GetComponent<Scripts.FNTManager>().songManager.isInSong)
                        { GorillaTagger.Instance.StartCoroutine(FNTManager.GetComponent<Scripts.FNTManager>().songManager.PlaySong(-1)); }}
                }
                if(isPlayerTwo)
                {
                    Vector3 v3 = FNTManager.transform.GetChild(1).transform.position;
                    v3.y += 1;
                    GorillaLocomotion.Player.Instance.transform.position = v3;

                    ThirdPersonCamera.gameObject.transform.position = FNTManager.transform.GetChild(2).GetChild(2).transform.position;
                    ThirdPersonCamera.gameObject.transform.rotation = FNTManager.transform.GetChild(2).GetChild(2).transform.rotation;

                    if (Cinemachinecamera != null)
                    {
                        Cinemachinecamera.enabled = false;
                    }
                    else
                    {
                        Cinemachinecamera = ThirdPersonCamera.gameObject.transform.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
                        if (Cinemachinecamera != null)
                        {
                            Cinemachinecamera.enabled = false;
                        }
                    }
                    if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                    {
                        if (FNTManager.GetComponent<Scripts.FNTManager>().songManager != null)
                        {
                            if (!FNTManager.GetComponent<Scripts.FNTManager>().songManager.isInSong)
                            { GorillaTagger.Instance.StartCoroutine(FNTManager.GetComponent<Scripts.FNTManager>().songManager.PlaySong(-1)); }
                        }
                        if (!FNTManager.GetComponent<Scripts.FNTManager>().songManager.isInSong)
                        { GorillaTagger.Instance.StartCoroutine(FNTManager.GetComponent<Scripts.FNTManager>().songManager.PlaySong(-1)); }
                    }
                }
            }

            /* Fixed Modded Lobbies */
            if(PhotonNetwork.InRoom && GorillaComputer.instance.currentGameMode.Value.Contains("MODDED"))
            {
                if(!inModdedRoom)
                {
                    OnJoinModdedRoom();
                    inModdedRoom = true;
                }
            }
            else
            {
                if (inModdedRoom)
                {
                    OnLeftModdedRoom();
                    inModdedRoom = false;
                }
            }
        }

        void OnJoinModdedRoom()
        {
            FNTManager.SetActive(true);
        }
        void OnLeftModdedRoom()
        {
            FNTManager.SetActive(false);
        }

        bool HasAPlayerSelected(float one)
        {
            bool isreal = false;
            Player[] playerList = PhotonNetwork.PlayerList;
            string whattocheck = "FNTIsSelected" + one;
            foreach (Player obj in playerList)
            {
                if(obj.CustomProperties.ContainsKey(whattocheck) && obj.CustomProperties.ContainsValue("true"))
                {
                    isreal = true;
                    break;
                }
                else
                {
                    isreal = false;
                }
            }
            return isreal;
        }
    }
}
