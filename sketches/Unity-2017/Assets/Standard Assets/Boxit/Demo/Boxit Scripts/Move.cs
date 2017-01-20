using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to move a file or folder remotely. This saves you from having to 
/// download locally, delete remotely, then re-upload a file. The process will be faster and
/// use less bandwidth.
/// </summary>
public class Move : _Demo_Base 
{
	void Awake()
	{
		_title = "Move";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Move Remote Folder or File From", ref _remotePath);
		DrawTextField("Move Remote Folder or File To", ref _toRemotePath);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Move", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Move the remote file or folder
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
			boxitClient.Move(
                                GetSelectedRoot(),                  // [REQ] Root from which to start
                                _remotePath,                        // [REQ] The file or folder to move from. The path is relative to the root
								_toRemotePath,                      // [REQ] The final file or folder path to move to. The path is relative to the root.
                                Success,                            // [REQ] The success delegate to call when the process is complete
                                Failure,                            // [opt] The failure delegate to call if the process fails
                                (_locale == "" ? null : _locale)    // [opt] The locale to apply to the MetaData results
								);
		}	
	}

    /// <summary>
    /// This delegate is called if the procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The MetaData structure where the results are stored</param>
	private void Success(long requestID, Boxit.MetaData metaData)
	{
		_results = "Success: " + metaData.ToString();
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
