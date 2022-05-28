using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Management;
using System.Security.Cryptography;
using System.Security;
using System.Collections;
using System.Text;
using Microsoft.Win32;
using System.Net;
namespace appSchool.ViewModels
{
  public  class MachineActivation
    {

        private static string fingerPrint = string.Empty;
        public bool IsValidMachine(int mCompID)
        {
            bool isvalid = false;
            switch (mCompID)
            { 
                case 1: //Nirmala Convent
                    if (GetMachineValue() == "HPQOEM - 20080905MS1C88R63501860" || GetMachineValue() == "DELL   - 6dBTWW342002NF" || GetMachineValue() == "HPQOEM - 9GEBF416007CE" || GetMachineValue() == "_ASUS_ - 1072009M80-4C007901453" || GetMachineValue() == "LENOVO - 1290                    " || GetMachineValue() == "HPQOEM - 20141105INA503ZYF5")
                        isvalid = true;
                    break;
                case 2: //St. Marks College 
                    if (GetMachineValue() == "LENOVO - 20080704To be filled by O.E.M." || GetMachineValue() == "DELL   - 6dBTWW32400VCW" ||
                                    GetMachineValue() == "LENOVO - 1WB09493313" || GetMachineValue() == "ACRSYS - 1072009To be filled by O.E.M."
                                    || GetMachineValue() == "_ASUS_ - 1072009UH81218230717" || GetMachineValue() == "ACRSYS - 1072009150341087500573" || GetMachineValue() == "_ASUS_ - 1072009M80-4C007901453" || GetMachineValue() == "LENOVO - 1290                    " || GetMachineValue() == "HPQOEM - 20141105INA503ZYF5" || GetMachineValue() == "LGE    - 20100121 " || GetMachineValue() == "LENOVO - 1WB10376387" ||  GetMachineValue() == "HP     - 1SGH750S3LM"
                                    || GetMachineValue() ==  "SUPERM - 1072009ZM203S018450")
                         isvalid = true;
                    break;
                case 3: //KVJ 
                    if (GetMachineValue() == "INTEL  - 13BQFL340001XS" || GetMachineValue() == "_ASUS_ - 1072009M80-4C007901453" || GetMachineValue() == "LENOVO - 1WB09493313" || GetMachineValue() == "_ASUS_ - 13BQFL340001XS" || GetMachineValue() == "LENOVO - 1290                    ")
                        isvalid = true;
                    break;
                case 4: //St Xviear
                    if (GetMachineValue() == "_ASUS_ - 1072009M80-4C007901453" || GetMachineValue() == "ACRSYS - 1072009To be filled by O.E.M." || GetMachineValue() == "ACRSYS - 1072009.DKXFQ12.CN7016344M06QW.           " || GetMachineValue() == "DELL   - 1072009To be filled by O.E.M." || GetMachineValue() == "LENOVO - 1290                    " || GetMachineValue() == "HPQOEM - 20141105INA503ZYF5" || GetMachineValue() == "HP     - 1SGH750S3LM"  || GetMachineValue() ==  "SUPERM - 1072009ZM203S018450") 
                        isvalid = true;
                    break;
                case 5: //St joseph
                    if (GetMachineValue() == "_ASUS_ - 1072009M80-4C007901453" || GetMachineValue() == "ACRSYS - 20110421To be filled by O.E.M." || GetMachineValue() == "ACRSYS - 107200900000000" || GetMachineValue() == "LENOVO - 1290                    " || GetMachineValue() == "HPQOEM - 20141105INA503ZYF5")
                        isvalid = true;
                    break;
                case 6: //St lawrence
                    if (GetMachineValue() == "_ASUS_ - 1072009M80-4C007901453" || GetMachineValue() == "HCLINF - 1072009 " || GetMachineValue() == "HCLINF - 1072009" || GetMachineValue() == "LENOVO - 1290                    " || GetMachineValue() == "HPQOEM - 20141105INA503ZYF5")
                        isvalid = true;
                    break;
                case 7: //shanti dham
                    if (GetMachineValue() == "ACRSYS - 1072009None" || GetMachineValue() == "LENOVO - 1290                    " || GetMachineValue() == "HCLINF - 1072009" || GetMachineValue() == "LENOVO - 1290                    "
                        || GetMachineValue() == "ACRSYS - 1072009.DFB6T72.CN7016359C03XC." || GetMachineValue() == "HPQOEM - 20141105INA503ZYF5")
                        isvalid = true;
                    break;

            }
            
            return isvalid ;
        }
        public static string GetMachineValue()
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = biosId() + baseId();//+ "CPU >> " + cpuId() + "\nBIOS >> " + 
                //biosId() + "\nBASE >> " + baseId()   +             
                //videoId()  ;
            }
            return fingerPrint;
        }
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";

            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return //identifier("Win32_BIOS", "Manufacturer")
                //  identifier("Win32_BIOS", "SMBIOSBIOSVersion");
                //+ identifier("Win32_BIOS", "IdentificationCode")
                //identifier("Win32_BIOS", "SerialNumber");
                //+ identifier("Win32_BIOS", "ReleaseDate")
           identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model");
            //+ identifier("Win32_DiskDrive", "Manufacturer")
            //+ identifier("Win32_DiskDrive", "Signature")
            //+ identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return //identifier("Win32_BaseBoard", "Model")
                //+ identifier("Win32_BaseBoard", "Manufacturer")
                //+ identifier("Win32_BaseBoard", "Name")
             identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
        #endregion
    }
}
