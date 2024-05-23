namespace Praktiskt_Prov
{
    public class Snail: Creature
    {
        private int SpecialAttackChance;
        public Snail(): base(50, 10, "Giant Snail", 150){
            Random rng = new Random();
            SpecialAttackChance = rng.Next(10,50); //ger oss ett värde mellan 10 och 50;
        }
        public override bool Attack(Creature target)
        {
            Random rng = new Random();
            int RandomNumber = rng.Next(0,100);
            if(SpecialAttackChance >= RandomNumber){ // ger sniglen en random chans att träffa med sitt snigelslem
                Console.WriteLine("Du har blivet snigelslemmad! Du är förlamad i en runda");
                target.Health -= damage;
                return true;
            }
            else{
                return base.Attack(target);

            }
        }
    }
}