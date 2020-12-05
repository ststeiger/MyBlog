
namespace SslCertificateGenerator
{


    public class Program
    {


        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            // 1. Root certificate to pfx 
            // 2. Read root certificate 
            // 3. Sign SSL certificate
            Test();

            // https://www.selfsignedcertificate.com/
            // https://askubuntu.com/questions/73287/how-do-i-install-a-root-certificate
            // https://www.digicert.com/kb/csr-ssl-installation/nginx-openssl.htm
            // https://serverfault.com/questions/10854/nginx-https-serving-with-same-config-as-http
            // https://nginx.org/en/docs/http/configuring_https_servers.html#single_http_https_server
            // https://www.thesslstore.com/knowledgebase/ssl-install/nginx-ssl-installation/
            // https://www.digitalocean.com/community/tutorials/how-to-secure-nginx-with-let-s-encrypt-on-ubuntu-18-04
            // https://www.digitalocean.com/community/tutorials/how-to-create-a-self-signed-ssl-certificate-for-nginx-in-ubuntu-18-04

            ReadPkFromCertificate();

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();

            await System.Threading.Tasks.Task.CompletedTask;
        } // End Sub Main 


        public static void ReadPkFromCertificate()
        {
            string pfxLocation = @"obelix.pfx";


            Org.BouncyCastle.Pkcs.Pkcs12Store store = null;

            using (System.IO.Stream pfxStream = System.IO.File.OpenRead(pfxLocation))
            {
                store = new Org.BouncyCastle.Pkcs.Pkcs12Store(pfxStream, "".ToCharArray());
            }

            System.Console.WriteLine(store);

            foreach (string alias in store.Aliases)
            {
                System.Console.WriteLine(alias);

                // https://7thzero.com/blog/bouncy-castle-convert-a-bouncycastle-asymmetrickeyentry-to-a-.ne
                if (store.IsKeyEntry(alias))
                {
                    Org.BouncyCastle.Pkcs.AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                    System.Console.WriteLine(keyEntry);
                    Org.BouncyCastle.Crypto.AsymmetricKeyParameter privateKey = keyEntry.Key;
                    System.Console.WriteLine(privateKey.IsPrivate);
                } // End if (store.IsKeyEntry((string)alias))


                Org.BouncyCastle.Pkcs.X509CertificateEntry certEntry = store.GetCertificate(alias);
                Org.BouncyCastle.X509.X509Certificate cert = certEntry.Certificate;
                System.Console.WriteLine(cert);

                Org.BouncyCastle.Crypto.AsymmetricKeyParameter publicKey = cert.GetPublicKey();
                System.Console.WriteLine(publicKey);

                // Org.BouncyCastle.Pkcs.X509CertificateEntry[] chain = store.GetCertificateChain(alias);

                // System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(cert.GetEncoded());
                // Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);

                System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(pfxLocation);
                // cert2.PrivateKey = null;

                if (cert2.HasPrivateKey)
                {
                    System.Console.WriteLine(cert2.PrivateKey);
                }

            }

        }



        public static void Test()
        {
            // https://www.digitalocean.com/community/tutorials/how-to-secure-nginx-with-let-s-encrypt-on-ubuntu-18-04

            // Many times nginx -s reload does not work as expected.
            // On many systems(Debian, etc.), you would need to use /etc/init.d/nginx reload.

            Org.BouncyCastle.Security.SecureRandom random = new Org.BouncyCastle.Security.SecureRandom(NonBackdooredPrng.Create());

            Org.BouncyCastle.X509.X509Certificate rootCertificate = GenerateRootCertificate();

            PrivatePublicPemKeyPair kpk = new PrivatePublicPemKeyPair();
            kpk.PrivateKey = @"issuer_priv.pem";
            kpk.PrivateKey = System.IO.File.ReadAllText(kpk.PrivateKey);


            // SelfSignSslCertificate(random, rootCertificate, kpk);


            System.Security.Cryptography.X509Certificates.X509Certificate2 c0 = new System.Security.Cryptography.X509Certificates.X509Certificate2("obelix.pfx", "");


            // c0.PrivateKey
            // c0.PublicKey;


            System.Security.Cryptography.X509Certificates.X509Certificate2 c1 = System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromPemFile(@"obelix.crt", @"obelix_priv.pem");
            // System.Security.Cryptography.X509Certificates.X509Certificate2 c1 = System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromPemFile(@"obelix.cer", @"obelix_priv.pem"); // Wrong! Doesn't work



            // https://stackoverflow.com/questions/50227580/create-x509certificate2-from-pem-file-in-net-core
            // https://stackoverflow.com/questions/48905438/digital-signature-in-c-sharp-without-using-bouncycastle

            // Org.BouncyCastle.X509.X509Certificate
            // Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);

            // Org.BouncyCastle.X509.X509CertificateParser x509 = new Org.BouncyCastle.X509.X509CertificateParser(); 
            // x509.ReadCertificate()

            // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2.createfrompem?view=net-5.0
            // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2.createfrompemfile?view=net-5.0
            // https://github.com/dotnet/runtime/issues/19581


        } // End Sub Test 


