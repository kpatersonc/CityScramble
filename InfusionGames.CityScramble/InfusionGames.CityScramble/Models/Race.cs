using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace InfusionGames.CityScramble.Models
{
    public class Race
    {
        /// <summary>
        /// Race id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of Race
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Flag to indicate user is enrolled in this race
        /// </summary>
        public bool Enrolled { get; set; }

        /// <summary>
        /// List of Teams enrolled in this race
        /// </summary>
        /// <remarks>
        /// Only populated when querying for a specific race.
        /// </remarks>
        public IEnumerable<Team> Teams { get; set; }

        /// <summary>
        /// Start Date of Race if specified
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// End Date of Race if specified
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Race starting coordinates (latitude)
        /// </summary>
        public double StartLatitude { get; set; }

        /// <summary>
        /// Race starting coordinates (longitude)
        /// </summary>
        public double StartLongitude { get; set; }

        /// <summary>
        /// Race starting coordinates (latitude)
        /// </summary>
        public double EndLatitude { get; set; }

        /// <summary>
        /// Race starting coordinates (longitude)
        /// </summary>
        public double EndLongitude { get; set; }

        public Position GetPosition()
        {
            return new Position(StartLatitude, StartLongitude);
        }

        public bool IsActive()
        {
            return Status() == ActiveStatus.Active;
        }

        public ActiveStatus Status()
        {
            if (!StartDate.HasValue)
                return ActiveStatus.Active;

            if (!EndDate.HasValue)
                return ActiveStatus.Active;

            if (DateTimeOffset.UtcNow < StartDate.Value)
                return ActiveStatus.Upcoming;

            if (DateTimeOffset.UtcNow > EndDate.Value)
                return ActiveStatus.Finished;

            return ActiveStatus.Active;
        }

        public enum ActiveStatus
        {
            Upcoming,
            Active,
            Finished
        }
    }
}