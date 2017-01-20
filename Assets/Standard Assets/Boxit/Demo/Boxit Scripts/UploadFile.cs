using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to upload a file to Dropbox
/// </summary>
public class UploadFile : _Demo_Base 
{	
	void Awake()
	{
		_title = "Upload File";
	}	
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Local File Name", ref _localPath);
		DrawTextField("Remote File Name", ref _remotePath);
		DrawToggleField("Overwrite Remote File", ref _overwriteRemote);
		DrawTextField("Parent Rev", ref _parentRev);
		DrawTextField("Locale", ref _locale);

		GUILayout.EndVertical();
		
		if (GUILayout.Button("Upload", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // add a leading forward slash in case the user accidentally forgot it
            if (_remotePath.Substring(0, 1) != @"/")
            {
                _remotePath = @"/" + _remotePath;
            }

            // Upload file
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.UploadFile(GetSelectedRoot(),                       // [REQ] Root from which to start
                                    _localPath,                             // [REQ] The local path to upload from. The path is relative to your application's persistent data path (working directory)
                                    _remotePath,                            // [REQ] The location to upload to. The path is relative to the root
                                    UploadSuccess,                          // [REQ] The success delegate to call when the process is complete
                                    UploadFailure,                          // [opt] The failure delegate to call if the process fails
                                    UploadProgress,                         // [opt] The progress delegate to call as the download occurs
                                    _overwriteRemote,                       // [opt] This value, either true (default) or false, determines what happens when there's already a file at the specified path. If true, the existing file will be overwritten by the new one. If false, the new file will be automatically renamed (for example, test.txt might be automatically renamed to test (1).txt). The new name can be obtained from the returned metadata.
									(_parentRev == "" ? null : _parentRev), // [opt] The revision of the file you're editing. If parentRev matches the latest version of the file on the user's Dropbox, that file will be replaced. Otherwise, the new file will be automatically renamed (for example, test.txt might be automatically renamed to test (conflicted copy).txt). If you specify a revision that doesn't exist, the file will not save (error 400). Get the most recent rev by performing a call to GetMetaData.
                                    (_locale == "" ? null : _locale)        // [opt] The locale to apply to the MetaData results
									);
		}	
	}

    /// <summary>
    /// This delegate is called if the upload procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The MetaData structure where the temporary results are stored. Note 
    /// we will use this data to move to the final name</param>
	private void UploadSuccess(long requestID, Boxit.MetaData metaData)
	{
        if (metaData.Path != _remotePath)
        {
			// the temporarily uploaded file does not match our intended name,
			// so we will move it to its final destination (only happens when using utf-8 characters in the file name)
			MoveTemporaryFile(metaData.Path);
        }
        else
        {
            // the remote file path matched the metadata path, so we are done
            _results = "Success: " + metaData.ToString();
        }
	}
		
    /// <summary>
    /// This delegate is called if the upload procedure fails. If this delegate is not
    /// specified, then Boxit will display an error log instead
    /// </summary>
    /// <param name="requestID">The ID of the request that failed</param>
    /// <param name="error">Error description</param>
	private void UploadFailure(long requestID, string error)
	{
		_results = "Upload Failure: " + error;
	}

    /// <summary>
    /// This delegate is updated as the file progress changes
    /// </summary>
    /// <param name="requestID">The ID of the request in progress</param>
    /// <param name="progress">The value of the progress between zero (0) and one (1.0f)</param>
	private void UploadProgress(long requestID, float progress)
	{
		_progress = progress;
	}
	
	
	
	
	
	
	
	
	
	// All code below this point is only necessary when the uploaded file
	// contains utf-8 characters, such as Russian or Japanese. Otherwise the 
	// file would have uploaded successfully in UploadSuccess. If you know 
	// your file names will not contain utf-8 encoded characters then you
	// can ignore using the following code.
	
	
	
	
	
	/// <summary>
	/// the temporary path of our file if it is uploaded with
	/// utf-8 characters in the file name. If it is, we will
	/// need to later move it to its final destination
	/// </summary>
	private string _tempRemotePath;

	
	/// <summary>
	/// Moves a temporarily uploaded file to its final destination (if possible).
	/// This is only necessary if the file name has utf-8 characters.
	/// </summary>
	/// <param name='tempRemotePath'>The temporary upload path</param>
	private void MoveTemporaryFile(string tempRemotePath)
	{
        // the upload was a success, but now we need to call "Move" to rename the file
        // to its final name. This is only necessary if the file name uses non-western
        // characters, such as Russian or Japanese.

		// first we store the newly uploaded file location in our temporary path variable
		// we'll later use this when moving it to its final destination
		_tempRemotePath = tempRemotePath;
			
		// first we need to see if the file should be overwritten to determine the 
		// chained command path we need to take
		
		if (_overwriteRemote)
		{
			// since we are overwriting any file already there, we will just
			// delete the existing file
			
			boxitClient.Delete(GetSelectedRoot(),
								_remotePath,
								DeleteOverwriteSuccess,
								DeleteOverwriteFailure,
								(_locale == "" ? null : _locale)
								);
		}
		else
		{
			// we are not overwriting, so we will first need to check 
			// if the file already exists to avoid copy or move
			// errors
			
			boxitClient.GetMetaData(GetSelectedRoot(),
									_remotePath,
									GetMetaDataSuccess,
									GetMetaDataFailure
									);
									
			
		}
	}
	
