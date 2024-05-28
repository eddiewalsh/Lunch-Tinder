using Lunch_Tinder.Models;

namespace Lunch_Tinder.Data
{
	public partial class LunchTinderContext
	{

		public void AddVote(Vote vote)
		{
			try
			{
				Votes.Add(vote);
				SaveChanges();
			} catch (Exception ex) 
			{
				_logger.Error(ex, "Failed to Add a vote to the Db");
			}
		}

		public void UpdateVote(Vote vote)
		{
			try
			{
				Votes.Update(vote);
				SaveChanges();
			} catch (Exception ex) 
			{ 
				_logger.Error(ex, "Failed to Update a vote to the Db");
			}
		}

		public bool VerifyVoteExists(int userid, int eventid)
		{
			return Votes.Any(vote => vote.EventVoteID == eventid && vote.UserVoteID == userid);
		}

		public Vote? GetVoteByUserAndEvent(int userid, int eventid)
		{
			return Votes.Where(vote => vote.EventVoteID == eventid && vote.UserVoteID == userid).FirstOrDefault() ?? new Vote();
		}


        public List<Vote> GetAllVotesInAnEvent(int eventID)
        {
            return Votes.Where(v => v.EventVoteID == eventID).ToList();
        }
    }
}
