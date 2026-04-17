using HarmonyLib;
using System;
using System.Reflection;

namespace MakerPen
{
    public class HarmonyPatches
    {
        public static bool IsPatched { get; private set; }

        internal static void ApplyHarmonyPatches()
        {
            bool flag = !HarmonyPatches.IsPatched;
            if (flag)
            {
                bool flag2 = HarmonyPatches.instance == null;
                if (flag2)
                {
                    HarmonyPatches.instance = new Harmony("org.alta.gorillatag.makerpen");
                }
                HarmonyPatches.instance.PatchAll(Assembly.GetExecutingAssembly());
                HarmonyPatches.IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            bool flag = HarmonyPatches.instance != null && HarmonyPatches.IsPatched;
            if (flag)
            {
                HarmonyPatches.instance.UnpatchSelf();
                HarmonyPatches.IsPatched = false;
            }
        }

        private static Harmony instance;
        public const string InstanceId = "org.alta.gorillatag.makerpen";
    }
}
