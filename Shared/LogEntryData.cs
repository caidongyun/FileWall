using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace VitaliiPianykh.FileWall.Shared
{
    public enum AccessType { FILESYSTEM, REGISTRY };


    [Serializable]
    public class LogEntryData
    {
        /// <summary>Constructs new <see cref="LogEntryData"/>.</summary>
        public LogEntryData(string date, bool isAllowed, AccessType accessType, string path, string processPath)
        {
            Date        = date;
            IsAllowed   = isAllowed;
            AccessType = accessType;
            Path        = path;
            ProcessPath = processPath;
        }

        // Date strored in string because we cant's test with DateTime class.
        public string Date              { get; private set; }
        public bool IsAllowed           { get; private set; }
        public AccessType AccessType   { get; private set; }
        public string Path              { get; private set; }
        public string ProcessPath       { get; private set; }

        /// <summary>Serializes <see cref="LogEntryData"/> to byte array.</summary>
        public static byte[] Serialize(LogEntryData data)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data cannot be null.");

            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);

            var bytes = stream.ToArray();
            stream.Close();
            return bytes;
        }

        /// <summary>Deserializes data contained in byte array.</summary>
        public static LogEntryData Deserialize(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data cannot be null.");
            if(data.Length == 0)
                throw new ArgumentException("data cannot be empty", "data");

            var stream = new MemoryStream(data);
            var formatter = new BinaryFormatter();
            var entryData = (LogEntryData)formatter.Deserialize(stream);
            stream.Close();
            return entryData;
        }
    }
}