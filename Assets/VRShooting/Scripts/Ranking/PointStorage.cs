using System;
using UnityEngine;

namespace VRShooting.Scripts.Ranking
{
    public class PointStorage : MonoBehaviour
    {
        private static Point NowPoint;

        private void Awake()
        {
            NowPoint = new Point();
        }

        /// <summary>
        /// pointを獲得する関数
        /// </summary>
        /// <param name="_getPoint">獲得ポイント数</param>
        public static void PointUp(int _getPoint)
        {
            var upCount = new Point(_getPoint);
            NowPoint = NowPoint.Add(upCount);
        }
        /// <summary>
        /// ポイントを下げる関数
        /// </summary>
        /// <param name="_downPoint"></param>
        public static void PointDown(int _downPoint)
        {
            var downCount = new Point(_downPoint);
            NowPoint = NowPoint.Del(downCount);
        }

        public static int GetPoint()
        {
            return NowPoint.point;
        }
    }

    public class Point
    {
        public int point { get; }
        public Point(int _init = 0)
        {
            point = _init;
        }

        public Point Add(Point up)
        {
            return new Point(point + up.point);
        }

        public Point Del(Point down)
        {
            if (point - down.point < 0)
            {
                return this;
            }
            return new Point(point - down.point);
        }
    }
}