﻿using System.Collections.Generic;
using UnityEngine;
using System;

public class Tuple<T>
{
    public T Item1 { get; private set;}
    public T Item2 { get; private set;}

    public Tuple(T t1, T t2)
    {
        this.Item1 = t1;
        this.Item2 = t2;
    }

    public bool CrossEqual(Tuple<T> anotherTuple)
    {
        return this.Equals(anotherTuple) 
            && anotherTuple.Item1.Equals(this.Item2) 
            && anotherTuple.Item2.Equals(this.Item1);
    }

    public override bool Equals(object obj)
    {
        var tuple = (Tuple<T>) obj;
        if (tuple == null)
        {
            return false;
        }
        return tuple.Item1.Equals(this.Item1) && tuple.Item2.Equals(this.Item2);
    }
    //http://stackoverflow.com/a/1646913/173190
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + this.Item1.GetHashCode();
            hash = hash * 31 + this.Item2.GetHashCode();
            return hash;
        }
    }
}

public static class Extensions
{
    public static IList<T> xShuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public static IList<T> xExchange<T>(this IList<T> list, int a, int b)
    {
        if (list.Count <= a || list.Count <= a )
        {
            throw new ArgumentOutOfRangeException();
        }

        T temp = list[a];
        list[a] = list[b];
        list[b] = temp;
        return list;
    }

    public static int xRandomIndex<T>(this IList<T> list)
    {
        return UnityEngine.Random.Range(0, list.Count-1);
    }

    public static T xRandomItem<T>(this IList<T> list)
    {
        return list[list.xRandomIndex()];
    }

    public static T xPop<T>(this IList<T> list)
    {
        var t = list[0];
        list.RemoveAt(0);
        return t;
    }

    public static Vector2 xWorldToCanvas(this Canvas canvas, Vector3 worldPos, Camera camera = null)
    {
         if (camera == null)
         {
             camera = Camera.main;
         }
 
        var viewportPosition = camera.WorldToViewportPoint(worldPos);
        var canvasRect = canvas.GetComponent<RectTransform>();
 
        return new Vector2((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                            (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
     }

    public static float xAngleSigned(this Vector3 v1, Vector3 v2, Vector3 rotationAxis)
    {
        return Mathf.Atan2(
            Vector3.Dot(rotationAxis, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    public static void xLookAt(this Transform trans, Vector3 point)
    {
        Vector3 fromPosToPoint = point - trans.position;
        Vector3 dir = fromPosToPoint.normalized;
        float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        trans.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
 }