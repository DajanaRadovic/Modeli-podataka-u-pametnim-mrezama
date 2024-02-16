//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN {
    using System;
    using FTN;
    
    
    /// Specifies a set of equipment that works together to control a power system quantity such as voltage or flow.
    public class RegulatingControl : PowerSystemResource {
        
        /// The regulation is performed in a discrete mode.
        private System.Boolean? cim_discrete;
        
        private const bool isDiscreteMandatory = true;
        
        private const string _discretePrefix = "cim";
        
        /// The regulating control mode presently available.  This specifications allows for determining the kind of regualation without need for obtaining the units from a schedule.
        private RegulatingControlModeKind? cim_mode;
        
        private const bool isModeMandatory = true;
        
        private const string _modePrefix = "cim";
        
        /// Phase voltage controlling this regulator, measured at regulator location.
        private PhaseCode? cim_monitoredPhase;
        
        private const bool isMonitoredPhaseMandatory = true;
        
        private const string _monitoredPhasePrefix = "cim";
        
        /// This is the case input target range.   This performs the same function as the value2 attribute on the regulation schedule in the case that schedules are not used.   The units of those appropriate for the mode.
        private System.Single? cim_targetRange;
        
        private const bool isTargetRangeMandatory = true;
        
        private const string _targetRangePrefix = "cim";
        
        /// The target value specified for case input.   This value can be used for the target value wihout the use of schedules. The value has the units appropriate to the mode attribute.
        private System.Single? cim_targetValue;
        
        private const bool isTargetValueMandatory = true;
        
        private const string _targetValuePrefix = "cim";
        
        /// The terminal associated with this regulating control.  The terminal is associated instead of a node, since the terminal could connect into either a topological node (bus in bus-branch model) or a connectivity node (detailed switch model).  Sometimes it is useful to model regulation at a terminal of a bus bar object since the bus bar can be present in both a bus-branch model or a model with switch detail.
        private Terminal cim_Terminal;
        
        private const bool isTerminalMandatory = false;
        
        private const string _TerminalPrefix = "cim";
        
        public virtual bool Discrete {
            get {
                return this.cim_discrete.GetValueOrDefault();
            }
            set {
                this.cim_discrete = value;
            }
        }
        
        public virtual bool DiscreteHasValue {
            get {
                return this.cim_discrete != null;
            }
        }
        
        public static bool IsDiscreteMandatory {
            get {
                return isDiscreteMandatory;
            }
        }
        
        public static string DiscretePrefix {
            get {
                return _discretePrefix;
            }
        }
        
        public virtual RegulatingControlModeKind Mode {
            get {
                return this.cim_mode.GetValueOrDefault();
            }
            set {
                this.cim_mode = value;
            }
        }
        
        public virtual bool ModeHasValue {
            get {
                return this.cim_mode != null;
            }
        }
        
        public static bool IsModeMandatory {
            get {
                return isModeMandatory;
            }
        }
        
        public static string ModePrefix {
            get {
                return _modePrefix;
            }
        }
        
        public virtual PhaseCode MonitoredPhase {
            get {
                return this.cim_monitoredPhase.GetValueOrDefault();
            }
            set {
                this.cim_monitoredPhase = value;
            }
        }
        
        public virtual bool MonitoredPhaseHasValue {
            get {
                return this.cim_monitoredPhase != null;
            }
        }
        
        public static bool IsMonitoredPhaseMandatory {
            get {
                return isMonitoredPhaseMandatory;
            }
        }
        
        public static string MonitoredPhasePrefix {
            get {
                return _monitoredPhasePrefix;
            }
        }
        
        public virtual float TargetRange {
            get {
                return this.cim_targetRange.GetValueOrDefault();
            }
            set {
                this.cim_targetRange = value;
            }
        }
        
        public virtual bool TargetRangeHasValue {
            get {
                return this.cim_targetRange != null;
            }
        }
        
        public static bool IsTargetRangeMandatory {
            get {
                return isTargetRangeMandatory;
            }
        }
        
        public static string TargetRangePrefix {
            get {
                return _targetRangePrefix;
            }
        }
        
        public virtual float TargetValue {
            get {
                return this.cim_targetValue.GetValueOrDefault();
            }
            set {
                this.cim_targetValue = value;
            }
        }
        
        public virtual bool TargetValueHasValue {
            get {
                return this.cim_targetValue != null;
            }
        }
        
        public static bool IsTargetValueMandatory {
            get {
                return isTargetValueMandatory;
            }
        }
        
        public static string TargetValuePrefix {
            get {
                return _targetValuePrefix;
            }
        }
        
        public virtual Terminal Terminal {
            get {
                return this.cim_Terminal;
            }
            set {
                this.cim_Terminal = value;
            }
        }
        
        public virtual bool TerminalHasValue {
            get {
                return this.cim_Terminal != null;
            }
        }
        
        public static bool IsTerminalMandatory {
            get {
                return isTerminalMandatory;
            }
        }
        
        public static string TerminalPrefix {
            get {
                return _TerminalPrefix;
            }
        }
    }
}
