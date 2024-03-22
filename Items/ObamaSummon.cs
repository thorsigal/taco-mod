using TacoMikesMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Items
{
    public class ObamaSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Number 44");
			// Tooltip.SetDefault("\'My fellow Terrarians...\'");
		}
		Projectile oldProjectile = null;
		public override void SetDefaults()
		{
			Item.damage = 1453;
			Item.noMelee = true;
			Item.width = 10;
			Item.height = 10;
			Item.useTime = 240;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.knockBack = 0;
			Item.value = 1504237;
			Item.rare = ItemRarityID.Blue;
			Item.sentry = true;
			Item.shoot = ModContent.ProjectileType<ObamaSentry>();
			Item.UseSound = SoundID.Item124;
			Item.autoReuse = false;
			Item.shootSpeed = 10f;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			type = ProjectileID.None;
			
			if (player.altFunctionUse == 2)
			{
				if (oldProjectile != null)
				{

					((ObamaSentry)oldProjectile.ModProjectile).Attack(Main.MouseWorld);
				}
			}
			else
			{
				if (oldProjectile != null)
				{
					oldProjectile.Kill();
				}
				oldProjectile = null;
				oldProjectile = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<ObamaSentry>(), damage, knockback, player.whoAmI);
				oldProjectile.timeLeft = 36000;
			}
			return false;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}
