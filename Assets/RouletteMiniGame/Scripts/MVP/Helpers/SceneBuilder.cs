
using System.Collections.Generic;
using RouletteMiniGame.Animations;
using RouletteMiniGame.Data;
using UnityEngine;

namespace RouletteMiniGame.MVP.Helpers
{
    public class SceneBuilder : MonoBehaviour
    {
        List<GameObject> _circleRowList = new List<GameObject>();
        private Slot[] _slots;
        private GameObject _circleRowPrefab;

        public List<DynamicGridLayout.Directions> directionsList;
        public int pixels;
        public int columns;
        public int rows;
        public float padding;
        public float spacing;

        public void Initialize(Slot[] slots, GameObject circleRowPrefab)
        {
            _slots = slots;
            _circleRowPrefab = circleRowPrefab;
            InitializeGrids();
        }

        private void InitializeGrids()
        {
            GameObject CircleRowParentObject = new GameObject("CircleRowParentObject");
            RouletteAnimations rouletteAnimations = CircleRowParentObject.AddComponent<RouletteAnimations>();
            foreach (var slot in _slots)
            {
                GameObject newCircleRow = GameObject.Instantiate(_circleRowPrefab, CircleRowParentObject.transform);
                newCircleRow.GetComponent<CircleRowScript>().SetSlot(slot);
                _circleRowList.Add(newCircleRow);
            }

            rouletteAnimations.InitializeDependencies(0.1f, 0.2f, _circleRowList);
            DynamicGridLayout.ArrangeSlots(_circleRowList, pixels, columns, rows, padding, spacing, directionsList);

        }
    }
}