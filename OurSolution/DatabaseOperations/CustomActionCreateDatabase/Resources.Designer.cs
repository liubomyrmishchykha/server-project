﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCustomAction {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MyCustomAction.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF DB_ID(&apos;testDb&apos;) IS NULL
        ///	CREATE DATABASE testDb
        ///GO
        ///
        ///﻿IF OBJECT_ID(&apos;Users&apos;, &apos;U&apos;) IS NULL
        ///	CREATE TABLE Users
        ///	(
        ///		Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        ///		Name VARCHAR(255) NOT NULL,
        ///		Password VARCHAR(64) NOT NULL
        ///	)
        ///GO
        ///
        ///﻿IF OBJECT_ID(&apos;Instances&apos;, &apos;U&apos;) IS NULL
        ///	CREATE TABLE Instances
        ///	(
        ///		Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        ///		HostName VARCHAR(64) NOT NULL,
        ///		InstanceName VARCHAR(64) NULL,
        ///		Status INT NULL,
        ///		Version VARCHAR(64) NULL,
        ///		Added DATETIME NOT NULL,
        ///		Modified [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateDb {
            get {
                return ResourceManager.GetString("CreateDb", resourceCulture);
            }
        }
    }
}
