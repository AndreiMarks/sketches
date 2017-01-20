using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// This demo shows how to do a full sync process which comprises three parts:
/// 
/// 1) Download the remote file from Dropbox if it is newer than the local file. This will get all the changes that have occured by other apps accessing our file.
/// 2) Make our updates to the file. This should be done quickly so that there is less chance of other apps "stomping" on our data.
/// 3) Upload the file to Dropbox. Here we don't check the time stamps since we want to force the upload.
/// 
/// This demo processes a text file, so be sure the file you supply can be modified like a text file by appending a string.
/// Note that you can use whatever file you want in your application, but this demo just happens to work on text files.
/// </summary>
public class DownloadUpdateUpload : _Demo_Base 
{
	private string _appendString = "";
	
	void Awake()
	{
		_title = "Download, Update, & Upload";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Local Text File Name", ref _localPath);
		DrawTextField("Remote Text File Name", ref _remotePath);
		DrawTextField("String to Append", ref _appendString);
		
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Update", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();
			
            // 1) Begin the download, update, and upload process with a download if necessary
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
		
		GUILayout.EndHorizontal();
	}

    /// <summary>
    /// This delegate is called if the DownloadFileIfRemoteNewer procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The full path to the local file after the download. If you want the local path, just reference the path used when calling DownloadFile</param>
	private void DownloadSuccess(long requestID, string localFilePath)
	{
		// 2) We have downloaded the file, so now we make our update.
		// Try to minimize the amount of time spent updating so
		// that there is less chance of another app overwriting our
		// data.
		FileInfo fi = new FileInfo(localFilePath);
        using (StreamWriter sw = fi.AppendText()) 
        {
            sw.WriteLine(_appendString);
        }
		
        // 3) We have downloaded and update the file, now we upload. Note that this
		// is not calling UploadFileIfLocalNewer because we want to force the upload.
		// This process only works if the update is done fairly quickly and 
		// there are not a lot of updates occuring at the same time.
		//
        // REQ = required parameter. opt = optional parameter. 
        // Optional parameters don't need to be sent to the function, 
        // but will be here for full illustration.
		boxitClient.UploadFile(
                                GetSelectedRoot(),  // [REQ] Root from which to start
                                _remotePath,        // [REQ] The path from which to download. The remote path is relative to root.
								_localPath,         // [REQ] The path to save to. The local path is relative to the application's persistent data path (working directory)
                                UploadSuccess,    // [REQ] The success delegate to call when the process is complete
                                Failure,            // [opt] The failure delegate to call if the process fails
                                Progress            // [opt] The progress delegate to call as the download occurs
								);		
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
