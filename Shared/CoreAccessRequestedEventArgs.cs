using System;


namespace VitaliiPianykh.FileWall.Shared
{
    [Serializable]
    public class CoreAccessRequestedEventArgs: MarshalByRefObject
    {
        public CoreAccessRequestedEventArgs(string operationName, string protectedPath, string processPath)
        {
            OperationName = operationName;
            ProtectedPath = protectedPath;
            ProcessPath = processPath;
        }

        public bool Allow { get; set; }
        public string OperationName { get; private set; }
        public string ProtectedPath { get; private set; }
        public string ProcessPath { get; private set; }
        public bool CreateRule { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string RuleName { get; set; }

        // TODO: Test it.
        public override bool Equals(object obj)
        {
            if (obj is CoreAccessRequestedEventArgs)
            {
                var CastedObj = (CoreAccessRequestedEventArgs) obj;
                if(CastedObj.OperationName != OperationName)
                    return false;
                if (CastedObj.ProtectedPath != ProtectedPath)
                    return false;
                if (CastedObj.ProcessPath != ProcessPath)
                    return false;
            }
            else
                return false;

            return true;
        }
    }
}