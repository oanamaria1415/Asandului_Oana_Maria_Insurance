using Grpc.Net.Client;
using GrpcCustomersService;
using Microsoft.AspNetCore.Mvc;

namespace Asandului_Oana_Maria_Insurance.Controllers
{
    public class CustomersGrpcController : Controller
    {
        private readonly GrpcChannel channel;

        public CustomersGrpcController()
        {
            
            channel = GrpcChannel.ForAddress("https://localhost:7155");
        }

        public IActionResult Index()
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            CustomerList cust = client.GetAll(new Empty());
            return View(cust);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            var client = new CustomerService.CustomerServiceClient(channel);
            client.Insert(customer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var client = new CustomerService.CustomerServiceClient(channel);
            var customer = client.Get(new CustomerId { Id = id.Value });
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int customerId)
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            client.Delete(new CustomerId { Id = customerId });
            return RedirectToAction(nameof(Index));
        }

        // EDIT (cerința labului)
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = new CustomerService.CustomerServiceClient(channel);
            var customer = client.Get(new CustomerId { Id = id.Value });
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            var client = new CustomerService.CustomerServiceClient(channel);
            client.Update(customer);
            return RedirectToAction(nameof(Index));
        }
    }
}
