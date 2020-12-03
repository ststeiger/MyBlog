
using System;


namespace SslCertificateGenerator
{


    class Program
    {


        static void Main(string[] args)
        {
            string pfxLocation = "";
            string password = "";

            // PfxGenerator
            // KeyImportExport

            // https://stackoverflow.com/questions/50227580/create-x509certificate2-from-pem-file-in-net-core
            // https://stackoverflow.com/questions/48905438/digital-signature-in-c-sharp-without-using-bouncycastle

            // Org.BouncyCastle.X509.X509Certificate
            // Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);

            // var x509 = new Org.BouncyCastle.X509.X509CertificateParser(); ;
            // x509.ReadCertificate()

            // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2.createfrompem?view=net-5.0
            // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2.createfrompemfile?view=net-5.0
            // https://github.com/dotnet/runtime/issues/19581


            System.Security.Cryptography.X509Certificates.X509Certificate2 cert =
                new System.Security.Cryptography.X509Certificates.X509Certificate2(
                  pfxLocation
                , password);

            Console.WriteLine("Hello World!");
        }


    }


}
