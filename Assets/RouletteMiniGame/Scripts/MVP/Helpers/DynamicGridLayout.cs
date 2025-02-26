using System;
using System.Collections.Generic;
using UnityEngine;

namespace RouletteMiniGame.MVP.Helpers
{
    public class DynamicGridLayout
    {
        public enum Directions
        {
            Left,
            Right,
            Up,
            Down,
            None
        };

        public static void ArrangeSlots(List<GameObject> slotGameObjects, int pixels, int columns, int rows,
            float padding, float spacing, List<Directions> DirectionList, float ppu = 100)
        {
            Vector3 originPosition = Vector3.zero;
            float height = 2f * Camera.main.orthographicSize;
            float width = (height * Camera.main.aspect) - padding;
            float slotSize = (width - ((columns - 1) * spacing)) / columns;
            float scale = slotSize / (pixels / ppu);
            Vector3 zeroPosition = CalculateGridZeroPosition();
            Vector3 newScale = new Vector3(scale, scale, 1f);
            int row = 0;
            int col = 0;
            for (int i = 0; i < slotGameObjects.Count; i++)
            {
                switch (DirectionList[i])
                {
                    case Directions.Right:
                        col += 1;
                        break;
                    case Directions.Left:
                        col -= 1;
                        break;
                    case Directions.Up:
                        row -= 1;
                        break;
                    case Directions.Down:
                        row += 1;
                        break;
                    case Directions.None:
                        break;
                }

                slotGameObjects[i].transform.position = new Vector3(
                    col * (slotSize + spacing),
                    -row * (slotSize + spacing),
                    0
                ) + zeroPosition;
                slotGameObjects[i].transform.localScale = newScale;
            }

            Vector3 CalculateGridZeroPosition()
            {
                return new Vector3(
                    originPosition.x - (columns - 1) * (slotSize + spacing) / 2f,
                    originPosition.y + (rows - 1) * slotSize / 2f,
                    0
                );
            }
        }
    }
}