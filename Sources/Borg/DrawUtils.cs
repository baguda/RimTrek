using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x02000006 RID: 6
	[StaticConstructorOnStartup]
	internal static class DrawUtils
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022C8 File Offset: 0x000004C8
		public static void DrawRadiusRingEx(IntVec3 center, float radius)
		{
			DrawUtils.ringDrawCells.Clear();
			int num = GenRadial.NumCellsInRadius(radius);
			for (int i = 0; i < num; i++)
			{
				DrawUtils.ringDrawCells.Add(center + GenRadial.RadialPattern[i]);
			}
			DrawUtils.DrawFieldEdgesEx(DrawUtils.ringDrawCells);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002321 File Offset: 0x00000521
		public static void DrawFieldEdgesEx(List<IntVec3> cells)
		{
			DrawUtils.DrawFieldEdgesEx(cells, Color.white);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002321 File Offset: 0x00000521
		public static void DrawFieldEdges(List<IntVec3> cells)
		{
			DrawUtils.DrawFieldEdgesEx(cells, Color.white);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002330 File Offset: 0x00000530
		public static void DrawFieldEdgesEx(List<IntVec3> cells, Color color)
		{
			Map currentMap = Find.CurrentMap;
			MaterialRequest materialRequest = default(MaterialRequest);
			materialRequest.shader = ShaderDatabase.Transparent;
			materialRequest.color = color;
			materialRequest.BaseTexPath = "UI/Overlays/TargetHL";
			Material material = MaterialPool.MatFrom(materialRequest);
			material.GetTexture("_MainTex").wrapMode = 1;
			bool flag = DrawUtils.fieldGrid == null;
			bool flag2 = flag;
			if (flag2)
			{
				DrawUtils.fieldGrid = new BoolGrid(currentMap);
			}
			else
			{
				DrawUtils.fieldGrid.ClearAndResizeTo(currentMap);
			}
			int x = currentMap.Size.x;
			int z = currentMap.Size.z;
			int count = cells.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag3 = GenGrid.InBounds(cells[i], currentMap);
				bool flag4 = flag3;
				if (flag4)
				{
					DrawUtils.fieldGrid[cells[i].x, cells[i].z] = true;
				}
			}
			for (int j = 0; j < count; j++)
			{
				IntVec3 intVec = cells[j];
				bool flag5 = GenGrid.InBounds(intVec, currentMap);
				bool flag6 = flag5;
				if (flag6)
				{
					DrawUtils.rotNeeded[0] = (intVec.z < z - 1 && !DrawUtils.fieldGrid[intVec.x, intVec.z + 1]);
					DrawUtils.rotNeeded[1] = (intVec.x < x - 1 && !DrawUtils.fieldGrid[intVec.x + 1, intVec.z]);
					DrawUtils.rotNeeded[2] = (intVec.z > 0 && !DrawUtils.fieldGrid[intVec.x, intVec.z - 1]);
					DrawUtils.rotNeeded[3] = (intVec.x > 0 && !DrawUtils.fieldGrid[intVec.x - 1, intVec.z]);
					for (int k = 0; k < 4; k++)
					{
						bool flag7 = DrawUtils.rotNeeded[k];
						bool flag8 = flag7;
						if (flag8)
						{
							Mesh plane = MeshPool.plane10;
							Vector3 vector = intVec.ToVector3ShiftedWithAltitude(31);
							Rot4 rot;
							rot..ctor(k);
							Graphics.DrawMesh(plane, vector, rot.AsQuat, material, 0);
						}
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002594 File Offset: 0x00000794
		public static void RenderMouseoverTarget()
		{
			Vector3 vector = UI.MouseCell().ToVector3ShiftedWithAltitude(31);
		}

		// Token: 0x04000009 RID: 9
		private static List<IntVec3> ringDrawCells = new List<IntVec3>();

		// Token: 0x0400000A RID: 10
		private static BoolGrid fieldGrid;

		// Token: 0x0400000B RID: 11
		private static bool[] rotNeeded = new bool[4];
	}
}
