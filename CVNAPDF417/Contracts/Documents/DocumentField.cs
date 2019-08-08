using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVNAPDF417.Contracts.Documents
{
    public class DocumentField
    {
        public string FieldName { get; set; }
        public string FieldAbbrevation { get; set; }
        public string Value { get; set; }

        // This is not cool
        public Validator validator = new Validator();

        public bool IsValid => validator.ValidationMap
            [FieldAbbrevation].All(validator => validator(Value));

        public DocumentField Parse()
        {
            var containsAbbreviation = Value.Contains(FieldAbbrevation);

            var startIndex = containsAbbreviation
                ? Value.IndexOf(FieldAbbrevation) + 3
                : 0;

            var abbreviationIsStart = containsAbbreviation && startIndex < 4;


            var reparsed = abbreviationIsStart
                ? Value.Substring(3)
                : Value.Substring(startIndex);

            if (reparsed != Value)
            {
                var updateSucceeded = TrySetValue(reparsed);

                if (!updateSucceeded)
                {
                    Value = "Could Not Read Value";
                }
            }

            return this;
        }

        public bool TrySetValue(string updateValue)
        {
            var oldValue = Value;

            Value = updateValue;

            if (!IsValid)
            {
                Value = oldValue;
                Console.WriteLine("Failed to update value");
                return false;
            }
            return true;
        }
    }
}
