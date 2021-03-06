﻿using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly double TOLERANCE = 0.0001;
    
    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }

        direction.Normalize();
        if (IsBlocked(transform.position, direction))
        {
            return false;
        }

        transform.Translate(direction);
        return true;
    }

    private bool IsBlocked(Vector3 position, Vector2 direction)
    {
        var newPosition = new Vector2(position.x, position.y) + direction;
        var walls = GameObject.FindGameObjectsWithTag("Wall");

        if (walls.Any(wall => Math.Abs(wall.transform.position.x - newPosition.x) < TOLERANCE 
                              && Math.Abs(wall.transform.position.y - newPosition.y) < TOLERANCE))
        {
            return true;
        }

        var boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (Math.Abs(box.transform.position.x - newPosition.x) < TOLERANCE 
                && Math.Abs(box.transform.position.y - newPosition.y) < TOLERANCE)
            {
                var bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }

                return true;
            }
        }

        return false;
    }
}