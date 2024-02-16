using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
   public class DayType: IdentifiedObject
    {
        private List<long> seasonDTShedules = new List<long>();
        public DayType(long globalId) : base(globalId)
        {
        }

        public List<long> SeasonDTShedules { get => seasonDTShedules; set => seasonDTShedules = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                DayType x = (DayType)obj;
                return (CompareHelper.CompareLists(x.seasonDTShedules, this.seasonDTShedules));
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
                case ModelCode.DAYTYPE_SEASONDAYTYPESCHEDULES:
                    return true;
                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.DAYTYPE_SEASONDAYTYPESCHEDULES:
                    prop.SetValue(seasonDTShedules);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }

        }
        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }

        public override bool IsReferenced
        {
            get
            {
                return seasonDTShedules.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (seasonDTShedules != null && seasonDTShedules.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.DAYTYPE_SEASONDAYTYPESCHEDULES] = seasonDTShedules.GetRange(0, seasonDTShedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:
                    seasonDTShedules.Add(globalId);
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
                case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:

                    if (seasonDTShedules.Contains(globalId))
                    {
                        seasonDTShedules.Remove(globalId);
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
