using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Items
{
	public class AdBreak : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Tome of Bees");
			// Tooltip.SetDefault("Next Generation, Truly Unlimited Bees");
		}
		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 3200;
			Item.useAnimation = 60;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<Projectiles.Spotify>();
			Item.shootSpeed = 5f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 100);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Projectiles.Spotify>(), damage, knockback, player.whoAmI);
			return false;
		}
	}
}
