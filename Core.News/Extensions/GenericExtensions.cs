using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class GlobalExtensions
    {
        /// <summary>
        /// Determines whether this instance is windows.
        /// </summary>
        /// <returns><c>true</c> if this instance is windows; otherwise, <c>false</c>.</returns>
        public static bool IsWindows()
        {
            return System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
        }
    }
}
