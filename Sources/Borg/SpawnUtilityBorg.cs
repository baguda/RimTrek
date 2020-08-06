using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace BorgAssimilate
{
	// Token: 0x02000007 RID: 7
	internal class SpawnUtilityBorg
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000262C File Offset: 0x0000082C
		~SpawnUtilityBorg()
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002658 File Offset: 0x00000858
		public void SpawnPawn(Pawn deadPawn, string factionName, Map map)
		{
			bool flag = false;
			bool flag2 = factionName.Equals(Faction.OfPlayer.def.defName);
			Faction faction = FactionUtility.DefaultFactionFrom(FactionDef.Named(factionName));
			PawnGenerationRequest pawnGenerationRequest;
			pawnGenerationRequest..ctor((!flag2) ? this.hostilePawnKind : this.playerPawnKind, faction, (!flag2) ? 2 : 1, -1, true, flag, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null);
			Pawn pawn = PawnGenerator.GeneratePawn(pawnGenerationRequest);
			GenSpawn.Spawn(pawn, deadPawn.Position, deadPawn.Map, 0);
			bool flag3 = !flag2;
			if (flag3)
			{
				LordJob lordJob = new LordJob_AssaultColony(faction, true, true, false, false, true);
				Lord lord = LordMaker.MakeNewLord(Faction.OfAncientsHostile, lordJob, deadPawn.Map, null);
				lord.AddPawn(pawn);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002758 File Offset: 0x00000958
		public void spawnBorgPawn(Pawn deadPawn, string factionName, Map map)
		{
			try
			{
				bool flag = factionName.Equals(Faction.OfPlayer.def.defName);
				Faction faction = FactionUtility.DefaultFactionFrom(FactionDef.Named(factionName));
				Pawn pawn = PawnGenerator.GeneratePawn(flag ? this.playerPawnKind : this.hostilePawnKind, faction);
				pawn.Position = deadPawn.Position;
				bool flag2 = map != null;
				if (flag2)
				{
					ModCore.Log("valid map");
					pawn.SpawnSetup(map, false);
				}
				else
				{
					ModCore.Log("invalid map");
				}
				bool flag3 = flag;
				if (flag3)
				{
					pawn.SetFaction(Faction.OfPlayer, null);
				}
				else
				{
					IntVec3 baseCenter;
					baseCenter..ctor(map.Size.x / 2, 0, map.Size.z / 2);
					LordJob_DefendShip lordJob_DefendShip = new LordJob_DefendShip(faction, baseCenter);
					Lord lord = LordMaker.MakeNewLord(faction, lordJob_DefendShip, map, null);
					lord.AddPawn(pawn);
				}
				bool flag4 = !deadPawn.Destroyed;
				if (flag4)
				{
					deadPawn.Destroy(0);
				}
				Corpse corpse = deadPawn.Corpse;
				bool flag5 = corpse != null;
				bool flag6 = flag5;
				if (flag6)
				{
					corpse.Destroy(0);
				}
			}
			catch
			{
				ModCore.Log("Spawn Error Handled");
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028A0 File Offset: 0x00000AA0
		public void spawnBorgPawn2(bool isPlayer, PawnKindDef pawnKind, IntVec3 location, Map map)
		{
			try
			{
				Pawn pawn = PawnGenerator.GeneratePawn((pawnKind != null) ? pawnKind : this.hostilePawnKind, isPlayer ? Faction.OfPlayer : Faction.OfAncientsHostile);
				pawn.Position = location;
				bool flag = map != null;
				if (flag)
				{
					ModCore.Log("valid map");
					pawn.SpawnSetup(map, false);
				}
				else
				{
					ModCore.Log("invalid map");
					pawn.SpawnSetup(pawn.Map, false);
				}
				if (isPlayer)
				{
					pawn.SetFaction(Faction.OfPlayer, null);
				}
				else
				{
					IntVec3 intVec;
					intVec..ctor(map.Size.x / 2, 0, map.Size.z / 2);
					LordJob lordJob = new LordJob_AssaultColony(Faction.OfAncientsHostile, true, true, false, false, true);
					Lord lord = LordMaker.MakeNewLord(Faction.OfAncientsHostile, lordJob, map, null);
					lord.AddPawn(pawn);
				}
			}
			catch
			{
				ModCore.Log("Spawn Error Handled");
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029A0 File Offset: 0x00000BA0
		public void spawnBorgPawn3(bool isPlayer, PawnKindDef pawnKind, IntVec3 location, Map map)
		{
			try
			{
				Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(DefDatabase<PawnKindDef>.GetNamed("BorgDrone3", true), Faction.OfAncientsHostile, 2, -1, false, false, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null));
				GenSpawn.Spawn(pawn, location, map, 0);
				pawn.Position = location;
				bool flag = map != null;
				if (flag)
				{
					ModCore.Log("valid map");
					pawn.SpawnSetup(map, false);
				}
				else
				{
					ModCore.Log("invalid map");
					pawn.SpawnSetup(pawn.Map, false);
				}
				if (isPlayer)
				{
					pawn.SetFaction(Faction.OfPlayer, null);
				}
				else
				{
					IntVec3 intVec;
					intVec..ctor(map.Size.x / 2, 0, map.Size.z / 2);
					LordJob lordJob = new LordJob_AssaultColony(Faction.OfAncientsHostile, true, true, false, false, true);
					Lord lord = LordMaker.MakeNewLord(Faction.OfAncientsHostile, lordJob, map, null);
					lord.AddPawn(pawn);
				}
			}
			catch
			{
				ModCore.Log("Spawn Error Handled");
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B08 File Offset: 0x00000D08
		public bool spawnBorgPawnX(bool isPlayer, PawnKindDef pawnKind, IntVec3 location, Map map)
		{
			ModCore.Log("spawn start");
			bool flag = this.hostilePawnKind == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int count = this.hostilePawnKind.lifeStages.Count;
				ModCore.Log("spawn cont");
				Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(this.hostilePawnKind, Faction.OfAncientsHostile, 2, -1, false, false, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, null, 1f, null, null, null, null, null, new float?(this.hostilePawnKind.race.race.lifeStageAges[count].minAge), null, null, null, null, null, null));
				ModCore.Log("spawn made");
				GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(location, map, 5, null), map, 0);
				ModCore.Log("spawn init");
				LordJob lordJob = new LordJob_AssaultColony(Faction.OfAncientsHostile, true, true, false, false, true);
				Lord lord = LordMaker.MakeNewLord(Faction.OfAncientsHostile, lordJob, map, null);
				lord.AddPawn(pawn);
				ModCore.Log("spawn assigned");
				result = true;
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C48 File Offset: 0x00000E48
		public static bool isBorg(Pawn pawn)
		{
			return pawn.def.race.body.defName.Equals("borg");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C7C File Offset: 0x00000E7C
		private PawnKindDef getPlayerKind()
		{
			return this.playerPawnKind;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C94 File Offset: 0x00000E94
		private PawnKindDef getHostileKind()
		{
			return this.hostilePawnKind;
		}

		// Token: 0x0400000C RID: 12
		public static BorgConfigDef borgDef = DefDatabase<BorgConfigDef>.GetNamed("BorgConfig", true);

		// Token: 0x0400000D RID: 13
		public float spawnChanceAI = 1f;

		// Token: 0x0400000E RID: 14
		public float spawnChancePlayer = 1f;

		// Token: 0x0400000F RID: 15
		public PawnKindDef playerPawnKind = PawnKindDef.Named(SpawnUtilityBorg.borgDef.playerSpawnkind);

		// Token: 0x04000010 RID: 16
		public PawnKindDef hostilePawnKind = PawnKindDef.Named(SpawnUtilityBorg.borgDef.AISpawnKind);

		// Token: 0x04000011 RID: 17
		private Faction factionHostile = Faction.OfAncientsHostile;

		// Token: 0x04000012 RID: 18
		public List<Pawn> spawnedPawns;
	}
}
