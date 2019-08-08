using CVNAPDF417.Contracts.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVNAPDF417.Contracts.Scanning.ParsingStrategies
{
    public sealed class PDF417<T> : Parser<T>
    {
        public static Dictionary<string, string> AbbreviationMap = new Dictionary<string, string>()
        {
            { "DAA", "Full Name"},
            { "DAB", "Last Name"},
            { "DAC", "First Name"},
            { "DAD", "Middle Name"},
            { "DAE", "Name Suffix"},
            { "DAF", "Name Prefix"},
            { "DAG", "Mailing Street Address1"},
            { "DAH", "Mailing Street Address2"},
            { "DAI", "Mailing City"},
            { "DAJ", "Mailing Jurisdiction Code"},
            { "DAK", "Mailing Postal Code"},
            { "DAL", "Residence Street Address1"},
            { "DAM", "Residence Street Address2"},
            { "DAN", "Residence City"},
            { "DAO", "Residence Jurisdiction Code"},
            { "DAP", "Residence Postal Code"},
            { "DAQ", "License or ID Number"},
            { "DAR", "License Classification Code"},
            { "DAS", "License Restriction Code"},
            { "DAT", "License Endorsements Code"},
            { "DAU", "Height in FT_IN"},
            { "DAV", "Height in CM"},
            { "DAW", "Weight in LBS"},
            { "DAX", "Weight in KG"},
            { "DAY", "Eye Color"},
            { "DAZ", "Hair Color"},
            { "DBA", "License Expiration Date"},
            { "DBB", "Date of Birth"},
            { "DBC", "Sex"},
            { "DBD", "License or ID Document Issue Date"},
            { "DBE", "Issue Timestamp"},
            { "DBF", "Number of Duplicates"},
            { "DBG", "Medical Indicator Codes"},
            { "DBH", "Organ Donor"},
            { "DBI", "Non-Resident Indicator"},
            { "DBJ", "Unique Customer Identifier"},
            { "DBK", "Social Security Number"},
            { "DBL", "Date Of Birth"},
            { "DBM", "Social Security Number"},
            { "DBN", "Full Name"},
            { "DBO", "Last Name"},
            { "DBP", "First Name"},
            { "DBQ", "Middle Name"},
            { "DBR", "Suffix"},
            { "DBS", "Prefix"},
            { "DCA", "Virginia Specific Class"},
            { "DCB", "Virginia Specific Restrictions"},
            { "DCD", "Virginia Specific Endorsements"},
            { "DCE", "Physical Description Weight Range"},
            { "DCF", "Document Discriminator"},
            { "DCG", "Country territory of issuance"},
            { "DCH", "Federal Commercial Vehicle Codes"},
            { "DCI", "Place of birth"},
            { "DCJ", "Audit information"},
            { "DCK", "Inventory Control Number"},
            { "DCL", "Race Ethnicity"},
            { "DCM", "Standard vehicle classification"},
            { "DCN", "Standard endorsement code"},
            { "DCO", "Standard restriction code"},
            { "DCP", "Jurisdiction specific vehicle classification description"},
            { "DCQ", "Jurisdiction-specific"},
            { "DCR", "Jurisdiction specific restriction code description"},
            { "DCS", "Last Name"},
            { "DCT", "First Name"},
            { "DCU", "Suffix"},
            { "DDA", "Compliance Type"},
            { "DDC", "HazMat Endorsement Expiry Date"},
            { "DDD", "Limited Duration Document Indicator"},
            { "DDH", "Under 18 Until"},
            { "DDI", "Under 19 Until"},
            { "DDJ", "Under 21 Until"},
            { "DDK", "Organ Donor Indicator"},
            { "DDL", "Veteran Indicator"},
            { "PAA", "Permit Classification Code"},
            { "PAB", "Permit Expiration Date"},
            { "PAC", "Permit Identifier"},
            { "PAD", "Permit IssueDate"},
            { "PAE", "Permit Restriction Code"},
            { "PAF", "Permit Endorsement Code"},
            { "ZVA", "Court Restriction Code"},
        };

        public static IEnumerable<string> ReadLines(string input)
        {
            string line;
            using (var sr = new StringReader(input))
                while ((line = sr.ReadLine()) != null)
                    yield return line;
        }
        public override Task<IDocument> Parse(T document)
        {
            var documentString = document.ToString();
            var documentList = ReadLines(documentString).ToList();

            // Harded document type "DriversLicense" makes the assumption every PDF417 is a drivers license
            IDocument scanDocument = new DriversLicense();

            return Task.Run(() =>
            {
                foreach (var key in AbbreviationMap.Keys)
                {
                    if (documentString.Contains(key))
                    {
                        var field = documentList.Where(value => value.Contains(key))
                            .Select(value => new DocumentField { FieldName = AbbreviationMap[key], Value = value.Substring(3), FieldAbbrevation = key })
                            .FirstOrDefault()?.Parse();

                        scanDocument.AddField(field);
                    }
                }
                return scanDocument;
            });
        }

       
    }
}
