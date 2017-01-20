using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// This class serves as the base for all the demos. Mostly, this class 
/// handles display of the screen. These functions have been consolidated here
/// to avoid having to recode for each demo scene and to allow focus
/// on the individual procedures in the child demos. This code can be mostly
/// ignored for purposes of learning Boxit.
/// </summary>
public class _Demo_Base : MonoBehaviour 
{
    private DemoController _controller;

	protected const int BORDER = 10;
	protected const float LINK_WIDTH = 100.0f;
	protected const float BUTTON_WIDTH = 200.0f;
	protected const float BUTTON_HEIGHT = 40.0f;
	protected const float INTPUT_WIDTH = 400.0f;
	protected const float FIELD_LABLE_WIDTH = 200.0f;
	protected const float ROOT_SELECTION_WIDTH = 200.0f;
	protected const float FIELD_SPACER_WIDTH = 10.0f;
	protected const float TITLE_WIDTH = 300.0f;
	
	protected GUIStyle _noBorderStyle;
	
	protected string _title = "";
	protected string _results = "";
	protected string _error = "";
	protected float _progress = -1.0f;
	protected string [] _roots = { 
									Boxit.ROOT.dropbox.ToString(), 
									Boxit.ROOT.sandbox.ToString() 
									};
	protected string _remotePath = "";
	protected string _toRemotePath = "";
	protected string _localPath = "";
	protected string _locale = "";
	protected string _rev = "";
	protected bool _overwriteRemote = true;
	protected string _parentRev = "";
	protected bool _overwriteLocal = true;
	protected string _fileLimit = "";
	protected Int64 _fileLimitNumber = 0;
	protected string _hash = "";
	protected bool _listContents = true;
	protected bool _includeDeleted = false;
	protected string _revLimit = "";
	protected Int64 _revLimitNumber = 0;
	protected string _query = "";
	protected bool _returnShortURL = true;
	protected Texture2D _thumbnail = null;
	protected Boxit.THUMBNAIL_FORMAT _thumbnailFormat = Boxit.THUMBNAIL_FORMAT.jpeg;
	protected Boxit.THUMBNAIL_SIZE _thumbnailSize = Boxit.THUMBNAIL_SIZE.small;
	protected string [] _thumbnailFormats = { 
											Boxit.THUMBNAIL_FORMAT.jpeg.ToString(), 
											Boxit.THUMBNAIL_FORMAT.png.ToString() 
											};
	protected string [] _thumbnailSizes = { 
											Boxit.THUMBNAIL_SIZE.small.ToString(), 
											Boxit.THUMBNAIL_SIZE.medium.ToString(),
											Boxit.THUMBNAIL_SIZE.large.ToString(),
											Boxit.THUMBNAIL_SIZE.s.ToString(),
											Boxit.THUMBNAIL_SIZE.m.ToString(),
											Boxit.THUMBNAIL_SIZE.l.ToString(),
											Boxit.THUMBNAIL_SIZE.xl.ToString()
											};
	protected bool _useCopyRef = false;
	protected string _copyRef = "";
	protected Vector2 _resultsScrollPosition = Vector2.zero;
	protected string _cursor = "";
	
