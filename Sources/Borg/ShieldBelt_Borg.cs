using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace BorgAssimilate
{
	// Token: 0x0200000E RID: 14
	[StaticConstructorOnStartup]
	public class ShieldBelt_Borg : Apparel
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000032B0 File Offset: 0x000014B0
		private float EnergyMax
		{
			get
			{
				return StatExtension.GetStatValue(this, StatDefOf.EnergyShieldEnergyMax, true);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000032D0 File Offset: 0x000014D0
		private float EnergyGainPerTick
		{
			get
			{
				return StatExtension.GetStatValue(this, StatDefOf.EnergyShieldRechargeRate, true) / 60f;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000032F4 File Offset: 0x000014F4
		public float Energy
		{
			get
			{
				return this.energy;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000330C File Offset: 0x0000150C
		public ShieldState ShieldState
		{
			get
			{
				bool flag = this.ticksToReset > 0;
				ShieldState result;
				if (flag)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003334 File Offset: 0x00001534
		private bool ShouldDisplay
		{
			get
			{
				Pawn wearer = base.Wearer;
				return wearer.Spawned && !wearer.Dead && !wearer.Downed && (wearer.InAggroMentalState || wearer.Drafted || (FactionUtility.HostileTo(wearer.Faction, Faction.OfPlayer) && !wearer.IsPrisoner) || Find.TickManager.TicksGame < this.lastKeepDisplayTick + this.KeepDisplayingTicks);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000033B0 File Offset: 0x000015B0
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<float>(ref this.energy, "energy", 0f, false);
			Scribe_Values.Look<int>(ref this.ticksToReset, "ticksToReset", -1, false);
			Scribe_Values.Look<int>(ref this.lastKeepDisplayTick, "lastKeepDisplayTick", 0, false);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003402 File Offset: 0x00001602
		public override IEnumerable<Gizmo> GetWornGizmos()
		{
			bool flag = Find.Selector.SingleSelectedThing == base.Wearer;
			if (flag)
			{
				yield return new Gizmo_EnergyShieldStatus_Borg
				{
					shield = this
				};
			}
			yield break;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003414 File Offset: 0x00001614
		public override float GetSpecialApparelScoreOffset()
		{
			return this.EnergyMax * this.ApparelScorePerEnergyMax;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003434 File Offset: 0x00001634
		public override void Tick()
		{
			base.Tick();
			bool flag = base.Wearer == null;
			if (flag)
			{
				this.energy = 0f;
			}
			else
			{
				bool flag2 = this.ShieldState == 1;
				if (flag2)
				{
					this.ticksToReset--;
					bool flag3 = this.ticksToReset <= 0;
					if (flag3)
					{
						this.Reset();
					}
				}
				else
				{
					bool flag4 = this.ShieldState == 0;
					if (flag4)
					{
						this.energy += this.EnergyGainPerTick;
						bool flag5 = this.energy > this.EnergyMax;
						if (flag5)
						{
							this.energy = this.EnergyMax;
						}
					}
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000034E4 File Offset: 0x000016E4
		public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
		{
			bool flag = this.ShieldState > 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = dinfo.Def == DamageDefOf.EMP;
				if (flag2)
				{
					this.energy = 0f;
					this.Break();
					result = false;
				}
				else
				{
					bool flag3 = dinfo.Def.isRanged || dinfo.Def.isExplosive;
					if (flag3)
					{
						bool flag4 = dinfo.Def == this.LastDamage;
						if (flag4)
						{
							this.energy -= dinfo.Amount * this.EnergyLossPerDamage;
							this.LastDamage = dinfo.Def;
							bool flag5 = this.energy < 0f;
							if (flag5)
							{
								this.Break();
							}
							else
							{
								this.AbsorbedDamage(dinfo);
							}
							result = true;
						}
						else
						{
							this.LastDamage = dinfo.Def;
							result = false;
						}
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000035DC File Offset: 0x000017DC
		public void KeepDisplaying()
		{
			this.lastKeepDisplayTick = Find.TickManager.TicksGame;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000035F0 File Offset: 0x000017F0
		private void AbsorbedDamage(DamageInfo dinfo)
		{
			SoundStarter.PlayOneShot(SoundDefOf.EnergyShield_AbsorbDamage, new TargetInfo(base.Wearer.Position, base.Wearer.Map, false));
			this.impactAngleVect = Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle);
			Vector3 vector = GenThing.TrueCenter(base.Wearer) + Vector3Utility.RotatedBy(this.impactAngleVect, 180f) * 0.5f;
			float num = Mathf.Min(10f, 2f + dinfo.Amount / 10f);
			MoteMaker.MakeStaticMote(vector, base.Wearer.Map, ThingDefOf.Mote_ExplosionFlash, num);
			int num2 = (int)num;
			for (int i = 0; i < num2; i++)
			{
				MoteMaker.ThrowDustPuff(vector, base.Wearer.Map, Rand.Range(0.8f, 1.2f));
			}
			this.lastAbsorbDamageTick = Find.TickManager.TicksGame;
			this.KeepDisplaying();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000036EC File Offset: 0x000018EC
		private void Break()
		{
			SoundStarter.PlayOneShot(SoundDefOf.EnergyShield_Broken, new TargetInfo(base.Wearer.Position, base.Wearer.Map, false));
			MoteMaker.MakeStaticMote(GenThing.TrueCenter(base.Wearer), base.Wearer.Map, ThingDefOf.Mote_ExplosionFlash, 12f);
			for (int i = 0; i < 6; i++)
			{
				MoteMaker.ThrowDustPuff(GenThing.TrueCenter(base.Wearer) + Vector3Utility.HorizontalVectorFromAngle((float)Rand.Range(0, 360)) * Rand.Range(0.3f, 0.6f), base.Wearer.Map, Rand.Range(0.8f, 1.2f));
			}
			this.energy = 0f;
			this.ticksToReset = this.StartingTicksToReset;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000037CC File Offset: 0x000019CC
		private void Reset()
		{
			bool spawned = base.Wearer.Spawned;
			if (spawned)
			{
				SoundStarter.PlayOneShot(SoundDefOf.EnergyShield_Reset, new TargetInfo(base.Wearer.Position, base.Wearer.Map, false));
				MoteMaker.ThrowLightningGlow(GenThing.TrueCenter(base.Wearer), base.Wearer.Map, 3f);
			}
			this.ticksToReset = -1;
			this.energy = this.EnergyOnReset;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000384C File Offset: 0x00001A4C
		public override void DrawWornExtras()
		{
			bool flag = this.ShieldState == null && this.ShouldDisplay;
			if (flag)
			{
				float num = Mathf.Lerp(1.2f, 1.55f, this.energy);
				Vector3 vector = base.Wearer.Drawer.DrawPos;
				vector.y = Altitudes.AltitudeFor(22);
				int num2 = Find.TickManager.TicksGame - this.lastAbsorbDamageTick;
				bool flag2 = num2 < 8;
				if (flag2)
				{
					float num3 = (float)(8 - num2) / 8f * 0.05f;
					vector += this.impactAngleVect * num3;
					num -= num3;
				}
				float num4 = (float)Rand.Range(0, 360);
				Vector3 vector2;
				vector2..ctor(num, 1f, num);
				Matrix4x4 matrix4x = default(Matrix4x4);
				matrix4x.SetTRS(vector, Quaternion.AngleAxis(num4, Vector3.up), vector2);
				Graphics.DrawMesh(MeshPool.plane10, matrix4x, ShieldBelt_Borg.BubbleMat, 0);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003940 File Offset: 0x00001B40
		public override bool AllowVerbCast(IntVec3 root, Map map, LocalTargetInfo targ, Verb verb)
		{
			return true;
		}

		// Token: 0x04000018 RID: 24
		public DamageDef LastDamage = null;

		// Token: 0x04000019 RID: 25
		private float energy;

		// Token: 0x0400001A RID: 26
		private int ticksToReset = -1;

		// Token: 0x0400001B RID: 27
		private int lastKeepDisplayTick = -9999;

		// Token: 0x0400001C RID: 28
		private Vector3 impactAngleVect;

		// Token: 0x0400001D RID: 29
		private int lastAbsorbDamageTick = -9999;

		// Token: 0x0400001E RID: 30
		private const float MinDrawSize = 1.2f;

		// Token: 0x0400001F RID: 31
		private const float MaxDrawSize = 1.55f;

		// Token: 0x04000020 RID: 32
		private const float MaxDamagedJitterDist = 0.05f;

		// Token: 0x04000021 RID: 33
		private const int JitterDurationTicks = 8;

		// Token: 0x04000022 RID: 34
		private int StartingTicksToReset = 3200;

		// Token: 0x04000023 RID: 35
		private float EnergyOnReset = 0.2f;

		// Token: 0x04000024 RID: 36
		private float EnergyLossPerDamage = 0.033f;

		// Token: 0x04000025 RID: 37
		private int KeepDisplayingTicks = 1000;

		// Token: 0x04000026 RID: 38
		private float ApparelScorePerEnergyMax = 0.25f;

		// Token: 0x04000027 RID: 39
		private static readonly Material BubbleMat = MaterialPool.MatFrom("Other/ShieldBubble", ShaderDatabase.Transparent);
	}
}
