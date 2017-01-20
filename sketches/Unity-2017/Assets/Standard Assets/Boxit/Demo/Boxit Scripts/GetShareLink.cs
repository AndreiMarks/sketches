using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to get a share link for a file. Creates and returns a Dropbox link 
/// to files or folders users can use to view a preview of the file in a web browser. This 
/// differs from GetMedia in that user's can preview the file, but cannot stream the media.
/// </summary>
public class GetShareLink : _Demo_Base
{
	void Awake()
	{
		_title = "Get Share Link";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Remote File or Path", ref _remotePath);
		DrawToggleField("Return Short URL", ref _returnShortURL);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Get Share Link", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Get the share link
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.GetShareLink(
                                        GetSelectedRoot(),                  // [REQ] Root from which to start
                                        _remotePath,                        // [REQ] The file from which to get the share link. The path is relative to the root
                                        Success,                            // [REQ] The success delegate to call when the process is complete
                                        Failure,                            // [opt] The failure delegate to call if the process fails
                                        _returnShortURL,                    // [opt] When true (default), the url returned will be shortened using the Dropbox url shortener. If false, the url will link directly to the file's preview page.
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
