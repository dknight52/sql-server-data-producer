﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLDataProducer {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class SQLDataProducerSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static SQLDataProducerSettings defaultInstance = ((SQLDataProducerSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SQLDataProducerSettings())));
        
        public static SQLDataProducerSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ConnectionString {
            get {
                return ((string)(this["ConnectionString"]));
            }
            set {
                this["ConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string QueryText {
            get {
                return ((string)(this["QueryText"]));
            }
            set {
                this["QueryText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::SQLDataProducer.Entities.OptionEntities.ExecutionTaskOptions ExecutionOptions {
            get {
                return ((global::SQLDataProducer.Entities.OptionEntities.ExecutionTaskOptions)(this["ExecutionOptions"]));
            }
            set {
                this["ExecutionOptions"] = value;
            }
        }
    }
}
