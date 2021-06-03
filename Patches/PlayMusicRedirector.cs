using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BugFables.AssetsRedirector
{
  // Most music are statically refferenced in prefabs so we have to redirect them before they play
  public class PlayMusicRedirector
  {
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MainManager), nameof(MainManager.ChangeMusic), 
                  new Type[] { typeof(AudioClip), typeof(float), typeof(int), typeof(bool) })]
    static bool RedirectSongs(ref AudioClip musicclip, float fadespeed, int id, bool seamless)
    {
      if (musicclip != null)
      {
        string path = Path.Combine(Path.GetDirectoryName(Common.Plugin.Info.Location), "Audio\\Music\\" + musicclip.name + ".wav");
        if (File.Exists(path))
        {
          string name = musicclip.name;
          musicclip = Common.LoadAudioClip(path);
          musicclip.name = name;
        }
      }

      return true;
    }
  }
}
