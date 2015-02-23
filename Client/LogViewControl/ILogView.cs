using System.ComponentModel;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Client
{
    public interface ILogView
    {
        BindingList<LogEntryData> Data { get; set; }
        bool DetailsVisible { get; set; }
    }	
}
