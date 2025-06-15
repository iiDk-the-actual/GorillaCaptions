using GorillaCaptions.Classes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GorillaCaptions.Managers
{
    public class BubbleManager : MonoBehaviour
    {
        public static BubbleManager instance { get; private set; }

        private GameObject BubbleAsset;
        public void Awake()
        {
            instance = this;
            BubbleAsset = AssetManager.LoadAsset<GameObject>("SpeechBubble");
        }

        private Dictionary<VRRig, GameObject> SpeechBubbles = new Dictionary<VRRig, GameObject> { };
        public void SpawnBubble(VRRig Rig, string Text)
        {
            if (SpeechBubbles.TryGetValue(Rig, out GameObject ExistingBubble))
                DestroyBubble(ExistingBubble);

            GameObject TargetBubble = Instantiate(BubbleAsset);
            TargetBubble.transform.SetParent(Rig.headMesh.transform, false);
            TargetBubble.transform.Find("Canvas/Background/Text")
                .GetComponent<TextMeshProUGUI>().text = Text;
            TargetBubble.AddComponent<SpeechBubble>().Owner = Rig;

            SpeechBubbles.Add(Rig, TargetBubble);
        }

        public void DestroyBubble(GameObject TargetBubble)
        {
            foreach (KeyValuePair<VRRig, GameObject> SpeechBubble in SpeechBubbles)
            {
                if (SpeechBubble.Value == TargetBubble)
                {
                    SpeechBubbles.Remove(SpeechBubble.Key);
                    Destroy(SpeechBubble.Value);
                    break;
                }
            }
        }
    }
}
