﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PE.ADP.Managers.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class SettingsAdapter : global::System.Configuration.ApplicationSettingsBase {
        
        private static SettingsAdapter defaultInstance = ((SettingsAdapter)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SettingsAdapter())));
        
        public static SettingsAdapter Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int ParcelSize {
            get {
                return ((int)(this["ParcelSize"]));
            }
            set {
                this["ParcelSize"] = value;
            }
        }
    }
}