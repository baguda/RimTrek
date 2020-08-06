using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x02000008 RID: 8
	[StaticConstructorOnStartup]
	public static class HarmonyPatches
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002CC0 File Offset: 0x00000EC0
		static HarmonyPatches()
		{
			Harmony harmony = new Harmony("rimworld.BorgRaceFork");
			harmony.Patch(AccessTools.Method(typeof(CompRefuelable), "ConsumeFuel", null, null), null, new HarmonyMethod(typeof(HarmonyPatches), "CompRefuelable_ConsumeFuel_PostFix", null), null, null);
			harmony.Patch(AccessTools.Method(typeof(Pawn), "PostApplyDamage", null, null), null, new HarmonyMethod(typeof(HarmonyPatches), "Pawn_PostApplyDamage_PostFix", null), null, null);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002D44 File Offset: 0x00000F44
		private static void Pawn_PostApplyDamage_PostFix(Pawn __instance, DamageInfo dinfo, float totalDamageDealt)
		{
			Pawn pawn = null;
			try
			{
				pawn = (dinfo.Instigator as Pawn);
			}
			catch
			{
				ModCore.Log("HarmonyPatches pawn = (dinfo.Instigator as Pawn); failed");
			}
			bool flag = pawn != null && __instance != null;
			if (flag)
			{
				bool dead = __instance.Dead;
				if (dead)
				{
					bool humanlike = __instance.RaceProps.Humanlike;
					bool flag2 = humanlike;
					if (flag2)
					{
						bool flag3 = pawn.def.race.body.defName.Equals("Borg");
						if (flag3)
						{
							bool flag4 = pawn.equipment.Primary.def.defName.Equals("Borg_InjectorWhipPlayer") || pawn.equipment.Primary.def.defName.Equals("Borg_InjectorWhip");
							if (flag4)
							{
								SpawnUtilityBorg spawnUtilityBorg = new SpawnUtilityBorg();
								spawnUtilityBorg.spawnBorgPawn(__instance, pawn.Faction.def.defName, pawn.Map);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002E58 File Offset: 0x00001058
		private static void CompRefuelable_ConsumeFuel_PostFix(CompRefuelable __instance, float amount)
		{
		}
	}
}
