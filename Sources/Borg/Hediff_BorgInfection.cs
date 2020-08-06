using System;
using RimWorld;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x0200000C RID: 12
	public class Hediff_BorgInfection : Hediff_Injury
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003080 File Offset: 0x00001280
		public override void Notify_PawnDied()
		{
			base.Notify_PawnDied();
			bool flag = !this.pawn.def.race.Animal;
			bool flag2 = flag;
			if (flag2)
			{
				Corpse corpse = this.pawn.Corpse;
				Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("BorgDrone3"), FactionUtility.DefaultFactionFrom(FactionDef.Named("BorgCollective")));
				pawn.Position = corpse.Position;
				pawn.SpawnSetup(corpse.Map, false);
				bool flag3 = corpse != null;
				bool flag4 = flag3;
				if (flag4)
				{
					corpse.Destroy(0);
				}
			}
			else
			{
				bool animal = this.pawn.def.race.Animal;
				bool flag5 = animal;
				if (flag5)
				{
					Messages.Message("an animal has succumbed to nanite infection, and have been deemed inappropriate for assimilation. The nanites have consumed and destroyed the corpse.", MessageTypeDefOf.NeutralEvent, true);
					this.pawn.Corpse.Destroy(0);
				}
			}
		}
	}
}
