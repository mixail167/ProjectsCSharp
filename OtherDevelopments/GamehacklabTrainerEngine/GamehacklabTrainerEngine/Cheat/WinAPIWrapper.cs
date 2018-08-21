using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GamehacklabTrainerEngine.Core;

namespace GamehacklabTrainerEngine
{
    class WinAPIWrapper
    {
        #region winapi

        #region structs

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MODULEINFO
        {
            public IntPtr lpBaseOfDll;
            public uint SizeOfImage;
            public IntPtr EntryPoint;
        }

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        #endregion

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("psapi.dll", SetLastError = true)]
        static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out MODULEINFO lpmodinfo, uint cb);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess( ProcessAccessFlags processAccess, bool bInheritHandle,int processId);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress,   UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory( IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        #endregion

        public static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
        {
            return OpenProcess(flags, false, proc.Id);
        }

        public static Int32 GetProcessIdByName(String processName)
        {
            try
            {
                return Process.GetProcessesByName(processName)[0].Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static IntPtr GetModuleBaseAddress(Int32 pid, String moduleName)
        {
            Process process = Process.GetProcessById(pid);
            IntPtr moduleAddress = IntPtr.Zero;

            foreach (ProcessModule module in process.Modules)
            {
                if (module.FileName.Contains(moduleName))
                {
                    moduleAddress = module.BaseAddress;
                    break;
                }
            }

            return moduleAddress;

        }

        public static List<IntPtr> ScanInt32Value(Int32 pid, Int32 baseAddress, Int32 value)
        {
            List<IntPtr> result = new List<IntPtr>();
            MEMORY_BASIC_INFORMATION mbi = new MEMORY_BASIC_INFORMATION();
            Int32 offset = 0;
            Int32 scanSize = 0;
            IntPtr byteread = IntPtr.Zero;

            IntPtr hProcess = OpenProcess(ProcessAccessFlags.All, false, pid);
            if (hProcess == IntPtr.Zero) return result;
            scanSize = 0x7FFFFFFF;

            while (offset < scanSize)
            {
                int count = VirtualQueryEx(hProcess, (IntPtr) baseAddress + offset, out mbi, (uint) Marshal.SizeOf(mbi));
                if (count == 0) break;
                if (mbi.State != 0x10000) // MEM_FREE
                {

                    byte[] buffer = new byte[(int) mbi.RegionSize];
                    ReadProcessMemory(hProcess, mbi.BaseAddress, buffer, (uint) mbi.RegionSize, out byteread);

                    for (int i = 0; i < buffer.Length; i += sizeof(Int32))
                    {
                        if (value == BitConverter.ToInt32(buffer, i))
                        {
                            result.Add((IntPtr) (uint) mbi.BaseAddress + i);
                        }
                    }
                }
                offset += (int) mbi.RegionSize;

            }

            CloseHandle(hProcess);
            return result;

        }

        public static List<IntPtr> ScanSignature(Int32 pid, Int32 BaseAddress, byte[] signature, bool single = false,
            String mask = "")
        {
            List<IntPtr> result = new List<IntPtr>();
            MEMORY_BASIC_INFORMATION mbi = new MEMORY_BASIC_INFORMATION();
            Int32 offset = 0;
            Int32 scanSize = 0;
            IntPtr byteread = IntPtr.Zero;

            IntPtr hProcess = OpenProcess(ProcessAccessFlags.All, false, pid);
            if (hProcess == IntPtr.Zero) return result;
            scanSize = 0x7FFFFFFF;

            while (offset < scanSize)
            {
                int count = VirtualQueryEx(hProcess, (IntPtr)BaseAddress + offset, out mbi, (uint)Marshal.SizeOf(mbi));
                if (count == 0) break;
                if (mbi.State != 0x10000) // MEM_FREE
                {

                    byte[] buffer = new byte[(int) mbi.RegionSize];
                    ReadProcessMemory(hProcess, mbi.BaseAddress, buffer, (uint) mbi.RegionSize, out byteread);

                    for (int i = 0; i < buffer.Length - signature.Length; i++)
                    {
                        if (CheckSignature(buffer, (uint) i, signature, mask))
                        {
                            result.Add((IntPtr) ((uint) mbi.BaseAddress + i));
                            if (single)
                            {
                                CloseHandle(hProcess);
                                return result;
                            }
                        }
                    }
                }
                offset += (int)mbi.RegionSize;

            }

            CloseHandle(hProcess);
            return result;
        }

        private static bool CheckSignature(byte[] buffer, uint StartOffset, byte[] signature, String mask = "")
        {
            for (int i = 0; i < signature.Length; i++)
            {
                if (mask.Length == 0)
                {
                    if (buffer[StartOffset + i] != signature[i]) return false;
                }
                else
                {
                    if (mask[i] != '?' && buffer[StartOffset + i] != signature[i]) return false;
                }
            }

            return true;

        }

        public static bool WriteMem(IntPtr address, byte[] data)
        {
            int pid = FindProcess(Constants.ProcessName);
            bool result = false;
            IntPtr hProcess = OpenProcess(ProcessAccessFlags.All, false, pid);
            if (hProcess == IntPtr.Zero) return false;
            uint oldProtect = 0;
            UIntPtr datalen = (UIntPtr) data.Length;

            // https://msdn.microsoft.com/ru-ru/library/windows/desktop/aa366786(v=vs.85).aspx Memory Protection Constants

            result = VirtualProtectEx(hProcess, address, datalen, 0x40, out oldProtect);
            if (result)
            {
                IntPtr bytesWritten = IntPtr.Zero;
                result = WriteProcessMemory(hProcess, address, data, (int)datalen, out bytesWritten);

                if (result && bytesWritten.Equals(datalen))
                {
                    result = VirtualProtectEx(hProcess, address, datalen, oldProtect, out oldProtect);
                }
                else
                {
                    result = false;
                }
            }

            CloseHandle(hProcess);
            return result;
        }

        public static String StringToSignature(String inSignature, out byte[] outSignature)
        {
            String mask = "";
            List<byte> bytes = new List<byte>();

            String[] sigBytes = inSignature.Split(' ');

            for (int i = 0; i < sigBytes.Length; i++)
            {
                if (sigBytes[i].Equals("xx") || sigBytes[i].Equals("??"))
                {
                    mask += "?";
                    bytes.Add(0);
                }
                else
                {
                    byte val = byte.Parse(sigBytes[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    bytes.Add(val);
                    mask += "x";
                }
            }

            outSignature = bytes.ToArray();
            return mask;
        }

        public static Int32 FindProcess(String processName)
        {
            return WinAPIWrapper.GetProcessIdByName(processName);
        }

        public static IntPtr AllocMem(int size)
        {
            Int32 pid = FindProcess(Constants.ProcessName);
            IntPtr hProcess = OpenProcess(ProcessAccessFlags.All, false, pid);
            if (hProcess == IntPtr.Zero) return IntPtr.Zero;
            IntPtr caveAddress = WinAPIWrapper.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)size, WinAPIWrapper.AllocationType.Commit,
                WinAPIWrapper.MemoryProtection.ExecuteReadWrite);
            WinAPIWrapper.CloseHandle(hProcess);
            return caveAddress;
        }

    }
}
