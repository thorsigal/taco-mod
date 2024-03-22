using TacoMikesMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TacoMikesMod.Items
{
	public class LightningRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lightning Rod");
			// Tooltip.SetDefault("Summons a deadly lightning bolt");
		}
		public override void SetDefaults()
		{
			Item.damage = 304;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 57;
			Item.useAnimation = 57;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 37;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<LightningArc>();
			Item.UseSound = SoundID.Item121;
			Item.shootSpeed = 5f;
			
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

			Terraria.Projectile arc = Terraria.Projectile.NewProjectileDirect(source, player.position, velocity, ProjectileID.CultistBossLightningOrbArc, damage, knockback, player.whoAmI);
			arc.hostile = false;
			float rotation = velocity.ToRotation();
			arc.rotation = rotation;
			arc.ai[0] = rotation;
			arc.ai[1] = Terraria.Main.rand.Next();
			arc.friendly = true;
			arc.penetrate = 999;
			arc.width = 28;
			arc.height = 28;
			arc.velocity = velocity;
			arc.maxPenetrate = 999;
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
