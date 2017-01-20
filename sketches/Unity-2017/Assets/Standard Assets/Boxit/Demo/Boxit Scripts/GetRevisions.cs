using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This demo shows how to get a file's revision history. This can be useful for restoring or
/// downloading a particular version of a file. This can also be useful to allow the user
/// to manage their backups and restores from within the application.
/// </summary>
public class GetRevisions : _Demo_Base 
{
	void Awake()
	{
		_title = "Get Revisions for a File";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Remote File", ref _remotePath);
		DrawTextField("Rev Limit", ref _revLimit);
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Get Revisions", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (CheckInput())
			{
				ResetResults();

                // Get the revisions
                // REQ = required parameter. opt = optional parameter. 
                // Optional parameters don't need to be sent to the function, 
                // but will be here for full illustration.
                boxitClient.GetRevisions(
                                            GetSelectedRoot(),                  // [REQ] Root from which to start
                                            _remotePath,                        // [REQ] The file from which to get the revision history. The path is relative to the root
                                            Success,                            // [REQ] The success delegate to call when the process is complete
                                            Failure,                            // [opt] The failure delegate to call if the process fails
                                            _revLimitNumber,                    // [opt] When listing a file, the service will not report listings containing more than the amount specified and will instead respond with a 406 (Not Acceptable) status response. Default = 10. Max = 1,000
                                            (_locale == "" ? null : _locale)    // [opt] The locale to apply to the MetaData results
										    );
			}
		}	
	}
	
	private bool CheckInput()
	{
		if (!StringIsLong(_revLimit, out _revLimitNumber))
		{
			_error = "Rev Limit must be a number";
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
		_results = "Success - List: " + metaDataList.Count.ToString() + " found. ";
		
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
