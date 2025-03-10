using System.Collections;
using System.Collections.Generic;
using SubnauticaEnhancer;
using UnityEngine;
using SubnauticaEnhancer.ESP;
using Logger = SubnauticaEnhancer.Logger;

namespace SubnauticaEnhancer.Collectors
{
    public class DebugCollector : MonoBehaviour
    {
        public static Dictionary<string, List<Vector3>> debugObjects = new Dictionary<string, List<Vector3>>();
        private float updateInterval = 2f;
        private void Start()
        {
            Logger.Debug("DebugCollector: Start method called.");
            StartCoroutine(UpdateDebugRoutine());
        }
        
        private IEnumerator UpdateDebugRoutine()
        {
            while (true)
            {
                if (MenuEngine.enableESP)
                {
                    UpdateObjectsOfDebug();
                }
                else
                {
                    if (debugObjects.Count != 0)
                    {
                        debugObjects.Clear();
                    }
                }
                yield return new WaitForSeconds(updateInterval);
            }
        }
        
        private static void UpdateObjectsOfDebug()
        {
            debugObjects.Clear();

            if (!MenuEngine.enableESP)
            {
                return;
            }
        
            var foundObjects = FindObjectsOfType<ResourceTracker>();
            Logger.Fatal($"UpdateObjectsOfCreatureEggs[{foundObjects.Length}]");

            foreach (var selectedObject in foundObjects)
            {
                if (!selectedObject) continue;
            
                var objectPosition = selectedObject.transform.position;
                if (GameHelper.IsActiveAndWithinDistance(objectPosition, 70))
                {
                    var objectName = selectedObject.name;

                    if (!debugObjects.ContainsKey(objectName))
                    {
                        debugObjects[objectName] = new List<Vector3>();
                    }

                    debugObjects[objectName].Add(objectPosition);
                }
            }
        }
    }
}