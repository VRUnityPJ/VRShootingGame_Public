using System;

namespace VRShooting.Scripts.Score
{
    public class Score
    {
        /// <summary>
        /// スコアの値
        /// </summary>
        public readonly float value;
        
        /// <summary>
        /// スコアの最小値
        /// </summary>
        private const float MIN_SCORE = 0f;
        
        private const string MIN_VALUE_ERROR_MESSAGE = "Scoreの下限よりも小さい値は入れられません。";
        
        /// <summary>
        /// スコアクラスのデフォルトコンストラクタ
        /// </summary>
        public Score()
        {
            value = MIN_SCORE;
        }
        
        /// <summary>
        /// スコアクラスのコンストラクタ
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException">スコアの最小値以下の値を入れようとしたときの例外</exception>
        public Score(in float value)
        {
            if (value < MIN_SCORE)
                throw new ArgumentOutOfRangeException(MIN_VALUE_ERROR_MESSAGE + "value : {value}");
            
            // スコアを設定
            this.value = value;
        }
        
        /// <summary>
        /// 加算されたスコアのインスタンスを返す
        /// </summary>
        /// <param name="addValue"></param>
        /// <returns></returns>
        public Score Add(in Score addValue)
        {
            return new Score(value + addValue.value);
        }
        
        /// <summary>
        /// 加算されたスコアのインスタンスを返す
        /// </summary>
        /// <param name="addValue"></param>
        /// <returns></returns>
        public Score Add(in float addValue)
        {
            return new Score(value + addValue);
        }
    }
}