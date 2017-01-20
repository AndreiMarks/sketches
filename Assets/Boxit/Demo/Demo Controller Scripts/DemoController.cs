using UnityEngine;

/// <summary>
/// This class controls the menu and switching between the 
/// different demos. It doesn't have anything to do directly
/// with Boxit or Dropbox.
/// </summary>
public class DemoController : MonoBehaviour
{
    private const int BORDER = 10;
    private const float LINK_WIDTH = 100.0f;

    private bool _inControl = true;
    private int _selectedMode = -1;
    private GameObject[] _demoGameObjects;

    private string [] demoModes = 
    {
        "Get AccountInfo",
        "Get MetaData",
        "Download File",
        "Upload File",
        "Download Thumbnail",
        "Get Revisions",
        "Restore",
        "Search",
        "Get ShareLink",
        "Get Media",
        "Get CopyRef",
        "Copy File",
        "Move",
        "Create Folder",
        "Delete",
        "Get Delta",
        "Sync Functions",
        "Download Update Upload"
    };

    public bool InControl { get { return _inControl; } }

    public Boxit.BoxitClient boxitClient;

    void Awake()
    {
        _demoGameObjects = new GameObject[demoModes.Length];

        GameObject go;
        int index = 0;
        _Demo_Base demoBase;
        foreach (string demoMode in demoModes)
        {
            go = GameObject.Find(demoMode);
            _demoGameObjects[index] = go;

            if (go != null)
            {
                demoBase = go.GetComponent<_Demo_Base>();
                if (demoBase != null)
                {
                    demoBase.Initialize(this);
                }

                go.SetActiveRecursively(false);
            }

            index++;
        }
    }

    void OnGUI()
    {
        if (_inControl)
        {
            GUILayout.BeginVertical();

            GUILayout.Space(BORDER);

            GUILayout.BeginHorizontal();

            GUILayout.Space(BORDER);

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Main Menu", GUILayout.Width(150.0f));
            GUILayout.FlexibleSpace();
            if (!boxitClient.IsLinked)
            {
                if (GUILayout.Button("Link", GUILayout.Width(LINK_WIDTH)))
                {
                    boxitClient.Link(LinkSuccess);
                }
            }
            else
            {
                if (GUILayout.Button("Unlink", GUILayout.Width(LINK_WIDTH)))
                {
                    boxitClient.Unlink();

                    Debug.Log("Successfully Unlinked from Dropbox!");
                }
            }
            GUILayout.EndHorizontal();

            if (boxitClient.IsLinked)
            {
                int newSelectedMode = GUILayout.SelectionGrid(_selectedMode, demoModes, 2);
                if (newSelectedMode != _selectedMode)
                {
                    if (_demoGameObjects[newSelectedMode] != null)
                    {
                        _demoGameObjects[newSelectedMode].SetActiveRecursively(true);
                        _selectedMode = newSelectedMode;

                        _inControl = false;
                    }
                }
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }

    public void DisableMode(GameObject go)
    {
        go.SetActiveRecursively(false);
        _selectedMode = -1;
        _inControl = true;
    }

    private void LinkSuccess(long requestID, Boxit.oAuthToken accessToken)
    {
        Debug.Log("Successfully linked to Dropbox!");
    }
}
