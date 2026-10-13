using UnityEngine;

namespace Assets._Scripts.FireExtinguisher.Elements
{
    public class Bottle : MonoBehaviour
    {
        [SerializeField]
        private float _capacity = 1000;
        private float _currentAmount;
        public bool IsEmpty => _currentAmount <= 0f;

        private void Awake()
        {
            _currentAmount = _capacity;
        }

        public void FuelConsumption(float amount)
        {
            _currentAmount = Mathf.Max(0, _currentAmount - amount);
            if (_currentAmount <= 0)
            {
                Debug.Log("Bottle is empty.");
            }
        }
    }
}
