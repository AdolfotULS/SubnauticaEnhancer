using System;
using System.Collections.Generic;
using SubnauticaEnhancer.ESP;
using UnityEngine;
using Logger = SubnauticaEnhancer.Logger;

namespace SubnauticaEnhancer
{
    public class MenuEngine : MonoBehaviour
    {
        private bool showMenu = false;
        private Rect windowRect = new Rect(20, 20, 600, 400);

        private enum MenuState
        {
            ESP,
            ESP_Minerals,
            ESP_Plants,
            ESP_World,
            ESP_Others,
            ESP_Settings,
            VERSION_INFO,
            AIMBOT,
            EXPLOITS
        }

        private MenuState currentMenu = MenuState.ESP;

        private void Start()
        {
            Logger.Debug("MenuEngine: Start method called.");
        }

        private void Update()
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.Insert))
                {
                    if (GameHelper.IsInGame())
                    {
                        showMenu = !showMenu;
                        Logger.Info($"MenuEngine: ShowMenu toggled to {showMenu}.");
                    }
                    else
                    {
                        showMenu = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error($"MenuEngine: Error in Update method - {ex.Message}");
            }
        }

        private void OnGUI()
        {
            try
            {
                if (showMenu)
                {
                    windowRect = GUILayout.Window(0, windowRect, DrawWindow, "Subnautica | SheratoDex");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error($"MenuEngine: Error in OnGUI method - {ex.Message}");
            }
        }

        private void DrawWindow(int windowID)
        {
            try
            {
                GUILayout.BeginHorizontal();

                // CATEGORIES
                GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(150));
                if (GUILayout.Button("Aimbot")) currentMenu = MenuState.AIMBOT;
                if (GUILayout.Button("ESP")) currentMenu = MenuState.ESP;
                if (GUILayout.Button("Exploits")) currentMenu = MenuState.EXPLOITS;
                if (GUILayout.Button("VERSION - INFO")) currentMenu = MenuState.VERSION_INFO;
                GUILayout.EndVertical();

                // SUB-CATEGORIES and CONTENT
                GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandHeight(true));
                switch (currentMenu)
                {
                    case MenuState.ESP:
                        RenderESPMenu();
                        break;
                    case MenuState.ESP_Minerals:
                        RenderMineralsMenu();
                        break;
                    case MenuState.ESP_Plants:
                        RenderPlantsMenu();
                        break;
                    case MenuState.ESP_World:
                        RenderWorldMenu();
                        break;
                    case MenuState.ESP_Others:
                        RenderOthersMenu();
                        break;
                    case MenuState.ESP_Settings:
                        RenderSettingsMenu();
                        break;
                    case MenuState.VERSION_INFO:
                        GUILayout.Label("VERSION - INFO Menu");
                        break;
                    case MenuState.AIMBOT:
                        GUILayout.Label("AIMBOT Menu");
                        break;
                    case MenuState.EXPLOITS:
                        GUILayout.Label("EXPLOITS Menu");
                        break;
                    default:
                        Logger.Error("MenuEngine: Current Tab Menu Error.");
                        break;
                }

                GUILayout.EndVertical();

                GUILayout.EndHorizontal();

                GUI.DragWindow();
            }
            catch (System.Exception ex)
            {
                Logger.Error($"MenuEngine: Error in DrawWindow method - {ex.Message}");
            }
        }

        public static bool enableESP = false;

        // ESP Features
        public static bool proximityBasedLabels = false;

        // MINERALS
        public static float mineralsMaxDistanceRender = 70f;
        public static bool breakableMineralsESP = false;
        public static bool pickupableMineralsESP = false;
        public static bool drillableMineralsESP = false;
        
        // WORLD
        public static bool creatureEggsESP = false;


        private void RenderESPMenu()
        {
            try
            {
                GUILayout.Label("ESP SETTINGS");
                GUILayout.Space(4);
                enableESP = GUILayout.Toggle(enableESP, "Enable");
                proximityBasedLabels = GUILayout.Toggle(proximityBasedLabels, "Proximity-Based Labels");
                GUILayout.Space(10);
                if (GUILayout.Button("Minerals")) currentMenu = MenuState.ESP_Minerals;
                if (GUILayout.Button("Plants")) currentMenu = MenuState.ESP_Plants;
                if (GUILayout.Button("World")) currentMenu = MenuState.ESP_World;
                if (GUILayout.Button("Others")) currentMenu = MenuState.ESP_Others;
                if (GUILayout.Button("Settings")) currentMenu = MenuState.ESP_Settings;
            }
            catch (Exception ex)
            {
                Logger.Error($"MenuEngine: Error in RenderESPMenu method - {ex.Message}");
            }
        }

        private void RenderMineralsMenu()
        {
            try
            {
                GUILayout.Label("MINERALS");
                GUILayout.Space(10);
                //mineralsMaxDistanceRender = GUILayout.HorizontalSlider(mineralsMaxDistanceRender, 1f, 70f);
                breakableMineralsESP = GUILayout.Toggle(breakableMineralsESP, "Breakable Stones");
                pickupableMineralsESP = GUILayout.Toggle(pickupableMineralsESP, "Pickupable Minerals");
                drillableMineralsESP = GUILayout.Toggle(drillableMineralsESP, "Drillable Minerals");
            }
            catch (Exception ex)
            {
                Logger.Error($"MenuEngine: Error in RenderMineralsMenu method - {ex.Message}");
            }
        }

        private void RenderPlantsMenu()
        {
            try
            {
                GUILayout.Label("PLANTS");
                // Implementar configuraciones específicas para plantas aquí.
            }
            catch (Exception ex)
            {
                Logger.Error($"MenuEngine: Error in RenderPlantsMenu method - {ex.Message}");
            }
        }

        private void RenderWorldMenu()
        {
            try
            {
                GUILayout.Label("WORLD");
                creatureEggsESP = GUILayout.Toggle(creatureEggsESP, "Creature Eggs");
            }
            catch (Exception ex)
            {
                Logger.Error($"MenuEngine: Error in RenderWorldMenu method - {ex.Message}");
            }
        }

        private void RenderOthersMenu()
        {
            try
            {
                GUILayout.Label("OTHERS");
                
            }
            catch (Exception ex)
            {
                Logger.Error($"MenuEngine: Error in RenderOthersMenu method - {ex.Message}");
            }
        }

        private void RenderSettingsMenu()
        {
            try
            {
                GUILayout.Label("SETTINGS");
                GUILayout.Space(10);
                GUILayout.Label($"Minerals Render Distance [{mineralsMaxDistanceRender:F0} m] : ");
                mineralsMaxDistanceRender = GUILayout.HorizontalSlider(mineralsMaxDistanceRender, 1f, 70f);
            }
            catch (Exception ex)
            {
                Logger.Error($"MenuEngine: Error in RenderSettingsMenu method - {ex.Message}");
            }
        }

        private void OnDestroy()
        {
            //Cursor.lockState = CursorLockMode.Locked; // Asegurarse de que el cursor esté bloqueado al destruir el objeto
            //Cursor.visible = false; // Asegurarse de que el cursor sea invisible al destruir el objeto
        }
    }
}
