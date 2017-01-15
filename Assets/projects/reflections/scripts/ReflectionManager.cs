using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prime31.ZestKit;

public class ReflectionManager : MonoBehaviour 
{
    // Events
    public event Action<ReflectionEntry> OnReflectionAccessed = (reflectionEntry) => {}; 

    private const string FILE_NAME = "reflections.json";
    private string FilePath { get { return Application.persistentDataPath + Path.DirectorySeparatorChar + FILE_NAME; } }

    private static ReflectionManager _instance;
    public static ReflectionManager Instance { get { return _instance; } }

    private ReflectionCollection _reflectionCollection;
    private List<ReflectionEntry> _currentReflections = new List<ReflectionEntry>();

    public ReflectionCollection CurrentReflectionCollection 
    {
        get
        {
            if ( _reflectionCollection == null ) LoadText();
            return _reflectionCollection;
        }
    }

    public int ReflectionCount { get { return _reflectionCollection.entries.Length; } }

    // Other Components
    public ReflectionCubeSpawner spawner;

    void Awake()
    {
        _instance = this;
    }
     
    private void Print()
    {
        Debug.Log( JsonUtility.ToJson( _reflectionCollection, prettyPrint: true ) );
    }

    public void RegisterReflection( ReflectionEntry newEntry )
    {
        _currentReflections.Add( newEntry );
        _reflectionCollection = new ReflectionCollection( _currentReflections.ToArray() );
    }

    public void ReportReflectionAccessed( ReflectionEntry reflection )
    {
        OnReflectionAccessed( reflection );
    }

    private void LoadText()
    {
        string text = File.ReadAllText( FilePath );
        _reflectionCollection = JsonUtility.FromJson<ReflectionCollection>( text );
        Print();
    }

    public void SaveText()
    {
        string text = JsonUtility.ToJson( _reflectionCollection );
        File.WriteAllText( FilePath, text );
        FileInfo file = new FileInfo( FilePath );

        //Debug.Log( JsonUtility.ToJson( _reflectionCollection, prettyPrint: true ) );
        //EditorUtility.RevealInFinder( FilePath );
    }

    #region Spawner Functions ==================================================
    public ReflectionCube GetCubeByIndex( int index )
    {
        return spawner.GetCubeByIndex( index );
    }
    #endregion
}
