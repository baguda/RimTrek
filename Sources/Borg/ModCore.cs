using System;
using InGameWiki;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x0200000A RID: 10
	public class ModCore : Mod
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002E82 File Offset: 0x00001082
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002E89 File Offset: 0x00001089
		public static ModCore Instance { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002E91 File Offset: 0x00001091
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002E99 File Offset: 0x00001099
		public ModWiki Wiki { get; internal set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002EA2 File Offset: 0x000010A2
		public ModCore(ModContentPack content) : base(content)
		{
			ModCore.Instance = this;
			ModCore.Log("We are Borg!");
			ModCore.Trace("Resistance is futile");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002ECA File Offset: 0x000010CA
		public static void Log(string msg)
		{
			Verse.Log.Message(msg ?? "<null>", false);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002ECA File Offset: 0x000010CA
		public static void Trace(string msg)
		{
			Verse.Log.Message(msg ?? "<null>", false);
		}
	}
}
