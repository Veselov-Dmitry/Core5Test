using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core5Test
{
    public class BonusType
    {
        public const string SCORE = "Score";
        public const string SPEED = "Speed";
        public const string IMMORTAL = "Immortal";
        public static List<string> m_List;
        public static List<string> List
        {
            get
            {
                if (m_List == null)
                {
                    m_List = new List<string>();
                    m_List.Add(SCORE);
                    m_List.Add(SPEED);
                    m_List.Add(IMMORTAL);
                }
                return m_List;
            }
        }

        public string Type;

        public BonusType()
        {
            Type = SCORE;
        }
        public BonusType(string type)
        {
            Type = type;
        }

        public static BonusType GetScore()
        {
            return new BonusType(SCORE);
        }
        public static BonusType GetSpeed()
        {
            return new BonusType(SPEED) ;
        }
        public static BonusType GetImmortal()
        {
            return new BonusType(IMMORTAL);
        }

        public Color GetColor()
        {
            switch(Type)
            {
                case SCORE:
                {
                    return Color.white;
                }
                case SPEED:
                {
                    return Color.green;
                }
                case IMMORTAL:
                {
                    return Color.yellow;
                }
                default:
                {
                    throw new System.Exception("Unknown type:"+ Type);
                }
            }
        }

        public static BonusType GetRandom()
        {
            return new BonusType(List[Random.Range(0, List.Count)]);
        }
    }
}
