using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SShell.Localization
{
    static class Language
    {
        public static Dictionary<string, string> en_US = new Dictionary<string, string>
        {
            #region SShell_Welcome
            { "SShell_Welcome_Title", "Welcome!" },
            { "SShell_Welcome_Description", "Welcome to SShell, a simple WPF shell created for fun.\nSome programs may ask for admin privledges." },
            #endregion

            #region SShell_System
            { "SShell_System_Camera", "Camera" },
            { "SShell_System_StickyNotes", "Sticky Notes" },
            { "SShell_System_Settings", "Settings" },
            { "SShell_System_ScreenshotTaken", "Screenshot taken" },
            #endregion

            #region SShell_UI
            { "SShell_UI_Open", "Open" },
            { "SShell_UI_Cut", "Cut" },
            { "SShell_UI_Copy", "Copy" },
            { "SShell_UI_Paste", "Paste" },
            { "SShell_UI_Save", "Save" },
            { "SShell_UI_Delete", "Delete" },
            #endregion

            #region SShell_Errors
            { "SShell_Error", "Error" },
            { "SShell_Error_GenericMessage", "An error has occured: {0}" },
            { "SShell_Error_ProcessFailure", "An error occured while trying to hook into process {0}: {1}" }
#endregion
        };
    }
}
