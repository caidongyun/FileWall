using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;


namespace VitaliiPianykh.FileWall.Shared
{
    public class ServiceInterface : MarshalByRefObject
    {
        private uint filesysBlocks;
        private uint filesysPermits;
        private uint registryBlocks;
        private uint registryPermits;
        private readonly Ruleset ruleset;

        #region Counters

        public uint FilesysBlocks
        {
            get { lock (this) { return filesysBlocks; } }
            set { lock (this) {  filesysBlocks = value; } }
        }
        
        public uint FilesysPermits
        {
            get { lock (this) { return filesysPermits; } }
            set { lock (this) { filesysPermits= value; } }
        }
        
        public uint RegistryBlocks
        {
            get { lock (this) { return registryBlocks; } }
            set { lock (this) { registryBlocks = value; } }
        }
        
        public uint RegistryPermits
        {
            get { lock (this) { return registryPermits; } }
            set { lock (this) { registryPermits = value; } }
        }

        #endregion


        public ServiceInterface(Ruleset ruleset)
        {
            if(ruleset == null)
                throw new ArgumentException();
            this.ruleset = ruleset;
        }


        public Ruleset GetRuleset()
        {
            ruleset.AcceptChanges();
            return ruleset;
        }

        public void UpdateRuleset(Ruleset changes)
        {
            ruleset.Merge(changes);
            ruleset.AcceptChanges();
        }

        public event AccessRequestedEventHandler AccessRequested;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void OnAccessRequested(CoreAccessRequestedEventArgs e)
        {
            if(AccessRequested == null)
                return;

            var InvocationList = AccessRequested.GetInvocationList();

            lock (this)
            {
                foreach (AccessRequestedEventHandler DelegateToInvoke in InvocationList)
                {
                    try
                    {
                        DelegateToInvoke.Invoke(this, e);
                    }
                    catch (Exception ex)
                    {
                        if (ex.GetType() == typeof(SocketException) ||
                            ex.GetType() == typeof(RemotingException))
                            AccessRequested -= DelegateToInvoke;
                        else
                            throw;
                    }
                }
            }
        }


        public static ServiceInterface Marshal(Ruleset ruleset)
        {
            BinaryClientFormatterSinkProvider clientProvider = null;
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["name"] = "FileWallChannel";
            props["portName"] = "localhost:9090";
            props["typeFilterLevel"] = TypeFilterLevel.Full;
            props["authorizedGroup"] = AdvEnvironment.EveryoneGroupName;
            var c = new IpcChannel(props, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(c, false);

            var infc = new ServiceInterface(ruleset);
            RemotingServices.Marshal(infc, "FileWall.rem");
            return infc;
        }

        public static void Disconnect(ServiceInterface serviceInterface)
        {
            RemotingServices.Disconnect(serviceInterface);

            var Channel = ChannelServices.GetChannel("FileWallChannel");
            ChannelServices.UnregisterChannel(Channel);
        }
    }
}