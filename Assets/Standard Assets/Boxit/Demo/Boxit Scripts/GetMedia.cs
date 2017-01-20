using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to get a media link to a file. This is similar to GetShareLink, except
/// that GetMedia will bypass the Dropbox webserver, used to provide a preview of 
/// a file, so that you can effectively stream contents of your media.
/// </summary>
public class GetMedia : _Demo_Base
{
	void Awake()
	{
		_title = "Get Media";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Remote File", ref _remotePath);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Get Media", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Get the media link
			boxitClient.GetMedia(
                                GetSelectedRoot(),                  // [REQ] Root from which to start
                                _remotePath,                        // [REQ] The file from which to get a media link. The path is relative to the root
                                Success,                            // [REQ] The success delegate to call when the process is complete
                                Failure,                            // [opt] The failure delegate to call if the process fails
                                (_locale == "" ? null : _locale)    // [opt] The locale to apply to the ShareLink results
								);
		}	
	}

    /// <summary>
    /// This delegate is called if the procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The ShareLink structure where the results are stored</param>
	private void Success(long requestID, Boxit.ShareLink shareLink)
	{
		_results = "Success: " + shareLink.ToString();
	}

    /// <summary>
    /// This delegate is called if the procedure fails. If this delegate is not
    /// specified, then Boxit will display an error log instead
    /// </summary>
    /// <param name="requestID">The ID of the request that failed</param>
    /// <param name="error">Error description</param>
	private void Failure(long requestID, string error)
	{
		_results = "Failure: " + error;
	}	
}
