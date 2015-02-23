using System;


namespace VitaliiPianykh.FileWall.Service.Native
{
    public class PathAlreadyAddedException: ApplicationException
    {
        public string Path { get; private set; }

        public PathAlreadyAddedException(string path, Exception innerException)
            : base("Equal path is already added to driver:\r\n(" + path + ")", innerException)
        {
            Path = path;
        }
    }

    public class IdNotFoundException: ApplicationException
    {
        public int ID { get; private set; }

        public IdNotFoundException(int id, Exception innerException)
            :base("Such ID was not added to driver.", innerException)
        {
            ID = id;
        }
    }
}
