using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        public BasicIntervalSchedule(long globalId) : base(globalId) { }

        private DateTime startTime = DateTime.Now;
        private UnitMultiplier valueOneMultipler;
        private UnitSymbol valueOneUnit;
        private UnitMultiplier valueTwoMultipler;
        private UnitSymbol valueTwoUnit;

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public UnitMultiplier ValueOneMultipler { get => valueOneMultipler; set => valueOneMultipler = value; }
        public UnitSymbol ValueOneUnit { get => valueOneUnit; set => valueOneUnit = value; }
        public UnitMultiplier ValueTwoMultipler { get => valueTwoMultipler; set => valueTwoMultipler = value; }
        public UnitSymbol ValueTwoUnit { get => valueTwoUnit; set => valueTwoUnit = value; }
       
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                BasicIntervalSchedule x = (BasicIntervalSchedule)obj;
                return (x.startTime == this.startTime &&
                       x.valueOneMultipler == this.valueOneMultipler &&
                       x.valueOneUnit == this.valueOneUnit &&
                       x.valueTwoMultipler == this.valueTwoMultipler &&
                       x.valueTwoUnit == this.valueTwoUnit);
            }
            else
            {
                return false;
            }
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTIME:
                case ModelCode.BASICINTERVALSCHEDULE_VALUEONEMULT:
                case ModelCode.BASICINTERVALSCHEDULE_VALUEONEUNIT:
                case ModelCode.BASICINTERVALSCHEDULE_VALUETWOMULT:
                case ModelCode.BASICINTERVALSCHEDULE_VALUETWOUNIT:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTIME:
                    property.SetValue(startTime);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUEONEMULT:
                    property.SetValue((short)valueOneMultipler);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUEONEUNIT:
                    property.SetValue((short)valueOneMultipler);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUETWOMULT:
                    property.SetValue((short)valueTwoMultipler);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUETWOUNIT:
                    property.SetValue((short)valueTwoUnit);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTIME:
                    startTime = property.AsDateTime();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUEONEMULT:
                    valueOneMultipler = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUEONEUNIT:
                    valueOneUnit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUETWOMULT:
                    valueTwoMultipler = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUETWOUNIT:
                    valueTwoUnit = (UnitSymbol)property.AsEnum();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}
