namespace ZomAPIs.Model
{
    public class UserRestaurant
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public bool Liked { get; set; }
        public int BeenThereCount { get; set; }
    }
}