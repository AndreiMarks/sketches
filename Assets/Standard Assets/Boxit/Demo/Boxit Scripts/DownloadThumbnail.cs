using UnityEngine;
using System.Collections;

/// <summary>
/// This demo shows how to download a thumbnail of a remote image file. The downloaded
/// thumbnail is stored as a Unity Texture2D, so it can be readily used in a Unity
/// scene. 
/// 
/// The texture format can be:
/// - JPEG: Good for photos
/// - PNG: Good for illustrations, screenshots, logos, and graphics
/// 
/// The texture size can be (width x height):
/// - small:  32   x 32
/// - medium: 64   x 64
/// - large:  128  x 128
/// - s:      64   x 64
/// - m:      128  x 128
/// - l:      640  x 480
/// - xl:     1204 x 768
/// </summary>
public class DownloadThumbnail : _Demo_Base
{
	void Awake()
	{
		_title = "Download Thumbnail";
	}
	
	protected override void DrawActionSection()
	{
		GUILayout.BeginVertical("box");
		
		DrawRootSelector();
		DrawTextField("Remote File", ref _remotePath);
		DrawThumbnailFormatSelector();
		DrawThumbnailSizeSelector();
		
		GUILayout.EndVertical();
		
		if (GUILayout.Button("Download Thumbnail", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			ResetResults();

            // Download the thumbnail
            // REQ = required parameter. opt = optional parameter. 
            // Optional parameters don't need to be sent to the function, 
            // but will be here for full illustration.
			boxitClient.DownloadThumbnail(
                                            GetSelectedRoot(),              // [REQ] Root from which to start
                                            _remotePath,                    // [REQ] The image file from which to create a thumbnail. The path is relative to the root
                                            Success,                        // [REQ] The success delegate to call when the process is complete
                                            Failure,                        // [opt] The failure delegate to call if the process fails
                                            Progress,                       // [opt] The progress delegate to call as the download occurs. Usually only relevant for larger image sizes
								            GetSelectedThumbnailFormat(),   // [opt] The thumbnail format. Default = JPEG
								            GetSelectedThumbnailSize()      // [opt] The thumbnail size. Default = small (32 x 32)
								            );
		}	
	}

    /// <summary>
    /// This delegate is called if the procedure completes successfully.
    /// </summary>
    /// <param name="requestID">The ID of the request that finished</param>
    /// <param name="fullLocalFilePath">The Texture2D where the thumbnail is stored</param>
	private void Success(long requestID, Texture2D thumbnail)
	{
		_thumbnail = thumbnail;
		
		_results = "Success";
	}

    /// <summary>
    /// This delegate is called if the procedure fails. If this delegate is not
    /// specified, then Boxit will display an error log instead
    /// </summary>
    /// <param name="requestID">The ID of the request that failed</param>
    /// <param name="error">Error description</param>
	private void Failure(long requestID, string error)
	{
		_thumbnail = null;
		
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
