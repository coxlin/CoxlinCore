using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CoxlinCore
{

    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }

    public static class DirectionUtils
    {
        public static Vector3 ClosestDirection(Vector3 v)
        {
            Vector3[] compass = new Vector3[]
            {
                Vector3.left,
                Vector3.right,
                Vector3.up,
                Vector3.down,
                Vector3.forward,
                Vector3.back
            };
                

            var maxDot = -Mathf.Infinity;
            var ret = Vector3.zero;

            foreach (var dir in compass)
            {
                var t = Vector3.Dot(v, dir);
                if (t > maxDot)
                {
                    ret = dir;
                    maxDot = t;
                }
            }

            return ret;
        }

        public static Direction CalcDir(Vector3 v)
        {
            var vec = ClosestDirection(v);
            if (vec == Vector3.forward)
            {
                return Direction.Up;
            }
            if (vec == Vector3.back)
            {
                return Direction.Down;
            }
            if (vec == Vector3.left)
            {
                return Direction.Left;
            }
            if (vec == Vector3.right)
            {
                return Direction.Right;
            }
            return Direction.None;
        }

        public static Direction CalcDir2D(Vector3 v)
        {
            var vec = ClosestDirection(v);
            if (vec == Vector3.up)
            {
                return Direction.Up;
            }
            if (vec == Vector3.down)
            {
                return Direction.Down;
            }
            if (vec == Vector3.left)
            {
                return Direction.Left;
            }
            if (vec == Vector3.right)
            {
                return Direction.Right;
            }
            return Direction.None;
        }
    }
}
