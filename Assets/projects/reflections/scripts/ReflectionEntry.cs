using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class ReflectionCollection
{
    public ReflectionEntry[] entries;

    public ReflectionCollection( ReflectionEntry[] entries )
    {
        this.entries = entries;
    }
}

[Serializable]
public class ReflectionEntry
{
    public int id;
    public string content;

    public ReflectionCube ReflectionCube { get; set; }

    public ReflectionEntry( int id, string content )
    {
        this.id = id;
        this.content = content;
    }
}
