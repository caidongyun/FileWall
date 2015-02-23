#include "stdafx.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace	Microsoft::VisualStudio::TestTools::UnitTesting;
using namespace VitaliiPianykh::FileWall::Service;
using namespace System::Runtime::InteropServices;

namespace TestTypesMarshaling
{
	[TestClass]
	public ref class TestTypesMarshaling
	{
	private:
		TestContext^ testContextInstance;

	public: 
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		property Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^ TestContext
		{
			Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^ get()
			{
				return testContextInstance;
			}
			System::Void set(Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^ value)
			{
				testContextInstance = value;
			}
		};

		// Test for data types size comparison.
		[TestMethod]
		void DataTypesSizes()
		{
			Assert::AreEqual(sizeof(_AP_COMMAND), (UInt32)Marshal::SizeOf(gcnew Native::COMMAND()));

			Assert::AreEqual(sizeof(_FS_ACCESS_REQUEST), (UInt32)Marshal::SizeOf(gcnew Native::ACCESS_REQUEST()));

			Assert::AreEqual(sizeof(_PERMISSION), (UInt32)Marshal::SizeOf(gcnew Native::PERMISSION()));
		};

		// Test for const values comparison.
		[TestMethod]
		void ConstValues()
		{
			List<Int32> ^c = gcnew List<Int32>();
			List<Int32> ^v = gcnew List<Int32>();

			//ACCESS_TYPE
			c->Add(ACCESS_FILESYSTEM);
			c->Add(ACCESS_REGISTRY);

			//COMMAND_TYPE
			c->Add(COMMAND_ADD);
			c->Add(COMMAND_DEL);

			//FS_OPERATION
			c->Add(FSOP_CREATE);
			c->Add(FSOP_OTHER);
			c->Add(FSOP_READ);
			c->Add(FSOP_WRITE);

			v->Add(Convert::ToInt32(Native::ACCESS_TYPE::FILESYSTEM));
			v->Add(Convert::ToInt32(Native::ACCESS_TYPE::REGISTRY));
			v->Add(Convert::ToInt32(Native::COMMAND_TYPE::ADD));
			v->Add(Convert::ToInt32(Native::COMMAND_TYPE::DEL));
			v->Add(Convert::ToInt32(Native::FILESYS_OPERATION::CREATE));
			v->Add(Convert::ToInt32(Native::FILESYS_OPERATION::OTHER));
			v->Add(Convert::ToInt32(Native::FILESYS_OPERATION::READ));
			v->Add(Convert::ToInt32(Native::FILESYS_OPERATION::WRITE));

			CollectionAssert::AreEqual(c, v);
		};
	};
}
