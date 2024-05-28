using Lunch_Tinder.Models;
using Microsoft.EntityFrameworkCore;

namespace Lunch_Tinder.Data
{
	public partial class LunchTinderContext
	{
		/// <summary>
		///  Gets all the restaurants in a list
		/// </summary>
		public List<Restaurant> GetAllRestaurants()
		{
			return Restaurants.ToList();
		}

		/// <summary>
		/// Gets lunch group by Lunch group Name, including its associated events and users
		/// </summary>
		/// <param name="name"></param>
		public Restaurant? GetRestaurantByName(string name)
		{
			return Restaurants.Include(retaurant => retaurant.Events)
							  .FirstOrDefault(retaurant => retaurant.RestaurantName == name);
		}
	}
}