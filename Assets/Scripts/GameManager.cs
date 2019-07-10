using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core5Test
{
    public enum GameState { Game = 1, GamePause = 0 }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static GameState _GameState = GameState.GamePause;
        public static GameState GameState
        {
            get
            {
                return _GameState;
            }
            private set
            {
                _GameState = value;
            }
        }
        public float PlayerSpeed
        {
            get
            {
                 return m_Settings.BasePlayerSpeed;
            }
        }
        public Transform GetPlayer
        {
            get
            {
                return m_Player.transform;
            }
        }
        public float StepTime
        {
            get
            {
                return m_Settings.StepTimeToAddBonus;
            }
        }

        [SerializeField]
        private Player m_Player = null;
        [SerializeField]
        private SettingsSO m_Settings = null;
        [SerializeField]
        private UIController m_UIController = null;
        [SerializeField]
        private ScoreManager m_ScoreManager = null;
        [SerializeField]
        private int m_Iteration = 0;
        [SerializeField]
        private ScoreDigits m_ScoreDigits = null;

        private Camera m_Cam;
        private Camera Cam
        {
            get
            {
                if (m_Cam == null)
                {
                    m_Cam = Camera.main;
                }
                return m_Cam;
            }
        }


        private IEnumerator m_EnemyGenerator;
        private IEnumerator m_BonusGenerator;

        private void OnEnable()
        {
            Enemy.OnFindPlayer += Enemy_OnFindPlayer;
            TimeCounter.OnStepOver += TimeCounter_OnStepOver;
            Player.OnFindBonus += Player_OnFindBonus;
        }
        private void OnDisable()
        {
            Enemy.OnFindPlayer -= Enemy_OnFindPlayer;
            TimeCounter.OnStepOver -= TimeCounter_OnStepOver;
            Player.OnFindBonus -= Player_OnFindBonus;
        }

        private void Awake()
        {
            if (GameManager.Instance == null)
            {
                GameManager.Instance = this;
            }
            else if (GameManager.Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Play()
        {
            m_Iteration = 0;
            if (m_EnemyGenerator != null)
            {
                StopCoroutine(m_EnemyGenerator);
            }
            m_EnemyGenerator = EnemyGeneratorCoroutine();
            StartCoroutine(m_EnemyGenerator);

            if (m_BonusGenerator != null)
            {
                StopCoroutine(m_BonusGenerator);
            }
            m_BonusGenerator = BonusGeneratorCoroutine();
            StartCoroutine(m_BonusGenerator);

            m_Player.SetSpeed(m_Settings.BasePlayerSpeed);

            GameState = GameState.Game;
            m_UIController.StartTime();
            m_ScoreManager.ResetScore();
            m_Player.transform.position = Vector3.zero;
            m_Player.transform.rotation = Quaternion.identity;
        }

        private IEnumerator EnemyGeneratorCoroutine()
        {
            while (true)
            {
                m_Iteration++;
                for (int i = m_Settings.EnemyCountPerTick - 1 ; i >= 0 ; i--)
                {
                    PoolItem enemy = EnemyPool.Instance.GetItem();
                    enemy.transform.position = new Vector3(
                        Cam.transform.position.x + Random.Range(3f, 5f) * (Random.Range(0, 1) > 0 ? 1 : -1),
                        Cam.transform.position.y + Random.Range(3f, 10f) * (Random.Range(0, 1) > 0 ? 1 : -1),
                        0);
                    enemy.gameObject.SetActive(true);
                    var movement = enemy.GetComponent<EnemyMovement>();
                    movement.SetSpeed(m_Settings.EnemySpeedIteration * m_Iteration + m_Settings.BaseEnemySpeed);
                }
                yield return new WaitForSeconds(m_Settings.SpawnEnemyesDelay);
            }
        }
        private IEnumerator BonusGeneratorCoroutine()
        {
            while (true)
            {
                for (int i = m_Settings.BonusCountPerTick - 1 ; i >= 0 ; i--)
                {
                    Bonus bonus = BonusPool.Instance.GetItem() as Bonus;
                    bonus.transform.position = new Vector3(
                        Cam.transform.position.x + Random.Range(1f, 3f) * (Random.Range(0, 1) > 0 ? 1 : -1),
                        Cam.transform.position.y + Random.Range(1f, 5f) * (Random.Range(0, 1) > 0 ? 1 : -1),
                        0);
                    bonus.gameObject.SetActive(true);
                    bonus.Set(BonusType.GetRandom());
                }
                yield return new WaitForSeconds(m_Settings.SpawnBonusesDelay);
            }
        }

        private void TimeCounter_OnStepOver()
        {
            m_ScoreManager.AppenScore(m_Settings.ScoreAppenStep);
        }

        private void Enemy_OnFindPlayer(Enemy enemy)
        {
            if ( GameState == GameState.GamePause)
            {
                return;
            }

            if (m_Player.Immortal)
            {
                enemy.Die();
            }
            else
            {
                //GAME OVER
                print("GAME OVER");
                GameState = GameState.GamePause;
                StopCoroutine(m_EnemyGenerator);
                StopCoroutine(m_BonusGenerator);
                m_UIController.GameOwer();
                EnemyPool.Instance.ReturnAllItems();
                BonusPool.Instance.ReturnAllItems();
            }
        }
        private void Player_OnFindBonus(Bonus bonus)
        {
            switch (bonus.Type.Type)
            {
                case BonusType.SCORE:
                {
                    m_ScoreManager.AppenScore(m_Settings.ScoreAppenBonus);
                    m_ScoreDigits.Play(m_Settings.ScoreAppenBonus.ToString(), bonus.transform.position);
                    break;
                }
                case BonusType.SPEED:
                {
                    SpeedBonus bonusSpeed = m_Player.gameObject.GetComponent<SpeedBonus>();
                    if (bonusSpeed)
                    {
                        bonusSpeed.ReActivate(m_Settings.BonusSpeedValue, m_Settings.BonusSpeedTime);
                    }
                    else
                    {
                        bonusSpeed = m_Player.gameObject.AddComponent<SpeedBonus>();
                        bonusSpeed.Activate(m_Settings.BonusSpeedValue, m_Settings.BonusSpeedTime);
                    }
                    break;
                }
                case BonusType.IMMORTAL:
                {
                    ImmortalBonus bonusImmortal = m_Player.gameObject.GetComponent<ImmortalBonus>();
                    if (bonusImmortal)
                    {
                        bonusImmortal.ReActivate(0, m_Settings.BonusImmortalTime);
                    }
                    else
                    {
                        bonusImmortal = m_Player.gameObject.AddComponent<ImmortalBonus>();
                        bonusImmortal.Activate(0, m_Settings.BonusImmortalTime);
                    }
                    break;
                }
                default:
                break;
            }
            bonus.Hide();
        }


    }
}
