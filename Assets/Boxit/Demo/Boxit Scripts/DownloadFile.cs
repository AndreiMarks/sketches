using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to download a file from Dropbox to your local
/// device. The local path is relative to the application's persistent
/// data path (working directory).
/// </summary>
public class DownloadFile : _Demo_Base 
{
	void Awake()
	{
		_title = "Download File";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Remote File Name", ref _remotePath);
		DrawTextField("Local File Name", ref _localPath);
		DrawToggleField("Overwrite Local File", ref _overwriteLocal);
		DrawTextField("Rev", ref _rev);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Download", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Download the file
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
			boxitClient.DownloadFile(
                                    GetSelectedRoot(),              // [REQ] Root from which to start
									_remotePath,                    // [REQ] The file to download. The path is relative to the root
									_localPath,                     // [REQ] The local path to download to. The path is relative to your application's persistent data path (working directory)
                                    Success,                        // [REQ] The success delegate to call when the process is complete
                                    Failure,                        // [opt] The failure delegate to call if the process fails
                                    Warning,                        // [opt] The warning delegate to call if the local file exists, but overwrite is set to false
                                    Progress,                       // [opt] The progress delegate to call as the download occurs
                                    _overwriteLocal,                // [opt] Whether or not to overwrite the local file. If set to false and the local file exist, the warning delegate will be called
									(_rev == "" ? null : _rev)      // [opt] The file's rev. This is useful to download a specific revision of a remote file that is not the current revision. See GetRevisions or GetMetaData for info on how to get the rev
									);
		}	
	}

	/// <summary>
    /// This delegate is called if the procedure completes successfully.
	/// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
	/// <param name="fullLocalFilePath">The full path to the local file after the download. If you want the local path, just reference the path used when calling DownloadFile</param>
	private void Success(long requestID, string fullLocalFilePath)
	{
		_results = "Success: " + fullLocalFilePath;
	}	
	
    /// <summary>
    /// This delegate is called if the local file exists, but overwrite is set to false. If this delegate
    /// is not specified, then Unity will output a warning log instead
    /// </summary>
    /// <param name="requestID">The ID of the request</param>
    /// <param name="warning">The warning text</param>
	private void Warning(long requestID, string warning)
	{
		_results = "Warning: " + warning;
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
	
    /// <summary>
    /// This delegate is updated as the file progress changes
    /// </summary>
    /// <param name="requestID">The ID of the request in progress</param>
    /// <param name="progress">The value of the progress between zero (0) and one (1.0f)</param>
	private void Progress(long requestID, float progress)
	{
		_progress = progress;
	}
}