        public static void SelfSignSslCertificate(Org.BouncyCastle.Security.SecureRandom random, Org.BouncyCastle.X509.X509Certificate caRoot, Org.BouncyCastle.Crypto.AsymmetricKeyParameter rootCertPrivateKey) // PrivatePublicPemKeyPair subjectKeyPair)
        {
            Org.BouncyCastle.X509.X509Certificate caSsl = null;

            string countryIso2Characters = "GA";
            string stateOrProvince = "Aremorica";
            string localityOrCity = "Erquy, Bretagne";
            string companyName = "Coopérative Ménhir Obelix Gmbh & Co. KGaA";
            string division = "NT (Neanderthal Technology)";
            string domainName = "localhost";
            domainName = "*.sql.guru";
            domainName = "localhost";
            string email = "webmaster@localhost";


            CertificateInfo ci = new CertificateInfo(
                  countryIso2Characters, stateOrProvince
                , localityOrCity, companyName
                , division, domainName, email
                , System.DateTime.UtcNow
                , System.DateTime.UtcNow.AddYears(5)
            );

            ci.AddAlternativeNames("localhost", System.Environment.MachineName, "127.0.0.1",
            "sql.guru", "*.sql.guru");

            // Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateEcKeyPair(curveName, random);
            Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateRsaKeyPair(2048, random);
            // Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateDsaKeyPair(1024, random);
            // Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateDHKeyPair(1024, random);



            ci.SubjectKeyPair = KeyImportExport.GetPemKeyPair(kp1);
            // ci.IssuerKeyPair.PrivateKey = rootCert.PrivateKey;

            // caSsl = CerGenerator.GenerateSslCertificate(ci, random, caRoot);

            Org.BouncyCastle.Crypto.AsymmetricKeyParameter subjectPublicKey = null;
            // This is the private key of the root certificate 
            Org.BouncyCastle.Crypto.AsymmetricKeyParameter issuerPrivateKey = null; 

            caSsl = CerGenerator.GenerateSslCertificate(
                  ci
                , subjectPublicKey
                , issuerPrivateKey
                , caRoot
                , random
            );


            CertificateToDerPem(caSsl);



            // Just to clarify, an X.509 certificate does not contain the private key
            // The whole point of using certificates is to send them more or less openly, 
            // without sending the private key, which must be kept secret.
            // An X509Certificate2 object may have a private key associated with it (via its PrivateKey property), 
            // but that's only a convenience as part of the design of this class.
            // System.Security.Cryptography.X509Certificates.X509Certificate2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(caRoot.GetEncoded());
            // System.Console.WriteLine(cc.PublicKey);
            // System.Console.WriteLine(cc.PrivateKey);

            bool val = CerGenerator.ValidateSelfSignedCert(caSsl, caRoot.GetPublicKey());
            System.Console.WriteLine(val);

            PfxGenerator.CreatePfxFile(@"obelix.pfx", caSsl, kp1.Private, "");
            CerGenerator.WritePrivatePublicKey("obelix", ci.SubjectKeyPair);


            CerGenerator.WriteCerAndCrt(@"ca", caRoot);
            CerGenerator.WriteCerAndCrt(@"obelix", caSsl);


        } // End Sub SelfSignSslCertificate 


