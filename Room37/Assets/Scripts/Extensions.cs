using System.Collections.Generic;
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
 }