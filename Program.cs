/*
 * Created by SharpDevelop.
 * User: lflfm
 * Date: 20-Feb-19
 * Time: 21:50
 * 
 * https://stackoverflow.com/questions/53746671/how-to-unpair-remove-bluetooth-device-in-windows-with-c-sharp-net
 * https://stackoverflow.com/questions/19450783/how-to-use-dllimport-in-c
 */
using System;
using System.Runtime.InteropServices; //all the dll import stuff

namespace btRemover
{
	class Program
	{
		public static void Main(string[] args)
		{
			 
			Console.WriteLine("HI! Pass your Bluetooth device address as parameter to this program and it will be unpaired w00t!");
			
			if (args.Length != 1) {
				Console.WriteLine("call this program from command line passing your bt device's address as parameter. eg. btRemover.exe F01DBCEA3E23");
			} else {
				UInt64 intAddress = UInt64.Parse(args[0], System.Globalization.NumberStyles.HexNumber);
				Console.WriteLine("\n  Removing BT device at address {0} ({1})",args[0],intAddress);
				UInt32 removingResult = BlueToothStuff.Unpair(intAddress);
				if (removingResult == 0) {
					Console.WriteLine("SUCCESS!\n you are welcome... :-D\n");
				} else {
					Console.Write("ERROR, and sorry, but I can't really tell why, most probable cause is wrong address... {0}",removingResult);
				}				
			}
			Console.WriteLine("\npress any key to exit");
			Console.ReadKey(true);
		}
	}
	class BlueToothStuff {
		static public UInt32 Unpair(UInt64 Address) {
			BLUETOOTH_ADDRESS Addr = new BLUETOOTH_ADDRESS();
			Addr.ullLong = Address;
			UInt32 result = BluetoothRemoveDevice(ref Addr);
			return result; //https://msdn.microsoft.com/en-us/library/cc231199.aspx?f=255&MSPPError=-2147217396
		}
		
		[StructLayout(LayoutKind.Explicit)]
		public struct BLUETOOTH_ADDRESS {
			[FieldOffset(0)]
			 [MarshalAs(UnmanagedType.I8)]
			 public UInt64 ullLong;
			[FieldOffset(0)]
			 [MarshalAs(UnmanagedType.U1)]
			 public Byte rgBytes_0;
			[FieldOffset(1)]
			 [MarshalAs(UnmanagedType.U1)]
			 public Byte rgBytes_1;
			[FieldOffset(2)]
			 [MarshalAs(UnmanagedType.U1)]
			 public Byte rgBytes_2;
			[FieldOffset(3)]
			 [MarshalAs(UnmanagedType.U1)]
			 public Byte rgBytes_3;
			[FieldOffset(4)]
			 [MarshalAs(UnmanagedType.U1)]
			 public Byte rgBytes_4;
			[FieldOffset(5)]
			 [MarshalAs(UnmanagedType.U1)]
			 public Byte rgBytes_5;
		};
		[DllImport("BluetoothAPIs.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		static extern UInt32 BluetoothRemoveDevice([param: In, Out] ref BLUETOOTH_ADDRESS pAddress);
//
//		some failed attempts to list the devices themselves
//		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
//		public struct BLUETOOTH_DEVICE_INFO {
//			int dwSize;
//			ulong Address;
//			uint ulClassofDevice;
//			[MarshalAs(UnmanagedType.Bool)]
//			bool fConnected;
//			[MarshalAs(UnmanagedType.Bool)]
//			private bool fRemembered;
//			[MarshalAs(UnmanagedType.Bool)]
//			bool fAuthenticated;
//			private SYSTEMTIME stLastSeen;
//			private SYSTEMTIME stLastUsed;
//			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 248)]
//			string szName;
//		};
//		[StructLayout(LayoutKind.Sequential)]
//		public struct SYSTEMTIME {
//			private ushort year;
//			private short month;
//			private short dayOfWeek;
//			private short day;
//			private short hour;
//			private short minute;
//			private short second;
//			private short millisecond;
//		};
//		[StructLayout(LayoutKind.Sequential)]
//        public struct BLUETOOTH_DEVICE_SEARCH_PARAMS
//        {
//            int dwSize;
//            bool fReturnAuthenticated;
//            bool fReturnRemembered;
//            bool fReturnUnknown;
//            bool fReturnConnected;
//            bool fIssueInquiry;
//            ushort cTimeoutMultiplier;
//            IntPtr hRadio;
//        }
//
//		[DllImport("BluetoothAPIs.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
//		static extern UInt32 BluetoothGetDeviceInfo([param: In] IntPtr hRadio, ref BLUETOOTH_DEVICE_INFO pbtdi);
//		[DllImport("BluetoothAPIs.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
//        internal static extern IntPtr BluetoothFindFirstDevice(
//                ref BLUETOOTH_DEVICE_SEARCH_PARAMS pbtsp,
//                ref BLUETOOTH_DEVICE_INFO pbtdi);
	}
}


