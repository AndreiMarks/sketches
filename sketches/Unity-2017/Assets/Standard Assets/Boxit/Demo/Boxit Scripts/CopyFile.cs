using UnityEngine;
using System.Collections;

/// <summary>
/// This demo copies a file from one location to another remotely. This
/// saves you from having to download a file, then reupload to a new
/// location. This method is much faster and saves bandwidth.
/// </summary>
public class CopyFile : _Demo_Base 
{
	void Awake()
	{
		_title = "Copy File From Path";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		// These controls gather the required and optional parameters for our function call
		DrawRootSelector();
		DrawToggleField("Use Copy Ref", ref _useCopyRef);
		if (_useCopyRef)
		{
			DrawTextField("From Copy Ref", ref _copyRef);
		}
		else
		{
			DrawTextField("From Remote File Name", ref _remotePath);
		}
		DrawTextField("To Remote File Name", ref _toRemotePath);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Copy File", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

			// If we are copying from a CopyRef, then we'll call that function.
			// A CopyRef is just a reference to a file that is returned from
			// the GetCopyRef function. 
            // REQ = required parameter. opt = optional parameter.
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            if (_useCopyRef)
			{
				boxitClient.CopyFileFromCopyRef(
										GetSelectedRoot(),					// [REQ] Root from which to start
										_copyRef, 							// [REQ] Reference to the file obtained by calling GetCopyRef
										_toRemotePath, 						// [REQ] The final location for the copied file (folder & file)
										Success, 							// [REQ] The success delegate to call when the process is complete
										Failure, 							// [opt] The failure delegate to call if the process fails
										(_locale == "" ? null : _locale)	// [opt] The locale to apply to the MetaData results
										);
			}
			else
			{
				boxitClient.CopyFileFromPath(
										GetSelectedRoot(),					// [REQ] Root from which to start
										_remotePath, 						// [REQ] The location from which to copy (folder & file)
										_toRemotePath, 						// [REQ] The final location for the copied file (folder & file)
										Success, 							// [REQ] The success delegate to call when the process is complete
										Failure, 							// [opt] The failure delegate to call if the process fails
										(_locale == "" ? null : _locale)	// [opt] The locale to apply to the MetaData results
										);
			}
		}	
	}
	
	/// <summary>
	/// This delegate is called when the procedure has completed
	/// </summary>
	/// <param name='requestID'>
	/// The ID of the request that finished
	/// </param>
	/// <param name='metaData'>
	/// The MetaData of the copied file
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
