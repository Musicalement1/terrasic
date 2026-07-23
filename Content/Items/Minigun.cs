using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrasic.Content.Items
{
	public class Minigun : ModItem
	{
		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Ranged;
            Item.useStyle = 5;
            Item.autoReuse = true;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.width = 50;
            Item.height = 18;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item11;
            Item.damage = 60;
            Item.shootSpeed = 3f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 35, 0, 0);
            Item.rare = 5;
            Item.knockBack = 1f;
            
		}
	}
}
