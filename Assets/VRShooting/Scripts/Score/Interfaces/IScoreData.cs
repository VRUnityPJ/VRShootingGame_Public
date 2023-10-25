using UniRx;

namespace VRShooting.Scripts.Score.Interfaces
{
    public interface IScoreData
    {
        /// <summary>
        /// スコアが変わったときに発火される
        /// </summary>
        public IReadOnlyReactiveProperty<Score> onChangeScore { get; }
        
        /// <summary>
        /// スコアを加算する
        /// </summary>
        /// <param name="value"></param>
        public void AddScore(float value);
        
        /// <summary>
        /// スコアを加算する
        /// </summary>
        /// <param name="value"></param>
        public void AddScore(Score value);
    }
}