using UnityEngine;
using System.Collections;

/// <summary>
/// This demo creates a folder remotely.
/// </summary>
public class CreateFolder : _Demo_Base 
{
	void Awake()
	{
		_title = "Create Folder";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		// These controls gather the required and optional parameters for our function call
		DrawRootSelector();
		DrawTextField("Remote Folder Path", ref _remotePath);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Create Folder", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			// Create the folder
            // REQ = required parameter. opt = optional parameter.
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.CreateFolder(
									GetSelectedRoot(),					// [REQ] Root from which to start
									_remotePath, 						// [REQ] Folder to create. You can supply nested directories and Dropbox will create them all.
									Success, 							// [REQ] The success delegate to call when the process is complete
									Failure,							// [opt] The failure delegate to call if the process fails
									(_locale == "" ? null : _locale)	// [opt] The locale to apply to the MetaData results
									);
		}	
	}
	
	/// <summary>
	/// This delegate is called when the procedure has completed
	/// </summary>
	/// <param name='requestID'>
	/// The ID of the request that finished
	/// </param>
	/// <param name='metaData'>
	/// The MetaData of the new directory
	/// </param>	
	private void Success(long requestID, Boxit.MetaData metaData)
	{
		_results = "Success: " + metaData.ToString();
	}	
	
	/// <summary>
	/// This delegate is called if the procedure fails. If this delegate is not
	/// specified, then Boxit will display an error log instead
	/// </summary>
	/// <param name='requestID'>
	/// The ID of the request that failed
	/// </param>
	/// <param name='error'>
	/// Error description
	/// </param>	
	private void Failure(long requestID, string error)
	{
		_results = "Failure: " + error;
	}
}
