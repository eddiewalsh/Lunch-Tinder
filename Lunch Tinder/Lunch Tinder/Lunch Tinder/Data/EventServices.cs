using Lunch_Tinder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Lunch_Tinder.Data
{
    public partial class LunchTinderContext
    {
        /// <summary>
        /// Adds an event to the database and saves that change, logs an exception if it fails
        /// </summary>
        /// <param name="item"></param>
        public void AddEvent(Event item)
        {
            try
            {
                Events.Add(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to add the event");
            }
        }

        /// <summary>
        /// Deletes an event to the database and saves that change, throws an exception if it fails
        /// </summary>
        /// <param name="item"></param>
        public void DeleteEvent(Event item)
        {
            try
            {
                Events.Remove(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to remove the Event");
            }
        }

        /// <summary>
        /// Updates the event in the database. error is logged if there is an exception
        /// </summary>
        /// <param name="item"></param>
		public void UpdateEvent(Event item)
        {
            try
            {
                Events.Update(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update Events");
            }
        }

        /// <summary>
        ///  gets all events from the database, throws an exception if it fails
        /// </summary>
        /// <returns>returns an empty list</returns>
        public List<Event> GetAllEvents()
        {
            try
            {
                return Events.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrive an event");
                return new List<Event>();
            }
        }

        /// <summary>
        /// Retrieves an event from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the event.</param>
        /// <returns>The event with the specified ID if found; otherwise, returns null.</returns>
        public Event GetEventById(int id)
        {
            try
            {
                return Events.Include(e => e.RestaurantOptions).FirstOrDefault(e => e.EventId == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Retrieve any entry");
                return null;
            }
        }

        /// <summary>
        /// Returns an Event by checking its voting end time matches the exact time
        /// </summary>
        /// <returns></returns>
        public List<Event>? GetEventsThatAreClosed()
        {
            return Events.Include(e => e.LunchGroups)
                         .Include(e => e.RestaurantOptions)
                         .Where(u => u.Status != "Closed" && DateTime.Now >= u.VotingEndTime)
                         .ToList();

            //DOUBLE TEST THIS
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_event"></param>
        public Restaurant CalculateEventWinner(Event _event)
        {
            Dictionary<Restaurant, int> votingpoll = new Dictionary<Restaurant, int>();
            List<Vote> votes = GetAllVotesInAnEvent(_event.EventId);
            Random random = new Random();

            if (votes.Count > 0)
            {
                votingpoll = _event.RestaurantOptions
                                   .SelectMany(options => votes, (options, vote) => new { Options = options, Vote = vote })
                                   .GroupBy(x => x.Options)
                                   .ToDictionary(
                                                   g => g.Key,
                                                   g => g.Count(x => x.Vote.RestaurantVoteID == g.Key.RestaurantID)
                                   );

                KeyValuePair<Restaurant, int> winner = votingpoll.OrderByDescending(kv => kv.Value)
                                                                 .FirstOrDefault();

                List<Restaurant> potentialWinners = votingpoll.Where(kv => kv.Value == winner.Value)
                                                              .Select(kv => kv.Key)
                                                              .ToList();

                if (potentialWinners.IsNullOrEmpty() || potentialWinners.Count() == 1 && potentialWinners.Contains(winner.Key))
                {
                    return winner.Key;
                }
                else
                {
                    int randomIndex = random.Next(potentialWinners.Count);

                    return potentialWinners[randomIndex];
                }
            }
            else
            {
                List<Restaurant>? options = _event?.RestaurantOptions?.ToList();

                if (options.Count > 0)
                {
                    int randomIndex = random.Next(options.Count);

                    return options[randomIndex];
                }
                else
                {
                    //Write random function for restaurant options
                    List<Restaurant>? restaurants = Restaurants.ToList();
                    int randomInddex = random.Next(restaurants.Count);

                    return restaurants[randomInddex];
                }
            }
        }

        /// <summary>
        /// this gets the events the user is involved in by checking the users lunchgroups 
        /// </summary>
        /// <param name="user"></param>
        public List<Event> GetAllEventsBasedOnUser(User user)
        {
            List<Event> events = new List<Event>();

            List<LunchGroup> groups = GetLunchGroupsByUser(user);

            events = groups.SelectMany(group => group.Events).Distinct().ToList();

            return events;
        }

        /// <summary>
        /// Setss the chosen events status to 'closed'
        /// </summary>
        /// <param name="_event"></param>
        public void SetEventStatusToClosed(Event _event)
        {
            _event.Status = "Closed";
            UpdateEvent(_event);
        }

        public void SetEventWinner(Event _event, string venueName)
        {
            try
            {
                _event.VenueWinner = venueName;
                UpdateEvent(_event);

            } catch (Exception ex) 
            {
                _logger.Error(ex, "Failed to Retrieve any entry");
            }
        }

        public bool ChecksEventForDuplicateRestaurantOptions(Event _eventt,Restaurant restaurant)
        {
            foreach(Restaurant lg in _eventt.RestaurantOptions)
            {
                if (lg.Equals(restaurant))
                {
                    return true;
                }
            }

            return false;
        }
    }
}