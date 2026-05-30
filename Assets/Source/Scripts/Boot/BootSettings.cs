using UnityEngine;

namespace Game.Boot
{
    [CreateAssetMenu(fileName = "BootSettings", menuName = "Game/Boot Settings")]
    public sealed class BootSettings : ScriptableObject
    {
        [SerializeField] private float _splashDelaySeconds = 1f;
        [SerializeField] private int _loadStepsCount = 5;
        [SerializeField] private float _loadStepDelaySeconds = 0.2f;

        public float SplashDelaySeconds => _splashDelaySeconds;
        public int LoadStepsCount => _loadStepsCount;
        public float LoadStepDelaySeconds => _loadStepDelaySeconds;
    }
}
