using System.Collections;
using System.Collections.Generic;
using SubnauticaEnhancer;
using UnityEngine;
using SubnauticaEnhancer.ESP;
using Logger = SubnauticaEnhancer.Logger;

namespace SubnauticaEnhancer.Collectors
{
    public class ResourceTrackerCollector : MonoBehaviour
    {
        public static Dictionary<string, List<Vector3>> breakableResourcesObjects =
            new Dictionary<string, List<Vector3>>();

        public static Dictionary<string, List<Vector3>> pickupableResourcesObjects =
            new Dictionary<string, List<Vector3>>();

        public static Dictionary<string, List<Vector3>> drillableObjects =
            new Dictionary<string, List<Vector3>>();

        public static Dictionary<string, List<Vector3>> creatureEggsObjects =
            new Dictionary<string, List<Vector3>>();

        public static Dictionary<string, List<Vector3>> scrapObjects =
            new Dictionary<string, List<Vector3>>();

        private float updateInterval = 2f;

        private static void ClearObjects(Dictionary<string, List<Vector3>> gameObjets)
        {
            if (gameObjets.Count != 0)
            {
                gameObjets.Clear();
            }
        }

        private void Start()
        {
            Logger.Debug("ResourceTrackerCollector: Start method called.");
            StartCoroutine(UpdateResourceTrackerRoutine());
        }

        private IEnumerator UpdateResourceTrackerRoutine()
        {
            while (true)
            {
                if (MenuEngine.enableESP)
                {
                    if (MenuEngine.breakableMineralsESP || MenuEngine.pickupableMineralsESP ||
                        MenuEngine.creatureEggsESP || MenuEngine.drillableMineralsESP)
                    {
                        UpdateObjectsOfResourceTracker();
                    }
                }
                else
                {
                    // Clear Objects
                    ClearObjects(breakableResourcesObjects);
                    ClearObjects(pickupableResourcesObjects);
                    ClearObjects(drillableObjects);
                    ClearObjects(creatureEggsObjects);
                }

                yield return new WaitForSeconds(updateInterval);
            }
        }

        private static void UpdateObjectsOfResourceTracker()
        {
            //Clear Resources
            ClearObjects(breakableResourcesObjects);
            ClearObjects(pickupableResourcesObjects);
            ClearObjects(drillableObjects);
            ClearObjects(creatureEggsObjects);

            if (!MenuEngine.enableESP)
            {
                return;
            }

            var foundObjects = FindObjectsOfType<ResourceTracker>();

            foreach (var selectedObject in foundObjects)
            {
                if (!selectedObject) continue;

                var objectPosition = selectedObject.transform.position;
                var getGameObject = selectedObject.gameObject;
                // MINERALS
                if (MenuEngine.breakableMineralsESP || MenuEngine.drillableMineralsESP)
                {
                    if (GameHelper.IsActiveAndWithinDistance(objectPosition, MenuEngine.mineralsMaxDistanceRender))
                    {
                        // BREAKABLE
                        if (MenuEngine.breakableMineralsESP)
                        {
                            if (getGameObject.TryGetComponent(out BreakableResource breakableResource))
                            {
                                string objectName = breakableResource.name;
                                if (MenuEngine.proximityBasedLabels)
                                {
                                    objectName = GameHelper.IsInProximity(objectPosition,
                                        MenuEngine.mineralsMaxDistanceRender)
                                        ? breakableResource.name
                                        : "Unknown";
                                }

                                if (!breakableResourcesObjects.ContainsKey(objectName))
                                {
                                    breakableResourcesObjects[objectName] = new List<Vector3>();
                                }

                                breakableResourcesObjects[objectName].Add(objectPosition);
                            }
                        }

                        // DRILABLE
                        if (MenuEngine.drillableMineralsESP)
                        {
                            if (getGameObject.TryGetComponent(out Drillable drillableResource))
                            {
                                var objectName = GameHelper.GetTechNativeName(selectedObject.overrideTechType);

                                if (MenuEngine.proximityBasedLabels)
                                {
                                    objectName = GameHelper.IsInProximity(objectPosition,
                                        MenuEngine.mineralsMaxDistanceRender)
                                        ? GameHelper.GetTechNativeName(selectedObject.overrideTechType)
                                        : "Unknown";
                                }

                                if (!drillableObjects.ContainsKey(objectName))
                                {
                                    drillableObjects[objectName] = new List<Vector3>();
                                }

                                drillableObjects[objectName].Add(objectPosition);
                            }
                        }
                        
                        // PICKUPABLE
                        
                        if (MenuEngine.pickupableMineralsESP)
                        {
                            var pickupableObject = selectedObject.pickupable;
                            if (pickupableObject != null && !getGameObject.TryGetComponent(out CreatureEgg creatureEgg) && !pickupableObject.GetTechType().ToString().Contains("scrap"))
                            {
                                var objectName = GameHelper.GetTechNativeName(pickupableObject.GetTechType());

                                if (MenuEngine.proximityBasedLabels)
                                {
                                    objectName = GameHelper.IsInProximity(objectPosition,
                                        MenuEngine.mineralsMaxDistanceRender)
                                        ? GameHelper.GetTechNativeName(pickupableObject.GetTechType())
                                        : "Unknown";
                                }

                                if (!pickupableResourcesObjects.ContainsKey(objectName))
                                {
                                    pickupableResourcesObjects[objectName] = new List<Vector3>();
                                }

                                pickupableResourcesObjects[objectName].Add(objectPosition);
                            }
                        }
                    }
                }
            }
        }
    }
}