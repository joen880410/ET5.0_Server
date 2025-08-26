using System;
using System.Collections.Generic;
using System.Linq;
using ETModel;
using MongoDB.Bson;

#if HOTFIX
using ETHotfix;

namespace ETHotfix.Share
{
#else
namespace ETModel.Share
{
#endif
    public static class GameUtility
    {
        /// <summary>
        /// 遊戲類型
        /// </summary>
        public enum GameType
        {
            None,
            Gameble, // 博弈
            TurnBased // 回合制
        }

        public enum TurnBasedData 
        {
            RoundCount,
        }

        public enum PlayerWinType
        {
            None,
            Win,
            Lose,
            Draw,
        }
        
        /// <summary>
        /// 剪刀石頭布出拳類型
        /// </summary>
        public enum RockAndPaperAndScissorsPunchType
        {
            None,
            Rock,
            Paper,
            Scissors
        }

        public enum BombStateType 
        {
            Safe,
            Warning,
            Danger,
            BFExplode,
            Exploded
        }

        public enum BombHitType
        {
            None,
            One,
            Two,
            Three,
            Four,
            Five,
        }

        public enum DiceConfigType 
        {
            None,
            Normal,
        }

        public enum CardCompareConfigType
        {
            None,
            Normal,
        }

        public enum CardCompareType
        {
            None,
            Bigger,
            Smaller,
        }
        
        public enum GambleResultType
        {
            Miss,
            BingoReward,
            BingoPunish,
        }
        
        public enum PlayerType
        {
            None,
            Player,
            Boss,
        }

        public enum CardType
        {
            PlumFlower,//梅花
            Diamond,//菱形
            Heart,//心型
            Spades,//黑桃
        }
    }
}