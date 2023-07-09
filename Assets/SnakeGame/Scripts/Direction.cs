using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace SnakeGame
{
    public static class Direction
    {
        public const int UP = 0;
        public const int RIGHT = 1;
        public const int DOWN = 2;
        public const int LEFT = 3;

        public static int GetDirection(Vector2 input)
        {
            if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
            {
                return input.y >= 0 ? UP : DOWN;
            }
            else
            {
                return input.x >= 0 ? RIGHT : LEFT;
            }
        }

        public static Quaternion GetRotation(int direction)
        {

            switch (direction)
            {
                case UP: return Quaternion.Euler(0f, 0f, 0f);
                case RIGHT: return Quaternion.Euler(0f, 0f, -90f);
                case DOWN: return Quaternion.Euler(0f, 0f, 180f);
                case LEFT: return Quaternion.Euler(0f, 0f, 90f);
            }

            return Quaternion.identity;
        }

        public static int GetDirection(Vector2Int input)
        {
            return GetDirection(input);
        }

        public static Vector2Int GetVector2IntDirection(int direction)
        {
            switch (direction)
            {
                case 0: return Vector2Int.up;
                case 1: return Vector2Int.right;
                case 2: return Vector2Int.down;
                case 3: return Vector2Int.left;
            }

            return Vector2Int.right;
        }

        public static Vector2 GetVector2Direction(int direction)
        {
            return (Vector2)GetVector2IntDirection(direction);
        }

        public static Vector2 GetRotatedCW(Vector2 input)
        {
            return new Vector2(-input.y, input.x);
        }

        public static Vector2 GetRotatedCCW(Vector2 input)
        {
            return new Vector2(input.y, -input.x);
        }

        public static Vector2 GetRotatedCW(Vector2Int input)
        {
            return new Vector2Int(-input.y, input.x);
        }

        public static Vector2 GetRotatedCCW(Vector2Int input)
        {
            return new Vector2Int(input.y, -input.x);
        }
    }
}
