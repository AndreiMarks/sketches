using UnityEngine;
using System;
using System.Collections;
using System.IO;
using Boxit;

public class DropboxController : Controller<DropboxController>
{
    public BoxitClient client;

    private FileInfo _lastFileInfo;

    private void GetAccountInfo()
    {
        client.GetAccountInfo( OnGetAccountInfoSuccess );    
    }

    private void OnDownloadSuccess( long requestID, string message )
    {
        Debug.Log( "Download successful: " + message );
    }

    private void OnLinkSuccess( long requestID, oAuthToken token )
    {
        Debug.Log("Link successful.");
        client.GetAccountInfo( OnGetAccountInfoSuccess );    
    }

    private void OnLinkSuccessResync( long requestID, oAuthToken token )
    {
        Debug.Log("Resync");
        SyncFileWithDropbox( _lastFileInfo );
    }

    private void OnLinkFailure( long requestID, string error )
    {
        Debug.Log( "Link failed. Error: " + error );
    }

    private void OnSyncSuccess( long requestID, DateTime successTime )
    {
        Debug.Log("Succeeded.");
    }

    private void OnSyncFailure( long requestID, string error )
    {
        Debug.Log("Failed.");
    }

    private void OnGetAccountInfoSuccess( long requestID, AccountInfo accountInfo )
    {
    } 

    #region Public functions
    public void TryLink()
    {
        if ( client.IsLinked ) 
        {
            Debug.Log("Already linked.");
            return;
        }

        client.Link( OnLinkSuccess, OnLinkFailure );
    }
    
    public void Unlink()
    {
        Debug.Log("Unlinking.");
        client.Unlink();
    }

    public void DownloadFromDropbox( FileInfo file )
    {
        if ( !client.IsLinked )
        {
            Debug.Log( "Client was not linked! Linking." );
            return;
        }

        client.DownloadFile( Boxit.ROOT.sandbox,
                            file.Name,
                            file.Name,
                            OnDownloadSuccess );
    }

    public void SyncFileWithDropbox( FileInfo file )
    {
        if ( !client.IsLinked )
        {
            Debug.Log( "Client was not linked! Linking." );
            return;
        }
        client.SyncFile( Boxit.ROOT.sandbox,
                            file.Name,
                            file.Name,
                            OnSyncSuccess,
                            OnSyncFailure );
    }
    #endregion
}
