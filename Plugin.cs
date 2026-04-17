using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MakerPen
{
    [BepInPlugin("org.alta.gorillatag.makerpen", "MakerPen", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            GorillaTagger.OnPlayerSpawned(new Action(this.OnGameInitialized));
        }

        private void OnGameInitialized()
        {
            GameObject gameObject = Plugin.LoadAsset("makerpen");
            Transform hand = GorillaTagger.Instance.offlineVRRig.transform.Find("rig/hand.R");

            gameObject.transform.SetParent(hand, false);

            gameObject.transform.localPosition = new Vector3(0.07f, 0.115f, 0.1f);
            gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 70f);
            gameObject.transform.localScale = Vector3.one * 0.8f;
        }
        
        public static GameObject LoadAsset(string assetName)
        {
            bool flag = Plugin.assetBundle == null;
            if (flag)
            {
                Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MakerPen.makerpen");
                Plugin.assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);
                manifestResourceStream.Close();
            }
            return UnityEngine.Object.Instantiate<GameObject>(Plugin.assetBundle.LoadAsset<GameObject>(assetName));
        }

        private static AssetBundle assetBundle;
    }
}
