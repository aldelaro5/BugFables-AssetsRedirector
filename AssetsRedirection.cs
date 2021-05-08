using BepInEx;
using HarmonyLib;
using System;
using System.Drawing;
using System.IO;
using UnityEngine;
using XUnity.ResourceRedirector;

namespace BugFables.AssetsRedirector
{
  public class Common
  {
    public static AssetsRedirection Plugin;
  }

  [BepInPlugin("com.aldelaro5.BugFables.plugins.AssetsRedirection", "Assets Redirector", "1.0.0")]
  [BepInProcess("Bug Fables.exe")]
  public class AssetsRedirection : BaseUnityPlugin
  {
    public void Awake()
    {
      Common.Plugin = this;
      var harmony = new Harmony("com.aldelaro5.BugFables.plugins.AssetsRedirection");
      harmony.PatchAll(typeof(EntitiesSpritesRedirector));
      harmony.PatchAll(typeof(PlayMusicRedirector));
      ResourceRedirection.EnableSyncOverAsyncAssetLoads();
      ResourceRedirection.RegisterResourceLoadedHook(HookBehaviour.OneCallbackPerResourceLoaded, AseetLoad);
    }

    private void AseetLoad(ResourceLoadedContext context)
    {
      // Only redirect sound effects, music are redirected via Harmony patch
      if (context.Asset is AudioClip && context.Parameters.Path.StartsWith("Audio\\Sounds"))
      {
        string path = Path.Combine(Path.GetDirectoryName(base.Info.Location), context.Parameters.Path + ".wav");
        if (File.Exists(path))
        {
          string name = context.Asset.name;
          context.Asset = LoadAudioClip(path);
          context.Asset.name = name;
          context.Complete();
        }
      }
      else if (context.Asset is Sprite)
      {
        string path = Path.Combine(Path.GetDirectoryName(base.Info.Location), context.Parameters.Path);
        if (File.Exists(path + ".png"))
        {
          ImageConversion.LoadImage(((Sprite)context.Asset).texture, File.ReadAllBytes(path + ".png"));
          context.Complete();
        }
      }
      else if (context.Asset is TextAsset)
      {
        string path = Path.Combine(Path.GetDirectoryName(base.Info.Location), context.Parameters.Path);
        if (File.Exists(path))
        {
          context.Asset = new TextAsset(File.ReadAllText(path));
          context.Complete();
        }
        else if (File.Exists(path + ".txt"))
        {
          context.Asset = new TextAsset(File.ReadAllText(path + ".txt"));
          context.Complete();
        }
        else if (File.Exists(path + ".bytes"))
        {
          context.Asset = new TextAsset(File.ReadAllText(path + ".bytes"));
          context.Complete();
        }
      }
      else if (context.Asset is Material)
      {
        Material mat = (Material)context.Asset;
        if (mat?.mainTexture?.name == "main1")
        {
          string path = Path.Combine(Path.GetDirectoryName(base.Info.Location), "Sprites\\Misc\\main1");
          if (File.Exists(path + ".png"))
          {
            ImageConversion.LoadImage((Texture2D)mat.mainTexture, File.ReadAllBytes(path + ".png"));
            context.Complete();
          }
        }
        if (mat?.mainTexture?.name == "main2")
        {
          string path = Path.Combine(Path.GetDirectoryName(base.Info.Location), "Sprites\\Misc\\main2");
          if (File.Exists(path + ".png"))
          {
            ImageConversion.LoadImage((Texture2D)mat.mainTexture, File.ReadAllBytes(path + ".png"));
            context.Complete();
          }
        }
      }
    }

    public static AudioClip LoadAudioClip(string path)
    {
      using (WWW www = new WWW(BepInEx.Utility.ConvertToWWWFormat(path)))
      {
        AudioClip clip = www.GetAudioClip();

        //Wait for the clip to be loaded before returning it
        while (clip.loadState != AudioDataLoadState.Loaded) { }

        return clip;
      }
    }
  }
}
