using Photon.Voice.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using System.IO;
using System.Reflection;

namespace GorillaCaptions.Classes
{
    public class SpeechBubble : MonoBehaviour
    {
        public VRRig Owner;
        private GameObject uiTarget;
        private float spawnTime;

        void Awake()
        {
            spawnTime = Time.time;
            uiTarget = transform.Find("Canvas").gameObject;
            gameObject.transform.localPosition = Vector3.up * 0.6f;
        }
            

        void Update()
        {
            if (Time.time > spawnTime + 10f || !GorillaParent.instance.vrrigs.Contains(Owner))
                Managers.BubbleManager.instance.DestroyBubble(gameObject);
        }

        void LateUpdate() =>
            uiTarget.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
    }
}
