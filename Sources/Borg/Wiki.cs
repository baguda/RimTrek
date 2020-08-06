using System;
using InGameWiki;
using Verse;

namespace BorgAssimilate
{
	// Token: 0x02000009 RID: 9
	[StaticConstructorOnStartup]
	internal static class Wiki
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002E5C File Offset: 0x0000105C
		static Wiki()
		{
			Mod instance = ModCore.Instance;
			ModWiki modWiki = ModWiki.Create(instance);
			modWiki.WikiTitle = "Borg Race Revisited";
		}
	}
}
