using UnityEngine;
using UnityEngine.SceneManagement;

namespace SubnauticaEnhancer
{
    public class GameHelper
    {
        public static bool WorldToScreen(Vector3 worldPosition, out Vector3 screenPosition)
        {
            var mainCamera = MainCamera.camera;
            screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            var isBehind = screenPosition.z < 0;
            screenPosition.y = Screen.height - screenPosition.y;

            return !isBehind && screenPosition.x >= 0 && screenPosition.x <= Screen.width && screenPosition.y >= 0 && screenPosition.y <= Screen.height;
        }
        
        public static float GetDistanceToPosition(Vector3 position)
        {
            return Vector3.Distance(Player.main.transform.position, position);
        }
        
        public static bool IsActiveAndWithinDistance(Vector3 objectPosition, float maxDistance)
        {
            var distance = GetDistanceToPosition(objectPosition);
            return distance <= maxDistance;
        }

        public static string GetTechNativeName(TechType type)
        {
            return Language.main.Get(type);
        }
        
        public static bool IsInGame()
        {
            //return SceneManager.GetActiveScene().name == "Main" && Player.main != null && MainCamera.camera != null;
            return Player.main != null && MainCamera.camera != null;
        }
        
        public static bool IsInProximity(Vector3 target, float maxDistance)
        {
            var distance = GameHelper.GetDistanceToPosition(target);
            int minDistance = 20;
            // To Close of Target
            if (distance < minDistance)
            {
                return true;
            }
            // Adaptive in Base of Max Distance
            var percentage = (int)(100 * distance / maxDistance);

            return percentage <= 25;
        }
    }
}