using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings", order = 1)]
    public class SettingsSO : ScriptableObject
    {
        [Header("Speed")]
        public float BasePlayerSpeed = 10f;
        public float BaseEnemySpeed = 2f;

        [Header("Enemy")]
        public int EnemyCountPerTick = 2;
        public float SpawnEnemyesDelay = 2f;
        public float EnemySpeedIteration = 1f;

        [Header("Bonus")]
        public float StepTimeToAddBonus = 5f;
        public int BonusCountPerTick = 1;
        public float SpawnBonusesDelay = 3f;

        [Header("Bonus-speed")]
        public float BonusSpeedValue = 2;
        public float BonusSpeedTime = 10;

        [Header("Bonus-immortal")]
        public float BonusImmortalTime = 15;

        [Header("Score")]
        public int ScoreAppenBonus = 10;
        public int ScoreAppenStep = 2;
    }
}
