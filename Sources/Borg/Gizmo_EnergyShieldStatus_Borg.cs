using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x0200000B RID: 11
	[StaticConstructorOnStartup]
	public class Gizmo_EnergyShieldStatus_Borg : Gizmo
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002EDE File Offset: 0x000010DE
		public Gizmo_EnergyShieldStatus_Borg()
		{
			this.order = -100f;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002EF4 File Offset: 0x000010F4
		public override float GetWidth(float maxWidth)
		{
			return 140f;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F0C File Offset: 0x0000110C
		public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth)
		{
			Rect rect;
			rect..ctor(topLeft.x, topLeft.y, this.GetWidth(maxWidth), 75f);
			Rect rect2 = GenUI.ContractedBy(rect, 6f);
			Widgets.DrawWindowBackground(rect);
			Rect rect3 = rect2;
			rect3.height = rect.height / 2f;
			Text.Font = 0;
			Widgets.Label(rect3, this.shield.LabelCap);
			Rect rect4 = rect2;
			rect4.yMin = rect2.y + rect2.height / 2f;
			float num = this.shield.Energy / Mathf.Max(1f, StatExtension.GetStatValue(this.shield, StatDefOf.EnergyShieldEnergyMax, true));
			Widgets.FillableBar(rect4, num, Gizmo_EnergyShieldStatus_Borg.FullShieldBarTex, Gizmo_EnergyShieldStatus_Borg.EmptyShieldBarTex, false);
			Text.Font = 1;
			Text.Anchor = 4;
			Widgets.Label(rect4, (this.shield.Energy * 100f).ToString("F0") + " / " + (StatExtension.GetStatValue(this.shield, StatDefOf.EnergyShieldEnergyMax, true) * 100f).ToString("F0"));
			Text.Anchor = 0;
			return new GizmoResult(0);
		}

		// Token: 0x04000015 RID: 21
		public ShieldBelt_Borg shield;

		// Token: 0x04000016 RID: 22
		private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.2f, 0.24f));

		// Token: 0x04000017 RID: 23
		private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);
	}
}
