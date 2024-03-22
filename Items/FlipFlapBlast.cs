using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Items
{
	public class FlipFlapBlast : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hyper Oblivion Flip-Flap Blaster");
			// Tooltip.SetDefault("\'obliviAWWWWWHhn\'");
		}
		public override void SetDefaults()
		{
			Item.damage = 1453;
			Item.noMelee = true;
			Item.width = 10;
			Item.height = 10;
			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 1504237;
			Item.rare = ItemRarityID.Blue;
			Item.DamageType = DamageClass.Ranged;
			Item.UseSound = SoundID.Item40;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.MoonlordBullet;
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
			uint time = Main.GameUpdateCount;
			float angle = 3f*(float)Math.Sin(((double)time)/60.0*2.0*3.14159*1.75);
			Vector2 velocityVector = velocity.RotatedBy(MathHelper.ToRadians(angle));
			float originalAngle = velocityVector.ToRotation();
			if (velocity.X <= 0)
			{
				originalAngle += 3.1415926535f;
			}
			player.itemRotation = originalAngle;
			return base.Shoot(player, source, position, velocityVector, type, damage, knockback);
        }
    }
}
