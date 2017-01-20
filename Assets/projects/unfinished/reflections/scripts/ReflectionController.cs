using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prime31.ZestKit;

public class ReflectionController : Controller<ReflectionController> 
{
    // Events
    public event Action<ReflectionEntry> OnReflectionAccessed = (reflectionEntry) => {}; 

    // Reflection Collection Data
    private ReflectionCollection _reflectionCollection;
    private List<ReflectionEntry> _currentReflections = new List<ReflectionEntry>();

    public ReflectionCollection CurrentReflectionCollection 
    {
        get
        {
            if ( _reflectionCollection == null ) TryLoadText();
            return _reflectionCollection;
        }
    }
    
    public int ReflectionCount { get { return _reflectionCollection.entries.Length; } }

    // Save and Load Data
    private FileInfo __file;
    private FileInfo _file
    {
        get
        {
            if ( __file == null ) __file = new FileInfo( _FilePath );
            return __file;
        }
    }

    private const string FILE_NAME = "reflections.json";
    private string _FilePathInstance { get { return Application.persistentDataPath + Path.DirectorySeparatorChar + FILE_NAME; } }
    private string _FilePath { get { return _FilePathInstance; } }

    // View Data
    public ReflectionCubeSpawner spawner;
    private int _currentIndex;

    #region Save and Load Functions ==================================================
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

    public void TryLoadText()
    {
        string text;

        if ( !File.Exists( _FilePath ) )
        {
            Debug.Log( "Local data doesn't exist." );
            text = "";
            DropboxController.Instance.DownloadFromDropbox( _file );
        } else {
            Debug.Log( "Local data exists." );
            text = File.ReadAllText( _FilePath );
        }

        SetCollectionText( text );
    }

    public void SetCollectionText( string text )
    {
        _reflectionCollection = JsonUtility.FromJson<ReflectionCollection>( text );
        Print();
    }

    public void SaveText()
    {
        Debug.Log( "Saving text." );
        string text = JsonUtility.ToJson( _reflectionCollection );
        File.WriteAllText( _FilePath, text );

        DropboxController.Instance.SyncFileWithDropbox( _file );
        //Debug.Log( JsonUtility.ToJson( _reflectionCollection, prettyPrint: true ) );
        //EditorUtility.RevealInFinder( FilePath );
    }
    
    public void DownloadText()
    {
        DropboxController.Instance.DownloadFromDropbox( _file );
    }

    public void TrySyncText()
    {
        DropboxController.Instance.SyncFileWithDropbox( _file );
    }
    #endregion

    #region View Functions ==================================================
    private void UpdateFocusInput()
    {
        if ( Input.GetKeyDown( KeyCode.UpArrow ) )
        {
            MoveFocusUp();
        }

        if ( Input.GetKeyDown( KeyCode.DownArrow ) )
        {
            MoveFocusDown();
        }
    }

    public void MoveFocusUp()
    {
        if ( _currentIndex < ReflectionCount - 1 ) _currentIndex++;
        AccessReflectionAtIndex( _currentIndex );
    }

    public void MoveFocusDown()
    {
        if ( _currentIndex > 0 ) _currentIndex--;
        AccessReflectionAtIndex( _currentIndex );
    }

    private void AccessReflectionAtIndex( int index )
    {
        OnReflectionAccessed( CurrentReflectionCollection.entries[index] );
    }

    public ReflectionCube GetCubeByIndex( int index )
    {
        return spawner.GetCubeByIndex( index );
    }
    #endregion
}
