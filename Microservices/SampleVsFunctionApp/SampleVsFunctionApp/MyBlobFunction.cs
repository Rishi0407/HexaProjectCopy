using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace SampleVsFunctionApp
{
    public static class MyBlobFunction
    {
        [FunctionName("MyBlobFunction")]
        public static void Run([BlobTrigger("myfiles/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
