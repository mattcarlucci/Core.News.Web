using static Core.News.Console.Scheduling._PROCESS_INFO_;

namespace Core.News.Console.Scheduling
{
    public class _PROCESS_INFO_
    {
        public class SafeHandle
        {
            public bool IsInvalid { get; set; }
            public bool IsClosed { get; set; }
        }

        public class Handle
        {
            public int value { get; set; }
        }

        public class MaxWorkingSet
        {
            public int value { get; set; }
        }

        public class MinWorkingSet
        {
            public int value { get; set; }
        }

        public class BaseAddress
        {
            public object value { get; set; }
        }

        public class EntryPointAddress
        {
            public object value { get; set; }
        }

        public class FileVersionInfo
        {
            public string Comments { get; set; }
            public string CompanyName { get; set; }
            public int FileBuildPart { get; set; }
            public string FileDescription { get; set; }
            public int FileMajorPart { get; set; }
            public int FileMinorPart { get; set; }
            public string FileName { get; set; }
            public int FilePrivatePart { get; set; }
            public string FileVersion { get; set; }
            public string InternalName { get; set; }
            public bool IsDebug { get; set; }
            public bool IsPatched { get; set; }
            public bool IsPrivateBuild { get; set; }
            public bool IsPreRelease { get; set; }
            public bool IsSpecialBuild { get; set; }
            public string Language { get; set; }
            public string LegalCopyright { get; set; }
            public string LegalTrademarks { get; set; }
            public string OriginalFilename { get; set; }
            public string PrivateBuild { get; set; }
            public int ProductBuildPart { get; set; }
            public int ProductMajorPart { get; set; }
            public int ProductMinorPart { get; set; }
            public string ProductName { get; set; }
            public int ProductPrivatePart { get; set; }
            public string ProductVersion { get; set; }
            public string SpecialBuild { get; set; }
        }

        public class Module
        {
            public string ModuleName { get; set; }
            public string FileName { get; set; }
            public BaseAddress BaseAddress { get; set; }
            public int ModuleMemorySize { get; set; }
            public EntryPointAddress EntryPointAddress { get; set; }
            public FileVersionInfo FileVersionInfo { get; set; }
            public object Site { get; set; }
            public object Container { get; set; }
        }

        public class ProcessorAffinity
        {
            public int value { get; set; }
        }

        public class StartAddress
        {
            public object value { get; set; }
        }

        public class Thread
        {
            public int BasePriority { get; set; }
            public int CurrentPriority { get; set; }
            public int Id { get; set; }
            public bool PriorityBoostEnabled { get; set; }
            public int PriorityLevel { get; set; }
            public StartAddress StartAddress { get; set; }
            public int ThreadState { get; set; }
            public int WaitReason { get; set; }
            public string PrivilegedProcessorTime { get; set; }
            public System.DateTime StartTime { get; set; }
            public string TotalProcessorTime { get; set; }
            public string UserProcessorTime { get; set; }
            public object Site { get; set; }
            public object Container { get; set; }
        }

        public class MainWindowHandle
        {
            public int value { get; set; }
        }

        public class MainModule
        {
            public string ModuleName { get; set; }
            public string FileName { get; set; }
            public BaseAddress BaseAddress { get; set; }
            public int ModuleMemorySize { get; set; }
            public EntryPointAddress EntryPointAddress { get; set; }
            public FileVersionInfo FileVersionInfo { get; set; }
            public object Site { get; set; }
            public object Container { get; set; }
        }

        public class Process
        {
            public SafeHandle SafeHandle { get; set; }
            public Handle Handle { get; set; }
            public int BasePriority { get; set; }
            public bool HasExited { get; set; }
            public System.DateTime StartTime { get; set; }
            public int Id { get; set; }
            public string MachineName { get; set; }
            public MaxWorkingSet MaxWorkingSet { get; set; }
            public MinWorkingSet MinWorkingSet { get; set; }
            public System.Collections.Generic.IList<Module> Modules { get; set; }
            public int NonpagedSystemMemorySize64 { get; set; }
            public int NonpagedSystemMemorySize { get; set; }
            public int PagedMemorySize64 { get; set; }
            public int PagedMemorySize { get; set; }
            public int PagedSystemMemorySize64 { get; set; }
            public int PagedSystemMemorySize { get; set; }
            public int PeakPagedMemorySize64 { get; set; }
            public int PeakPagedMemorySize { get; set; }
            public int PeakWorkingSet64 { get; set; }
            public int PeakWorkingSet { get; set; }
            public long PeakVirtualMemorySize64 { get; set; }
            public int PeakVirtualMemorySize { get; set; }
            public bool PriorityBoostEnabled { get; set; }
            public int PriorityClass { get; set; }
            public int PrivateMemorySize64 { get; set; }
            public int PrivateMemorySize { get; set; }
            public string ProcessName { get; set; }
            public ProcessorAffinity ProcessorAffinity { get; set; }
            public int SessionId { get; set; }
            public System.Collections.Generic.IList<Thread> Threads { get; set; }
            public int HandleCount { get; set; }
            public long VirtualMemorySize64 { get; set; }
            public int VirtualMemorySize { get; set; }
            public bool EnableRaisingEvents { get; set; }
            public int WorkingSet64 { get; set; }
            public int WorkingSet { get; set; }
            public bool Responding { get; set; }
            public string MainWindowTitle { get; set; }
            public MainWindowHandle MainWindowHandle { get; set; }
            public object SynchronizingObject { get; set; }
            public MainModule MainModule { get; set; }
            public string PrivilegedProcessorTime { get; set; }
            public string TotalProcessorTime { get; set; }
            public string UserProcessorTime { get; set; }
            public object Site { get; set; }
            public object Container { get; set; }
        }
    }
}