	protected int LastRootIndex
	{
		get
		{
			return PlayerPrefs.GetInt(boxitClient.ClientID + "LastRootIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt(boxitClient.ClientID + "LastRootIndex", Mathf.Clamp(value, 0, 1));
		}
	}
	
	protected int LastThumbnailFormatIndex
	{
		get
		{
			return PlayerPrefs.GetInt(boxitClient.ClientID + "LastThumbnailFormatIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt(boxitClient.ClientID + "LastThumbnailFormatIndex", Mathf.Clamp(value, 0, Enum.GetValues(typeof(Boxit.THUMBNAIL_FORMAT)).Length-1));
		}
	}	

	protected int LastThumbnailSizeIndex
	{
		get
		{
			return PlayerPrefs.GetInt(boxitClient.ClientID + "LastThumbnailSizeIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt(boxitClient.ClientID + "LastThumbnailSizeIndex", Mathf.Clamp(value, 0, Enum.GetValues(typeof(Boxit.THUMBNAIL_SIZE)).Length-1));
		}
	}	
	
	public Boxit.BoxitClient boxitClient;
	
	void Start () 
	{
        _noBorderStyle = new GUIStyle();
        _noBorderStyle.margin = new RectOffset(0, 0, 0, 0);
        _noBorderStyle.padding = new RectOffset(0, 0, 0, 0);		
	}
	
	void OnGUI()
	{
        if (_controller == null)
            return;

        if (!_controller.InControl)
        {
            GUILayout.BeginVertical();

            GUILayout.Space(BORDER);

            GUILayout.BeginHorizontal();

            GUILayout.Space(BORDER);

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Menu", GUILayout.Width(45.0f)))
            {
                _controller.DisableMode(this.gameObject);
            }
            GUILayout.Label(_title, GUILayout.Width(TITLE_WIDTH));
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
                GUILayout.Space(20.0f);
                DrawActionSection();
            }

            GUILayout.Space(10.0f);

            if (!string.IsNullOrEmpty(_error))
            {
                GUILayout.Label(_error);
            }
            else
            {
                if (_progress != -1.0f)
                {
                    GUILayout.Label("Progress: " + (_progress * 100) + "%");
                }

                GUILayout.Space(10.0f);

                if (_thumbnail != null)
                {
                    GUILayout.Label(_thumbnail, _noBorderStyle, GUILayout.Width(_thumbnail.width), GUILayout.Height(_thumbnail.height));
                }

                GUILayout.Space(10.0f);

                if (!string.IsNullOrEmpty(_results))
                {
                    GUILayout.BeginVertical("box");
                    GUILayout.Label("Results");
                    _resultsScrollPosition = GUILayout.BeginScrollView(_resultsScrollPosition);
                    GUILayout.Label(_results);
                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                }
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
	}
	
	private void LinkSuccess(long requestID, Boxit.oAuthToken accessToken)
	{
		Debug.Log ("Successfully linked to Dropbox!");
	}
	
	protected virtual void DrawActionSection()
	{
	}	
	
	protected Boxit.ROOT GetSelectedRoot()
	{
		return (Boxit.ROOT)Enum.Parse(typeof(Boxit.ROOT), _roots[LastRootIndex]);
	}
	
	protected Boxit.THUMBNAIL_FORMAT GetSelectedThumbnailFormat()
	{
		return (Boxit.THUMBNAIL_FORMAT)Enum.Parse(typeof(Boxit.THUMBNAIL_FORMAT), _thumbnailFormats[LastThumbnailFormatIndex]);
	}	

	protected Boxit.THUMBNAIL_SIZE GetSelectedThumbnailSize()
	{
		return (Boxit.THUMBNAIL_SIZE)Enum.Parse(typeof(Boxit.THUMBNAIL_SIZE), _thumbnailSizes[LastThumbnailSizeIndex]);
	}	
	
	protected bool StringIsLong(string numberString, out Int64 result)
	{
		if (string.IsNullOrEmpty(numberString))
		{
			result = 0;
			return true;
		}
		else
		{
			result = 0;
			return System.Int64.TryParse(numberString, out result);
		}
	}
	
	protected void DrawRootSelector()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Root", GUILayout.Width(FIELD_LABLE_WIDTH));
		GUILayout.Space(FIELD_SPACER_WIDTH);
		LastRootIndex = GUILayout.SelectionGrid(LastRootIndex, _roots, 2, GUILayout.Width(ROOT_SELECTION_WIDTH));
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();		
	}
	
	protected void DrawTextField(string fieldName, ref string fieldValue)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(fieldName, GUILayout.Width(FIELD_LABLE_WIDTH));
		GUILayout.Space(FIELD_SPACER_WIDTH);
		fieldValue = GUILayout.TextField(fieldValue, GUILayout.Width(INTPUT_WIDTH));
		GUILayout.EndHorizontal();		
	}
	
	protected void DrawToggleField(string fieldName, ref bool fieldValue)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(fieldName, GUILayout.Width(FIELD_LABLE_WIDTH));
		GUILayout.Space(FIELD_SPACER_WIDTH);
		fieldValue = GUILayout.Toggle(fieldValue, "");
		GUILayout.EndHorizontal();				
	}
	
	protected void DrawThumbnailFormatSelector()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Thumbnail Format", GUILayout.Width(FIELD_LABLE_WIDTH));
		GUILayout.Space (FIELD_SPACER_WIDTH);
		LastThumbnailFormatIndex = GUILayout.SelectionGrid(LastThumbnailFormatIndex, _thumbnailFormats, Enum.GetValues(typeof(Boxit.THUMBNAIL_FORMAT)).Length, GUILayout.Width(INTPUT_WIDTH));
		GUILayout.EndHorizontal();
	}
	
	protected void DrawThumbnailSizeSelector()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Thumbnail Size", GUILayout.Width(FIELD_LABLE_WIDTH));
		GUILayout.Space (FIELD_SPACER_WIDTH);
		LastThumbnailSizeIndex = GUILayout.SelectionGrid(LastThumbnailSizeIndex, _thumbnailSizes, Enum.GetValues(typeof(Boxit.THUMBNAIL_SIZE)).Length, GUILayout.Width(INTPUT_WIDTH));
		GUILayout.EndHorizontal();
	}
	
	protected void ResetResults()
	{
		_results = null;
		_error = null;
		_progress = -1.0f;
		_thumbnail = null;
	}

    public void Initialize(DemoController controller)
    {
        _controller = controller;
    }
}
