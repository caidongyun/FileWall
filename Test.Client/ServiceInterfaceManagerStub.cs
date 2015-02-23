using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Client
{
    sealed class ServiceInterfaceManagerStub : ServiceInterfaceManager
    {
        public ServiceInterfaceManagerStub()
        {
            ServiceInterface = new ServiceInterface(new Ruleset());
        }

        public override ServiceInterface GetMarshalledInteface()
        {
            IsMarshalled = true;
            return ServiceInterface;
        }

        public override void ClientDisconnect()
        {
            IsMarshalled = false;
        }

        //
        // Stub utilites
        //

        /// <summary>GetMarshalledInteface will return value of this property.</summary>
        public ServiceInterface ServiceInterface { get; set; }

        /// <summary>
        /// By default returns false.
        /// If client called GetMarshalledInteface retruns true.
        /// If client called ClientDisconnect returns false.
        /// </summary>
        public bool IsMarshalled { get; private set; }
    }

    [TestClass]
    public class TestServiceInterfaceManagerStub
    {
        [TestMethod]
        public void _ServiceInterface_ByDefaultIsNotNull()
        {
            var interfaceManager = new ServiceInterfaceManagerStub();
            Assert.IsNotNull(interfaceManager.ServiceInterface);
        }

        [TestMethod]
        public void GetMarshalledInteface_ReturnsSpecifiedInterface()
        {
            var expectedInterface = new ServiceInterface(new Ruleset());
            var interfaceManager = new ServiceInterfaceManagerStub();
            interfaceManager.ServiceInterface = expectedInterface;
            Assert.AreSame(expectedInterface, interfaceManager.GetMarshalledInteface());
        }
    }
}
