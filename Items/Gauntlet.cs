using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Items
{
	public class Gauntlet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gauntlet of Thebees");
			// Tooltip.SetDefault("Slightly mispronounced.");
		}
		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item97;
			Item.DamageType = DamageClass.Ranged;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Bee;
			Item.shootSpeed = 10f;
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
			type = ProjectileID.None;
			for(int i = 0;i<5;i++)
            {
				Vector2 varVector = velocity.RotatedByRandom(MathHelper.ToRadians(15.0f));
				Projectile bee = Projectile.NewProjectileDirect(source, position, varVector, ProjectileID.Bee, damage, knockback, player.whoAmI);
				bee.timeLeft = 180;
				bee.penetrate = 1;
				bee.usesLocalNPCImmunity=true;
            }
			Vector2 varVector2 = velocity.RotatedByRandom(MathHelper.ToRadians(15.0f));
			velocity.X = varVector2.X;
			velocity.Y = varVector2.Y;
			return false;
        }
    }
}
