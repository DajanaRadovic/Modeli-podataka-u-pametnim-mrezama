using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class Season : IdentifiedObject
    {
        private DateTime endDate = DateTime.Now;
        private DateTime startDate = DateTime.Now;
        private List<long> seasonDTShedules = new List<long>();
        public Season(long globalId) : base(globalId) { }

        public DateTime EndDate { get => endDate; set => endDate = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public List<long> SeasonDTShedules { get => seasonDTShedules; set => seasonDTShedules = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Season x = (Season)obj;
                return (x.EndDate == this.EndDate &&
                        x.StartDate == this.StartDate &&
                        CompareHelper.CompareLists(x.seasonDTShedules, this.seasonDTShedules));
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SEASON_ENDDATE:
                case ModelCode.SEASON_STARTDATE:
                case ModelCode.SEASON_DAYTYPESCHEDULES:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SEASON_ENDDATE:
                    prop.SetValue(endDate);
                    break;
                case ModelCode.SEASON_STARTDATE:
                    prop.SetValue(startDate);
                    break;
                case ModelCode.SEASON_DAYTYPESCHEDULES:
                    prop.SetValue(seasonDTShedules);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEASON_ENDDATE:
                    endDate = property.AsDateTime();
                    break;
                case ModelCode.SEASON_STARTDATE:
                    startDate = property.AsDateTime();
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
                return seasonDTShedules.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (seasonDTShedules != null && seasonDTShedules.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASON_DAYTYPESCHEDULES] = seasonDTShedules.GetRange(0, seasonDTShedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SEASONDAYTYPESCHEDULE_SEASON:
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
                case ModelCode.SEASONDAYTYPESCHEDULE_SEASON:

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
