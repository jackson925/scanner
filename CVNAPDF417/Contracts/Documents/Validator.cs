using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CVNAPDF417.Contracts.Documents
{
    public class Validator
    {
        private const string DateFormat = "ddMMyyyy";

        public Dictionary<string, List<Func<string, bool>>> ValidationMap = new Dictionary<string, List<Func<string, bool>>>
        {
            { "DBB", new List<Func<string, bool>>() { IsDate } },
            { "DBA", new List<Func<string, bool>>() { IsDate, IsNotExpired } },
            { "DAA", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAG", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAI", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAJ", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAK", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAQ", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DBD", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAC", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAD", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAU", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAW", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAX", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DAY", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DBC", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DCF", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DCG", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DCK", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DCS", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },
            { "DDA", new List<Func<string, bool>>() {  IsNotNullOrEmpty } },

        };

        public static Func<string, bool> IsNotNullOrEmpty = value => !string.IsNullOrEmpty(value);


        public static Func<string, bool> IsDate = rawDate =>
        {
            DateTime outDate;
            return DateTime.TryParseExact(
                rawDate,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out outDate
            );
        };

        public static Func<string, bool> IsNotExpired = date =>
        {
            var isNotExpired = parseDate(date) > DateTime.Now;
            return isNotExpired;
        };

        public static Func<string, DateTime> parseDate = rawDate =>
        {
            DateTime outDate;
            DateTime.TryParseExact(
                    rawDate,
                    DateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out outDate
            );

            return outDate;
        };

    }
}
