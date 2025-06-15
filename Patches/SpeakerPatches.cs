using GorillaCaptions.Classes;
using GorillaTag.Audio;
using HarmonyLib;
using Photon.Voice;
using Photon.Voice.Unity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GorillaCaptions.Patches
{
    [HarmonyPatch(typeof(Speaker))]
    [HarmonyPatch("StartPlaying", MethodType.Normal)]
    public class OnSpeakerStart
    {
        static void Postfix(Speaker __instance)
        {
            if (__instance.gameObject.GetComponent<SpeakerHook>() != null)
                return;

            __instance.gameObject.AddComponent<SpeakerHook>();
        }
    }

    [HarmonyPatch(typeof(Speaker))]
    [HarmonyPatch("StopPlaying", MethodType.Normal)]
    public class OnSpeakerEnd
    {
        static void Postfix(Speaker __instance, bool force)
        {
            if (__instance.gameObject.GetComponent<SpeakerHook>() == null)
                return;

            SpeakerHook SpeakerHookInstance = __instance.gameObject.GetComponent<SpeakerHook>();
            SpeakerHookInstance.enabled = false;
            UnityEngine.Object.Destroy(SpeakerHookInstance);
        }
    }

    /*
        Harmony is fucking retarded.
        As of 6/14 at 7:02 PM, I have spent over 3 HOURS trying to simply patch OnAudioFrame.
        I was confused on why nothing was logging. It wasn't running.
        Turns out, if you use Debug.Log, the patch immediately cancels out. I don't know why.
        I'm going to end my life.
    */

    [HarmonyPatch(typeof(GTSpeaker))]
    [HarmonyPatch("OnAudioFrame", MethodType.Normal)]
    public class OnAudioFramePatch
    {
        static void Postfix(GTSpeaker __instance, FrameOut<float> frame)
        {
            if (__instance.gameObject.GetComponent<SpeakerHook>() == null)
                return;

            __instance.gameObject.GetComponent<SpeakerHook>().ProcessAudioFrame(frame.Buf);
        }
    }
}
