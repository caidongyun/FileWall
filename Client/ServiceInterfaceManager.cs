using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Client
{
    /// <summary>
    /// This class is responsible for connecting for FileWallService's IPC channel.
    /// And for closing this connection.
    /// </summary>
    public class ServiceInterfaceManager
    {
        public virtual ServiceInterface GetMarshalledInteface()
        {
            BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["name"] = "FileWallChannel2";
            props["portName"] = "localhost:9091";
            props["typeFilterLevel"] = TypeFilterLevel.Full;
            props["authorizedGroup"] = AdvEnvironment.EveryoneGroupName;
            var chan = new IpcChannel(props, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(chan, false);

            var RemoteObj = Activator.GetObject(
                typeof(ServiceInterface),
                "ipc://localhost:9090/FileWall.rem");

            return (ServiceInterface)RemoteObj;
        }

        public virtual void ClientDisconnect()
        {
            var channel = ChannelServices.GetChannel("FileWallChannel2");
            if (channel == null)
                throw new InvalidOperationException("Error while disconnecting IPC port. Can't find FileWallChannel2.");

            ChannelServices.UnregisterChannel(channel);
        }
    }
}
