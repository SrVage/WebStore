using WebStore.ViewModels;

namespace WebStore.Services.Interfaces
{
	public interface ICartService
	{
		void Add(int ID);
		void Decrement(int ID);
		void Remove(int ID);
		void Clear();
		CartViewModel GetViewModel();
	}
}
