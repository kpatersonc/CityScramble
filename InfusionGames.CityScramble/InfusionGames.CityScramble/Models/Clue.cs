namespace InfusionGames.CityScramble.Models
{
    public class Clue : ILocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int SortOrder { get; set; }
        public ClueKind Kind { get; set; }
    }

    public class TeamClue : Clue
    {
        public int AwardedPoints { get; set; }

        public ClueStatus? Status { get; set; }

        public bool HasResponse()
        {
            return Status.HasValue;
        }

		public string PointsString
		{
			get 
			{
				return (Status.HasValue && Status == ClueStatus.Complete)
					? string.Format("{0}/{1}", AwardedPoints, Points)
					: Points.ToString();
			}
		}

		public string ClueText
		{
			get { return Name ?? Description; }
		}

    }

    public class ClueResponse
    {
        public string Id { get; set; }

        public string RaceId { get; set; }

        public string ClueId { get; set; }

        public string TeamId { get; set; }

        public string UserId { get; set; }

        public string Data { get; set; }

        public ClueStatus Status { get; set; }

        public byte[] Version { get; set; }
    }

    /// <summary>
    /// Represents the various states for a RaceResponse
    /// </summary>
    public enum ClueStatus
    {
        /// <summary>
        /// A created state. The response has been created but has not been finalized by the user.
        /// </summary>
        Created = 0,

        /// <summary>
        /// The user has completed their submission and is waiting for the judges to review it
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The judges have marked the item for review. At this stage, changes cannot be made to the response
        /// </summary>
        InReview,

        /// <summary>
        /// Judges have marked the submission
        /// </summary>
        Complete

    }

    /// <summary>
    /// Clue classification type
    /// </summary>
    public enum ClueKind
    {
        /// <summary>
        /// Standard clue
        /// </summary>
        Default = 0,

        /// <summary>
        /// Get as many of your team in the selfie
        /// </summary>
        Selfie = 1,

        /// <summary>
        /// Clue requires you to perform a special action or challenge
        /// </summary>
        Challenge = 2,

        /// <summary>
        /// Location based clue
        /// </summary>
        Location = 3
    }
}