using System;
using RimWorld;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x0200000D RID: 13
	public class Hediff_BorgInfectionPlayer : Hediff_Injury
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00003164 File Offset: 0x00001364
		public override void Notify_PawnDied()
		{
			base.Notify_PawnDied();
			bool flag = !this.pawn.def.race.Animal || !this.pawn.def.race.IsMechanoid;
			bool flag2 = flag;
			if (flag2)
			{
				Corpse corpse = this.pawn.Corpse;
				Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("PlayerBorgDrone"), FactionUtility.DefaultFactionFrom(FactionDef.Named("BorgCollective")));
				pawn.SetFaction(Faction.OfPlayer, null);
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
				bool isMechanoid = this.pawn.def.race.IsMechanoid;
				bool flag5 = animal;
				if (flag5)
				{
					Messages.Message("an animal has succumbed to the nanite infection, and have been deemed inappropriate for assimilation. The nanites have consumed and destroyed the corpse.", MessageTypeDefOf.NeutralEvent, true);
					this.pawn.Corpse.Destroy(0);
				}
				bool flag6 = isMechanoid;
				if (flag6)
				{
					Messages.Message("an Mechinoid has succumbed to the nanite infection, and have been deemed inappropriate for assimilation. The nanites have consumed and destroyed the corpse.", MessageTypeDefOf.NeutralEvent, true);
					this.pawn.Corpse.Destroy(0);
				}
			}
		}
	}
}
