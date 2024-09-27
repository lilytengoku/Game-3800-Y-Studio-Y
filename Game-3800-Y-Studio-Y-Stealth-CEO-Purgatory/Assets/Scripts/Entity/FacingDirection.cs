using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingDirection
{
    Dir direction;
    public FacingDirection() {
        direction = Dir.Right;
    }
    public enum Dir
    {
        Right,
        Down,
        Left,
        Up
    }

    public void Rotate(int rotations){
        bool cw = Math.Sign(rotations) >= 0;
        uint numRotations = (uint)Math.Abs(rotations);
        if (cw)
        {
            RotateCW(numRotations);
        }
        else {
            RotateCCW(numRotations);
        }
    }
    private void RotateCW(uint numRotations) {
        for (uint i = 0; i < numRotations; i++)
        {
            switch (direction)
            {
                case Dir.Right:
                    direction = Dir.Down;
                    break;
                case Dir.Down:
                    direction = Dir.Left;
                    break;
                case Dir.Left:
                    direction = Dir.Up;
                    break;
                case Dir.Up:
                    direction = Dir.Right;
                    break;
            }
        }
    }
    private void RotateCCW(uint numRotations)
    {
        for (uint i = 0; i < numRotations; i++)
        {
            switch (direction)
            {
                case Dir.Right:
                    direction = Dir.Up;
                    break;
                case Dir.Down:
                    direction = Dir.Right;
                    break;
                case Dir.Left:
                    direction = Dir.Down;
                    break;
                case Dir.Up:
                    direction = Dir.Left;
                    break;
            }
        }
    }

    public Vector3 GetVector() {
        switch (direction)
        {
            case Dir.Right:
                return Vector3.right;
            case Dir.Down:
                return Vector3.down;
            case Dir.Left:
                return Vector3.left;
            case Dir.Up:
                return Vector3.up;
            default:
                return Vector3.zero;
        }
    }
    public void setLeft() {
        direction = Dir.Left;
    }
    public void setRight()
    {
        direction = Dir.Right;
    }
    public void setDown()
    {
        direction = Dir.Down;
    }
    public void setUp()
    {
        direction = Dir.Up;
    }
    public void setDir(Dir dir)
    {
        direction = dir;
    }
}