using MongoDB.Bson;

namespace ETModel
{
    public static class GameEpisodeType
    {
        //副遊戲
        public const string Roulette = nameof(Roulette); // 輪盤
        public const string DiceBetSize = nameof(DiceBetSize); // 骰子比大小
        public const string CardCompare = nameof(CardCompare); // 抽卡比大小
        public const string Scratch = nameof(Scratch);//刮刮樂
        public const string Slot = nameof(Slot);//拉霸
        public const string FindBallInCup = nameof (FindBallInCup); // 猜杯子
        public const string FortuneStick = nameof(FortuneStick); // 抽廟籤
        public const string LadderLottery = nameof (LadderLottery); // 爬梯子(爬鬼腳)

        //主遊戲
        public const string RockAndPaperAndScissors = nameof(RockAndPaperAndScissors); // 猜拳
        public const string PassWord = nameof(PassWord);//猜密碼
        public const string DetonateBomb = nameof(DetonateBomb);//引爆炸彈
        public const string PassBomb = nameof (PassBomb); // 傳炸彈
        public const string TugOfWar = nameof (TugOfWar); // 拔河
    }
    
    public struct GameData
    {
        public BsonDocument data;
    }

    public class DiceBetSizeData
    {
        public int DiceNumber;
        public int PlayerDicePoint;
        public int NpcDicePoint;
    }

    public class CardCompareData 
    {
        public int CardType;
        public int PlayerCardPoint;
        public int PlayerCardType;
        public int NpcCardPoint;
        public int NpcCardType;
    }

    public class RockAndPaperAndScissorsData
    {
        public int PunchNumber;
        public int PlayerPowerType;
        public int BossPowerType;
        public int PlayerScore;
        public int BossScore;
        public int RockPunchCount;
        public int PaperPunchCount;
        public int ScissorsPunchCount;
    }

    public class BombData 
    {
        public int PlayerBombCount;
        public int Hp;
    }

    public class PassWordData 
    {
        public int MaxPassWord;
        public int MinPassWord;
        public int PlayerPassWord;
        public int AnswerPassWord;
        public int PlayerCoinSide;
    }
    
    public class QuizData 
    {
        public long QuizId;
        public int QuizAnswer;
        public int PlayerAnswer;
    }
    
    /// <summary>
    /// 後端紀錄-丟炸彈
    /// </summary>
    public class PassBombRecord
    {
        public int WinType; // 勝負
        public int BombHP; // 炸彈血量
        public int PlayerTotalHit; // 玩家敲擊次數
        public int BossTotalHit; // 魔王敲擊次數
    }
}