namespace Cooper.Models
{
    public class UsersConnection : Entity
    {
        #region Main attributes

        public bool AreFriends { get; set; }
        public bool BlackListed { get; set; }

        #endregion

        #region Interop attributes

        public User User1 { get; set; }
        public User User2 { get; set; }

        #endregion
    }
}
