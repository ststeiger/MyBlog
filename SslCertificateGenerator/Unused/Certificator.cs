
// https://gist.github.com/ststeiger/aa7da5031e689fd66527af86ad962cc0
// https://dotnetcodr.com/2016/01/21/using-client-certificates-in-net-part-4-working-with-client-certificates-in-code/
namespace SslCertificateGenerator 
{


    public class Certificator
    {


        public static string GetPath(System.Security.Cryptography.X509Certificates.X509Store store )
        {
            System.Type t = typeof(System.Security.Cryptography.X509Certificates.X509Store);
            System.Reflection.FieldInfo fi = t.GetField("_storePal"
                , System.Reflection.BindingFlags.Instance 
                | System.Reflection.BindingFlags.NonPublic
            );

            object obj = fi.GetValue(store);
            // System.Console.WriteLine(obj);

            // System.Type tt = System.Type.GetType("Internal.Cryptography.Pal.DirectoryBasedStoreProvider, System.Security.Cryptography.X509Certificates");
            System.Type tt = obj.GetType();
            // System.Console.WriteLine(obj);
            
            // private readonly string _storePath;
            System.Reflection.FieldInfo fi2 = tt.GetField("_storePath"
                , System.Reflection.BindingFlags.Instance 
                | System.Reflection.BindingFlags.NonPublic 
                | System.Reflection.BindingFlags.FlattenHierarchy);


            if (fi2 != null)
            {
                // object obj2 = fi2.GetValue(System.Convert.ChangeType(obj, tt));
                object obj2 = fi2.GetValue(obj);
                string path = System.Convert.ToString(obj2);
                return path;
            }

            return null;
        }


        // CurrentUser.My:/root/.dotnet/corefx/cryptography/x509stores/my
        // CurrentUser.Root:/root/.dotnet/corefx/cryptography/x509stores/root
        public static void ListCertificates()
        {
            System.Console.WriteLine("\r\nExists Certs Name and Location");
            System.Console.WriteLine("------ ----- -------------------------");
            
            foreach (System.Security.Cryptography.X509Certificates.StoreLocation storeLocation in (System.Security.Cryptography.X509Certificates.StoreLocation[]) 
                System.Enum.GetValues(typeof(System.Security.Cryptography.X509Certificates.StoreLocation)))
            {
                foreach (System.Security.Cryptography.X509Certificates.StoreName storeName in (System.Security.Cryptography.X509Certificates.StoreName[])
                    System.Enum.GetValues(typeof(System.Security.Cryptography.X509Certificates.StoreName)))
                {
                    using (System.Security.Cryptography.X509Certificates.X509Store store = new System.Security.Cryptography.X509Certificates.X509Store(storeName, storeLocation))
                    { 

                        try
                        {
                            store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.OpenExistingOnly);
                            string path = GetPath(store);
                            System.Console.WriteLine("{0}.{1}:{2}", storeLocation, storeName, path);
                            // Console.WriteLine("Yes {0,4}  {1}, {2}", store.Certificates.Count, store.Name, store.Location);
                        }
                        catch (System.Security.Cryptography.CryptographicException)
                        {
                            // Console.WriteLine("No {0}, {1}", store.Name, store.Location);
                        }

                    } // End Using store 

                } // Next storeName 

                System.Console.WriteLine();
            } // Next storeLocation 

        } // End Sub ListCertificates 


        public static System.Security.Cryptography.X509Certificates.X509Certificate2 CreateRootCertificate()
        {
            return null;
        }


        /// <summary>
        ///     Ensure certificates are setup (creates root if required).
        ///     Also makes root certificate trusted based on initial setup from proxy constructor for user/machine trust.
        /// </summary>
        public void EnsureRootCertificate()
        {
            System.Security.Cryptography.X509Certificates.X509Certificate2 rootCertificate = null;
            bool machineTrustRoot = false;

            if (rootCertificate == null)
            {
                rootCertificate = CreateRootCertificate();
            }

            TrustRootCertificate(rootCertificate, machineTrustRoot);
        } // End Sub EnsureRootCertificate 


