using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BugFables.AssetsRedirector
{
  public class Common
  {
    public static AssetsRedirection Plugin;

    public static AudioClip LoadAudioClip(string path)
    {
      using (WWW www = new WWW(BepInEx.Utility.ConvertToWWWFormat(path)))
      {
        AudioClip clip = www.GetAudioClip();

        // Wait for the clip to be loaded before returning it
        while (clip.loadState != AudioDataLoadState.Loaded) { }

        return clip;
      }
    }
  }
}
