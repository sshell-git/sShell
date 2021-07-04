using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sShell.Localization
{
    static class Language
    {
        public static Dictionary<string, string> en_US = new()
        {
            #region sShell_Welcome
            { "sShell_Welcome_Title", "Welcome!" },
            { "sShell_Welcome_Description", "Welcome to sShell, a simple WPF shell created for fun.\nSome programs may ask for admin privledges." },
            #endregion

            #region sShell_System
            { "sShell_System_Camera", "Camera" },
            { "sShell_System_StickyNotes", "Sticky Notes" },
            { "sShell_System_Settings", "Settings" },
            { "sShell_System_ScreenshotTaken", "Screenshot taken" },
            #endregion

            #region sShell_UI
            { "sShell_UI_Open", "Open" },
            { "sShell_UI_Cut", "Cut" },
            { "sShell_UI_Copy", "Copy" },
            { "sShell_UI_Paste", "Paste" },
            { "sShell_UI_Save", "Save" },
            { "sShell_UI_Delete", "Delete" },
            #endregion

            #region sShell_Errors
            { "sShell_Error", "Error" },
            { "sShell_Error_GenericMessage", "An error has occured: {0}" },
            { "sShell_Error_ProcessFailure", "An error occured while trying to hook into process {0}: {1}" }
            #endregion
        };

        public static Dictionary<string, string> fr_FR = new()
        {
            #region sShell_Welcome
            { "sShell_Welcome_Title", "Welcome!" },
            { "sShell_Welcome_Description", "Welcome to sShell, a simple WPF shell created for fun.\nSome programs may ask for admin privledges." },
            #endregion

            #region sShell_System
            { "sShell_System_Camera", "Camera" },
            { "sShell_System_StickyNotes", "Sticky Notes" },
            { "sShell_System_Settings", "Settings" },
            { "sShell_System_ScreenshotTaken", "Screenshot taken" },
            #endregion

            #region sShell_UI
            { "sShell_UI_Open", "Open" },
            { "sShell_UI_Cut", "Cut" },
            { "sShell_UI_Copy", "Copy" },
            { "sShell_UI_Paste", "Paste" },
            { "sShell_UI_Save", "Save" },
            { "sShell_UI_Delete", "Delete" },
            #endregion

            #region sShell_Errors
            { "sShell_Error", "Error" },
            { "sShell_Error_GenericMessage", "An error has occured: {0}" },
            { "sShell_Error_ProcessFailure", "An error occured while trying to hook into process {0}: {1}" }
            #endregion
        };
    }
}
