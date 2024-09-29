//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightQualityAnalyzer.Infrastructure {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FlightQualityAnalyzer.Infrastructure.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CSV line does not contain enough values to create a Flight object..
        /// </summary>
        public static string CsvLineError {
            get {
                return ResourceManager.GetString("CsvLineError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred..
        /// </summary>
        public static string FailureMessage {
            get {
                return ResourceManager.GetString("FailureMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified CSV file was not found..
        /// </summary>
        public static string FileNotFound {
            get {
                return ResourceManager.GetString("FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no next flight in the chain from {0} back to {1} after the arrival time of {2}..
        /// </summary>
        public static string InconsistentFlightChainFound {
            get {
                return ResourceManager.GetString("InconsistentFlightChainFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error parsing flight data: {0}.
        /// </summary>
        public static string ParsingError {
            get {
                return ResourceManager.GetString("ParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operation completed successfully..
        /// </summary>
        public static string SuccessMessage {
            get {
                return ResourceManager.GetString("SuccessMessage", resourceCulture);
            }
        }
    }
}
