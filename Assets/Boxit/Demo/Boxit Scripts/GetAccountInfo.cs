using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to retrieve the account information of the logged in user.
/// </summary>
public class GetAccountInfo : _Demo_Base 
{
	void Awake()
	{
		_title = "Get Account Info";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawTextField("Locale", ref _locale);
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Get Account Info", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Get the account information
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
            boxitClient.GetAccountInfo(
                                        Success,                            // [REQ] The success delegate to call when the process is complete
                                        Failure,                            // [opt] The failure delegate to call if the process fails
                                        (_locale == "" ? null : _locale)    // [opt] The locale to apply to the AccountInfo results
										);
		}	
	}

    /// <summary>
    /// This delegate is called if the procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The AccountInfo structure that stores the results</param>
	private void Success(long requestID, Boxit.AccountInfo accountInfo)
	{
		_results = "Success: " + accountInfo.ToString();
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
