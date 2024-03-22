﻿using Terraria;
using Terraria.ModLoader;

namespace TacoMikesMod.Buffs
{
	public class BaneDebuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Insane");
			// Description.SetDefault("You can hardly move...");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.runAcceleration*=0.1f;
			player.maxRunSpeed *= 0.1f;
		}
	}
}