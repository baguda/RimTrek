using System;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace RimWorld
{
	// Token: 0x02000003 RID: 3
	[StaticConstructorOnStartup]
	internal class LordToil_DefendShip : LordToil
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		public override IntVec3 FlagLoc
		{
			get
			{
				return this.baseCenter;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		public LordToil_DefendShip(IntVec3 baseCenter)
		{
			this.baseCenter = baseCenter;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
		public override void UpdateAllDuties()
		{
			for (int i = 0; i < this.lord.ownedPawns.Count; i++)
			{
				this.lord.ownedPawns[i].mindState.duty = new PawnDuty(LordToil_DefendShip.defendShip, this.baseCenter, -1f);
			}
		}

		// Token: 0x04000003 RID: 3
		private static DutyDef defendShip = DefDatabase<DutyDef>.GetNamed("SoSDefendShip", true);

		// Token: 0x04000004 RID: 4
		public IntVec3 baseCenter;
	}
}
