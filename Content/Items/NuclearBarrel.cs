using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrasic.Content.Items
{
	public class NuclearBarrel : ModItem
	{
		public override void SetDefaults()
		{
            Item.useStyle = 1;
            Item.shootSpeed = 5f;
            Item.shoot = ModContent.ProjectileType<Projectiles.NuclearBarrel>();
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 0, 3, 0);
            Item.damage = 0;
		}
	}
}
