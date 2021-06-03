using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BugFables.AssetsRedirector
{
  // Entities sprites are baked in the animations of them which makes them impossible
  // to redirect with the XUnity.ResourceRedirector. To workaround this, we patch the
  // spritesheet if found on the first update of the entity and keep track of the ones
  // we patched so we don't patch it again. Every so often, the list is being cleaned up
  // so the destroyed entities can be removed from it
  public class EntitiesSpritesRedirector
  {
    public static Dictionary<int, EntityControl> OverridenEntitiesSprite = new Dictionary<int, EntityControl>();

    [HarmonyPostfix]
    [HarmonyPatch(typeof(MainManager), nameof(MainManager.DoClock))]
    static void CleanupDestroyedEntities()
    {
      // Every 5 minutes
      if (MainManager.instance.clockmin % 5 == 0 && MainManager.instance.clocksec == 0)
      {
        List<int> keys = new List<int>();
        foreach (var item in OverridenEntitiesSprite)
        {
          if (item.Value == null)
            keys.Add(item.Key);
        }

        foreach (var key in keys)
          OverridenEntitiesSprite.Remove(key);
        MainManager.print("Cleaned up the overriden entities sprites dictionnary, current count: " + OverridenEntitiesSprite.Count);
      }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(EntityControl), "Update")]
    static void OverwriteSprite(EntityControl __instance)
    {
      int id = __instance.GetInstanceID();
      if (!OverridenEntitiesSprite.ContainsKey(id) && __instance?.sprite?.sprite != null)
      {
        string spriteSheetName = __instance.sprite.sprite.name.Split('_')[0];
        string path = Path.Combine(Path.GetDirectoryName(Common.Plugin.Info.Location), "Sprites\\Entities\\" + spriteSheetName);
        if (File.Exists(path + ".png"))
        {
          ImageConversion.LoadImage(__instance.sprite.sprite.texture, File.ReadAllBytes(path + ".png"));
          Vector2 standardisedPivot = new Vector2(__instance.sprite.sprite.pivot.x / __instance.sprite.sprite.rect.width, 
                                                  __instance.sprite.sprite.pivot.y / __instance.sprite.sprite.rect.height);
          Sprite newSprite = Sprite.Create(__instance.sprite.sprite.texture, __instance.sprite.sprite.rect, 
                                           standardisedPivot, __instance.sprite.sprite.pixelsPerUnit);
          newSprite.name = __instance.sprite.sprite.name;
          __instance.sprite.sprite = newSprite;
          OverridenEntitiesSprite.Add(id, __instance);
        }
      }
    }
  }
}
