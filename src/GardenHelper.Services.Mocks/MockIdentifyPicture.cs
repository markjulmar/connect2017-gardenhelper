using GardenHelper.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GardenHelper.Services.Mocks
{
    public class MockIdentifyPicture : IIdentifyPicture
    {
        public string ReturnedTag { get; set; }

        public MockIdentifyPicture()
        {
            ReturnedTag = "rose";
        }

        public Task<string> IdentifyAsync(Func<Stream> picture)
        {
            if (string.IsNullOrWhiteSpace(ReturnedTag))
            {
                throw new Exception("Failed to identify picture.");
            }

            return Task.FromResult(ReturnedTag);
        }
    }
}
