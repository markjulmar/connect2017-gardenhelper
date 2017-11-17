using System;
using System.IO;
using System.Threading.Tasks;

namespace GardenHelper.Interfaces
{
    public interface IIdentifyPicture
    {
        Task<string> IdentifyAsync(Func<Stream> picture);
    }
}
