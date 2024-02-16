using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private List<long> regulatingControl = new List<long>();
        private long conductionEquipment = 0;

        public List<long> RegulatingControl { get => regulatingControl; set => regulatingControl = value; }
        public long ConductionEquipment { get => conductionEquipment; set => conductionEquipment = value; }


        public Terminal(long globalId) : base(globalId) { }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return (x.ConductionEquipment == this.ConductionEquipment &&
                    CompareHelper.CompareLists(x.regulatingControl, this.regulatingControl));
            }
            else
            {
                return false;
            }

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.TERMINAL_REGULATINGCONTROL:
                case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TERMINAL_REGULATINGCONTROL:
                    prop.SetValue(regulatingControl);
                    break;
                case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
                    prop.SetValue(conductionEquipment);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id) {
                case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
                    conductionEquipment = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return regulatingControl.Count != 0 || base.IsReferenced;
            }
        }


        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (conductionEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONDUCTINGEQUIPMENT] = new List<long>();
                references[ModelCode.TERMINAL_CONDUCTINGEQUIPMENT].Add(conductionEquipment);
            }
            if (regulatingControl != null && regulatingControl.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_REGULATINGCONTROL] = regulatingControl.GetRange(0, regulatingControl.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATINGCONTROL_TERMINAL:
                    regulatingControl.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATINGCONTROL_TERMINAL:
                    if (regulatingControl.Contains(globalId))
                    {
                        regulatingControl.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

    }
}
