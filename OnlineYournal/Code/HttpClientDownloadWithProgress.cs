
namespace OnlineYournal
{


    // https://stackoverflow.com/questions/20661652/progress-bar-with-httpclient
    public static class StreamExtensions
    {

        public static async System.Threading.Tasks.Task CopyToAsync(this System.IO.Stream source
            , System.IO.Stream destination
            , int bufferSize
            , System.IProgress<long> progress = null
            , System.Threading.CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new System.ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new System.ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new System.ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new System.ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new System.ArgumentOutOfRangeException(nameof(bufferSize));

            byte[] buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            } // Whend 

        } // End Task CopyToAsync 


    } // End Class StreamExtensions 



    public static class HttpClientExtensions
    {


        public static async System.Threading.Tasks.Task Test()
        {
            string url = "https://speed.hetzner.de/100MB.bin";
            // url = "https://speed.hetzner.de/1GB.bin";
            // url = "https://speed.hetzner.de/10GB.bin";

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient()
            {
                Timeout = System.TimeSpan.FromDays(1)
            })
            {
                using (System.IO.Stream stream = System.IO.File.OpenWrite(@"D:\foo.bin"))
                {
                    System.Progress<float> cb = new System.Progress<float>(
                        delegate (float value)
                        {
                            System.Console.WriteLine("Progress: {0}%", value);
                        }
                    );

                    await client.DownloadAsync(url, stream, cb, System.Threading.CancellationToken.None);
                }

            }

        }


        public static async System.Threading.Tasks.Task DownloadAsync(this System.Net.Http.HttpClient client
            , string requestUri
            , System.IO.Stream destination
            , System.IProgress<float> progress = null
            , System.Threading.CancellationToken cancellationToken = default)
        {
            // Get the http headers first to examine the content length
            using (System.Net.Http.HttpResponseMessage response = await client.GetAsync(
                requestUri,
                System.Net.Http.HttpCompletionOption.ResponseHeadersRead)
            )
            {
                long? contentLength = response.Content.Headers.ContentLength;

                using (System.IO.Stream download = await response.Content.ReadAsStreamAsync())
                {

                    // Ignore progress reporting when no progress reporter was 
                    // passed or when the content length is unknown
                    if (progress == null || !contentLength.HasValue)
                    {
                        await download.CopyToAsync(destination);
                        return;
                    }

                    // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
                    System.Progress<long> relativeProgress = new System.Progress<long>(
                        delegate (long totalBytes)
                        {
                            if (contentLength.Value == 0)
                            {
                                progress.Report(99.99999f);
                                return;
                            }

                            float reportedValue = (float)totalBytes * 100.0f / contentLength.Value;
                            if (reportedValue == 100.0f)
                                reportedValue = 99.99999f;

                            progress.Report(reportedValue);
                        }
                    );

                    // Use extension method to report progress while downloading
                    await download.CopyToAsync(destination, 81920, relativeProgress, cancellationToken);
                    progress.Report(100.0f);
                } // End Using download 

            } // End Using response 

        } // End Task DownloadAsync 


    } // End Class HttpClientExtensions 


    public class HttpClientDownloadWithProgress
        : System.IDisposable
    {


        // await HttpClientDownloadWithProgress.Test();
        public static async System.Threading.Tasks.Task Test()
        {
            string url = "https://speed.hetzner.de/100MB.bin";
            // url = "https://speed.hetzner.de/1GB.bin";
            // url = "https://speed.hetzner.de/10GB.bin";

            using (HttpClientDownloadWithProgress client = new HttpClientDownloadWithProgress(url, @"D:\foo.bin"))
            {
                // if(false)
                client.ProgressChanged +=
                    delegate (long? totalFileSize, long totalBytesDownloaded, double? progressPercentage)
                    {

                        if (totalFileSize.HasValue)
                        {
                            System.Console.WriteLine("Total file size: {0}", totalFileSize.Value);
                        }

                        System.Console.WriteLine("Total byte downloaded: {0}", totalBytesDownloaded);

                        if (progressPercentage.HasValue)
                        {
                            System.Console.WriteLine("Progress: {0}%", progressPercentage.Value);
                        }


                    }
                ;

                await client.StartDownload();
            } // End Using client 

        } // End Task Test 



        public delegate void ProgressChangedHandler(
              long? totalFileSize
            , long totalBytesDownloaded
            , double? progressPercentage
        );


        protected readonly string _downloadUrl;
        protected readonly string _destinationFilePath;
        protected System.Net.Http.HttpClient _httpClient;

        public event ProgressChangedHandler ProgressChanged;


        public HttpClientDownloadWithProgress(string downloadUrl, string destinationFilePath)
        {
            _downloadUrl = downloadUrl;
            _destinationFilePath = destinationFilePath;
        } // End Constructor 


        public async System.Threading.Tasks.Task StartDownload()
        {
            _httpClient = new System.Net.Http.HttpClient
            {
                Timeout = System.TimeSpan.FromDays(1)
            };

            using (System.Net.Http.HttpResponseMessage response =
                await _httpClient.GetAsync(
                      _downloadUrl
                    , System.Net.Http.HttpCompletionOption.ResponseHeadersRead)
            )
            {
                await DownloadFileFromHttpResponseMessage(response);
            } // End Using response 

        } // End Task StartDownload 


        protected async System.Threading.Tasks.Task DownloadFileFromHttpResponseMessage(
            System.Net.Http.HttpResponseMessage response
        )
        {
            response.EnsureSuccessStatusCode();

            long? totalBytes = response.Content.Headers.ContentLength;

            using (System.IO.Stream contentStream = await response.Content.ReadAsStreamAsync())
            {
                await ProcessContentStream(totalBytes, contentStream);
            } // End Using contentStream 

        } // End Task DownloadFileFromHttpResponseMessage 


        protected async System.Threading.Tasks.Task ProcessContentStream(
              long? totalDownloadSize
            , System.IO.Stream contentStream
            )
        {
            long totalBytesRead = 0L;
            long readCount = 0L;
            byte[] buffer = new byte[8192];
            bool isMoreToRead = true;

            using (System.IO.Stream stream = new System.IO.FileStream(
                  _destinationFilePath
                , System.IO.FileMode.Create
                , System.IO.FileAccess.Write
                , System.IO.FileShare.None
                , 8192
                , true))
            {
                do
                {
                    int bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                        continue;
                    } // End if (bytesRead == 0) 

                    await stream.WriteAsync(buffer, 0, bytesRead);

                    totalBytesRead += bytesRead;
                    readCount += 1;

                    if (readCount % 100 == 0)
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                } while (isMoreToRead);

            } // End Using fs

        } // End Task ProcessContentStream 

        private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
        {
            if (ProgressChanged == null)
                return;

            double? progressPercentage = null;
            if (totalDownloadSize.HasValue)
                progressPercentage = System.Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

            ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
        } // End Sub TriggerProgressChanged 


        public void Dispose()
        {
            _httpClient?.Dispose();
        } // End Sub Dispose 


    } // End Class HttpClientDownloadWithProgress 


} // End Namespace OnlineYournal 
