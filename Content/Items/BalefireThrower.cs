using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrasic.Content.Items
{
	public class BalefireThrower : ModItem
	{
		public override void SetDefaults()
		{
            Item.useStyle = 5;
            Item.autoReuse = true;
            Item.useAnimation = 30;
            Item.useTime = 6;
            Item.width = 50;
            Item.height = 18;
            Item.shoot = ModContent.ProjectileType<Projectiles.Balefire>();
            Item.useAmmo = AmmoID.Gel;
            Item.UseSound = SoundID.Item34;
            Item.damage = 35;
            Item.knockBack = 0.3f;
            Item.shootSpeed = 7f;
            Item.noMelee = true;
            Item.value = 500000;
            Item.rare = 5;
            Item.DamageType = DamageClass.Ranged;
		}
	}
}
