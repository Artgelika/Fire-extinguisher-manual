using Assets._Scripts.FireExtinguisher.Behaviors.Interfaces;
using UnityEngine;

namespace Assets._Scripts.FireExtinguisher.Behaviors
{
    public class PowderSpray : MonoBehaviour, ISprayBehavior
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        public void StartSpray()
        {
            _particleSystem.Play();
        }

        public void StopSpray()
        {
            _particleSystem.Stop(
                true,
                ParticleSystemStopBehavior.StopEmitting
            );
        }
    }
}