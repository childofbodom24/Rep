using System;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Make10.Interfaces;
using System.Threading.Tasks;
using System.IO;

[assembly: Dependency(typeof(Make10.Droid.DependencyServices.ImageGalleryService))]
namespace Make10.Droid.DependencyServices
{
    public class ImageGalleryService : IImageGalleryService
    {
        public void GetImageStream(Action<Stream> onRead)
        {
            MainActivity.Instance.OnRead = onRead;

            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);
        }
    }
}