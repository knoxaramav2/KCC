
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CommonLangLib
{
    /// <summary>
    /// Upon instantiation, detect current platform info
    /// for default target configuration
    /// </summary>
    /// https://stackoverflow.com/questions/767613/identifying-the-cpu-architecture-type-using-c-sharp
    public class AutoArch
    {
        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        [DllImport("RuntimeInformation.dll")]
        private static extern bool IsRuntimePlatform();

        private const int PROC_ARCH_AMD64   = 9;
        private const int PROC_ARCH_IA64    = 6;
        private const int PROC_ARCH_ARM     = 5;
        private const int PROC_ARCH_INTEL   = 0;
        
        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_INFO
        {
            public short wProcessorArchitecture;
            public short wReserved;
            public int dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public int dwNumberOfProcessors;
            public int dwProcessorType;
            public int dwAllocationGranularity;
            public short wProcessorLevel;
            public short wProcessorRevision;
        }

        public ProcessorArchitecture Arch { get; internal set; }
        public OS OS { get; internal set; }
        public int MAX_BUS_WIDTH;

        /// <summary>
        /// AutoDetect system information
        /// </summary>
        public AutoArch()
        {
            DetectSystem();
            DetectPlatform();
        }

        private void DetectSystem()
        {
            var si = new SYSTEM_INFO();
            GetNativeSystemInfo(ref si);
            
            switch (si.wProcessorArchitecture)
            {
                
                case PROC_ARCH_INTEL:
                    Arch = ProcessorArchitecture.X86;
                    MAX_BUS_WIDTH = 4;
                    break;
                case PROC_ARCH_IA64:
                    Arch = ProcessorArchitecture.IA64;
                    MAX_BUS_WIDTH = 8;
                    break;
                case PROC_ARCH_AMD64:
                    Arch = ProcessorArchitecture.Amd64;
                    MAX_BUS_WIDTH = 8;
                    break;
                case PROC_ARCH_ARM:
                    Arch = ProcessorArchitecture.Arm;
                    MAX_BUS_WIDTH = 8;
                    break;
                default:
                    ErrorReporter.GetInstance().Add(
                        $"Unrecognized CPU family: {si.wProcessorArchitecture}", 
                        ErrorCode.UnrecognizedInstructionFamily);
                    break;
            }
        }

        private void DetectPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                OS = OS.Windows;
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                OS = OS.Linux;
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                OS = OS.OSX;
                ErrorReporter.GetInstance().Add("MacOS has no planned support", ErrorCode.UnsupportedOS);
            } else
            {
                OS = OS.NA;
                ErrorReporter.GetInstance().Add("Operating system can not be recognized: " +
                    $"{RuntimeInformation.OSDescription}", ErrorCode.UnrecognizedOS);
            }
        }
    }

    public enum OS
    {
        NA,
        Windows,
        Linux,
        OSX     //No plans to support
    }
}