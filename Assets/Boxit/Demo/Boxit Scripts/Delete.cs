using UnityEngine;
using System.Collections;

/// <summary>
/// This demo deletes a file or folder remotely.
/// </summary>
public class Delete : _Demo_Base 
{
	void Awake()
	{
		_title = "Delete";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Delete Remote Folder or File", ref _remotePath);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Delete", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Delete the file
            // REQ = required parameter. opt = optional parameter.
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.Delete(
								GetSelectedRoot(),                  // [REQ] Root from which to start
								_remotePath,                        // [REQ] The file or folder to delete. The path is relative to the root.
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
    /// <param name="metaData">The MetaData of the file or folder that is deleted</param>
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
