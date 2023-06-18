using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Java.Util;
using File = Java.IO.File;
using Environment = Android.OS.Environment;
using Android.Media;
using Stream = System.IO.Stream;
using Xamarin.Forms.Platform.Android;
using Proiect_Licenta;
using Android.App;
using Android.Graphics;

[assembly: Dependency(typeof(SaveAndroid))]

class SaveAndroid : ISave
{
	public static Activity activity { get; set; }

	public async System.Threading.Tasks.Task<byte[]> CaptureAsync()
	{
		var activity1 = Forms.Context as Activity;

		var view = activity1.Window.DecorView;
		view.DrawingCacheEnabled = true;

		Bitmap bitmap = view.GetDrawingCache(true);
       Bitmap target = Bitmap.CreateBitmap(bitmap,0, 240, Convert.ToInt32(bitmap.Width - bitmap.Width * 0.001), Convert.ToInt32(bitmap.Height - bitmap.Height * 0.30));

	    byte[] bitmapData;

        using (var stream = new MemoryStream())
		{
            //bitmap.Height = bitmap.Height-360;
            target.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
			bitmapData = stream.ToArray();
		}

		return bitmapData;
	}

	//Method to save document as a file in Android and view the saved document
	public async Task SaveAndView(string fileName, String contentType, MemoryStream stream)
    {

            var fileUri= saveToGallery(stream);
       
            var intent = new Intent();
            intent.SetFlags(ActivityFlags.ClearTop);
            intent.SetFlags(ActivityFlags.NewTask);
            intent.SetAction(Intent.ActionSend);
            intent.SetType("*/*");
            intent.PutExtra(Intent.ExtraStream, fileUri);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);

            var chooserIntent = Intent.CreateChooser(intent, "Send File");
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(chooserIntent);
        
    

    }
    
    public Android.Net.Uri saveToGallery(MemoryStream root)
    {
        Android.Net.Uri contentCollection = null;


        ContentResolver resolver = Android.App.Application.Context.ContentResolver;

        if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Q)
        {
            contentCollection = MediaStore.Downloads.GetContentUri(MediaStore.VolumeExternalPrimary);
        }
        else
        {
            contentCollection = MediaStore.Downloads.ExternalContentUri;
        }

        ContentValues valuesmedia = new ContentValues();
        valuesmedia.Put(MediaStore.MediaColumns.DisplayName, "Output.pdf");
        valuesmedia.Put(MediaStore.MediaColumns.MimeType, "application/pdf");
        valuesmedia.Put(MediaStore.MediaColumns.RelativePath, Environment.DirectoryDownloads);

        Android.Net.Uri savedPdfUri = resolver.Insert(contentCollection, valuesmedia);
        try
        {
            Stream outdata = resolver.OpenOutputStream(savedPdfUri);
            outdata.Write(root.ToArray());

            valuesmedia.Clear();
            Toast.MakeText(Android.App.Application.Context, "Pdf saved to Downloads", ToastLength.Short).Show();
        }
        catch (Exception exception)
        {
            Toast.MakeText(Android.App.Application.Context, "Pdf not saved to Downloads", ToastLength.Short).Show();
        }
        return savedPdfUri;
    }



}
