using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
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
