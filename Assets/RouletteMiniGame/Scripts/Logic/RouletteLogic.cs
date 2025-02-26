using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteMiniGame.Data;
using UnityEngine;


namespace RouletteMiniGame.Logic
{
    public class RouletteLogic
    {
        private System.Random _random;
        Slot[] slots;
        List<Slot> ActiveSlots;
        private float totalWeight;

        public RouletteLogic(Slot[] slots)
        {
            _random = new System.Random();
            this.slots = slots;
            ActiveSlots = new List<Slot>(slots);
        }

        private void CalculateTotalWeight()
        {
            totalWeight = 0;
            foreach (Slot slot in ActiveSlots)
                totalWeight += slot.Weight;
            if (totalWeight < 1)
            {
                Debug.LogWarning("Total weight is less than 1");
            }
        }

        public Task<Slot> GetSelectSlotAsTask()
        {
            Slot selectedSlot = GetRandomSlotByWeight();
            ActiveSlots.Remove(selectedSlot);
            selectedSlot.IsActive = false;
            if (ActiveSlots.Count == 0)
                selectedSlot.IsLast = true;
            return Task.FromResult(selectedSlot);
        }

        public Slot GetRandomSlotByWeight()
        {
            // here will be more complex calculation which will take few frames time to execute
            //Random _random = new Random(); // im not sure if shared random is thread safe maybe this will be used instead
            CalculateTotalWeight();
            float currentWeight = 0;
            int randomValue = _random.Next(1, (int)totalWeight + 1);
            foreach (Slot slot in ActiveSlots)
            {
                currentWeight += slot.Weight;
                if (currentWeight >= randomValue)
                    return slot;
            }

            throw new System.InvalidOperationException("current weight exceeded total weight");
        }
    }
}