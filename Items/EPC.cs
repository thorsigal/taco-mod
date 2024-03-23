using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using TacoMikesMod;

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
			Item.useAnimation = 30;
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
			if(player.HasBuff(ModContent.BuffType<Buffs.EPCBuff>())) {
				reduce -= 12f;
			}
        }
		int EPCIndex = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(!player.HasBuff(ModContent.BuffType<Buffs.EPCBuff>())) {
				//no reapply means buff needs to be 3 ticks
				player.AddBuff(ModContent.BuffType<Buffs.EPCBuff>(),3);
            	EPCIndex = Projectile.NewProjectile(source, position, velocity*3, ModContent.ProjectileType<Projectiles.EPC2>(), damage, knockback);
			} else {
				if(Main.projectile[EPCIndex].active) {
					((Projectiles.EPC2)Main.projectile[EPCIndex].ModProjectile).addCharge(position,velocity,3);
				}
				player.AddBuff(ModContent.BuffType<Buffs.EPCBuff>(),3);
			}
            return false;
        }
    }
}
