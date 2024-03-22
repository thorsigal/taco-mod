using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Items
{
	public class FatBoy : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hyper Oblivion Flip-Flap Blaster");
			// Tooltip.SetDefault("\'obliviAWWWWWHhn\'");
		}
		public override void SetDefaults()
		{
			Item.damage = 455;
			Item.noMelee = true;
			Item.width = 10;
			Item.height = 10;
			Item.useTime = 90;
			Item.useAnimation = 90;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 1504237;
			Item.rare = ItemRarityID.Blue;
			Item.DamageType = DamageClass.Ranged;
			Item.UseSound = new Terraria.Audio.SoundStyle("TacoMikesMod/Sounds/Item/PGL");
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<Projectiles.FatBoyShell>();
			Item.shootSpeed = 7f;
		}

		public override void AddRecipes()
		{
			/**Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LastPrism, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();**/
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override bool CanUseItem(Player player)
        {
			return player.HasItem(ItemID.Grenade);

		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(player.HasItem(ItemID.Grenade)) {
				player.ConsumeItem(ItemID.Grenade);
				return base.Shoot(player, source, position, velocity, type, damage, knockback);
			}
			return false;
        }

    }
}
