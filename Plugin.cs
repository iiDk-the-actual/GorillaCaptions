using BepInEx;
using GorillaCaptions.Classes;
using GorillaTag.Audio;
using HarmonyLib;
using Photon.Voice;
using System.Reflection;
using UnityEngine;

namespace GorillaCaptions
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin instance;

        void Awake()
        {
            instance = this;
            Debug.Log("<GorillaCaptions> Created by goldentrophy <3");
        }

        void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();

            GameObject ClassHolder = new GameObject("GorillaCaptions");
            ClassHolder.AddComponent<Managers.SynthesizerManager>();
            ClassHolder.AddComponent<Managers.BubbleManager>();
            DontDestroyOnLoad(ClassHolder);

            Debug.Log("<GorillaCaptions> Fully initialized");
        }
    }
}