	/// <summary>
    /// This delegate is called if we are overwriting the final destination
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="metaData">The MetaData structure where the results are stored</param>	
	private void DeleteOverwriteSuccess(long requestID, Boxit.MetaData metaData)
	{
		// we successfully deleted the file because we wanted to overwrite it.
		// Now we can move the temporary file into its final destination
		
		boxitClient.Move(GetSelectedRoot(),
							_tempRemotePath,
							_remotePath,
							MoveSuccess,
							MoveFailure,
							(_locale == "" ? null : _locale)
						);
	}
	
	/// <summary>
    /// This delegate is called if we are overwriting the final destination and it failed.
    /// </summary>
    /// <param name="requestID">The ID of the request that failed</param>
    /// <param name="error">Error description</param>
	private void DeleteOverwriteFailure(long requestID, string error)
	{
		if (error.Contains("404 None"))
		{
			// this just means that the final file didn't exist, not really a problem, so we
			// go ahead and move the temporary file

			boxitClient.Move(GetSelectedRoot(),
								_tempRemotePath,
								_remotePath,
								MoveSuccess,
								MoveFailure,
								(_locale == "" ? null : _locale)
							);
		}
		else
		{
			_results = "Delete (overwrite) Failure: " + error;
		}
	}
	
	/// <summary>
    /// This delegate is called if are not overwriting the final destination file. We
    /// need to use this meta data to determine if the newly upload temporary file
    /// can be moved to its final destination (if the final file is deleted or doesn't exist)
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="metaData">The MetaData structure where the results are stored</param>		
	private void GetMetaDataSuccess(long requestID, Boxit.MetaData metaData)
	{
		if (metaData.Is_Deleted)
		{
			// the file exists, but it has been deleted, so it is safe to 
			// move the temporary file to its final destination
			
			boxitClient.Move (GetSelectedRoot(),
								_tempRemotePath,
								_remotePath,
								MoveSuccess,
								MoveFailure,
								(_locale == "" ? null : _locale)
								);
		}
		else
		{
			// the file already exists. Since we have overwrite off, we will
			// just delete the temporary path
			
			boxitClient.Delete(GetSelectedRoot(),
								_tempRemotePath,
								DeleteNoOverwriteSuccess,
								DeleteNoOverwriteFailure,
								(_locale == "" ? null : _locale)
								);
		}
	}
	
	/// <summary>
    /// This delegate is called if we are not overwriting the final file and the
    /// final file does not exist. In the 404 error case, the error is a good thing becuase
    /// it means the final file cannot be found, and thus the temporary file can be
    /// moved safely.
    /// </summary>
    /// <param name="requestID">The ID of the request that failed</param>
    /// <param name="error">Error description</param>	
	private void GetMetaDataFailure(long requestID, string error)
	{
		if (error.Contains("404 None"))
		{
			// no file exists, so we can safely move the existing temporary file into its final destination
			
			boxitClient.Move (GetSelectedRoot(),
								_tempRemotePath,
								_remotePath,
								MoveSuccess,
								MoveFailure,
								(_locale == "" ? null : _locale)
								);
		}
		else
		{
			_results = "Get MetaData Failure: " + error;
		}
	}	
	
	/// <summary>
    /// This delegate is called if the move command is successful.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="metaData">The MetaData structure where the results are stored</param>
    private void MoveSuccess(long requestID, Boxit.MetaData metaData)
    {
        _results = "Success: " + metaData.ToString();
    }

    /// <summary>
    /// This delegate is called if the move procedure fails. If this delegate is not
    /// specified, then Boxit will display an error log instead
    /// </summary>
    /// <param name="requestID">The ID of the request that failed</param>
    /// <param name="error">Error description</param>
    private void MoveFailure(long requestID, string error)
    {
        _results = "Move Failure: " + error;
    }
	
	/// <summary>
	/// This delegate is called if we uploaded with overwrite off and the file already exists.
	/// In this scenario, we just delete the temporarily uploaded file
	/// </summary>
	/// <param name='requestID'>The ID of the request to delete</param>
	/// <param name='metaData'>The meta data of the structure (not relevant since we are deleting a temporary file)</param>
	private void DeleteNoOverwriteSuccess(long requestID, Boxit.MetaData metaData)
	{
		_results = "File already exists";
	}
	
	/// <summary>
	/// This delegate is called if we uploaded with overwrite off and the file already exists.
	/// Something went wrong with the final delete
	/// </summary>
	/// <param name='requestID'>The ID of the request to delete</param>
	/// <param name='metaData'>Error description</param>	
	private void DeleteNoOverwriteFailure(long requestID, string error)
	{
		_results = "Delete (no overwrite) Failure: " + error;
	}
}
