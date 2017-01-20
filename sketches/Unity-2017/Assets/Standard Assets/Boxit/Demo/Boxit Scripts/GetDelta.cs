using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to get the changes that have occured to a Dropbox account.
/// The cursor keeps track of the last time the Delta was called. You can then 
/// call Delta again with the cursor to get a snapshot of changes between cursors.
/// If no cursor is supplied, all changes are listed.
/// </summary>
public class GetDelta : _Demo_Base {

	void Awake()
	{
		_title = "Get Delta";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Cursor", ref _cursor);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Get Delta", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Call Delta on the Dropbox account
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.GetDelta(
								_cursor,                            // [REQ] though the cursor is required, you can pass a blank ("") to get all changes
                                Success,                            // [REQ] The success delegate to call when the process is complete
                                Failure,                            // [opt] The failure delegate to call if the process fails
                                (_locale == "" ? null : _locale)    // [opt] The locale to apply to the Delta results
								);
		}	
	}

    /// <summary>
    /// This delegate is called if the procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The Delta structure where the results are stored</param>
	private void Success(long requestID, Boxit.Delta delta)
	{
		_results = "Success - Results: " + delta.ToString();
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
