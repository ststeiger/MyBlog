
namespace SslCertificateGenerator 
{


    public class TestX509Certificate2Signature 
    {


        // https://git.itextsupport.com/projects/I5N/repos/itextsharp/browse/src/core/iTextSharp/text/pdf/security
        public static void Test(System.Security.Cryptography.X509Certificates.X509Certificate2 cert)
        { 
            Org.BouncyCastle.X509.X509CertificateParser parser = new Org.BouncyCastle.X509.X509CertificateParser(); 
            Org.BouncyCastle.X509.X509Certificate bouncyCertificate = parser.ReadCertificate(cert.RawData); 
            string algorithm = DigestAlgorithms.GetDigest(bouncyCertificate.SigAlgOid); 
            X509Certificate2Signature signature = new X509Certificate2Signature(cert, algorithm); 
        }


    } 


}
