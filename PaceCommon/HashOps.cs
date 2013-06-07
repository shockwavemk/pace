using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace PaceCommon
{
    /*
     * Verifing that a String is Uncorrupted during Transmission - Reference: 690ff c# Cookbook
     */
    public class HashOps
    {
        public static string CreateStringHash(string unHashedString)
        {
            var encodedUnHashedString = Encoding.Unicode.GetBytes(unHashedString);
            var hashingObj = new SHA256Managed();
            var hashCode = hashingObj.ComputeHash(encodedUnHashedString);
            var hashBase64 = Convert.ToBase64String(hashCode);
            var stringWithHash = unHashedString + hashBase64;
            hashingObj.Clear();
            return (stringWithHash);
        }

        public static bool TestReceivedStringHash(string stringWithHash, out string originalStr)
        {
            if (stringWithHash.Length < 45)
            {
                originalStr = null;
                return (true);
            }

            var hashCodeString = stringWithHash.Substring(stringWithHash.Length - 44);
            var unHashedString = stringWithHash.Substring(0, stringWithHash.Length - 44);
            var hashCode = Convert.FromBase64String(hashCodeString);
            var encodedUnHashedString = Encoding.Unicode.GetBytes(unHashedString);
            var hashingObj = new SHA256Managed();
            var receivedHashCode = hashingObj.ComputeHash(encodedUnHashedString);
            bool hasBeenTamperedWith = false;
            
            for (int counter = 0; counter < receivedHashCode.Length; counter++)
            {
                if (receivedHashCode[counter] != hashCode[counter])
                {
                    hasBeenTamperedWith = true;
                    break;
                }
            }

            if (!hasBeenTamperedWith)
            {
                originalStr = unHashedString;
            }
            else
            {
                originalStr = null;
            }

            hashingObj.Clear();
            return (hasBeenTamperedWith);
        }

        public string CalculateConnectionHash()
        {
            // try to use static information of system to (re-)identify a client - for example if ip changed because of reconnect or restart of system
            // TODO: make it more secure
            return CreateStringHash(GetFqdn());
        }

        public static string GetFqdn()
        {
            string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string hostName = Dns.GetHostName();
            string fqdn;
            if (!hostName.Contains(domainName))
                fqdn = hostName + "." + domainName;
            else
                fqdn = hostName;

            return fqdn;
        }
    }
}
