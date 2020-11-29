
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace OnlineYournal
{
    public class Program
    {
        

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        } // End Sub Main 


        // https://github.com/dotnet/aspnetcore/discussions/28238
        // https://github.com/aspnet/KestrelHttpServer/issues/2103
        // https://ayende.com/blog/181281-A/building-a-lets-encrypt-acme-v2-client
        // https://weblog.west-wind.com/posts/2016/feb/22/using-lets-encrypt-with-iis-on-windows
        // https://medium.com/@MaartenSikkema/automatically-request-and-use-lets-encrypt-certificates-in-dotnet-core-9d0d152a59b5
        // https://github.com/dotnet/aspnetcore/issues/1190
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-5.0#code-try-30
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    delegate(IWebHostBuilder webBuilder)
                    {
                        webBuilder.UseStartup<Startup>();
                        // webBuilder.ConfigureKestrel(serverOptions 

                        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-5.0#code-try-30
                        webBuilder.ConfigureKestrel(
                                delegate (Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions serverOptions)
                                {
                                    // On Linux, CipherSuitesPolicy can be used to filter TLS handshakes on a per-connection basis:
                                    serverOptions.ConfigureHttpsDefaults(
                                        delegate (Microsoft.AspNetCore.Server.Kestrel.Https.HttpsConnectionAdapterOptions listenOptions)
                                        {
                                            
                                            listenOptions.OnAuthenticate = 
                                                delegate (Microsoft.AspNetCore.Connections.ConnectionContext connectionContext, System.Net.Security.SslServerAuthenticationOptions sslOptions)
                                                {
                                                    sslOptions.CipherSuitesPolicy = new System.Net.Security.CipherSuitesPolicy(
                                                           new System.Net.Security.TlsCipherSuite[]
                                                           {
                                                            System.Net.Security.TlsCipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256,
                                                            System.Net.Security.TlsCipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384,
                                                            System.Net.Security.TlsCipherSuite.TLS_CHACHA20_POLY1305_SHA256,
                                                               // ...
                                                           });
                                                }; // End OnAuthenticate 

                                        }); // End ConfigureHttpsDefaults 


                                    // serverOptions.Listen(System.Net.IPAddress.Loopback, 5001,
                                    serverOptions.ListenAnyIP(5005, 
                                        delegate (Microsoft.AspNetCore.Server.Kestrel.Core.ListenOptions listenOptions)
                                        {
                                            // listenOptions.UseHttps("testCert.pfx", "testPassword");

                                            

                                            listenOptions.UseHttps(
                                                delegate (Microsoft.AspNetCore.Server.Kestrel.Https.HttpsConnectionAdapterOptions httpsOptions)
                                                {
                                                    System.Security.Cryptography.X509Certificates.X509Certificate2 localhostCert = Microsoft.AspNetCore.Server.Kestrel.Https.CertificateLoader.LoadFromStoreCert(
                                                        "localhost", "My", System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser,
                                                        allowInvalid: true);

                                                    System.Security.Cryptography.X509Certificates.X509Certificate2 exampleCert = Microsoft.AspNetCore.Server.Kestrel.Https.CertificateLoader.LoadFromStoreCert(
                                                        "example.com", "My", System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser,
                                                        allowInvalid: true);

                                                    System.Security.Cryptography.X509Certificates.X509Certificate2 subExampleCert = Microsoft.AspNetCore.Server.Kestrel.Https.CertificateLoader.LoadFromStoreCert(
                                                        "sub.example.com", "My", System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser,
                                                        allowInvalid: true);

                                                    System.Collections.Generic.Dictionary<string, System.Security.Cryptography.X509Certificates.X509Certificate2> certs = 
                                                        new System.Collections.Generic.Dictionary<string, System.Security.Cryptography.X509Certificates.X509Certificate2>(
                                                            System.StringComparer.OrdinalIgnoreCase
                                                    );

                                                    certs["localhost"] = localhostCert;
                                                    certs["example.com"] = exampleCert;
                                                    certs["sub.example.com"] = subExampleCert;


                                                    httpsOptions.ServerCertificateSelector = 
                                                        delegate(Microsoft.AspNetCore.Connections.ConnectionContext connectionContext, string name) 
                                                        {
                                                            if (name != null && certs.TryGetValue(name, out var cert))
                                                            {
                                                                return cert;
                                                            }

                                                            return exampleCert;
                                                        };

                                                }); // End ListenOptions.UseHttps

                                        }); // End ListenAnyIp 

                                }) // End ConfigureKestrel 
                        ;

                    } // End ConfigureWebHostDefaults 
                )
            ;
        } // End Function CreateHostBuilder 


    } // End Class Program 


} // End Namespace OnlineYournal 
