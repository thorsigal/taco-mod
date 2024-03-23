using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using TacoMikesMod;
using TacoMikesMod.Players;
using System;

namespace TacoMikesMod.Items
{
	public class EPC : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Tome of Bees");
			// Tooltip.SetDefault("Next Generation, Truly Unlimited Bees");
		}
		public override void SetDefaults()
		{
			Item.damage = 27;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 2;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 13;
			//Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.EPC2>();
			Item.shootSpeed = 3f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 100);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
			//charge shouldn't cost ridiculous mana
			BikeGangPlayer modPlayer;
			if(player.TryGetModPlayer<BikeGangPlayer>(out modPlayer)) {
				if(!modPlayer.isCharging) {
					reduce -= 12f;
				}
			}
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {	

			Vector2 gunTip = velocity;
			gunTip.Normalize();
			gunTip *= 50f;
			gunTip += position;
			Dust.NewDust(gunTip, 0, 0, DustID.ArgonMoss);
			BikeGangPlayer modPlayer;
			if(player.TryGetModPlayer<BikeGangPlayer>(out modPlayer)) {
				if(!modPlayer.isCharging) {
					modPlayer.isCharging = true;
					modPlayer.EPCChargeIndex = Projectile.NewProjectile(source, gunTip, velocity*3, ModContent.ProjectileType<Projectiles.EPC2>(), damage, knockback);

				}
				modPlayer.charge += 3;
				modPlayer.epcShootPosition = gunTip;
				modPlayer.epcVelocity = velocity;
				if(modPlayer.charge > 90) {
					float angleMax = ((float)modPlayer.charge-90)/18f;
					Vector2 newDir = velocity.RotatedByRandom(MathHelper.ToRadians(angleMax));
					float newAngle = newDir.ToRotation();
					if (newDir.X <= 0)
					{
						newAngle += 3.1415926535f;
					}
					player.itemRotation = newAngle;
				}

			}
            return false;
        }
    }
}
