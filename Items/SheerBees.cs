using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Items
{
	public class SheerBees : ModItem
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
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Beenade;
			Item.shootSpeed = 5f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 100);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
