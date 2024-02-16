namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				//PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimIdentifiedObject, rd);
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		

		public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
		{
			if ((cimBasicIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

				if (cimBasicIntervalSchedule.StartTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTIME, cimBasicIntervalSchedule.StartTime));
				}
				if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUEONEMULT, (short)GetDMSUnitMultipler(cimBasicIntervalSchedule.Value1Multiplier)));
				}
				if (cimBasicIntervalSchedule.Value1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUEONEUNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
				}
				if (cimBasicIntervalSchedule.Value2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUETWOMULT, (short)GetDMSUnitMultipler(cimBasicIntervalSchedule.Value2Multiplier)));
				}
				if (cimBasicIntervalSchedule.Value2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUETWOUNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value2Unit)));
				}
			}
		}

		public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
		{
			if ((cimRegularIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);
			}
		}

		public static void PopulateSeasonDayTypeSheduleProperties(FTN.SeasonDayTypeSchedule cimSeasonDayTypeShedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeasonDayTypeShedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeShedule, rd);

				if (cimSeasonDayTypeShedule.DayTypeHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSeasonDayTypeShedule.DayType.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeShedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeShedule.ID);
						report.Report.Append("\" - Failed to set reference to Regulating Cond Eq: rdfID \"").Append(cimSeasonDayTypeShedule.DayType.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE, gid));
				}
				if (cimSeasonDayTypeShedule.SeasonHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSeasonDayTypeShedule.Season.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeShedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeShedule.ID);
						report.Report.Append("\" - Failed to set reference to Regulating Cond Eq: rdfID \"").Append(cimSeasonDayTypeShedule.Season.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SEASONDAYTYPESCHEDULE_SEASON, gid));
				}

				

			}
		}

		/*	public static void PopulateRegulationScheduleProperties(FTN.RegulationSchedule cimSeasonDayTypeShedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
			{
				if ((cimSeasonDayTypeShedule != null) && (rd != null))
				{
					PowerTransformerConverter.PopulateSeasonDayTypeSheduleProperties(cimSeasonDayTypeShedule, rd, importHelper, report);

					if (cimSeasonDayTypeShedule.RegulatingControlHasValue)
					{
						long gid = importHelper.GetMappedGID(cimSeasonDayTypeShedule.RegulatingControl.ID);
						if (gid < 0)
						{
							report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeShedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeShedule.ID);
							report.Report.Append("\" - Failed to set reference to Regulating Cond Eq: rdfID \"").Append(cimSeasonDayTypeShedule.Season.ID).AppendLine(" \" is not mapped to GID!");
						}
						rd.AddProperty(new Property(ModelCode.REGULATIONSCHEDULE_REGULATIONCNTRL, gid));
					}
				}
			}*/

			public static void PopulateRegulationScheduleProperties(FTN.RegulationSchedule cimRegulationShedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegulationShedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSeasonDayTypeSheduleProperties(cimRegulationShedule, rd, importHelper, report);

				if (cimRegulationShedule.RegulatingControlHasValue)
				{
					long gid = importHelper.GetMappedGID(cimRegulationShedule.RegulatingControl.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimRegulationShedule.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulationShedule.ID);
						report.Report.Append("\" - Failed to set reference to Regulating Cond Eq: rdfID \"").Append(cimRegulationShedule.RegulatingControl.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL, gid));
				}
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}
				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment,rd);

			}
		}

		public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTerminal != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

				if (cimTerminal.ConductingEquipmentHasValue)
				{
					long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
						report.Report.Append("\" - Failed to set reference to Regulating Cond Eq: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.TERMINAL_CONDUCTINGEQUIPMENT, gid));
				}
			}
		}

		public static void PopulateTapChangerProperties(FTN.TapChanger cimTapChanger, ResourceDescription rd)
		{
			if ((cimTapChanger != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimTapChanger, rd);
			}
		}

		public static void PopulateTapShedulesProperties(FTN.TapSchedule cimTapShedules, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTapShedules != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSeasonDayTypeSheduleProperties(cimTapShedules, rd, importHelper, report);
				if (cimTapShedules.TapChangerHasValue)
				{
					long gid = importHelper.GetMappedGID(cimTapShedules.TapChanger.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimTapShedules.GetType().ToString()).Append(" rdfID = \"").Append(cimTapShedules.ID);
						report.Report.Append("\" - Failed to set reference to TapSchedule: rdfID \"").Append(cimTapShedules.TapChanger.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.TAPSCHEDULE_TAPCHANGER, gid));
				}
			}
		}

		public static void PopulateDayTypeProperties(FTN.DayType cimDayType, ResourceDescription rd)
		{
			if ((cimDayType != null) && (rd != null))
			{ 				
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimDayType, rd);
			}
		}

		public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulationControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegulationControl != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimRegulationControl, rd);

				if (cimRegulationControl.DiscreteHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_DISCRETE, cimRegulationControl.Discrete));
				}
				if (cimRegulationControl.ModeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MODE, (short)GetDMSRegulatingControlModelKind(cimRegulationControl.Mode)));
				}
				if (cimRegulationControl.MonitoredPhaseHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MONITOREDPHASE, (short)GetDMSPhaseCode(cimRegulationControl.MonitoredPhase)));
				}
				if (cimRegulationControl.TargetRangeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETRANGE, cimRegulationControl.TargetRange));
				}
				if (cimRegulationControl.TargetValueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETVALUE, cimRegulationControl.TargetValue));
				}
				if (cimRegulationControl.TerminalHasValue)
				{
					long gid = importHelper.GetMappedGID(cimRegulationControl.Terminal.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimRegulationControl.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulationControl.ID);
						report.Report.Append("\" - Failed to set reference to Regulating Control: rdfID \"").Append(cimRegulationControl.Terminal.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TERMINAL, gid));
				}
			}
		}

		public static void PopulateSeasonProperties(FTN.Season cimSeason, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeason != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSeason, rd);

				if (cimSeason.EndDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEASON_ENDDATE, cimSeason.EndDate));
				}
				if (cimSeason.StartDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEASON_STARTDATE, cimSeason.StartDate));
				}
			}
		}




		#endregion Populate ResourceDescription

		#region Enums convert
		public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.Unknown;
			}
		}

		public static RegulatingControlModelKind GetDMSRegulatingControlModelKind(FTN.RegulatingControlModeKind phase)
		{
			switch (phase)
			{

				case FTN.RegulatingControlModeKind.voltage:
					return RegulatingControlModelKind.Voltage;
				case FTN.RegulatingControlModeKind.activePower:
					return RegulatingControlModelKind.ActivePower;
				case FTN.RegulatingControlModeKind.reactivePower:
					return RegulatingControlModelKind.ReactivePover;
				case FTN.RegulatingControlModeKind.currentFlow:
					return RegulatingControlModelKind.CurrentFlow;
				case FTN.RegulatingControlModeKind.@fixed:
					return RegulatingControlModelKind.Fixed;
				case FTN.RegulatingControlModeKind.admittance:
					return RegulatingControlModelKind.Admittance;
				case FTN.RegulatingControlModeKind.timeScheduled:
					return RegulatingControlModelKind.TimeScheduled;
				case RegulatingControlModeKind.temperature:
					return RegulatingControlModelKind.Temperature;
				case FTN.RegulatingControlModeKind.powerFactor:
					return RegulatingControlModelKind.PowerFactor;
				default:
					return RegulatingControlModelKind.Unknown;


			}
		}

		public static UnitMultiplier GetDMSUnitMultipler(FTN.UnitMultiplier unitMultipler)
		{
			switch (unitMultipler)
			{

				case FTN.UnitMultiplier.c:
					return UnitMultiplier.c;
				case FTN.UnitMultiplier.d:
					return UnitMultiplier.d;
				case FTN.UnitMultiplier.G:
					return UnitMultiplier.G;
				case FTN.UnitMultiplier.k:
					return UnitMultiplier.k;
				case FTN.UnitMultiplier.m:
					return UnitMultiplier.m;
				case FTN.UnitMultiplier.M:
					return UnitMultiplier.M;
				case FTN.UnitMultiplier.micro:
					return UnitMultiplier.micro;
				case FTN.UnitMultiplier.n:
					return UnitMultiplier.n;
				case FTN.UnitMultiplier.none:
					return UnitMultiplier.none;
				case FTN.UnitMultiplier.p:
					return UnitMultiplier.p;
				case FTN.UnitMultiplier.T:
					return UnitMultiplier.T;

				default:
					return UnitMultiplier.Unknown;


			}


		}

		public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
		{
			switch (unitSymbol)
			{

				case FTN.UnitSymbol.A:
					return UnitSymbol.A;
				case FTN.UnitSymbol.F:
					return UnitSymbol.F;
				case FTN.UnitSymbol.H:
					return UnitSymbol.H;
				case FTN.UnitSymbol.Hz:
					return UnitSymbol.Hz;
				default:
					return UnitSymbol.Unknown;
			}

		}

				#endregion Enums convert
	}
}
