using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Make10.Interfaces
{
    public interface IImageGalleryService
    {
        void GetImageStream(Action<Stream> onRead);
    }
}
