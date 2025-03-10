/*using System.Collections;
using System.Collections.Generic;
using SubnauticaEnhancer;
using UnityEngine;
using SubnauticaEnhancer.ESP;
using Logger = SubnauticaEnhancer.Logger;

namespace SubnauticaEnhancer.Collectors
{
    public class CreatureEggCollector : MonoBehaviour
    {
        public static Dictionary<string, List<Vector3>> creatureEggsObjects = new Dictionary<string, List<Vector3>>();
        private float updateInterval = 2f;

        private void Start()
        {
            Logger.Debug("CreatureEggCollector: Start method called.");
            StartCoroutine(UpdateCreatureEggsRoutine());
        }

        private static TechType GetEggTechType(CreatureEgg egg)
        {
            if (egg.gameObject.TryGetComponent(out Pickupable pickupableResource))
            {
                return pickupableResource.GetTechType();
            }

            return TechType.None;
        }

        private IEnumerator UpdateCreatureEggsRoutine()
        {
            while (true)
            {
                if (MenuEngine.creatureEggsESP && MenuEngine.enableESP)
                {
                    UpdateObjectsOfCreatureEggs();
                }
                else
                {
                    if (creatureEggsObjects.Count != 0)
                    {
                        creatureEggsObjects.Clear();
                    }
                }

                yield return new WaitForSeconds(updateInterval);
            }
        }

        public static void UpdateObjectsOfCreatureEggs()
        {
            creatureEggsObjects.Clear();

            if (!MenuEngine.enableESP || !MenuEngine.creatureEggsESP)
            {
                return;
            }

            var foundObjects = FindObjectsOfType<CreatureEgg>();
            Logger.Fatal($"UpdateObjectsOfCreatureEggs[{foundObjects.Length}]");

            foreach (var selectedObject in foundObjects)
            {
                if (!selectedObject) continue;

                var objectPosition = selectedObject.transform.position;
                if (GameHelper.IsActiveAndWithinDistance(objectPosition, MenuEngine.pickupableMaxDistanceRender))
                {
                    var techType = GetEggTechType(selectedObject);
                    var objectName = GameHelper.GetTechNativeName(techType);

                    if (MenuEngine.proximityBasedLabels)
                    {
                        objectName = GameHelper.IsInProximity(objectPosition, MenuEngine.pickupableMaxDistanceRender)
                            ? GameHelper.GetTechNativeName(techType)
                            : "Unknown";
                    }

                    if (!creatureEggsObjects.ContainsKey(objectName))
                    {
                        creatureEggsObjects[objectName] = new List<Vector3>();
                    }

                    creatureEggsObjects[objectName].Add(objectPosition);
                }
            }
        }
    }
}*/