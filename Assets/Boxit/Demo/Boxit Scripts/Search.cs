using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This demo shows how to search for files and folders based on a query string.
/// </summary>
public class Search : _Demo_Base
{
	void Awake()
	{
		_title = "Search";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Remote Path", ref _remotePath);
		DrawTextField("Query", ref _query);
		DrawTextField("File Limit", ref _fileLimit);
		DrawToggleField("Include Deleted", ref _includeDeleted);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Search", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (CheckInput())
			{
				ResetResults();

                // Search for the query string
                // REQ = required parameter. opt = optional parameter. 
                // Optional parameters don't need to be sent to the function, 
                // but will be here for full illustration.
				boxitClient.Search(
                                    GetSelectedRoot(),                  // [REQ] Root from which to start
                                    _remotePath,                        // [REQ] The path from which to start searching. The remote path is relative to root.
									_query,                             // [REQ] The query string to search for in the files and folders
                                    Success,                            // [REQ] The success delegate to call when the process is complete
                                    Failure,                            // [opt] The failure delegate to call if the process fails
                                    _fileLimitNumber,                   // [opt] The maximum and default value is 1,000. No more than fileLimit search results will be returned.
                                    _includeDeleted,                    // [opt] If this parameter is set to true, then files and folders that have been deleted will also be included in the search.
                                    (_locale == "" ? null : _locale)    // [opt] The locale to apply to the MetaData results
									);
			}
		}	
	}
	
	private bool CheckInput()
	{
		if (!StringIsLong(_fileLimit, out _fileLimitNumber))
		{
			_error = "File Limit must be a number";
			return false;
		}
		
		return true;
	}

    /// <summary>
    /// This delegate is called if the procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The List of MetaDatas where the results are stored</param>
	private void Success(long requestID, List<Boxit.MetaData> metaDataList)
	{
		_results = "Success - Results: " + metaDataList.Count.ToString() + " found. ";
		
		foreach (Boxit.MetaData metaData in metaDataList)
		{
			_results += metaData.ToString() + "\n";
		}
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
