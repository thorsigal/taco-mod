using TacoMikesMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Items
{
	public class Grobbler : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grobbler");
			// Tooltip.SetDefault("\'Our stars will blot out the sun.\'");
		}
		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 120;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 17;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Grob>();
			Item.shootSpeed = 40f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 target = Main.screenPosition + new Vector2(Main.screenHeight,0);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			// Loop these functions 300 times.
			for (int i = 0; i < 2000; i++)
			{
				position = player.Center + new Vector2((Main.rand.NextFloat()-0.5f)*Main.screenWidth*1.3f, -Main.screenHeight);
				Vector2 heading = new Vector2(Main.rand.NextFloat()-0.5f,-1f);

				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}

				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= Item.shootSpeed*(Main.rand.NextFloat()*.8f+0.1f);
				heading.Y += Main.rand.Next(-40, 41) * 0.02f;
				Terraria.Projectile starProj = Projectile.NewProjectileDirect(source, position, heading, ModContent.ProjectileType<Grob>(),damage,knockback,player.whoAmI);
			}

			return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
