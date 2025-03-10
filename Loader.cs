using System;
using System.Text;
using SubnauticaEnhancer.Collectors;
using SubnauticaEnhancer.ESP;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;
using Logger = SubnauticaEnhancer.Logger;

namespace SubnauticaEnhancer
{
    public class Loader : MonoBehaviour
    {
        public static float debugVersion = 1.0f;
        
        internal static GameObject subnauticaEnhancer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Load()
        {
            if (subnauticaEnhancer != null)
            {
                Logger.Debug("Loader: subnauticaEnhancer already loaded.");
                return;
            }

            Logger.Initialize(Logger.LogLevel.DEBUG);
            Logger.Debug("Logger initialized.");

            string objectName = GenerateRandomString(8);
            subnauticaEnhancer = new GameObject(objectName);
            
            Logger.Debug($"Created GameObject: [{objectName}]");
            subnauticaEnhancer.AddComponent<MenuEngine>();
            subnauticaEnhancer.AddComponent<RenderEngine>();
            //subnauticaEnhancer.AddComponent<DebugCollector>();
            subnauticaEnhancer.AddComponent<ResourceTrackerCollector>();
            Object.DontDestroyOnLoad(subnauticaEnhancer);
            
            Logger.Debug("Mod initialized and console is open.");
        }
        
        public static void Unload()
        {
            if (subnauticaEnhancer != null)
            {
                Object.Destroy(subnauticaEnhancer);
                subnauticaEnhancer = null;
                Logger.Info("Mod unloaded.");
                Logger.CloseConsole();
            }
            else
            {
                Logger.Debug("Loader: subnauticaEnhancer was not loaded.");
            }
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
    }
}
