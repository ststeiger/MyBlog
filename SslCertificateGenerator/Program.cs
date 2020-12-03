
using Org.BouncyCastle.Crypto.Parameters;


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

            
            // var parser = new X509CertificateParser();
            // var bouncyCertificate = parser.ReadCertificate(cert.RawData);
            // var algorithm = DigestAlgorithms.GetDigest(bouncyCertificate.SigAlgOid);
            // var signature = new X509Certificate2Signature(cert, algorithm);
            

            string pemOrDerFile = "";
            
            
            
            var kpp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate cert = kpp.ReadCertificate(System.IO.File.OpenRead(pemOrDerFile));
            Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);
            System.Security.Cryptography.X509Certificates.X509Certificate msCert = Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);
            var ms2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(msCert);


            Org.BouncyCastle.Pkcs.AsymmetricKeyEntry keyEntry = null; //store.GetKey(alias);
            Org.BouncyCastle.Crypto.AsymmetricKeyParameter privateKey = keyEntry.Key;
            
            ms2.PrivateKey = Org.BouncyCastle.Security.DotNetUtilities.ToRSA((Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters)keyEntry.Key);

            byte[] ba = System.IO.File.ReadAllBytes("");


            System.ReadOnlySpan<char> bs = null;
            
            using (System.IO.TextReader reader = new System.IO.StreamReader(System.IO.File.OpenRead("path"))) 
            {
                //getting Span from string
                bs = System.MemoryExtensions.AsSpan(reader.ReadToEnd());
                // var span = reader.ReadToEnd().AsSpan();
                //getting string from Span
                // var str = span.ToString();
            }

            System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromPem(bs, bs);
            System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromPemFile("cert", "key");
            System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromCertFile("crt");
            
                
                
                
            
            System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 =
                new System.Security.Cryptography.X509Certificates.X509Certificate2(
                  pfxLocation
                , password);

            System.Console.WriteLine("Hello World!");
        }


    }


}
