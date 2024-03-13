namespace LearnGame.Pickup
{
    public class PickupSpeedBoost : PickUpItem
    {
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetSpeedBoost(2.5f);
        }
    }
}