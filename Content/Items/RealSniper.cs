using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrasic.Content.Items
{
	public class RealSniper : ModItem
	{
		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Ranged;
            Item.useStyle = 5;
            Item.autoReuse = false;
            Item.useAnimation = 120;
            Item.useTime = 120;
            Item.width = 50;
            Item.height = 18;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item11;
            Item.damage = 3000;
            Item.shootSpeed = 100f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 35, 0, 0);
            Item.rare = 5;
            Item.knockBack = 5f;
            Item.crit = 76;
		}
	}
}
