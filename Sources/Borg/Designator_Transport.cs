using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x02000005 RID: 5
	public class Designator_Transport : Designator_Zone
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002190 File Offset: 0x00000390
		public Designator_Transport()
		{
			this.defaultLabel = Translator.Translate("KFM_DesignatorRallyLabel");
			this.defaultDesc = Translator.Translate("KFM_DesignatorRallyDesc");
			this.soundDragSustain = SoundDefOf.Designate_DragAreaDelete;
			this.soundDragChanged = null;
			this.soundSucceeded = SoundDefOf.Designate_ZoneDelete;
			this.useMouseIcon = true;
			this.icon = ContentFinder<Texture2D>.Get("UI/Designators/Flag", true);
			this.hotKey = KeyBindingDefOf.Misc4;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000220F File Offset: 0x0000040F
		public override void SelectedUpdate()
		{
			base.SelectedUpdate();
			this.drawCircle(UI.MouseCell());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002227 File Offset: 0x00000427
		private void drawCircle(IntVec3 pos)
		{
			DrawUtils.DrawRadiusRingEx(pos, 6f);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002238 File Offset: 0x00000438
		public override AcceptanceReport CanDesignateCell(IntVec3 sq)
		{
			bool flag = !GenGrid.InBounds(sq, base.Map);
			bool flag2 = flag;
			AcceptanceReport result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002274 File Offset: 0x00000474
		public override int DraggableDimensions
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002288 File Offset: 0x00000488
		public override bool DragDrawMeasurements
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000229B File Offset: 0x0000049B
		public override void DesignateMultiCell(IEnumerable<IntVec3> cells)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022A3 File Offset: 0x000004A3
		public override void DesignateSingleCell(IntVec3 c)
		{
			this.pos = c;
			this.cmap = Current.Game.CurrentMap;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022BD File Offset: 0x000004BD
		protected override void FinalizeDesignationSucceeded()
		{
			base.FinalizeDesignationSucceeded();
		}

		// Token: 0x04000007 RID: 7
		private IntVec3 pos;

		// Token: 0x04000008 RID: 8
		private Map cmap;
	}
}
