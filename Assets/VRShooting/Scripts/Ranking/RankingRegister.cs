using NCMB;
using VRShooting.Scripts.Ranking;

public class RankingRegister
{
    /// <summary>
    /// ランキングに登録する関数
    /// </summary>
    public static void Register()
    {
        var finalPoint = PointStorage.GetPoint();
        var playerName = PlayerNameStorage.GetPlayerName();
        bool isHard = StageData.isHard;
        int stageNum = StageData.stageNumber;
        
        NCMBObject obj = new NCMBObject("Stage" + StageData.stageNumber.ToString());
        
        obj["Name"] = playerName;
        obj["Score"] = finalPoint;
        obj["IsHard"] = isHard;
        obj["StageNumber"] = stageNum;
        
        obj.SaveAsync();
    }
}
