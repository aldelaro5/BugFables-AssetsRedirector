using BepInEx;
using HarmonyLib;
using System;
using System.Drawing;
using System.IO;
using UnityEngine;
using XUnity.ResourceRedirector;

namespace BugFables.AssetsRedirector
{
  [BepInPlugin("com.aldelaro5.BugFables.plugins.AssetsRedirector", "Assets Redirector", "1.0.0")]
  [BepInProcess("Bug Fables.exe")]
  public class AssetsRedirection : BaseUnityPlugin
  {
    public void Awake()
    {
      Common.Plugin = this;
      var harmony = new Harmony("com.aldelaro5.BugFables.plugins.AssetsRedirector");
      harmony.PatchAll(typeof(EntitiesSpritesRedirector));
      harmony.PatchAll(typeof(PlayMusicRedirector));
      ResourceRedirection.EnableSyncOverAsyncAssetLoads();
      ResourceRedirection.RegisterResourceLoadedHook(HookBehaviour.OneCallbackPerResourceLoaded, AseetLoad);
    }

    private void AseetLoad(ResourceLoadedContext context)
    {
      // Only redirect sound effects, music are redirected via Harmony patch
      if (context.Asset is AudioClip && context.Parameters.Path.StartsWith("Audio/Sounds"))
        RedirectSoundEffect(context);
      else if (context.Asset is Sprite)
        RedirectSprites(context);
      else if (context.Asset is TextAsset)
        RedirectTextAsset(context);
      else if (context.Asset is Material)
        RedirectMaterialTexture(context);
    }

    private void RedirectMaterialTexture(ResourceLoadedContext context)
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

    private void RedirectTextAsset(ResourceLoadedContext context)
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

    private void RedirectSprites(ResourceLoadedContext context)
    {
      string path = Path.Combine(Path.GetDirectoryName(base.Info.Location), context.Parameters.Path);
      if (File.Exists(path + ".png"))
      {
        Sprite ogSprite = (Sprite)context.Asset;
        ImageConversion.LoadImage(ogSprite.texture, File.ReadAllBytes(path + ".png"));
        Vector2 standardisedPivot = new Vector2(ogSprite.pivot.x / ogSprite.rect.width, ogSprite.pivot.y / ogSprite.rect.height);
        Sprite newSprite = Sprite.Create(ogSprite.texture, ogSprite.rect, standardisedPivot, ogSprite.pixelsPerUnit);
        newSprite.name = ogSprite.name;
        context.Asset = newSprite;
        context.Complete();
      }
    }

    private void RedirectSoundEffect(ResourceLoadedContext context)
    {
      string path = Path.Combine(Path.GetDirectoryName(base.Info.Location), context.Parameters.Path);
      if (File.Exists(path + ".wav"))
      {
        string name = context.Asset.name;
        context.Asset = Common.LoadAudioClip(path + ".wav");
        context.Asset.name = name;
        context.Complete();
      }
      else if (File.Exists(path + ".ogg"))
      {
        string name = context.Asset.name;
        context.Asset = Common.LoadAudioClip(path + ".ogg");
        context.Asset.name = name;
        context.Complete();
      }
    }
  }
}