        // https://stackoverflow.com/questions/51703109/nginx-the-ssl-directive-is-deprecated-use-the-listen-ssl
        public static Org.BouncyCastle.X509.X509Certificate GenerateRootCertificate()
        {
            string countryIso2Characters = "EA";
            string stateOrProvince = "Europe";
            string localityOrCity = "NeutralZone";
            string companyName = "Skynet Earth Inc.";
            string division = "Skynet mbH";
            string domainName = "Skynet";
            string email = "root@sky.net";


            Org.BouncyCastle.Security.SecureRandom sr = new Org.BouncyCastle.Security.SecureRandom(NonBackdooredPrng.Create());

            Org.BouncyCastle.X509.X509Certificate caRoot = null;
            Org.BouncyCastle.X509.X509Certificate caSsl = null;

            // string curveName = "curve25519"; curveName = "secp256k1";


            CertificateInfo caCertInfo = new CertificateInfo(
                  countryIso2Characters, stateOrProvince
                , localityOrCity, companyName
                , division, domainName, email
                , System.DateTime.UtcNow
                , System.DateTime.UtcNow.AddYears(5)
            );


            // Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateEcKeyPair(curveName, sr);
            Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateRsaKeyPair(2048, sr);
            // Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateDsaKeyPair(1024, sr);
            // Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp1 = KeyGenerator.GenerateDHKeyPair(1024, sr);

            // kp1 = KeyGenerator.GenerateGhostKeyPair(4096, s_secureRandom.Value);

            caCertInfo.SubjectKeyPair = KeyImportExport.GetPemKeyPair(kp1);
            caCertInfo.IssuerKeyPair = KeyImportExport.GetPemKeyPair(kp1);


            caRoot = CerGenerator.GenerateRootCertificate(caCertInfo, sr);


            PfxGenerator.CreatePfxFile(@"ca.pfx", caRoot, kp1.Private, null);
            CerGenerator.WritePrivatePublicKey("issuer", caCertInfo.IssuerKeyPair);

            return caRoot;
        } // End Sub GenerateRootCertificate 


        public static void BouncyCert()
        {
            string pemOrDerFile = "";

            Org.BouncyCastle.X509.X509CertificateParser kpp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate cert = kpp.ReadCertificate(System.IO.File.OpenRead(pemOrDerFile));
            Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);
            System.Security.Cryptography.X509Certificates.X509Certificate msCert = Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);
            System.Security.Cryptography.X509Certificates.X509Certificate2 ms2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(msCert);

            Org.BouncyCastle.Pkcs.AsymmetricKeyEntry keyEntry = null; //store.GetKey(alias);
            Org.BouncyCastle.Crypto.AsymmetricKeyParameter privateKey = keyEntry.Key;

            ms2.PrivateKey = Org.BouncyCastle.Security.DotNetUtilities.ToRSA((Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters)keyEntry.Key);
        }


        public static void CertificateToDerPem(Org.BouncyCastle.X509.X509Certificate caSsl)
        {
            System.Security.Cryptography.X509Certificates.X509Certificate inputCert1 = null;
            inputCert1 = Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(caSsl);

            System.Security.Cryptography.X509Certificates.X509Certificate2 inputCert2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(inputCert1);


            string pemOrDerFile = "foo.derpem";

            string foo = CerGenerator.ToPem(caSsl.GetEncoded());
            System.IO.File.WriteAllText(pemOrDerFile, foo, System.Text.Encoding.ASCII);

            Org.BouncyCastle.X509.X509CertificateParser kpp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate cert = kpp.ReadCertificate(System.IO.File.OpenRead(pemOrDerFile));
            System.Console.WriteLine(cert);

            System.Security.Cryptography.X509Certificates.X509Certificate dotCert1 = null;
            dotCert1 = Org.BouncyCastle.Security.DotNetUtilities.ToX509Certificate(cert);

            System.Security.Cryptography.X509Certificates.X509Certificate2 dotCert = new System.Security.Cryptography.X509Certificates.X509Certificate2(dotCert1);

            System.Console.WriteLine(dotCert.PublicKey);
            System.Console.WriteLine(dotCert.PrivateKey);
        }



        public static System.Security.Cryptography.X509Certificates.X509Certificate2 CertFromPem()
        {
            System.ReadOnlySpan<char> bs = null;

            using (System.IO.TextReader reader = new System.IO.StreamReader(System.IO.File.OpenRead("path")))
            {
                //getting Span from string
                bs = System.MemoryExtensions.AsSpan(reader.ReadToEnd());
                // bs = reader.ReadToEnd().AsSpan(); // requires using System; 
                // getting string from Span
                // string str = span.ToString();
            }

            System.Security.Cryptography.X509Certificates.X509Certificate2 c1 = System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromPemFile("cert", "key");
            // System.Security.Cryptography.X509Certificates.X509Certificate2 c2 = System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromPem(bs, bs);
            // System.Security.Cryptography.X509Certificates.X509Certificate2 c3 = System.Security.Cryptography.X509Certificates.X509Certificate2.CreateFromCertFile("crt");

            return c1;
        }


        public static System.Security.Cryptography.X509Certificates.X509Certificate2 CertFromPfx()
        {
            string pfxLocation = "";
            string password = "";

            System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 =
                  new System.Security.Cryptography.X509Certificates.X509Certificate2(
                  pfxLocation
                , password
            );

            return cert2;
        }


    } // End Class Program 


} // End Namespace SslCertificateGenerator 
