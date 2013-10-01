using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using KT.Extensions;
using System.Text.RegularExpressions;

namespace CZAOSMail.Mail
{
    public class AddressHelper
    {
        public const string DELIMITER = ",";

        public AddressHelper(string addresses)
        {
            mstrAddressString = GetValidEmailList(addresses);
        }


        public AddressHelper(MailAddressCollection addresses)
        {
            string strAddresses = string.Empty;
            foreach (MailAddress address in addresses)
            {
                strAddresses += address.Address + DELIMITER;
            }
            mstrAddressString = GetValidEmailList(strAddresses);

        }


        public AddressHelper(List<string> addresses)
        {
            string strAddresses = string.Empty;
            foreach (string address in addresses)
            {
                strAddresses += address + DELIMITER;
            }
            mstrAddressString = GetValidEmailList(strAddresses);

        }

        private int mintValidCount;
        private int mintInvalidCount;

        private string mstrAddressString = string.Empty;
        public int InvalidCount
        {
            get { return mintInvalidCount; }
        }

        public int ValidCount
        {
            get { return mintValidCount; }
        }

        public string AddressString
        {
            //GetValidEmailList(mstrAddressString)
            get { return mstrAddressString; }
        }

        public List<string> AddressList
        {
            get
            {
                List<string> addresses = new List<string>();


                if (mstrAddressString.Contains(DELIMITER))
                {

                    foreach (string item in mstrAddressString.Split(Convert.ToChar(DELIMITER)))
                    {
                        if (item.IsNotNullOrEmpty())
                        {
                            addresses.Add(item.Trim());
                        }

                    }

                    //single address
                }
                else
                {
                    if (mstrAddressString.IsValidEmailAddress())
                    {
                        addresses.Add(mstrAddressString);
                    }

                }

                return addresses;
                //New List(Of String)(mstrAddressString.Split(CChar(DELIMITER)))

            }
        }

        public string[] AddressArray
        {
            //Return Split(mstrAddressString, DELIMITER)
            get { return this.AddressList.ToArray(); }
        }

        public MailAddressCollection AddressCollection
        {
            get
            {
                List<string> validAddresses = this.AddressList;
                MailAddressCollection collection = new MailAddressCollection();

                foreach (string address in validAddresses)
                {
                    collection.Add(address);
                }

                return collection;

            }
        }

        public string GetValidEmailList(string addresses)
        {

            string strReturn = string.Empty;


            if (addresses.Contains(DELIMITER))
            {

                foreach (string address in addresses.Split(Convert.ToChar(DELIMITER)))
                {

                    if (this.MemberIsValidEmailAddress(address))
                    {
                        if (!strReturn.ToLower().Contains(address.ToLower()))
                        {
                            mintValidCount += 1;
                            strReturn += address.Trim() + ",";
                        }

                    }
                    else
                    {
                        mintInvalidCount += 1;
                    }

                }

                return strReturn.TrimEnd(',');
                // Web.UITools.SiteHelpers.TrimEnd(strReturn, ",")

            }
            else
            {
                if (this.MemberIsValidEmailAddress(addresses))
                {
                    mintValidCount = 1;
                    return addresses;
                }
                else
                {
                    mintInvalidCount = 1;
                    return string.Empty;
                }

            }

        }

        #region " Misc "


        public const string EMAIL_REGEX = "^(\\w([&-._\\w]*\\w)*@(\\w[-_\\w]*\\w\\.)+\\w{2,9})$";
        public static bool IsValidEmailAddress(string emailAddress)
        {

            Regex rexEmail = new Regex(EMAIL_REGEX);
            Match rexMatch = default(Match);
            string strEmailAddress = null;
            int intStart = default(int);
            int intEnd = default(int);

            if ((emailAddress == null))
            {
                emailAddress = "";
            }

            strEmailAddress = emailAddress;

            //Parse email to see if in this format: Name <Address>
            // If so, remove the address & just check that
            intStart = emailAddress.IndexOf("<");

            if (intStart > 0)
            {
                intStart += 1;
                intEnd = strEmailAddress.IndexOf(">");

                if (intEnd == 0)
                {
                    intEnd = strEmailAddress.Length;
                }

                strEmailAddress = emailAddress.Substring(intStart, intEnd - intStart).Trim();

            }

            rexMatch = rexEmail.Match(strEmailAddress);

            return rexMatch.Success;

        }

        private bool MemberIsValidEmailAddress(string emailAddress)
        {

            Regex rexEmail = new Regex(EMAIL_REGEX);
            Match rexMatch = default(Match);
            int intStart = default(int);
            int intEnd = default(int);

            //  IsNothing(emailAddress) Then
            if (emailAddress.IsNullOrEmpty())
            {
                return false;
                //emailAddress = ""
            }

            //Parse email to see if in this format: Name <Address>
            // If so, remove the address & just check that
            intStart = emailAddress.IndexOf("<");

            if (intStart > 0)
            {
                intStart += 1;
                intEnd = emailAddress.IndexOf(">");

                if (intEnd == 0)
                {
                    intEnd = emailAddress.Length;
                }

                emailAddress = emailAddress.Substring(intStart, intEnd - intStart).Trim();

            }

            rexMatch = rexEmail.Match(emailAddress);

            return rexMatch.Success;

        }

        #endregion

    }

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik, @toddanglin
    //Facebook: facebook.com/telerik
    //=======================================================

}
