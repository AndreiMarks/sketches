using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows some of the more advanced calls that are actually just a series of basic calls
/// 
/// DownloadFileIfRemoteNewer: Downloads a file only if the remote file has a more recent modified date than the local file.
/// UpladFileIfLocalNewer: Uploads a file only if the local file has a more recent modified / creation date than the remote file.
/// SyncFile: Will make sure that the most recent file exists locally and remotely.
/// </summary>
public class SyncFunctions : _Demo_Base 
{
	void Awake()
	{
		_title = "Sync Functions";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Local File Name", ref _localPath);
		DrawTextField("Remote File Name", ref _remotePath);
		
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Download If Remote Newer", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();
			
            // Download the file if the remote file is more recent
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
			boxitClient.DownloadFileIfRemoteNewer(
                                                    GetSelectedRoot(),  // [REQ] Root from which to start
                                                    _remotePath,        // [REQ] The path from which to download. The remote path is relative to root.
													_localPath,         // [REQ] The path to save to. The local path is relative to the application's persistent data path (working directory)
                                                    DownloadSuccess,    // [REQ] The success delegate to call when the process is complete
                                                    Failure,            // [opt] The failure delegate to call if the process fails
                                                    Progress            // [opt] The progress delegate to call as the download occurs
													);
		}			
		
		if (GUILayout.Button("Upload if Local Newer", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

			// Upload the file if the local file is more recent
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.UploadFileIfLocalNewer(
												GetSelectedRoot(),  // [REQ] Root from which to start
                                                _localPath,         // [REQ] The path to save to. The local path is relative to the application's persistent data path (working directory)
                                                _remotePath,        // [REQ] The path to upload to. The remote path is relative to root.
                                                UploadSuccess,      // [REQ] The success delegate to call when the process is complete
                                                Failure,            // [opt] The failure delegate to call if the process fails
                                                Progress            // [opt] The progress delegate to call as the download occurs
												);
		}	
		
		if (GUILayout.Button("Sync", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Make sure the most recent file is local and remote
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.SyncFile(
                                GetSelectedRoot(),  // [REQ] Root from which to start
                                _localPath,         // [REQ] The path to save to. The local path is relative to the application's persistent data path (working directory)
                                _remotePath,        // [REQ] The path from which to download or upload. The remote path is relative to root.
                                SyncSuccess,        // [REQ] The success delegate to call when the process is complete
                                Failure,            // [opt] The failure delegate to call if the process fails
                                Progress            // [opt] The progress delegate to call as the download occurs
								);
		}		
		
		GUILayout.EndHorizontal();
	}

    /// <summary>
    /// This delegate is called if the DownloadFileIfRemoteNewer procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The full path to the local file after the download. If you want the local path, just reference the path used when calling DownloadFile</param>
	private void DownloadSuccess(long requestID, string localFilePath)
	{
		_results = "Success: " + localFilePath;
	}

    /// <summary>
    /// This delegate is called if the UploadFileIfLocalNewer procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The MetaData structure where the results are stored</param>
	private void UploadSuccess(long requestID, Boxit.MetaData metaData)
	{
		_results = "Success: " + metaData.ToString();
	}

    /// <summary>
    /// This delegate is called if the SyncFile procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The date and time of the most recent file</param>
	private void SyncSuccess(long requestID, System.DateTime syncDate)
	{
		_results = "Success: " + syncDate.ToString("MM/dd/yyyy HH:mm:ss");
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