        /// <summary>
        ///     Trusts the root certificate in user store, optionally also in machine store.
        ///     Machine trust would require elevated permissions (will silently fail otherwise).
        /// </summary>
        public void TrustRootCertificate(
            System.Security.Cryptography.X509Certificates.X509Certificate2 cert, 
            bool machineTrusted = false)
        {
            // currentUser\personal
            InstallCertificate(cert, System.Security.Cryptography.X509Certificates.StoreName.My
                , System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser);

            if (!machineTrusted)
            {
                // currentUser\Root
                InstallCertificate(cert, System.Security.Cryptography.X509Certificates.StoreName.Root
                    , System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser);
            }
            else
            {
                // current system
                InstallCertificate(cert, System.Security.Cryptography.X509Certificates.StoreName.My
                    , System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine);

                // this adds to both currentUser\Root & currentMachine\Root
                InstallCertificate(cert, System.Security.Cryptography.X509Certificates.StoreName.Root
                    , System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine);
            }
        } // End Sub TrustRootCertificate 


        private static System.Security.Cryptography.X509Certificates.X509Certificate Find(
            string serialNumber, System.Security.Cryptography.X509Certificates.StoreLocation location)
        {
            using (System.Security.Cryptography.X509Certificates.X509Store store = 
                new System.Security.Cryptography.X509Certificates.X509Store(location))
            {
                store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.OpenExistingOnly);
                System.Security.Cryptography.X509Certificates.X509Certificate2Collection certs = 
                    store.Certificates.Find(
                        System.Security.Cryptography.X509Certificates.X509FindType
                        .FindBySerialNumber, serialNumber, true);

                //return certs.OfType<X509Certificate>().FirstOrDefault();

                foreach (System.Security.Cryptography.X509Certificates.X509Certificate2 thisCertificate 
                    in certs)
                {
                    return thisCertificate;
                }

            }

