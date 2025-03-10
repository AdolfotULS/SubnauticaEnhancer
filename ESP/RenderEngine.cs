using System.Collections.Generic;
using SubnauticaEnhancer.Collectors;
using UnityEngine;

namespace SubnauticaEnhancer.ESP
{
    public class RenderEngine : MonoBehaviour
    {
        private Dictionary<string, Color> resourceRenderColors = new Dictionary<string, Color>
        {
            { "creatureEggs", Color.green },
        };

        private void Start()
        {
            Logger.Debug("RenderEngine: Start method called.");
        }

        void OnGUI()
        {
            if (GameHelper.IsInGame() && MenuEngine.enableESP)
            {
                /*if (MenuEngine.creatureEggsESP)
                {
                    RenderGameObject(CreatureEggCollector.creatureEggsObjects, MenuEngine.pickupableMaxDistanceRender, "EGG", Color.green);
                }*/
                if (MenuEngine.breakableMineralsESP)
                {
                    RenderGameObject(ResourceTrackerCollector.breakableResourcesObjects, MenuEngine.mineralsMaxDistanceRender, "BREAKABLE", Color.yellow);
                }

                if (MenuEngine.pickupableMineralsESP)
                {
                    RenderGameObject(ResourceTrackerCollector.pickupableResourcesObjects, MenuEngine.mineralsMaxDistanceRender, "MINERAL", Color.white);
                }

                if (MenuEngine.drillableMineralsESP)
                {
                    RenderGameObject(ResourceTrackerCollector.drillableObjects, MenuEngine.mineralsMaxDistanceRender, "DRILLABLE", Color.green);
                }

                //RenderGameObject(DebugCollector.debugObjects,  70, "DEBUG", Color.cyan);
            }
        }

        private void RenderGameObject(Dictionary<string, List<Vector3>> gameObjects, float maxDistance, string category,
            Color renderColor)
        {
            if (gameObjects == null)
            {
                return;
            }

            bool proximityLabels = MenuEngine.proximityBasedLabels;

            foreach (var kvp in gameObjects)
            {
                foreach (var position in kvp.Value)
                {
                    if (!GameHelper.IsActiveAndWithinDistance(position, maxDistance))
                    {
                        continue;
                    }

                    if (!GameHelper.WorldToScreen(position, out var screenPos))
                    {
                        continue;
                    }

                    var objectScreen = new Vector2(screenPos.x, screenPos.y);
                    int distance = (int)GameHelper.GetDistanceToPosition(position);
                    string objectName = $"[{category}] {kvp.Key} ({distance} m)";

                    if (proximityLabels && !GameHelper.IsInProximity(position, maxDistance))
                    {
                        objectName = $"[{category} | {distance} m]";
                    }

                    //Logger.Debug($"Rendering egg: {objectName} at screen position {objectScreen}");
                    Render.RenderText(objectScreen, objectName, renderColor, Color.black);
                }
            }
        }

        /*private void RenderCreatureEggs(Dictionary<string, List<Vector3>> creatureEggsObjects)
        {
            if (creatureEggsObjects == null)
            {
                Logger.Fatal("RenderCreatureEggs: No creature eggs to render.");
                return;
            }

            float maxDistance = MenuEngine.pickupableMaxDistanceRender;
            bool proximityLabels = MenuEngine.proximityBasedLabels;
            Color renderColor = resourceRenderColors["creatureEggs"];

            foreach (var kvp in creatureEggsObjects)
            {
                foreach (var position in kvp.Value)
                {
                    if (!GameHelper.IsActiveAndWithinDistance(position, maxDistance))
                    {
                        continue;
                    }

                    if (!GameHelper.WorldToScreen(position, out var screenPos))
                    {
                        continue;
                    }

                    var objectScreen = new Vector2(screenPos.x, screenPos.y);
                    int distance = (int)GameHelper.GetDistanceToPosition(position);
                    string objectName = $"[EGG] {kvp.Key} ({distance} m)";

                    if (proximityLabels && !GameHelper.IsInProximity(position, maxDistance))
                    {
                        objectName = $"[EGG | {distance} m]";
                    }

                    //Logger.Debug($"Rendering egg: {objectName} at screen position {objectScreen}");
                    Render.RenderText(objectScreen, objectName, renderColor);
                }
            }
        }*/
    }
}