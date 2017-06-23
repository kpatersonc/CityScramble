namespace InfusionGames.CityScramble.Models
{
    public class Profile
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        /// <summary>
        /// Url to Profile Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Google oAuth Access Token to enable client-side Google+ api calls
        /// </summary>
        /// <remarks>
        /// Only available from /api/profile
        /// </remarks>
        public string GoogleAuthToken { get; set; }

        public TeamMembership[] Teams { get; set; }

        public bool HasTeams()
        {
            return Teams != null && Teams.Length > 0;
        }
    }

    public class TeamMembership
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}