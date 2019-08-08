using System;
using System.Collections.Generic;
using System.Text;

namespace CVNAPDF417.Contracts.Documents
{
    public interface IDocument
    {
        bool IsValid();

        void AddField(DocumentField field);

        void RemoveField(DocumentField field);
        DocumentField GetFieldByName(string fieldName);
        DocumentField GetFieldByAbbreviation(string fieldAbbreviation);

        void PrintFields();
    }
}
