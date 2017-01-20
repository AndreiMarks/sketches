using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to get a copy reference string for a file. This copy ref
/// can be used to copy a file to another user's Dropbox using the CopyFile function.
/// </summary>
public class GetCopyRef : _Demo_Base
{
	void Awake()
	{
		_title = "Get Copy Ref";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Remote File", ref _remotePath);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Get Copy Ref", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Get the copy ref
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
			boxitClient.GetCopyRef(
                                    GetSelectedRoot(),  // [REQ] Root from which to start
                                    _remotePath,        // [REQ] The file from which to retrieve a copy ref. The path is relative to the root
                                    Success,            // [REQ] The success delegate to call when the process is complete
                                    Failure             // [opt] The failure delegate to call if the process fails
								    );
		}	
	}

    /// <summary>
    /// This delegate is called if the procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The CopyRef structure where the results are stored</param>
	private void Success(long requestID, Boxit.CopyRef copyRef)
	{
		_results = "Success: " + copyRef.ToString();
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
