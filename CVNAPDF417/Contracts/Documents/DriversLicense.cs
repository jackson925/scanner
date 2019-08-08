using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVNAPDF417.Contracts.Documents
{
    public class DriversLicense : IDocument
    {

        public List<string> RequiredFields = new List<string>() { "First Name" };

        public List<DocumentField> Fields = new List<DocumentField>();

        private Validator validator = new Validator();

        public string BarcodeType { get; set; }

        public DriversLicense()
        {

        }
        public DriversLicense(List<string> requiredFields, string barcodeType)
        {
            RequiredFields = requiredFields;
            BarcodeType = barcodeType;
        }

        public bool IsValid()
        {
            var fieldsValid = ValidateFields();
            var minimumRequirementsMet = ValidateAgainstMinimumRequirements();

            return fieldsValid && minimumRequirementsMet;
        }

        private bool ValidateAgainstMinimumRequirements()
        {
            return Fields.All(field =>
            {
                if (RequiredFields.Contains(field.FieldName) && string.IsNullOrEmpty(field.Value))
                {
                    return false;
                }

                return true;
            });
        }

        public bool ValidateFields()
        {
            return Fields.All(field => field.IsValid);
        }

        public void PrintFields()
        {
            Fields.ForEach(field => Console.WriteLine($"FieldName: {field.FieldName} - Field Abbreviation: {field.FieldAbbrevation} - Field Value: {field.Value}"));
        }

        public void AddField(DocumentField addField)
        {
            Fields.Add(addField);
        }

        public void RemoveField(DocumentField removeField)
        {
            Fields.Remove(removeField);
        }

        public DocumentField GetFieldByName(string fieldName)
        {
            return Fields.FirstOrDefault(field => field.FieldName == fieldName) ?? null;
        }

        public DocumentField GetFieldByAbbreviation(string fieldAbbreviation)
        {
            return Fields.FirstOrDefault(field => field.FieldAbbrevation == fieldAbbreviation) ?? null;
        }


    }
}