            return null;
        }

        public static bool certExists()
        {
            // TODO: 
            // StoreLocation.CurrentUser)

            using (System.Security.Cryptography.X509Certificates.X509Store store = 
                new System.Security.Cryptography.X509Certificates.X509Store(System.Security.Cryptography.X509Certificates.StoreName.Root, System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine))
            {
                store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);

                System.Security.Cryptography.X509Certificates.X509Certificate2Collection certificates = 
                    store.Certificates.Find(
                        System.Security.Cryptography.X509Certificates.X509FindType.FindBySubjectName,
                        "subjectName",
                        false
                );

                if (certificates != null && certificates.Count > 0)
                {
                    System.Console.WriteLine("Certificate already exists");
                    return true;
                }
            }

            return false;
        }


        public static void PrintCertificateInfo(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
        {
            System.Console.WriteLine("Name: {0}", certificate.FriendlyName);
            System.Console.WriteLine("Issuer: {0}", certificate.IssuerName.Name);
            System.Console.WriteLine("Subject: {0}", certificate.SubjectName.Name);
            System.Console.WriteLine("Version: {0}", certificate.Version);
            System.Console.WriteLine("Valid from: {0}", certificate.NotBefore);
            System.Console.WriteLine("Valid until: {0}", certificate.NotAfter);
            System.Console.WriteLine("Serial number: {0}", certificate.SerialNumber);
            System.Console.WriteLine("Signature Algorithm: {0}", certificate.SignatureAlgorithm.FriendlyName);
            System.Console.WriteLine("Thumbprint: {0}", certificate.Thumbprint);
            System.Console.WriteLine();
        }

        public static void EnumCertificates(System.Security.Cryptography.X509Certificates.StoreName name
            , System.Security.Cryptography.X509Certificates.StoreLocation location)
        {
            using (System.Security.Cryptography.X509Certificates.X509Store store = 
                new System.Security.Cryptography.X509Certificates.X509Store(name, location))
            {
                try
                {
                    store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);
                    foreach (System.Security.Cryptography.X509Certificates.X509Certificate2 certificate 
                        in store.Certificates)
                    {
                        PrintCertificateInfo(certificate);
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {
                    store.Close();
                }
            }
        }

        public static void EnumCertificates(
              string name
            , System.Security.Cryptography.X509Certificates.StoreLocation location)
        {
            using (System.Security.Cryptography.X509Certificates.X509Store store = 
                new System.Security.Cryptography.X509Certificates.X509Store(name, location))
            {
                try
                {
                    store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);
                    foreach (System.Security.Cryptography.X509Certificates.X509Certificate2 certificate 
                        in store.Certificates)
                    {
                        PrintCertificateInfo(certificate);
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {
                    store.Close();
                }
            }
        }


        // <summary>
        ///     For CertificateEngine.DefaultWindows to work we need to also check in personal store
        /// </summary>
        /// <param name="storeLocation"></param>
        /// <returns></returns>
        private bool RootCertificateInstalled(
            System.Security.Cryptography.X509Certificates.StoreLocation storeLocation, 
            string issuer)
        {
            //string issuer = $"{RootCertificate.Issuer}";
            // && (CertificateEngine != CertificateEngine.DefaultWindows
            return FindCertificates(
                System.Security.Cryptography.X509Certificates.StoreName.Root, storeLocation, issuer).Count > 0  
                       || FindCertificates(
                           System.Security.Cryptography.X509Certificates.StoreName.My, 
                           storeLocation, issuer).Count > 0;
        } // End Function FindCertificates 


        private static System.Security.Cryptography.X509Certificates.X509Certificate2Collection 
            FindCertificates(
              System.Security.Cryptography.X509Certificates.StoreName storeName
            , System.Security.Cryptography.X509Certificates.StoreLocation storeLocation
            , string findValue)
        {
            System.Security.Cryptography.X509Certificates.X509Certificate2Collection results = null;

            using (System.Security.Cryptography.X509Certificates.X509Store x509Store = 
                new System.Security.Cryptography.X509Certificates.X509Store(storeName, storeLocation))
            {
                try
                {
                    x509Store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.OpenExistingOnly);
                    results = x509Store.Certificates.Find(
                        System.Security.Cryptography.X509Certificates.X509FindType
                        .FindBySubjectDistinguishedName, findValue, false);
                }
                finally
                {
                    x509Store.Close();
                }

            } // End Using x509Store 

            return results;
        } // End Function FindCertificates 

        // https://www.codeguru.com/csharp/csharp/cs_misc/security/article.php/c16491/Working-with-Digital-Certificates-in-NET.htm
        public static bool ExportCertificate(
           string certificateName,
           string path,
           System.Security.Cryptography.X509Certificates.StoreName storeName,
           System.Security.Cryptography.X509Certificates.StoreLocation location)
        {
            bool success = false;

            System.Security.Cryptography.X509Certificates.X509Store store = 
                new System.Security.Cryptography.X509Certificates.X509Store(storeName, location);

            store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2Collection certs
                   = store.Certificates.Find(
                       System.Security.Cryptography.X509Certificates.X509FindType.FindBySubjectName, 
                       certificateName, true
                );

                if (certs != null && certs.Count > 0)
                {
                    byte[] data = certs[0].Export(
                        System.Security.Cryptography.X509Certificates.X509ContentType.Cert
                    );
                    success = WriteFile(data, path);
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                store.Close();
            }

            return success;
        }

        public static bool ExportCertificate(
           string certificateName,
           string path,
           string storeName,
           System.Security.Cryptography.X509Certificates.StoreLocation location)
        {
            bool success = false;

            using (System.Security.Cryptography.X509Certificates.X509Store store = 
                new System.Security.Cryptography.X509Certificates.X509Store(storeName, location))
            {
                store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);
                try
                {
                    System.Security.Cryptography.X509Certificates.X509Certificate2Collection certs
                       = store.Certificates.Find(
                           System.Security.Cryptography.X509Certificates.X509FindType.FindBySubjectName, 
                           certificateName, true
                    );

                    if (certs != null && certs.Count > 0)
                    {
                        byte[] data = certs[0].Export(
                            System.Security.Cryptography.X509Certificates.X509ContentType.Cert
                            );
                        success = WriteFile(data, path);
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {
                    store.Close();
                }
            }

            return success;
        }

        private static bool WriteFile(byte[] data, string filename)
        {
            bool ret = false;
            try
            {
                using (System.IO.FileStream f = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    f.Write(data, 0, data.Length);
                    f.Flush();
                    f.Close();
                }
                
                ret = true;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return ret;
        }



        /// <summary>
        ///     Loads root certificate from current executing assembly location with expected name rootCert.pfx.
        /// </summary>
        /// <returns></returns>
        public System.Security.Cryptography.X509Certificates.X509Certificate2 
            LoadRootCertificate(string fileName
            , string pfxPassword)
        {
            return LoadRootCertificate(fileName, pfxPassword, 
                System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable
            );
        } // End Function LoadRootCertificate 


        /// <summary>
        ///     Loads root certificate from current executing assembly location with expected name rootCert.pfx.
        /// </summary>
        /// <returns></returns>
        public System.Security.Cryptography.X509Certificates.X509Certificate2 
            LoadRootCertificate(
            string fileName,
            string pfxPassword,
            System.Security.Cryptography.X509Certificates.X509KeyStorageFlags StorageFlag )
        {
            bool pfxFileExists = System.IO.File.Exists(fileName);
            if (!pfxFileExists)
            {
                return null;
            }

            try
            {
                return new System.Security.Cryptography.X509Certificates.X509Certificate2(
                    fileName, pfxPassword, StorageFlag);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        } // End Function LoadRootCertificate 



        /// <summary>
        ///     Remove the Root Certificate trust
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="storeLocation"></param>
        /// <param name="certificate"></param>
        public static void UninstallCertificate(
              System.Security.Cryptography.X509Certificates.X509Certificate2 certificate
            , System.Security.Cryptography.X509Certificates.StoreName storeName
            , System.Security.Cryptography.X509Certificates.StoreLocation storeLocation
            )
        {
            if (certificate == null)
            {
                throw new System.Exception("Could not remove certificate as it is null or empty.");
            }

            using (System.Security.Cryptography.X509Certificates.X509Store x509Store = 
                new System.Security.Cryptography.X509Certificates.X509Store(storeName, storeLocation))
            {
                try
                {
                    x509Store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadWrite);
                    x509Store.Remove(certificate);
                }
                catch (System.Exception e)
                {
                    throw new System.Exception("Failed to remove root certificate trust "
                                      + $" for {storeLocation} store location. You may need admin rights.", e);
                }
                finally
                {
                    x509Store.Close();
                }
            } // End Using x509Store 

        } // End Sub UninstallCertificate 



        /// <summary>
        ///     Make current machine trust the Root Certificate used by this proxy
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="storeLocation"></param>
        /// <param name="certificate"></param>
        public static void InstallCertificate(
              System.Security.Cryptography.X509Certificates.X509Certificate2 certificate
            , System.Security.Cryptography.X509Certificates.StoreName storeName
            , System.Security.Cryptography.X509Certificates.StoreLocation storeLocation
            
        )
        {
            
            if (certificate == null)
            {
                throw new System.Exception("Could not install certificate as it is null or empty.");
            }

            using (System.Security.Cryptography.X509Certificates.X509Store x509Store = 
                new System.Security.Cryptography.X509Certificates.X509Store(storeName, storeLocation))
            {
                // todo
                // also it should do not duplicate if certificate already exists
                try
                {
                    x509Store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadWrite);
                    x509Store.Add(certificate);
                }
                catch (System.Exception e)
                {
                    throw new System.Exception("Failed to make system trust root certificate "
                                      + $" for {storeName}\\{storeLocation} store location. You may need admin rights.",
                            e);
                }
                finally
                {
                    x509Store.Close();
                }
            } // End Using x509Store 

        } // End Sub InstallCertificate 


    } // End Class Certificator 
    

} // End Namespace RedmineMailService.CertSSL 
