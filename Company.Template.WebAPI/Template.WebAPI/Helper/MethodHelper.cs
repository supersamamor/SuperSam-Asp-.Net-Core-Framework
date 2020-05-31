using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace Template.WebAPI.Helper2
{
    public static class MethodHelper2
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(this ControllerBase controller)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }
    }

}
