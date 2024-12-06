using System;
using System.Text;
using System.Text.RegularExpressions;

namespace FleXFrame.UtilityHub
{
    public static class IDGenerator
    {
        private static readonly Random _random = new Random();

        // Generates ID based on a pattern, sequence number, and optional padding for the sequence

        /// <summary>
        /// Generates a customizable ID based on a pattern, sequence number, and optional padding for the sequence.
        /// </summary>
        /// <param name="pattern">
        /// The pattern for generating the ID, with placeholders:
        /// - <c>{P}</c>: Prefix (customizable),
        /// - <c>{S}</c>: Sequence number with padding,
        /// - <c>{D}</c>: Current date (yyyyMMdd),
        /// - <c>{R}</c>: Random alphanumeric string,
        /// - <c>{T}</c>: Current timestamp (HHmmss).
        /// </param>
        /// <param name="sequenceNumber">The incremental sequence number for the ID.</param>
        /// <param name="padding">Number of digits for the sequence, with leading zeros if necessary.</param>
        /// <returns>A formatted ID string based on the specified pattern.</returns>
        /// <example>
        /// Example usage:
        /// <code>
        /// var userID = IDGenerator.GenerateID("USER-{S}", 7, 3);       // Result: USER-007
        /// var empID = IDGenerator.GenerateID("EMP-{D}-{S}", 123, 5);   // Result: EMP-20241026-00123
        /// var customID = IDGenerator.GenerateID("CUSTOM-{S}", 9, 2);   // Result: CUSTOM-09
        /// </code>
        /// </example>
        public static string GenerateID(string pattern, int sequenceNumber, int padding = 4)
        {
            var sb = new StringBuilder();
            var sequenceFormatted = sequenceNumber.ToString($"D{padding}"); // Dynamic padding based on the parameter

            // Pattern parsing:
            // {P} - Prefix, {S} - Sequence, {D} - Date, {R} - Random Alphanumeric, {T} - Timestamp
            var segments = Regex.Split(pattern, "(\\{[^}]+\\})");

            foreach (var segment in segments)
            {
                switch (segment)
                {
                    case "{P}": // Prefix (customizable via method parameter)
                        sb.Append("PRE"); // Default prefix; change as per requirement
                        break;
                    case "{S}": // Sequence (incremental number with dynamic padding)
                        sb.Append(sequenceFormatted);
                        break;
                    case "{D}": // Date
                        sb.Append(DateTime.Now.ToString("yyyyMMdd"));
                        break;
                    case "{T}": // Timestamp
                        sb.Append(DateTime.Now.ToString("HHmmss"));
                        break;
                    case "{R}": // Random Alphanumeric (length customizable)
                        sb.Append(GenerateRandomString(4)); // Default random 4 chars
                        break;
                    default:
                        sb.Append(segment); // Other text in pattern
                        break;
                }
            }

            return sb.ToString();
        }

        // Method to generate a random alphanumeric string of specified length
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }
            return result.ToString();
        }
    }
}
