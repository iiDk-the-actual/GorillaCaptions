using Photon.Voice.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace GorillaCaptions.Managers
{
    public class SynthesizerManager : MonoBehaviour
    {
        public static SynthesizerManager instance { get; private set; }
        public static string voskPath;

        public void Awake()
        {
            instance = this;

            string resourcePath = "GorillaCaptions.Resources.QuickVosk.exe";
            voskPath = Path.Combine(Path.GetTempPath(), "QuickVosk.exe");

            if (File.Exists(voskPath))
                File.Delete(voskPath);

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            using FileStream fs = new FileStream(voskPath, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fs);
        }

        public async Task<string> SynthesizeAudio(string audioPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = voskPath,
                Arguments = audioPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using Process proc = new Process { StartInfo = psi };
            proc.Start();

            string output = await proc.StandardOutput.ReadToEndAsync();
            await Task.Run(() => proc.WaitForExit());
            
            string[] lines = output.Split("\n");
            string finalText = lines[lines.Length - 2];

            if (finalText.Length <= 1)
                return null;

            finalText = char.ToUpper(finalText[0]) + finalText.Substring(1);

            return finalText;
        }
    }
}
