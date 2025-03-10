using System.Collections.Generic;
using UnityEngine;

namespace SubnauticaEnhancer
{
    public static class Render
    {
        private static GUIStyle guiStyle = new GUIStyle
        {
            fontSize = 12,
            normal = { textColor = Color.white }
        };

        public static void RenderSnapline(Vector2 start, Vector2 end, Color color)
        {
            DrawLine(start, end, color, 1f);
        }

        public static void RenderLineBetweenCoords(Vector2 start, Vector2 end, Color color, float width = 1f)
        {
            DrawLine(start, end, color, width);
        }

        public static void RenderText(Vector2 position, string text, Color textColor, Color shadowColor)
        {
            Vector2 textSize = guiStyle.CalcSize(new GUIContent(text));
            Color oldColor = GUI.color;

            // Render shadow text
            GUI.color = shadowColor;
            guiStyle.normal.textColor = shadowColor;
            GUI.Label(new Rect(position.x + 1, position.y + 1, textSize.x, textSize.y), text, guiStyle);
            GUI.Label(new Rect(position.x - 1, position.y + 1, textSize.x, textSize.y), text, guiStyle);
            GUI.Label(new Rect(position.x + 1, position.y - 1, textSize.x, textSize.y), text, guiStyle);
            GUI.Label(new Rect(position.x - 1, position.y - 1, textSize.x, textSize.y), text, guiStyle);

            // Render main text
            GUI.color = textColor;
            guiStyle.normal.textColor = textColor;
            GUI.Label(new Rect(position.x, position.y, textSize.x, textSize.y), text, guiStyle);
            GUI.color = oldColor;
        }

        /*public static void RenderTextAndBox(Vector2 position, string text, Color textColor, Color boxColor)
        {
            Vector2 textSize = guiStyle.CalcSize(new GUIContent(text));
            RenderBox(position, textSize, boxColor);
            RenderText(position, text, textColor);
        }*/

        public static void RenderBox(Vector2 position, Vector2 size, Color color)
        {
            Color oldColor = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(new Rect(position.x, position.y, size.x, 1), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y + size.y, size.x, 1), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y, 1, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x + size.x, position.y, 1, size.y), Texture2D.whiteTexture);
            GUI.color = oldColor;
        }

        public static void RenderBones(Vector2[] positions, Color color)
        {
            for (int i = 0; i < positions.Length - 1; i++)
            {
                RenderLineBetweenCoords(positions[i], positions[i + 1], color);
            }
        }

        private static void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            Matrix4x4 matrix = GUI.matrix;
            Color oldColor = GUI.color;

            GUI.color = color;
            float angle = Vector3.Angle(end - start, Vector2.right);
            if (start.y > end.y) angle = -angle;
            GUIUtility.RotateAroundPivot(angle, start);
            GUI.DrawTexture(new Rect(start.x, start.y, (end - start).magnitude, width), Texture2D.whiteTexture);

            GUI.matrix = matrix;
            GUI.color = oldColor;
        }
    }
}