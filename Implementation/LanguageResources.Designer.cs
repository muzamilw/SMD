﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMD.Implementation {
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
    internal class LanguageResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LanguageResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SMD.Implementation.LanguageResources", typeof(LanguageResources).Assembly);
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
        ///   Looks up a localized string similar to No identity provider claim specified. Cannot proceed..
        /// </summary>
        internal static string ClaimsSecurityService_IdentityProviderClaim {
            get {
                return ResourceManager.GetString("ClaimsSecurityService_IdentityProviderClaim", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No name identifier claim specified. Cannot proceed..
        /// </summary>
        internal static string ClaimsSecurityService_MissingNameIdentifierClaim {
            get {
                return ResourceManager.GetString("ClaimsSecurityService_MissingNameIdentifierClaim", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UserName or Password is Invalid..
        /// </summary>
        internal static string WebApiUserService_InvalidCredentials {
            get {
                return ResourceManager.GetString("WebApiUserService_InvalidCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is invalid..
        /// </summary>
        internal static string WebApiUserService_InvalidEmail {
            get {
                return ResourceManager.GetString("WebApiUserService_InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Login info found for provided email..
        /// </summary>
        internal static string WebApiUserService_LoginInfoNotFound {
            get {
                return ResourceManager.GetString("WebApiUserService_LoginInfoNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Provider Key is invalid..
        /// </summary>
        internal static string WebApiUserService_ProviderKeyInvalid {
            get {
                return ResourceManager.GetString("WebApiUserService_ProviderKeyInvalid", resourceCulture);
            }
        }
    }
}
