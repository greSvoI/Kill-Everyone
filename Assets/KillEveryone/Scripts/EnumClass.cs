namespace KillEveryone
{
	public class EnumClass
	{
		public enum BodyParts 
		{
			Head, Body, Hand, Leg 
		}
		public enum WeaponClass
		{
			None, Pistol, Rifle, Sniper, Rocket, Melee
		}
		public enum WeaponHolder
		{
			None = 0, LowRight = 1, LowLeft = 2, HighRight = 3, HighLeft = 4
		}
		public enum WeaponHolderBody
		{
			None = 0, Spine = 1, Hand = 2, LeftLeg =3, RightLeg = 4  
		}
		public BodyParts Body;
		public WeaponClass weapon;	
		public WeaponHolder holder;
		public WeaponHolderBody body;

	}
	
}
